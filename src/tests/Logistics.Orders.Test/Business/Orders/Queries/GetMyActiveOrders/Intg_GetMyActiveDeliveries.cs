// -----------------------------------------------------------------------
// <copyright file="Intg_GetMyActiveOrders.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Queries.GetMyActiveOrders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Patterns.CQRS;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Orders.Queries.GetMyActiveOrders;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.Test.Fakers;
    using Xunit;

    /// <summary>
    /// Intg_GetMyActiveOrders class is the integration test for GetMyActiveOrders.
    /// </summary>
    public class Intg_GetMyActiveOrders : OrdersIntegrationTestBase<GetMyActiveOrdersQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_GetMyActiveOrders"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_GetMyActiveOrders(AppTestFixture<Startup> testFixture)
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

            var response = await this.GetAsync<ResponseSet<OrderVM>>($"/orders/active/{query.ClientUserName}");

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(query.ClientUserName, response.Payload.Items.First().ClientUserName);
            Assert.NotEmpty(response.Payload.Items.SelectMany(x => x.Statuses).Where(x => x.Value == StatusCode.Assigned.Value));
        }

        /// <summary>
        /// Should be able to get my booked delivery.
        /// </summary>
        [Fact(DisplayName = "Should be able to get my booked delivery")]
        public async Task Should_be_able_to_get_my_booked_delivery()
        {
            var query = this.Build();
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

            var response = await this.GetAsync<ResponseSet<OrderVM>>($"/orders/active/{query.ClientUserName}");

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(query.ClientUserName, response.Payload.Items.First().ClientUserName);
            Assert.NotEmpty(response.Payload.Items.SelectMany(x => x.Statuses).Where(x => x.Value == StatusCode.Booked.Value));
        }

        /// <summary>
        /// Should be able to get my assigned delivery.
        /// </summary>
        [Fact(DisplayName = "Should be able to get my assigned delivery")]
        public async Task Should_be_able_to_get_my_picked_up_delivery()
        {
            var query = this.Build();
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

            var response = await this.GetAsync<ResponseSet<OrderVM>>($"/orders/active/{query.ClientUserName}");

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(query.ClientUserName, response.Payload.Items.First().ClientUserName);
            Assert.NotEmpty(response.Payload.Items.SelectMany(x => x.Statuses).Where(x => x.Value == StatusCode.PickedUp.Value));
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A query.</returns>
        protected override GetMyActiveOrdersQuery Build()
        {
            var driverEmail = this.Faker.Person.Email;
            return new GetMyActiveOrdersQuery { ClientUserName = driverEmail };
        }
    }
}