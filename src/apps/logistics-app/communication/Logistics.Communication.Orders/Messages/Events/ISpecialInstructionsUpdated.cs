// -----------------------------------------------------------------------
// <copyright file="ISpecialInstructionsUpdated.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Orders.Messages.Events
{
    using System;
    using System.Collections.Generic;
    using Logistics.Communication.Orders.DTOs;

    /// <summary>
    /// Event that gets fired when a special instruction has been updated.
    /// </summary>
    public interface ISpecialInstructionsUpdated
    {
        /// <summary>
        /// Gets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the special instructions.
        /// </summary>
        /// <value>
        /// The special instructions.
        /// </value>
        IList<ISpecialInstructionDTO> SpecialInstructions { get; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        DateTime Timestamp { get; }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; }
    }
}