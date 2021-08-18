// -----------------------------------------------------------------------
// <copyright file="GetMyOrdersQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Queries.GetMyOrders
{
    using System;
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;

    /// <summary>
    /// The GetMyOrdersQuery is a query object used for getting orders of a specific user.
    /// </summary>
    public class GetMyOrdersQuery : QueryRequestBase<ResponseSet<OrderVM>>
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string ClientUserName { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public StatusCode Status { get; set; }
    }
}