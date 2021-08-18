// -----------------------------------------------------------------------
// <copyright file="OrderStatus.cs" company="RyanAd" createdOn="06-20-2020 12:25 PM" updatedOn="06-20-2020 12:28 PM" >
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Constants
{
    using Devkit.Patterns;

    /// <summary>
    /// The order status codes.
    /// </summary>
    public class StatusCode : EnumerationBase
    {
        /// <summary>
        /// The status when an order has been assigned to the driver.
        /// </summary>
        public static readonly StatusCode Assigned = new StatusCode(2, "assigned");

        /// <summary>
        /// The default status for orders as soon as they're created.
        /// </summary>
        public static readonly StatusCode Booked = new StatusCode(1, "booked");

        /// <summary>
        /// The status when a order has been cancelled by the client.
        /// </summary>
        public static readonly StatusCode ClientDisputed = new StatusCode(-1, "client-disputed");

        /// <summary>
        /// The package has been delivered.
        /// </summary>
        public static readonly StatusCode Completed = new StatusCode(4, "completed");

        /// <summary>
        /// The status when a order has been cancelled by the driver.
        /// </summary>
        public static readonly StatusCode DriverDisputed = new StatusCode(-2, "driver-disputed");

        /// <summary>
        /// The driver picked up the package.
        /// </summary>
        public static readonly StatusCode PickedUp = new StatusCode(3, "picked-up");

        /// <summary>
        /// The unknown state.
        /// </summary>
        public static readonly StatusCode Unknown = new StatusCode(0, "unknown");

        /// <summary>
        /// The status that is only used for filtering booked, assigned, and picked-up.
        /// </summary>
        internal static readonly StatusCode Active = new StatusCode(100, "active");

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusCode"/> class.
        /// </summary>
        public StatusCode()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusCode"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="displayName">The display name.</param>
        private StatusCode(int value, string displayName)
            : base(value, displayName)
        {
        }
    }
}