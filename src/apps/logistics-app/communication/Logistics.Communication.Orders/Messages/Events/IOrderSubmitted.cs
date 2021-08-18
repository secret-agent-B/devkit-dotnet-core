// -----------------------------------------------------------------------
// <copyright file="IOrderSubmitted.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Orders.Messages.Events
{
    using System;
    using Logistics.Communication.Orders.DTOs;

    /// <summary>
    /// Event that gets fired when an order submitted.
    /// </summary>
    public interface IOrderSubmitted
    {
        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string ClientUserName { get; }

        /// <summary>
        /// Gets the destination.
        /// </summary>
        /// <value>
        /// The destination address.
        /// </value>
        ILocationDTO Destination { get; }

        /// <summary>
        /// Gets the name of the driver user.
        /// </summary>
        /// <value>
        /// The name of the driver user.
        /// </value>
        string DriverUserName { get; }

        /// <summary>
        /// Gets the estimated distance.
        /// </summary>
        /// <value>
        /// The estimated distance.
        /// </value>
        int EstimatedDistance { get; }

        /// <summary>
        /// Gets the estimated weight.
        /// </summary>
        /// <value>
        /// The estimated weight.
        /// </value>
        double EstimatedItemWeight { get; }

        /// <summary>
        /// Gets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        string Id { get; }

        /// <summary>
        /// Gets the origin.
        /// </summary>
        /// <value>
        /// The origin address.
        /// </value>
        ILocationDTO Origin { get; }

        /// <summary>
        /// Gets the name of the receiver.
        /// </summary>
        /// <value>
        /// The name of the receiver.
        /// </value>
        string RecipientName { get; }

        /// <summary>
        /// Gets the recipient phone.
        /// </summary>
        /// <value>
        /// The recipient phone.
        /// </value>
        string RecipientPhone { get; }

        /// <summary>
        /// Gets a value indicating whether [request insulation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [request insulation]; otherwise, <c>false</c>.
        /// </value>
        public bool RequestInsulation { get; }

        /// <summary>
        /// Gets a value indicating whether [request signature].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [request signature]; otherwise, <c>false</c>.
        /// </value>
        public bool RequestSignature { get; }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        DateTime Timestamp { get; }
    }
}