// -----------------------------------------------------------------------
// <copyright file="IWorkPickedUp.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Orders.Messages.Events
{
    using System;

    /// <summary>
    /// Event that gets fired when a work has been picked up.
    /// </summary>
    public interface IWorkPickedUp
    {
        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        string Comment { get; }

        /// <summary>
        /// Gets the name of the driver user.
        /// </summary>
        /// <value>
        /// The name of the driver user.
        /// </value>
        string DriverUserName { get; }

        /// <summary>
        /// Gets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
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