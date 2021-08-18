// -----------------------------------------------------------------------
// <copyright file="UpdateWorkCommand.cs" company="RyanAd" createdOn="06-20-2020 12:25 PM" updatedOn="06-20-2020 12:28 PM" >
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.UpdateSpecialInstructions
{
    using System.Collections.Generic;
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// Update delivery command.
    /// </summary>
    public class UpdateSpecialInstructionsCommand : CommandRequestBase<OrderVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSpecialInstructionsCommand"/> class.
        /// </summary>
        public UpdateSpecialInstructionsCommand()
        {
        }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the special instructions.
        /// </summary>
        /// <value>
        /// The special instructions.
        /// </value>
        public List<SpecialInstructionVM> SpecialInstructions { get; set; }

        /// <summary>
        /// Gets or sets the name of the user requesting the update.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}