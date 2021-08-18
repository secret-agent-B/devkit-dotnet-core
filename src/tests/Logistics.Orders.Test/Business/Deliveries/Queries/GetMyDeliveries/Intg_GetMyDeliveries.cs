// -----------------------------------------------------------------------
// <copyright file="Intg_GetMyDeliveries.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Queries.GetMyDeliveries
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Patterns.CQRS;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Deliveries.Queries.GetMyDeliveries;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Data.Models;
    using Xunit;

    /// <summary>
    /// Integration test for GetMyDeliveries.
    /// </summary>
    public class Intg_GetMyDeliveries : OrdersIntegrationTestBase<GetMyDeliveriesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_GetMyDeliveries"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_GetMyDeliveries(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to get orders.
        /// </summary>
        [Fact(DisplayName = "Should be able to get orders")]
        public async Task Should_be_able_to_get_orders_by_client_name_and_status()
        {
            var order = this.Repository.All<Order>().First();
            var invalidUserName = order.DriverUserName;
            var startDate = order.CreatedOn.AddDays(-30).ToString(CultureInfo.InvariantCulture);
            var endDate = order.CreatedOn.ToString(CultureInfo.InvariantCulture);

            var response = await this.GetAsync<ResponseSet<OrderVM>>($"/deliveries/booked/{invalidUserName}/?startDate={startDate}&endDate={endDate}");

            Assert.True(response.IsSuccessfulStatusCode);

            foreach (var item in response.Payload.Items)
            {
                Assert.Equal(item.ClientUserName, order.ClientUserName);
            }
        }

        /// <summary>
        /// Should return an empty collection if found no match.
        /// </summary>
        [Fact(DisplayName = "Should return an empty collection if found no match")]
        public async Task Should_return_an_empty_collection_if_found_no_match()
        {
            var order = this.Repository.All<Order>().First();
            var invalidUserName = order.DriverUserName + "_INVALIDATE_USERNAME";
            var startDate = DateTime.Now.Date.AddDays(-30).ToUniversalTime().ToString(CultureInfo.InvariantCulture);
            var endDate = DateTime.Now.Date.ToUniversalTime().ToString(CultureInfo.InvariantCulture);

            var response = await this.GetAsync<ResponseSet<OrderVM>>($"/deliveries/booked/{invalidUserName}/?startDate={startDate}&endDate={endDate}");

            Assert.True(response.IsSuccessfulStatusCode);

            foreach (var item in response.Payload.Items)
            {
                Assert.Equal(item.ClientUserName, order.ClientUserName);
            }
        }
    }
}