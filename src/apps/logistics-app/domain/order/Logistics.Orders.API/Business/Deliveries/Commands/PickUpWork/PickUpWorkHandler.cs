// -----------------------------------------------------------------------
// <copyright file="PickUpWorkHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.PickUpWork
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.FileStore.Extensions;
    using Devkit.Communication.FileStore.Messages;
    using Devkit.Communication.Security.Messages;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.API.ServiceBus.Extensions;
    using MassTransit;
    using MongoDB.Driver;

    /// <summary>
    /// The CompleteWorkHandler handles the CompleteWorkCommand.
    /// </summary>
    public class PickUpWorkHandler : CommandHandlerBase<PickUpWorkCommand, OrderVM>
    {
        /// <summary>
        /// The get users client.
        /// </summary>
        private readonly IRequestClient<IGetUsers> _getUsersClient;

        /// <summary>
        /// The upload base64 image client.
        /// </summary>
        private readonly IRequestClient<IUploadBase64Image> _uploadFileClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="PickUpWorkHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="bus">The bus.</param>
        public PickUpWorkHandler(IRepository repository, IBus bus)
            : base(repository)
        {
            this._getUsersClient = bus.CreateRequestClient<IGetUsers>();
            this._uploadFileClient = bus.CreateRequestClient<IUploadBase64Image>();
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
            var pickupPhoto = await this._uploadFileClient.UploadBase64Image(this.Request.Photo, cancellationToken);

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

            if (lastStatus.Value == StatusCode.PickedUp.Value)
            {
                this.Response.Exceptions.Add(
                    nameof(Order),
                    new[]
                    {
                        $"Order ({this.Request.Id}) has already been picked up by the driver."
                    });
                return;
            }

            if (lastStatus.Value != StatusCode.Assigned.Value)
            {
                this.Response.Exceptions.Add(
                    nameof(Order),
                    new[] {
                        $"Order ({this.Request.Id}) is not assigned to any driver."
                    });
                return;
            }

            // Add a new status indicating the change to this order.
            statuses.Add(new Status
            {
                Comments = this.Request.Comments ?? $"Order has been picked up by the driver ({order.DriverUserName}).",
                Timestamp = DateTime.UtcNow,
                UserName = this.Request.UserName,
                Value = StatusCode.PickedUp.Value
            });

            var updatedOrder = this.Repository.UpdateWithAudit<Order>(
                x => x.Id == this.Request.Id,
                builder => builder
                    // Drivers photo on pickup
                    .Set(x => x.PickedUpPhoto, pickupPhoto)
                    // Update the current status
                    .Set(x => x.CurrentStatus, statuses.Last().Value)
                    // Set the updated statuses.
                    .Set(x => x.Statuses, statuses));

            this.Response = new OrderVM(updatedOrder);

            await this.Response.SetUserInfos(this._getUsersClient);
        }
    }
}