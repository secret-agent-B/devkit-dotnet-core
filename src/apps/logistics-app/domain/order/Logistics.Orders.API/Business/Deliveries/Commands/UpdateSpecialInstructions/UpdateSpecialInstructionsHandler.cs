// -----------------------------------------------------------------------
// <copyright file="UpdateWorkHandler.cs" company="RyanAd" createdOn="06-20-2020 12:25 PM" updatedOn="06-20-2020 12:28 PM" >
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.UpdateSpecialInstructions
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.Security.Messages;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.API.ServiceBus.Extensions;
    using MassTransit;
    using MongoDB.Driver;

    /// <summary>
    /// Update Order handler.
    /// </summary>
    /// <seealso cref="CommandHandlerBase{UpdateDeliveryCommand, DeliveryVM}" />
    public class UpdateSpecialInstructionsHandler : CommandHandlerBase<UpdateSpecialInstructionsCommand, OrderVM>
    {
        /// <summary>
        /// The get users client.
        /// </summary>
        private readonly IRequestClient<IGetUsers> _getUsersClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSpecialInstructionsHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="bus">The bus.</param>
        public UpdateSpecialInstructionsHandler(IRepository repository, IBus bus)
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
            var statuses = this.Repository.GetOneOrDefault<Order>(x => x.Id == this.Request.OrderId)?.Statuses;

            if (statuses == null)
            {
                throw new RequestException($"Could not find {nameof(Order)} ({this.Request.OrderId}).");
            }

            var lastStatusCode = statuses.OrderBy(x => x.Timestamp).Last().Value;

            statuses.Add(new Status
            {
                Value = lastStatusCode, // keep the latest status code.
                Comments = $"Order special instruction(s) has been updated by user ({this.Request.UserName}).",
                Timestamp = DateTime.UtcNow,
                UserName = this.Request.UserName
            });

            var updateDelivery = this.Repository.UpdateWithAudit<Order>(
                x => x.Id == this.Request.OrderId,
                builder => builder
                    .Set(x => x.SpecialInstructions, this.Request.SpecialInstructions.Select(x => new SpecialInstruction
                    {
                        Description = x.Description,
                        IsCompleted = x.IsCompleted
                    }).ToList())
                    .Set(x => x.Statuses, statuses));

            this.Response = new OrderVM(updateDelivery);

            await this.Response.SetUserInfos(this._getUsersClient);
        }
    }
}