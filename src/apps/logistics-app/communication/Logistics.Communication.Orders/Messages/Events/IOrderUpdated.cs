// -----------------------------------------------------------------------
// <copyright file="IOrderUpdated.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Orders.Messages.Events
{
    using System;

    /// <summary>
    /// Event that gets fired when an order has been updated.
    /// </summary>
    public interface IOrderUpdated
    {
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

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; }
    }
}