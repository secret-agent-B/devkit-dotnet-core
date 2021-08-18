// -----------------------------------------------------------------------
// <copyright file="Order.cs" company="RyanAd" createdOn="06-20-2020 11:56 AM" updatedOn="06-20-2020 2:02 PM" >
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Data.Models
{
    using System.Collections.Generic;
    using Devkit.Data;

    /// <summary>
    /// A Order requested by the client.
    /// </summary>
    public class Order : DocumentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Order" /> class.
        /// </summary>
        public Order()
        {
            this.SpecialInstructions = new List<SpecialInstruction>();
            this.Statuses = new List<Status>();
        }

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
        public DeliveryCost Cost { get; set; }

        /// <summary>
        /// Gets or sets the current status code.
        /// </summary>
        /// <value>
        /// The current status code.
        /// </value>
        public int CurrentStatus { get; set; }

        /// <summary>
        /// Gets or sets the destination.
        /// </summary>
        /// <value>
        /// The destination.
        /// </value>
        public Location Destination { get; set; }

        /// <summary>
        /// Gets or sets the driver identifier.
        /// </summary>
        /// <value>
        /// The driver identifier.
        /// </value>
        public string DriverUserName { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>
        /// The distance.
        /// </value>
        public Distance EstimatedDistance { get; set; }

        /// <summary>
        /// Gets or sets the estimated weight.
        /// </summary>
        /// <value>
        /// The estimated weight.
        /// </value>
        public double EstimatedItemWeight { get; set; }

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
        /// Gets or sets the origin.
        /// </summary>
        /// <value>
        /// The origin.
        /// </value>
        public Location Origin { get; set; }

        /// <summary>
        /// Gets or sets the origin photo.
        /// </summary>
        /// <value>
        /// The origin photo.
        /// </value>
        public string OriginPhoto { get; set; }

        /// <summary>
        /// Gets or sets the pickup photo.
        /// </summary>
        /// <value>
        /// The pickup photo.
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
        /// <c>true</c> if [requires insulation]; otherwise, <c>false</c>.
        /// </value>
        public bool RequestInsulation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [request signature].
        /// </summary>
        /// <value>
        /// <c>true</c> if [request signature]; otherwise, <c>false</c>.
        /// </value>
        public bool RequestSignature { get; set; }

        /// <summary>
        /// Gets or sets the special instructions.
        /// </summary>
        /// <value>
        /// The special instructions.
        /// </value>
        public List<SpecialInstruction> SpecialInstructions { get; set; }

        /// <summary>
        /// Gets or sets the statuses.
        /// </summary>
        /// <value>
        /// The statuses.
        /// </value>
        public List<Status> Statuses { get; set; }
    }
}