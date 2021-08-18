// -----------------------------------------------------------------------
// <copyright file="UpdateOrderCommand.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Commands.UpdateOrder
{
    using System.Collections.Generic;
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// Update Order command.
    /// </summary>
    public class UpdateOrderCommand : CommandRequestBase<OrderVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateOrderCommand"/> class.
        /// </summary>
        public UpdateOrderCommand()
        {
            this.SpecialInstructions = new List<SpecialInstructionVM>();
        }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public DeliveryCostVM Cost { get; set; }

        /// <summary>
        /// Gets or sets the destination coordinates.
        /// </summary>
        /// <value>
        /// The destination coordinates.
        /// </value>
        public LocationVM Destination { get; set; }

        /// <summary>
        /// Gets or sets the estimated distance.
        /// </summary>
        /// <value>
        /// The distance.
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
        /// Gets or sets the origin coordinates.
        /// </summary>
        /// <value>
        /// The origin coordinates.
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
        /// Gets or sets a value indicating whether [request insulation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [request insulation]; otherwise, <c>false</c>.
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
        /// Gets or sets the special instructions.
        /// </summary>
        /// <value>
        /// The special instructions.
        /// </value>
        public ICollection<SpecialInstructionVM> SpecialInstructions { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}