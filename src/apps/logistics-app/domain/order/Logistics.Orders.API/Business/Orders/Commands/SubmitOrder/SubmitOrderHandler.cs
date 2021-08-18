// -----------------------------------------------------------------------
// <copyright file="SubmitOrderHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Commands.SubmitOrder
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
    using Logistics.Communication.Orders.Messages.Events;
    using Logistics.Orders.API.Business.Deliveries.Commands.AssignWork;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.API.ServiceBus.Extensions;
    using MassTransit;
    using MediatR;
    using MongoDB.Driver.GeoJsonObjectModel;

    /// <summary>
    /// Create Order handler.
    /// </summary>
    /// <seealso cref="CommandHandlerBase{SubmitOrderCommand, SubmitOrderResponse}" />
    public class SubmitOrderHandler : CommandHandlerBase<SubmitOrderCommand, OrderVM>
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
        /// The mediator.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The upload file client.
        /// </summary>
        private readonly IRequestClient<IUploadBase64Image> _uploadFileClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitOrderHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="bus">The bus.</param>
        /// <param name="mediator">The mediator.</param>
        public SubmitOrderHandler(IRepository repository, IMediator mediator, IBus bus)
            : base(repository)
        {
            this._bus = bus;
            this._getUsersClient = bus.CreateRequestClient<IGetUsers>();
            this._uploadFileClient = bus.CreateRequestClient<IUploadBase64Image>();

            this._mediator = mediator;
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var order = await this.BuildOrder(cancellationToken);

            this.Repository.AddWithAudit(order);
            this.Response = new OrderVM(order);

            if (!string.IsNullOrEmpty(this.Request.DriverUserName))
            {
                var assignedOrder = await this._mediator.Send(new AssignWorkCommand
                {
                    UserName = this.Request.DriverUserName,
                    Id = order.Id
                }, cancellationToken);

                // Pull the updated order from the database again.
                this.Response.DriverUserName = assignedOrder.DriverUserName;
                this.Response.Driver = assignedOrder.Driver;
                this.Response.Statuses.Add(assignedOrder.Statuses.Last());
            }

            await this.Response.SetUserInfos(this._getUsersClient);
        }

        /// <summary>
        /// Posts the processing.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken.</param>
        protected async override Task PostProcessing(CancellationToken cancellationToken)
        {
            // Emit event that the order has been submitted.
            await this._bus.Publish<IOrderSubmitted>(
                new
                {
                    this.Response.ClientUserName,
                    Destination = new
                    {
                        this.Response.Destination.DisplayAddress,
                        this.Response.Destination.Lat,
                        this.Response.Destination.Lng
                    },
                    this.Response.DriverUserName,
                    EstimatedDistance = this.Response.EstimatedDistance.Value,
                    this.Response.RecipientName,
                    this.Response.RecipientPhone,
                    this.Response.EstimatedItemWeight,
                    this.Response.Id,
                    Origin = new
                    {
                        this.Response.Origin.DisplayAddress,
                        this.Response.Origin.Lat,
                        this.Response.Origin.Lng
                    },
                    this.Response.RequestInsulation,
                    this.Response.RequestSignature,
                    Timestamp = this.Response.CreatedOn
                }, cancellationToken);
        }

        /// <summary>
        /// Builds the order.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// An order.
        /// </returns>
        private async Task<Order> BuildOrder(CancellationToken cancellationToken)
        {
            var itemPhoto = await this._uploadFileClient.UploadBase64Image(this.Request.ItemPhoto, cancellationToken);
            var originPhoto = await this._uploadFileClient.UploadBase64Image(this.Request.OriginPhoto, cancellationToken);

            var order = new Order
            {
                ClientUserName = this.Request.ClientUserName,
                RecipientName = this.Request.RecipientName,
                RecipientPhone = this.Request.RecipientPhone,
                Cost =
                    new DeliveryCost
                    {
                        DistanceInKm = this.Request.Cost.DistanceInKm,
                        SystemFee = this.Request.Cost.SystemFee,
                        DriverFee = this.Request.Cost.DriverFee,
                        Total = this.Request.Cost.Total
                    },
                CurrentStatus = StatusCode.Booked.Value,
                Origin =
                    new Location
                    {
                        DisplayAddress = this.Request.Origin.DisplayAddress,
                        Coordinates = new GeoJson2DGeographicCoordinates(this.Request.Origin.Lng,
                            this.Request.Origin.Lat)
                    },
                Destination =
                    new Location
                    {
                        DisplayAddress = this.Request.Destination.DisplayAddress,
                        Coordinates = new GeoJson2DGeographicCoordinates(this.Request.Destination.Lng,
                            this.Request.Destination.Lat)
                    },
                EstimatedDistance =
                    new Distance
                    {
                        Text = this.Request.EstimatedDistance.Text,
                        Value = this.Request.EstimatedDistance.Value,
                        Time = this.Request.EstimatedDistance.Time
                    },
                OriginPhoto = originPhoto,
                ItemPhoto = itemPhoto,
                ItemName = this.Request.ItemName,
                EstimatedItemWeight = this.Request.EstimatedItemWeight,
                RequestSignature = this.Request.RequestSignature,
                RequestInsulation = this.Request.RequestInsulation
            };

            order.Statuses.Add(new Status
            {
                Value = StatusCode.Booked.Value,
                Comments = $"Order created by {this.Request.ClientUserName}.",
                Timestamp = DateTime.UtcNow,
                UserName = this.Request.ClientUserName
            });

            order.SpecialInstructions.AddRange(
                this.Request.SpecialInstructions.Select(x =>
                    new SpecialInstruction
                    {
                        Description = x,
                        IsCompleted = false
                    }).ToList());

            return order;
        }
    }
}