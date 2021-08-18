// -----------------------------------------------------------------------
// <copyright file="CompleteWorkCommand.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.CompleteWork
{
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// The CompleteWorkCommand is the command that is issued by the a user to complete a delivery work.
    /// </summary>
    /// <seealso cref="CommandRequestBase{OrderVM}" />
    public class CompleteWorkCommand : CommandRequestBase<OrderVM>
    {
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
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        /// <value>
        /// The photo.
        /// </value>
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets the name of the driver user.
        /// </summary>
        /// <value>
        /// The name of the driver user.
        /// </value>
        public string UserName { get; set; }
    }
}