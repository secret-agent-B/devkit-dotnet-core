// -----------------------------------------------------------------------
// <copyright file="FindWorkQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Queries.FindWork
{
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// The FindWorkQuery is the input object for finding work for a driver.
    /// </summary>
    public class FindWorkQuery : QueryRequestBase<ResponseSet<OrderVM>>
    {
        /// <summary>
        /// Gets or sets the name of the driver user.
        /// </summary>
        /// <value>
        /// The name of the driver user.
        /// </value>
        public string ExcludeUserName { get; set; }

        /// <summary>
        /// Gets or sets the lat.
        /// </summary>
        /// <value>
        /// The lat.
        /// </value>
        public double Lat { get; set; }

        /// <summary>
        /// Gets or sets the LNG.
        /// </summary>
        /// <value>
        /// The LNG.
        /// </value>
        public double Lng { get; set; }

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>
        /// The page.
        /// </value>
        public int Page { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; }
    }
}