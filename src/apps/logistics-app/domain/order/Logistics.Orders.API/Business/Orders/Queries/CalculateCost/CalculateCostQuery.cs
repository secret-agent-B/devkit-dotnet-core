// -----------------------------------------------------------------------
// <copyright file="CalculateCostQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Queries.CalculateCost
{
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// The CalculateCostQuery is the query that is used to calculate the cost of a delivery.
    /// </summary>
    public class CalculateCostQuery : QueryRequestBase<DeliveryCostVM>
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public double DistanceInKm { get; set; }
    }
}