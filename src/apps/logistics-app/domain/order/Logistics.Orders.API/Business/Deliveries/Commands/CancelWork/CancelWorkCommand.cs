// -----------------------------------------------------------------------
// <copyright file="CancelWorkCommand.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.CancelWork
{
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// The CancelWorkCommand is the command that is issued by the driver to cancel the work.
    /// </summary>
    /// <seealso cref="CommandRequestBase{OrderVM}" />
    public class CancelWorkCommand : CommandRequestBase<OrderVM>
    {
        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the driver user.
        /// </summary>
        /// <value>
        /// The name of the driver user.
        /// </value>
        public string UserName { get; set; }
    }
}