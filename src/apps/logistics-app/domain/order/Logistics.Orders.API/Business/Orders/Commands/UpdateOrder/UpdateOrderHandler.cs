// -----------------------------------------------------------------------
// <copyright file="UpdateOrderHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Commands.UpdateOrder
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.FileStore.Extensions;
    using Devkit.Communication.FileStore.Messages;
    using Devkit.Communication.Security.Messages;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Patterns.Exceptions;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.API.ServiceBus.Extensions;
    using MassTransit;
    using MongoDB.Driver;
    using MongoDB.Driver.GeoJsonObjectModel;

    /// <summary>
    /// Update Order handler.
    /// </summary>
    /// <seealso cref="CommandHandlerBase{UpdateOrderCommand, UpdateOrderResponse}" />
    public class UpdateOrderHandler : CommandHandlerBase<UpdateOrderCommand, OrderVM>
    {
        /// <summary>
        /// The get users client.
        /// </summary>
        private readonly IRequestClient<IGetUsers> _getUsersClient;

        /// <summary>
        /// The upload file client.
        /// </summary>
        private readonly IRequestClient<IUploadBase64Image> _uploadFileClient;

        /// <summary>
        /// The bus.
        /// </summary>
        private readonly IBus _bus;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrderHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="bus">The bus.</param>
        public UpdateOrderHandler(IRepository repository, IBus bus)
            : base(repository)
        {
            this._getUsersClient = bus.CreateRequestClient<IGetUsers>();
            this._uploadFileClient = bus.CreateRequestClient<IUploadBase64Image>();
            this._bus = bus;
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
                throw new AppException($"Could not find {nameof(Order)} ({this.Request.Id}).");
            }

            var statuses = order.Statuses;
            var lastStatus = statuses.Last();

            if (lastStatus.Value != StatusCode.Booked.Value)
            {
                var lastStatusDisplay = EnumerationBase.FromValue<StatusCode>(lastStatus.Value).DisplayName;
                throw new AppException($"Order ({this.Request.Id}) cannot be updated. Current status is {lastStatusDisplay}.");
            }

            statuses.Add(new Status
            {
                Comments = $"Order updated by user ({this.Request.UserName})",
                Timestamp = DateTime.UtcNow,
                UserName = this.Request.UserName,
                Value = statuses.Last().Value
            });

            var itemPhoto = await this.GetPhotoId(this.Request.ItemPhoto, order.ItemPhoto, cancellationToken);
            var originPhoto = await this.GetPhotoId(this.Request.OriginPhoto, order.OriginPhoto, cancellationToken);

            var updatedOrder = this.Repository.UpdateWithAudit<Order>(
                x => x.Id == this.Request.Id,
                builder => builder
                    .Set(x => x.RecipientName, this.Request.RecipientName)
                    .Set(x => x.RecipientPhone, this.Request.RecipientPhone)
                    .Set(x => x.EstimatedItemWeight, this.Request.EstimatedItemWeight)
                    .Set(x => x.ItemName, this.Request.ItemName)
                    .Set(x => x.ItemPhoto, itemPhoto)
                    .Set(x => x.OriginPhoto, originPhoto)
                    .Set(x => x.RequestSignature, this.Request.RequestSignature)
                    .Set(x => x.RequestInsulation, this.Request.RequestInsulation)
                    .Set(x => x.Origin, new Location
                    {
                        DisplayAddress = this.Request.Origin.DisplayAddress,
                        Coordinates = new GeoJson2DGeographicCoordinates(this.Request.Origin.Lng, this.Request.Origin.Lat)
                    })
                    .Set(x => x.Cost, new DeliveryCost
                    {
                        DistanceInKm = this.Request.Cost.DistanceInKm,
                        SystemFee = this.Request.Cost.SystemFee,
                        DriverFee = this.Request.Cost.DriverFee,
                        Total = this.Request.Cost.Total
                    })
                    .Set(x => x.Destination, new Location
                    {
                        DisplayAddress = this.Request.Destination.DisplayAddress,
                        Coordinates = new GeoJson2DGeographicCoordinates(this.Request.Destination.Lng, this.Request.Destination.Lat)
                    })
                    .Set(x => x.EstimatedDistance, new Distance
                    {
                        Text = this.Request.EstimatedDistance.Text,
                        Value = this.Request.EstimatedDistance.Value,
                        Time = this.Request.EstimatedDistance.Time
                    })
                    .Set(x => x.SpecialInstructions, this.Request.SpecialInstructions.Select(x => new SpecialInstruction
                    {
                        Description = x.Description,
                        IsCompleted = x.IsCompleted
                    }).ToList())
                    .Set(x => x.Statuses, statuses));

            this.Response = new OrderVM(updatedOrder);

            await this.Response.SetUserInfos(this._getUsersClient);
        }

        /// <summary>
        /// Tries the update photo.
        /// </summary>
        /// <param name="base64Image">The base64 image.</param>
        /// <param name="currentPhotoId">The current photo identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A photo id.
        /// </returns>
        private async Task<string> GetPhotoId(string base64Image, string currentPhotoId, CancellationToken cancellationToken)
        {
            if (base64Image == currentPhotoId)
            {
                return currentPhotoId;
            }

            var newImageId = await this._uploadFileClient.UploadBase64Image(base64Image, cancellationToken);

            await this._bus.Publish<IDeleteFile>(
                new
                {
                    Id = currentPhotoId
                }, cancellationToken);

            return newImageId;
        }
    }
}