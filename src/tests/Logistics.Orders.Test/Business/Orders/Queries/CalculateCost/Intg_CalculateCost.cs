// -----------------------------------------------------------------------
// <copyright file="Intg_CalculateCost.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Queries.CalculateCost
{
    using System.Threading.Tasks;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Orders.Queries.CalculateCost;
    using Logistics.Orders.API.Business.ViewModels;
    using Xunit;

    /// <summary>
    /// The integration test base for calculating the cost.
    /// </summary>
    /// <seealso cref="IntegrationTestBase{CalculateCostQuery, Startup}" />
    public class Intg_CalculateCost : OrdersIntegrationTestBase<CalculateCostQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_CalculateCost"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_CalculateCost(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should calculate delivery cost.
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <param name="driverFee">The driver fee.</param>
        /// <param name="systemFee">The system fee.</param>
        /// <param name="total">The total.</param>
        [Theory(DisplayName = "Should calculate delivery cost")]
        [InlineData(10, 95.2, 23.8, 119)]
        [InlineData(5.0, 67.2, 16.8, 84)]
        [InlineData(3.0, 56, 14, 70)]
        [InlineData(1.0, 56, 14, 70)]
        public async Task Should_calculate_delivery_cost(double distance, double driverFee, double systemFee, double total)
        {
            var response = await this.GetAsync<DeliveryCostVM>($"/orders/cost?distanceInKm={distance}");

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(driverFee, response.Payload.DriverFee);
            Assert.Equal(systemFee, response.Payload.SystemFee);
            Assert.Equal(total, response.Payload.Total);
        }
    }
}