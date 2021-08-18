// -----------------------------------------------------------------------
// <copyright file="DeliveryOptions.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Options
{
    /// <summary>
    /// The delivery options.
    /// </summary>
    public class DeliveryOptions
    {
        /// <summary>
        /// The configuration key.
        /// </summary>
        public const string Section = "DeliveryOptions";

        /// <summary>
        /// Gets or sets the base cost.
        /// </summary>
        /// <value>
        /// The base cost.
        /// </value>
        public double BaseCost { get; set; }

        /// <summary>
        /// Gets or sets the base distance in km.
        /// </summary>
        /// <value>
        /// The base distance in km.
        /// </value>
        public double BaseDistanceInKm { get; set; }

        /// <summary>
        /// Gets or sets the cost per km.
        /// </summary>
        /// <value>
        /// The cost per km.
        /// </value>
        public double CostPerKm { get; set; }

        /// <summary>
        /// Gets or sets the system fee percentage.
        /// </summary>
        /// <value>
        /// The system fee percentage.
        /// </value>
        public double SystemFeePercentage { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        public double Tax { get; set; }
    }
}