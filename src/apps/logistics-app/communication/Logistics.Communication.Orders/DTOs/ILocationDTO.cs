// -----------------------------------------------------------------------
// <copyright file="ILocationDTO.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Orders.DTOs
{
    /// <summary>
    /// The location data transfer object.
    /// </summary>
    public interface ILocationDTO
    {
        /// <summary>
        /// Gets the display address.
        /// </summary>
        /// <value>
        /// The display address.
        /// </value>
        string DisplayAddress { get; }

        /// <summary>
        /// Gets the lat.
        /// </summary>
        /// <value>
        /// The lat.
        /// </value>
        double Lat { get; }

        /// <summary>
        /// Gets the LNG.
        /// </summary>
        /// <value>
        /// The LNG.
        /// </value>
        double Lng { get; }
    }
}