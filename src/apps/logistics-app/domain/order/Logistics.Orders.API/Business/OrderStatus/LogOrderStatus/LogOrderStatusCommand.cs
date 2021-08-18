// -----------------------------------------------------------------------
// <copyright file="TrackStatusCommand.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.OrderStatus.LogOrderStatus
{
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;

    /// <summary>
    /// The UpdateStatusCommand is the request that comes in to update an order status.
    /// </summary>
    public class LogOrderStatusCommand : CommandRequestBase<OrderVM>
    {
        /// <summary>
        /// Gets or sets the order status.
        /// </summary>
        /// <value>
        /// The order status.
        /// </value>
        public StatusCode Code { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public string UserId { get; set; }
    }
}