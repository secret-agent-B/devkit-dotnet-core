// -----------------------------------------------------------------------
// <copyright file="LocationVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.ViewModels
{
    using System.Diagnostics.CodeAnalysis;
    using Logistics.Orders.API.Data.Models;

    /// <summary>
    /// Location view model contract.
    /// </summary>
    public class LocationVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationVM"/> class.
        /// </summary>
        public LocationVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationVM"/> class.
        /// </summary>
        /// <param name="location">The location.</param>
        public LocationVM([NotNull] Location location)
        {
            this.DisplayAddress = location.DisplayAddress;
            this.Lat = location.Coordinates.Latitude;
            this.Lng = location.Coordinates.Longitude;
        }

        /// <summary>
        /// Gets or sets the display address.
        /// </summary>
        /// <value>
        /// The display address.
        /// </value>
        public string DisplayAddress { get; set; }

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>
        /// The latitude.
        /// </value>
        public double Lat { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>
        /// The longitude.
        /// </value>
        public double Lng { get; set; }
    }
}