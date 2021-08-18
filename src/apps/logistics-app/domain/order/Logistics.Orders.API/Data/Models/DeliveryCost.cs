// -----------------------------------------------------------------------
// <copyright file="DeliveryCost.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Data.Models
{
    /// <summary>
    /// The delivery cost.
    /// </summary>
    public class DeliveryCost
    {
        /// <summary>
        /// Gets or sets the distance in km.
        /// </summary>
        /// <value>
        /// The distance in km.
        /// </value>
        public double DistanceInKm { get; set; }

        /// <summary>
        /// Gets or sets the drivers fee.
        /// </summary>
        /// <value>
        /// The drivers fee.
        /// </value>
        public double DriverFee { get; set; }

        /// <summary>
        /// Gets or sets the system fee.
        /// </summary>
        /// <value>
        /// The system fee.
        /// </value>
        public double SystemFee { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        public double Tax { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>
        /// The total amount.
        /// </value>
        public double Total { get; set; }
    }
}