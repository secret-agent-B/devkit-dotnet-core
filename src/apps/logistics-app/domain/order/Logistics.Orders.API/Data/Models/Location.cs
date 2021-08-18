// -----------------------------------------------------------------------
// <copyright file="Location.cs" company="RyanAd" createdOn="06-20-2020 1:30 PM" updatedOn="06-20-2020 2:02 PM" >
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Data.Models
{
    using MongoDB.Driver.GeoJsonObjectModel;

    /// <summary>
    /// Represents a 2d coordinates from the map.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        public GeoJson2DGeographicCoordinates Coordinates { get; set; }

        /// <summary>
        /// Gets or sets the display address.
        /// </summary>
        /// <value>
        /// The display address.
        /// </value>
        public string DisplayAddress { get; set; }
    }
}