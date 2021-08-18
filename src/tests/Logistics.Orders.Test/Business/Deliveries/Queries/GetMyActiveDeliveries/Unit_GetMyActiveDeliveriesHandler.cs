// -----------------------------------------------------------------------
// <copyright file="Unit_GetMyActiveDeliveriesHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Queries.GetMyActiveDeliveries
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Orders.API.Business.Deliveries.Queries.GetMyActiveDeliveries;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.Test.Fakers;
    using Xunit;

    /// <summary>
    /// Unit_GetMyActiveDeliveriesHandler class is the unit test for GetMyActiveDeliveriesHandler.
    /// </summary>
    public class Unit_GetMyActiveDeliveriesHandler : OrdersUnitTestBase<(GetMyActiveDeliveriesQuery query, GetMyActiveDeliveriesHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_GetMyActiveDeliveriesHandler"/> class.
        /// </summary>
        public Unit_GetMyActiveDeliveriesHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Should return assigned order.
        /// </summary>
        [Fact(DisplayName = "Should return assigned order")]
        public async Task Should_return_assigned_order()
        {
            var (query, handler) = this.Build();

            // assigned order
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

            var response = await handler.Handle(query, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(query.DriverUserName, response.Items.First().DriverUserName);
            Assert.NotEmpty(response.Items.SelectMany(x => x.Statuses).Where(x => x.Value == StatusCode.Assigned.Value));
        }

        /// <summary>
        /// Should return picked-up order.
        /// </summary>
        [Fact(DisplayName = "Should return picked-up order")]
        public async Task Should_return_picked_up_order()
        {
            var (query, handler) = this.Build();

            // picked up orders
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

            var response = await handler.Handle(query, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(query.DriverUserName, response.Items.First().DriverUserName);
            Assert.NotEmpty(response.Items.SelectMany(x => x.Statuses).Where(x => x.Value == StatusCode.PickedUp.Value));
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (GetMyActiveDeliveriesQuery query, GetMyActiveDeliveriesHandler handler) Build()
        {
            var handler = new GetMyActiveDeliveriesHandler(this.Repository, this.TestHarness.Bus);
            var query = new GetMyActiveDeliveriesQuery
            {
                DriverUserName = this.Faker.Person.Email
            };

            return (query, handler);
        }
    }
}