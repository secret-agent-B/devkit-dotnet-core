// -----------------------------------------------------------------------
// <copyright file="CancelWorkHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.CancelWork
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.Messages;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns;
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.API.ServiceBus.Extensions;
    using MassTransit;
    using MongoDB.Driver;

    /// <summary>
    /// The CancelWorkHandler handles the CancelWorkCommand.
    /// </summary>
    public class CancelWorkHandler : CommandHandlerBase<CancelWorkCommand, OrderVM>
    {
        /// <summary>
        /// The get users client.
        /// </summary>
        private readonly IRequestClient<IGetUsers> _getUsersClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelWorkHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="bus">The bus.</param>
        public CancelWorkHandler(IRepository repository, IBus bus)
            : base(repository)
        {
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

            if (order.DriverUserName != this.Request.UserName)
            {
                this.Response.Exceptions.Add(
                    nameof(Order),
                    new[]
                    {
                        $"Cannot cancel an order that is not assigned to driver ({this.Request.UserName})."
                    });
                return;
            }

            var statuses = order.Statuses;
            var lastStatus = statuses.Last();
            var lastStatusDisplay = EnumerationBase.FromValue<StatusCode>(lastStatus.Value).DisplayName;

            if (lastStatus.Value == StatusCode.ClientDisputed.Value
               || lastStatus.Value == StatusCode.DriverDisputed.Value
               || lastStatus.Value != StatusCode.Assigned.Value)
            {
                this.Response.Exceptions.Add(
                    nameof(Order),
                    new[] {
                        $"Order ({this.Request.Id}) cannot be cancelled. Current status is {lastStatusDisplay}."
                    });
                return;
            }

            // Add a new status indicating the change to this order.
            statuses.Add(new Status
            {
                Comments = this.Request.Comment,
                Timestamp = DateTime.UtcNow,
                UserName = this.Request.UserName,
                Value = StatusCode.DriverDisputed.Value
            });

            statuses.Add(new Status
            {
                Comments = $"Resetting status to {StatusCode.Booked.DisplayName}",
                Timestamp = DateTime.UtcNow,
                UserName = order.ClientUserName,
                Value = StatusCode.Booked.Value
            });

            var updatedOrder = this.Repository.Update<Order>(
                x => x.Id == this.Request.Id,
                builder => builder
                    // Update the current status
                    .Set(x => x.CurrentStatus, statuses.Last().Value)
                    // Set the updated statuses.
                    .Set(x => x.Statuses, statuses));

            this.Response = new OrderVM(updatedOrder);

            await this.Response.SetUserInfos(this._getUsersClient);
        }
    }
}