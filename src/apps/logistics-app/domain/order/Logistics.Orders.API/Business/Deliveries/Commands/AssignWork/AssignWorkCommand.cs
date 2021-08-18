// -----------------------------------------------------------------------
// <copyright file="AssignWorkCommand.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.AssignWork
{
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// The AssignWorkCommand is the command that is issued to assign a driver to an order/work.
    /// </summary>
    public class AssignWorkCommand : CommandRequestBase<OrderVM>
    {
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