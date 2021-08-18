// -----------------------------------------------------------------------
// <copyright file="Unit_CalculateCostHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Queries.CalculateCost
{
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Orders.API.Business.Orders.Queries.CalculateCost;
    using Logistics.Orders.Test.Fakers;
    using Xunit;

    /// <summary>
    /// The calculate cost handler unit test base.
    /// </summary>
    /// <seealso cref="OrdersIntegrationTestBase{(CalculateCostQuery query, CalculateCostHandler handler)}" />
    public class Unit_CalculateCostHandler : OrdersUnitTestBase<(CalculateCostQuery query, CalculateCostHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_CalculateCostHandler"/> class.
        /// </summary>
        public Unit_CalculateCostHandler()
        {
        }

        /// <summary>
        /// Should calculate cost of a delivery.
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <param name="driverFee">The driver fee.</param>
        /// <param name="systemFee">The system fee.</param>
        /// <param name="total">The total.</param>
        [Theory(DisplayName = "Should calculate delivery cost")]
        [InlineData(10, 95.2, 23.8, 119)]
        [InlineData(10, 95.2, 23.8, 119)]
        [InlineData(5.0, 67.2, 16.8, 84)]
        [InlineData(3.0, 56, 14, 70)]
        [InlineData(1.0, 56, 14, 70)]
        public async Task Should_calculate_delivery_cost(double distance, double driverFee, double systemFee, double total)
        {
            var (query, handler) = this.Build();

            query.DistanceInKm = distance;

            var response = await handler.Handle(query, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(driverFee, response.DriverFee);
            Assert.Equal(systemFee, response.SystemFee);
            Assert.Equal(total, response.Total);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (CalculateCostQuery query, CalculateCostHandler handler) Build()
        {
            var query = new CalculateCostQuery { DistanceInKm = 10 };
            var options = new DeliveryOptionsFaker();
            var handler = new CalculateCostHandler(this.Repository, options.Generate());

            return (query, handler);
        }
    }
}