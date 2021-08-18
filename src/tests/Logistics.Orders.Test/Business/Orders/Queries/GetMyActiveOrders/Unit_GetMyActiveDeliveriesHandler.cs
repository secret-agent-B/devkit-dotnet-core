// -----------------------------------------------------------------------
// <copyright file="Unit_GetMyActiveOrdersHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Queries.GetMyActiveOrders
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Orders.API.Business.Orders.Queries.GetMyActiveOrders;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.Test.Fakers;
    using Xunit;

    /// <summary>
    /// Unit_GetMyActiveOrdersHandler class is the unit test for GetMyActiveOrdersHandler.
    /// </summary>
    public class Unit_GetMyActiveOrdersHandler : OrdersUnitTestBase<(GetMyActiveOrdersQuery query, GetMyActiveOrdersHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_GetMyActiveOrdersHandler"/> class.
        /// </summary>
        public Unit_GetMyActiveOrdersHandler()
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

            query.ClientUserName = assignedOrders.ClientUserName;
            assignedOrders.Statuses.Add(new Status
            {
                Value = StatusCode.Assigned.Value,
                UserName = query.ClientUserName,
                Comments = $"Order has been assigned to driver ({query.ClientUserName}).",
                Timestamp = DateTime.UtcNow
            });
            assignedOrders.CurrentStatus = assignedOrders.Statuses.Last().Value;

            this.Repository.Add(assignedOrders);

            var response = await handler.Handle(query, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(query.ClientUserName, response.Items.Last().ClientUserName);
            Assert.NotEmpty(response.Items.SelectMany(x => x.Statuses).Where(x => x.Value == StatusCode.Assigned.Value));
        }

        /// <summary>
        /// Should return booked order.
        /// </summary>
        [Fact(DisplayName = "Should return booked order")]
        public async Task Should_return_booked_order()
        {
            var (query, handler) = this.Build();

            // booked order
            var bookedOrders = new OrderFaker().Generate();

            query.ClientUserName = bookedOrders.ClientUserName;
            bookedOrders.Statuses.Add(new Status
            {
                Value = StatusCode.Booked.Value,
                UserName = query.ClientUserName,
                Comments = $"Order created by {query.ClientUserName}.",
                Timestamp = DateTime.UtcNow
            });
            bookedOrders.CurrentStatus = bookedOrders.Statuses.Last().Value;

            this.Repository.Add(bookedOrders);

            var response = await handler.Handle(query, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(query.ClientUserName, response.Items.Last().ClientUserName);
            Assert.NotEmpty(response.Items.SelectMany(x => x.Statuses).Where(x => x.Value == StatusCode.Booked.Value));
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

            query.ClientUserName = pickedUpOrder.ClientUserName;
            pickedUpOrder.Statuses.Add(new Status
            {
                Value = StatusCode.PickedUp.Value,
                UserName = query.ClientUserName,
                Comments = $"Order has been picked up by the driver ({query.ClientUserName}).",
                Timestamp = DateTime.UtcNow
            });
            pickedUpOrder.CurrentStatus = pickedUpOrder.Statuses.Last().Value;

            this.Repository.Add(pickedUpOrder);

            var response = await handler.Handle(query, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(query.ClientUserName, response.Items.Last().ClientUserName);
            Assert.NotEmpty(response.Items.SelectMany(x => x.Statuses).Where(x => x.Value == StatusCode.PickedUp.Value));
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (GetMyActiveOrdersQuery query, GetMyActiveOrdersHandler handler) Build()
        {
            var handler = new GetMyActiveOrdersHandler(this.Repository, this.TestHarness.Bus);
            var query = new GetMyActiveOrdersQuery
            {
                ClientUserName = this.Faker.Person.Email
            };

            return (query, handler);
        }
    }
}