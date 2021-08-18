// -----------------------------------------------------------------------
// <copyright file="CalculateCostHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Queries.CalculateCost
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Options;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// The CalculateCostHandler handles the request CalculateCostHandler and returns the DeliveryCostVM.
    /// </summary>
    /// <seealso cref="QueryHandlerBase{CalculateCostHandler, DeliveryCostVM}" />
    public class CalculateCostHandler : QueryHandlerBase<CalculateCostQuery, DeliveryCostVM>
    {
        /// <summary>
        /// The delivery options.
        /// </summary>
        private readonly DeliveryOptions _deliveryOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateCostHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="options">The options.</param>
        public CalculateCostHandler(IRepository repository, [NotNull] IOptions<DeliveryOptions> options)
            : base(repository)
        {
            this._deliveryOptions = options.Value;
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override Task ExecuteAsync(CancellationToken cancellationToken) => Task.Run(
            () =>
            {
                var systemPercent = this._deliveryOptions.SystemFeePercentage / 100;
                var cost = this._deliveryOptions.BaseCost;

                if (this.Request.DistanceInKm > this._deliveryOptions.BaseDistanceInKm)
                {
                    var additionalDistance = this.Request.DistanceInKm - this._deliveryOptions.BaseDistanceInKm;
                    var additionalCost = additionalDistance * this._deliveryOptions.CostPerKm;

                    cost += additionalCost;
                }

                var systemFee = cost * systemPercent;
                var taxPercent = this._deliveryOptions.Tax > 0
                    ? (this._deliveryOptions.Tax / 100)
                    : 0;
                var tax = cost * taxPercent;
                var totalCost = cost + tax;

                this.Response.DistanceInKm = this.Request.DistanceInKm;
                this.Response.SystemFee = Math.Round(systemFee, 2);
                this.Response.DriverFee = Math.Round(cost - systemFee, 2);
                this.Response.Tax = tax;
                this.Response.Total = Math.Round(totalCost, 2);
            }, cancellationToken);
    }
}