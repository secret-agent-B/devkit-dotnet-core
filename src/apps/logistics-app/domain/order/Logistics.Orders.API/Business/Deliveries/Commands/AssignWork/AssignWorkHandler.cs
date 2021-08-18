// -----------------------------------------------------------------------
// <copyright file="AssignWorkHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.AssignWork
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.ChatR.Messages;
    using Devkit.Communication.Security.Messages;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Patterns.Exceptions;
    using Devkit.ServiceBus.Exceptions;
    using Logistics.Communication.Store.DTOs;
    using Logistics.Communication.Store.Messages;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.API.ServiceBus.Extensions;
    using MassTransit;
    using MongoDB.Driver;

    /// <summary>
    /// The handler to assign a work to a driver.
    /// </summary>
    /// <seealso cref="CommandHandlerBase{AssignWorkCommand, OrderVM}" />
    public class AssignWorkHandler : CommandHandlerBase<AssignWorkCommand, OrderVM>
    {
        /// <summary>
        /// The bus.
        /// </summary>
        private readonly IBus _bus;

        /// <summary>
        /// The get account.
        /// </summary>
        private readonly IRequestClient<IGetAccount> _getAccount;

        /// <summary>
        /// The get users client.
        /// </summary>
        private readonly IRequestClient<IGetUsers> _getUsersClient;

        /// <summary>
        /// The account.
        /// </summary>
        private IAccountDTO _account;

        /// <summary>
        /// The order.
        /// </summary>
        private Order _order;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignWorkHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="bus">The bus.</param>
        public AssignWorkHandler(IRepository repository, IBus bus)
            : base(repository)
        {
            this._bus = bus;
            this._getUsersClient = bus.CreateRequestClient<IGetUsers>();
            this._getAccount = bus.CreateRequestClient<IGetAccount>();
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
            var statuses = this._order.Statuses;
            var lastStatus = statuses.Last();
            var lastStatusDisplay = EnumerationBase.FromValue<StatusCode>(lastStatus.Value).DisplayName;

            if (lastStatus.Value == StatusCode.Assigned.Value)
            {
                // Order has already been assigned to someone.
                this.Response.Exceptions.Add(nameof(Order), new[]
                {
                    $"Order ({this.Request.Id}) has already been assigned to a driver."
                });
                return;
            }

            if (lastStatus.Value != StatusCode.Booked.Value)
            {
                // Order can only be assigned if the status is 'Booked', inform the driver that the assign work request failed.
                this.Response.Exceptions.Add(nameof(Order), new[]
                {
                    $"Order ({this.Request.Id}) cannot be cancelled. Current status is {lastStatusDisplay}."
                });
                return;
            }

            // Add a new status indicating the change to this order.
            statuses.Add(new Status
            {
                Comments = $"Order has been assigned to driver ({this.Request.UserName}).",
                Timestamp = DateTime.UtcNow,
                UserName = this.Request.UserName,
                Value = StatusCode.Assigned.Value
            });

            var updatedOrder = this.Repository.Update<Order>(
                x => x.Id == this.Request.Id,
                builder => builder
                    // Update the current status
                    .Set(x => x.CurrentStatus, StatusCode.Assigned.Value)
                    // Set the updated statuses.
                    .Set(x => x.Statuses, statuses)
                    // Set the driver.
                    .Set(x => x.DriverUserName, this.Request.UserName));

            this.Response = new OrderVM(updatedOrder);

            await this.Response.SetUserInfos(this._getUsersClient);
        }

        /// <summary>
        /// Posts the processing.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken.</param>
        protected async override Task PostProcessing(CancellationToken cancellationToken)
        {
            // Send a command to start a ChatR session.
            await this._bus.Publish<IStartSession>(
                new
                {
                    Id = $"{this.Response.Id}_{this.Response.DriverUserName}",
                    Topic = this.Response.ItemName,
                    Participants = new[]
                    {
                        new
                        {
                            Role = "client",
                            UserName = this.Response.ClientUserName,
                            PhoneNumber = this.Response.Client["PhoneNumber"]?.ToString()
                        },
                        new
                        {
                            Role = "driver",
                            UserName = this.Response.DriverUserName,
                            PhoneNumber = this.Response.Driver["PhoneNumber"]?.ToString()
                        }
                    }
                }, cancellationToken);
        }

        /// <summary>
        /// Pre processing routine.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.
        /// </returns>
        protected override async Task PreProcessing(CancellationToken cancellationToken)
        {
            await this.GetAccount(cancellationToken);

            // Find the order
            this._order = this.Repository.GetOneOrDefault<Order>(x => x.Id == this.Request.Id);

            // If null, it means we could not find the order by the provided id.
            if (this._order == null)
            {
                throw new AppException($"Could not find {nameof(Order)} ({this.Request.Id}).");
            }

            // Check if the driver has enough credits on the account.
            if (this._order.Cost.SystemFee > this._account.AvailableCredits)
            {
                this.Response.AddException(nameof(this._account.AvailableCredits), "Not enough credits.");
            }
        }

        /// <summary>
        /// Gets the account.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        private async Task GetAccount(CancellationToken cancellationToken)
        {
            var (account, exception) = await this._getAccount.GetResponse<IAccountDTO, IConsumerException>(
                new
                {
                    this.Request.UserName
                }, cancellationToken);

            if (account.IsCompletedSuccessfully)
            {
                this._account = (await account).Message;
            }
            else
            {
                this.Response.AddException(nameof(IGetAccount), (await exception).Message.ErrorMessage);
            }
        }
    }
}