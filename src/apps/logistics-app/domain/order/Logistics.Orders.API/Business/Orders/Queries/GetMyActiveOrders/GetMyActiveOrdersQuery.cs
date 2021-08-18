// -----------------------------------------------------------------------
// <copyright file="GetMyActiveOrdersQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Queries.GetMyActiveOrders
{
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// GetMyActiveOrdersQuery class is the query to pull all on-going (active, assigned, or picked-up orders by a client.
    /// </summary>
    public class GetMyActiveOrdersQuery : QueryRequestBase<ResponseSet<OrderVM>>
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string ClientUserName { get; set; }
    }
}