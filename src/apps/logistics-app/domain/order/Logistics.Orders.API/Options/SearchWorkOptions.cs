// -----------------------------------------------------------------------
// <copyright file="SearchWorkOptions.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Options
{
    /// <summary>
    /// The SearchWorkOptions dictates the search raduis when pulling work items.
    /// </summary>
    public class SearchWorkOptions
    {
        /// <summary>
        /// The configuration key.
        /// </summary>
        public const string Section = "SearchWorkOptions";

        /// <summary>
        /// Gets or sets the maximum size of the page.
        /// </summary>
        /// <value>
        /// The maximum size of the page.
        /// </value>
        public int MaxPageSize { get; set; }

        /// <summary>
        /// Gets or sets the maximum search raduis.
        /// </summary>
        /// <value>
        /// The maximum search raduis.
        /// </value>
        public double MaxSearchDistanceInKm { get; set; }
    }
}