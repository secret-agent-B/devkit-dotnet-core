// -----------------------------------------------------------------------
// <copyright file="Intg_GetMyActiveDeliveries.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Queries.GetMyActiveDeliveries
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Patterns.CQRS;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Deliveries.Queries.GetMyActiveDeliveries;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.Test.Fakers;
    using Xunit;

    /// <summary>
    /// Intg_GetMyActiveDeliveries class is the integration test for GetMyActiveDeliveries.
    /// </summary>
    public class Intg_GetMyActiveDeliveries : OrdersIntegrationTestBase<GetMyActiveDeliveriesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_GetMyActiveDeliveries"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_GetMyActiveDeliveries(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to get my assigned delivery.
        /// </summary>
        [Fact(DisplayName = "Should be able to get my assigned delivery")]
        public async Task Should_be_able_to_get_my_assigned_delivery()
        {
            var query = this.Build();
            var assignedOrders = new OrderFaker().Generate();

            assignedOrders.DriverUserName = query.DriverUserName;
            assignedOrders.Statuses.Add(new Status
            {
                Value = StatusCode.Assigned.Value,
                UserName = query.DriverUserName,
                Comments = $"Order has been assigned to driver ({query.DriverUserName}).",
                Timestamp = DateTime.UtcNow
            });
            assignedOrders.CurrentStatus = assignedOrders.Statuses.Last().Value;

            this.Repository.Add(assignedOrders);

            var response = await this.GetAsync<ResponseSet<OrderVM>>($"/deliveries/active/{query.DriverUserName}");

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(query.DriverUserName, response.Payload.Items.First().DriverUserName);
            Assert.NotEmpty(response.Payload.Items.SelectMany(x => x.Statuses).Where(x => x.Value == StatusCode.Assigned.Value));
        }

        /// <summary>
        /// Should be able to get my assigned delivery.
        /// </summary>
        [Fact(DisplayName = "Should be able to get my assigned delivery")]
        public async Task Should_be_able_to_get_my_picked_up_delivery()
        {
            var query = this.Build();
            var pickedUpOrder = new OrderFaker().Generate();

            pickedUpOrder.DriverUserName = query.DriverUserName;
            pickedUpOrder.Statuses.Add(new Status
            {
                Value = StatusCode.PickedUp.Value,
                UserName = query.DriverUserName,
                Comments = $"Order has been picked up by the driver ({query.DriverUserName}).",
                Timestamp = DateTime.UtcNow
            });
            pickedUpOrder.CurrentStatus = pickedUpOrder.Statuses.Last().Value;

            this.Repository.Add(pickedUpOrder);

            var response = await this.GetAsync<ResponseSet<OrderVM>>($"/deliveries/active/{query.DriverUserName}");

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(query.DriverUserName, response.Payload.Items.First().DriverUserName);
            Assert.NotEmpty(response.Payload.Items.SelectMany(x => x.Statuses).Where(x => x.Value == StatusCode.PickedUp.Value));
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A query.</returns>
        protected override GetMyActiveDeliveriesQuery Build()
        {
            var driverEmail = this.Faker.Person.Email;
            return new GetMyActiveDeliveriesQuery { DriverUserName = driverEmail };
        }
    }
}