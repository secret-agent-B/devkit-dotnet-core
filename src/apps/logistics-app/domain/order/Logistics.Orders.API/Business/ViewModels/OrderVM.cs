// -----------------------------------------------------------------------
// <copyright file="OrderVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Devkit.Patterns.CQRS;
    using Logistics.Orders.API.Data.Models;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Order view model.
    /// </summary>
    public class OrderVM : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderVM"/> class.
        /// </summary>
        public OrderVM()
        {
            this.SpecialInstructions = new List<SpecialInstructionVM>();
            this.Statuses = new List<StatusVM>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderVM" /> class.
        /// </summary>
        /// <param name="order">The order.</param>
        public OrderVM([NotNull] Order order)
        {
            this.Id = order.Id;
            this.ClientUserName = order.ClientUserName;
            this.CompletedPhoto = order.CompletedPhoto;
            this.CreatedOn = order.CreatedOn;
            this.CurrentStatus = order.CurrentStatus;
            this.DriverUserName = order.DriverUserName;
            this.EstimatedItemWeight = order.EstimatedItemWeight;
            this.ItemName = order.ItemName;
            this.ItemPhoto = order.ItemPhoto;
            this.LastUpdatedOn = order.LastUpdatedOn;
            this.OriginPhoto = order.OriginPhoto;
            this.PickedUpPhoto = order.PickedUpPhoto;
            this.RecipientName = order.RecipientName;
            this.RecipientPhone = order.RecipientPhone;
            this.RequestInsulation = order.RequestInsulation;
            this.RequestSignature = order.RequestSignature;

            this.Destination = new LocationVM(order.Destination);
            this.EstimatedDistance = new DistanceVM(order.EstimatedDistance);
            this.Origin = new LocationVM(order.Origin);
            this.Cost = new DeliveryCostVM(order.Cost);

            this.SpecialInstructions = order.SpecialInstructions
                .Select(x => new SpecialInstructionVM(x))
                .ToList();

            this.Statuses = order.Statuses
                .Select(x => new StatusVM(x))
                .ToList();
        }

        /// <summary>
        /// Gets or sets the client.
        /// </summary>
        /// <value>
        /// The client.
        /// </value>
        public JObject Client { get; set; }

        /// <summary>
        /// Gets or sets the client identifier.
        /// </summary>
        /// <value>
        /// The client identifier.
        /// </value>
        public string ClientUserName { get; set; }

        /// <summary>
        /// Gets or sets the completed photo.
        /// </summary>
        /// <value>
        /// The completed photo.
        /// </value>
        public string CompletedPhoto { get; set; }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public DeliveryCostVM Cost { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        /// <value>
        /// The created on.
        /// </value>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the current status.
        /// </summary>
        /// <value>
        /// The current status.
        /// </value>
        public int CurrentStatus { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public LocationVM Destination { get; set; }

        /// <summary>
        /// Gets or sets the driver.
        /// </summary>
        /// <value>
        /// The driver.
        /// </value>
        public JObject Driver { get; set; }

        /// <summary>
        /// Gets or sets the name of the driver user.
        /// </summary>
        /// <value>
        /// The name of the driver user.
        /// </value>
        public string DriverUserName { get; set; }

        /// <summary>
        /// Gets or sets the estimated distance.
        /// </summary>
        /// <value>
        /// The estimated distance.
        /// </value>
        public DistanceVM EstimatedDistance { get; set; }

        /// <summary>
        /// Gets or sets the estimated item weight.
        /// </summary>
        /// <value>
        /// The estimated item weight.
        /// </value>
        public double EstimatedItemWeight { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public string ItemName { get; set; }

        /// <summary>
        /// Gets or sets the item photo.
        /// </summary>
        /// <value>
        /// The item photo.
        /// </value>
        public string ItemPhoto { get; set; }

        /// <summary>
        /// Gets or sets the last updated on.
        /// </summary>
        /// <value>
        /// The last updated on.
        /// </value>
        public DateTime? LastUpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public LocationVM Origin { get; set; }

        /// <summary>
        /// Gets or sets the origin photo.
        /// </summary>
        /// <value>
        /// The origin photo.
        /// </value>
        public string OriginPhoto { get; set; }

        /// <summary>
        /// Gets or sets the pick up photo.
        /// </summary>
        /// <value>
        /// The pick up photo.
        /// </value>
        public string PickedUpPhoto { get; set; }

        /// <summary>
        /// Gets or sets the name of the receiver.
        /// </summary>
        /// <value>
        /// The name of the receiver.
        /// </value>
        public string RecipientName { get; set; }

        /// <summary>
        /// Gets or sets the receiver phone.
        /// </summary>
        /// <value>
        /// The receiver phone.
        /// </value>
        public string RecipientPhone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [requires insulation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [requires insulation]; otherwise, <c>false</c>.
        /// </value>
        public bool RequestInsulation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [request signature].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [request signature]; otherwise, <c>false</c>.
        /// </value>
        public bool RequestSignature { get; set; }

        /// <summary>
        /// Gets the wishes.
        /// </summary>
        /// <value>
        /// The wishes.
        /// </value>
        public List<SpecialInstructionVM> SpecialInstructions { get; }

        /// <summary>
        /// Gets the statuses.
        /// </summary>
        /// <value>
        /// The statuses.
        /// </value>
        public List<StatusVM> Statuses { get; }
    }
}