// -----------------------------------------------------------------------
// <copyright file="Unit_GetMyDeliveriesHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Queries.GetMyDeliveries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Patterns;
    using Logistics.Orders.API.Business.Deliveries.Queries.GetMyDeliveries;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Xunit;

    /// <summary>
    /// Unit test for GetMyDeliveries.
    /// </summary>
    public class Unit_GetMyDeliveriesHandler : OrdersUnitTestBase<(GetMyDeliveriesQuery request, GetMyDeliveriesHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_GetMyDeliveriesHandler"/> class.
        /// </summary>
        public Unit_GetMyDeliveriesHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Should name of the be able to get Deliveries by client user.
        /// </summary>
        [Fact(DisplayName = "Should name of the be able to get Deliveries by client user")]
        public async Task Should_be_able_to_get_Deliveries_by_client_user_name()
        {
            var (request, handler) = this.Build();
            var deliveries = this.Repository.All<Order>();
            var delivery = this.Faker.PickRandom(deliveries);

            request.DriverUserName = delivery.DriverUserName;
            request.StartDate = delivery.CreatedOn;
            request.EndDate = delivery.CreatedOn.AddDays(1);
            request.Status = EnumerationBase.FromValue<StatusCode>(delivery.Statuses.Last().Value);

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
            var deliveries = this.Repository.All<Order>();
            var delivery = this.Faker.PickRandom(deliveries);

            request.DriverUserName = delivery.DriverUserName + "_INVALIDATE_USERNAME";
            request.StartDate = delivery.CreatedOn;
            request.EndDate = delivery.CreatedOn.AddDays(1);
            request.Status = EnumerationBase.FromValue<StatusCode>(delivery.Statuses.Last().Value);

            var response = await handler.Handle(request, CancellationToken.None);

            Assert.Empty(response.Items);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (GetMyDeliveriesQuery request, GetMyDeliveriesHandler handler) Build()
        {
            return (new GetMyDeliveriesQuery(), new GetMyDeliveriesHandler(this.Repository, this.TestHarness.Bus));
        }
    }
}