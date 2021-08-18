// -----------------------------------------------------------------------
// <copyright file="Unit_GetMyOrdersHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Queries.GetMyOrders
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Patterns;
    using Logistics.Orders.API.Business.Orders.Queries.GetMyOrders;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Xunit;

    /// <summary>
    /// Unit test for GetMyOrders.
    /// </summary>
    public class Unit_GetMyOrdersHandler : OrdersUnitTestBase<(GetMyOrdersQuery request, GetMyOrdersHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_GetMyOrdersHandler"/> class.
        /// </summary>
        public Unit_GetMyOrdersHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Should be able to get orders by client user.
        /// </summary>
        [Fact(DisplayName = "Should be able to get orders by client user")]
        public async Task Should_be_able_to_get_orders_by_client_user_name()
        {
            var (request, handler) = this.Build();
            var orders = this.Repository.All<Order>();
            var order = this.Faker.PickRandom(orders);

            request.ClientUserName = order.ClientUserName;
            request.StartDate = order.CreatedOn;
            request.EndDate = order.CreatedOn.AddDays(1);
            request.Status = EnumerationBase.FromValue<StatusCode>(order.Statuses.Last().Value);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.NotEmpty(response.Items);
        }

        /// <summary>
        /// Should return an empty result.
        /// </summary>
        [Fact(DisplayName = "Should return an empty result")]
        public async Task Should_return_an_empty_result()
        {
            var (request, handler) = this.Build();
            var orders = this.Repository.All<Order>();
            var order = this.Faker.PickRandom(orders);

            request.ClientUserName = order.ClientUserName + "_INVALIDATE_USERNAME";
            request.StartDate = order.CreatedOn;
            request.EndDate = order.CreatedOn.AddDays(1);
            request.Status = EnumerationBase.FromValue<StatusCode>(order.Statuses.Last().Value);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Empty(response.Items);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (GetMyOrdersQuery request, GetMyOrdersHandler handler) Build()
        {
            var order = this.Repository.All<Order>().First();

            return (new GetMyOrdersQuery
            {
                ClientUserName = order.ClientUserName
            }, new GetMyOrdersHandler(this.Repository, this.TestHarness.Bus));
        }
    }
}