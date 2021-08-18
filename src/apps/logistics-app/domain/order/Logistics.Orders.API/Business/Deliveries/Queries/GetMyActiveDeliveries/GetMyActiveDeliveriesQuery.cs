// -----------------------------------------------------------------------
// <copyright file="GetMyActiveDeliveriesQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Queries.GetMyActiveDeliveries
{
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// Query for getting active deliveries.
    /// </summary>
    public class GetMyActiveDeliveriesQuery : QueryRequestBase<ResponseSet<OrderVM>>
    {
        /// <summary>
        /// Gets or sets the user name of the driver.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string DriverUserName { get; set; }
    }
}