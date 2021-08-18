// -----------------------------------------------------------------------
// <copyright file="IWorkAssigned.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Orders.Messages.Events
{
    using System;

    /// <summary>
    /// Event that gets fired when a work has been assigned.
    /// </summary>
    public interface IWorkAssigned
    {
        /// <summary>
        /// Gets the assigner user identifier.
        /// </summary>
        /// <value>
        /// The assigner user identifier.
        /// </value>
        string AssignerUserName { get; }

        /// <summary>
        /// Gets the driver user identifier.
        /// </summary>
        /// <value>
        /// The driver user identifier.
        /// </value>
        string DriverUserName { get; }

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        DateTime Timestamp { get; }
    }
}