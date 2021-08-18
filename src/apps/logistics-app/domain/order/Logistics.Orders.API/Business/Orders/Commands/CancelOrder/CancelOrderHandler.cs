// -----------------------------------------------------------------------
// <copyright file="CancelOrderHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Commands.CancelOrder
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.Messages;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns;
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Communication.Orders.Messages.Events;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.API.ServiceBus.Extensions;
    using MassTransit;
    using MongoDB.Driver;

    /// <summary>
    /// The CancelOrderHandler handles the CancelOrderCommand.
    /// </summary>
    public class CancelOrderHandler : CommandHandlerBase<CancelOrderCommand, OrderVM>
    {
        /// <summary>
        /// The bus.
        /// </summary>
        private readonly IBus _bus;

        /// <summary>
        /// The get users client.
        /// </summary>
        private readonly IRequestClient<IGetUsers> _getUsersClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelOrderHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="bus">The bus.</param>
        public CancelOrderHandler(IRepository repository, IBus bus)
            : base(repository)
        {
            this._bus = bus;
            this._getUsersClient = bus.CreateRequestClient<IGetUsers>();
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            // Find the order
            var order = this.Repository.GetOneOrDefault<Order>(x => x.Id == this.Request.Id);

            // If null, it means we could not find the order by the provided id.
            if (order == null)
            {
                this.Response.Exceptions.Add(
                    nameof(Order),
                    new[]
                    {
                        $"Could not find {nameof(Order)} ({this.Request.Id})."
                    });
                return;
            }

            var statuses = order.Statuses;
            var lastStatus = statuses.Last();
            var lastStatusDisplay = EnumerationBase.FromValue<StatusCode>(lastStatus.Value).DisplayName;

            if (lastStatus.Value == StatusCode.ClientDisputed.Value
                || lastStatus.Value == StatusCode.DriverDisputed.Value
                || lastStatus.Value == StatusCode.PickedUp.Value
                || lastStatus.Value == StatusCode.Completed.Value)
            {
                this.Response.Exceptions.Add(
                    nameof(Order),
                    new[] {
                        $"Order ({this.Request.Id}) cannot be cancelled. Current status is {lastStatusDisplay}."
                    });
                return;
            }

            var updateTimestamp = DateTime.UtcNow;

            // Add a new status indicating the change to this order.
            statuses.Add(new Status
            {
                Comments = this.Request.Comment,
                Timestamp = updateTimestamp,
                UserName = this.Request.UserName,
                Value = StatusCode.ClientDisputed.Value
            });

            var updatedOrder = this.Repository.Update<Order>(
                x => x.Id == this.Request.Id,
                builder => builder
                    // Update the current status
                    .Set(x => x.CurrentStatus, statuses.Last().Value)
                    // Update the statuses
                    .Set(x => x.Statuses, statuses));

            this.Response = new OrderVM(updatedOrder);

            await this.Response.SetUserInfos(this._getUsersClient);
        }

        /// <summary>
        /// Posts the processing.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.
        /// </returns>
        protected async override Task PostProcessing(CancellationToken cancellationToken)
        {
            await this._bus.Publish<IOrderCancelled>(
                new
                {
                    this.Request.Comment,
                    this.Request.Id,
                    this.Request.UserName,
                    Timestamp = this.Response.LastUpdatedOn
                }, cancellationToken);
        }
    }
}