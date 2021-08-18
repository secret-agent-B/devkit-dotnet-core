// -----------------------------------------------------------------------
// <copyright file="Intg_FindWork.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Queries.FindWork
{
    using System.Linq;
    using System.Threading.Tasks;
    using Devkit.Patterns.CQRS;
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Deliveries.Queries.FindWork;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Xunit;

    /// <summary>
    /// The Intg_FindWork is the integration test for FindWork.
    /// </summary>
    public class Intg_FindWork : OrdersIntegrationTestBase<FindWorkQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_FindWork"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_FindWork(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to get a list of orders.
        /// </summary>
        [Fact(DisplayName = "Should be able to get a list of orders")]
        public async Task Should_be_able_to_get_a_list_of_orders()
        {
            var order = this.Faker.PickRandom(this.Repository.GetMany<Order>(x => x.CurrentStatus == StatusCode.Booked.Value));
            var query = this.Build();
            var response = await this.GetAsync<ResponseSet<OrderVM>>($"/deliveries/{query.ExcludeUserName}" +
                $"?lng={order.Origin.Coordinates.Longitude}" +
                $"&lat={order.Origin.Coordinates.Latitude}" +
                $"&page={query.Page}" +
                $"&pageSize={query.PageSize}");

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.NotEmpty(response.Payload.Items);

            foreach (var item in response.Payload.Items)
            {
                var databaseValue = this.Repository.GetOneOrDefault<Order>(x => x.Id == item.Id);

                Assert.Equal(databaseValue.Id, item.Id);
                Assert.Equal(databaseValue.ClientUserName, item.ClientUserName);
                Assert.Equal(databaseValue.CreatedOn, item.CreatedOn);
                Assert.Equal(databaseValue.Destination.DisplayAddress, item.Destination.DisplayAddress);
                Assert.Equal(databaseValue.Destination.Coordinates.Latitude, item.Destination.Lat);
                Assert.Equal(databaseValue.Destination.Coordinates.Longitude, item.Destination.Lng);
                Assert.Equal(databaseValue.DriverUserName, item.DriverUserName);
                Assert.Equal(databaseValue.EstimatedDistance.Text, item.EstimatedDistance.Text);
                Assert.Equal(databaseValue.EstimatedDistance.Value, item.EstimatedDistance.Value);
                Assert.Equal(databaseValue.EstimatedItemWeight, item.EstimatedItemWeight);
                Assert.Equal(databaseValue.ItemName, item.ItemName);
                Assert.Equal(databaseValue.ItemPhoto, item.ItemPhoto);
                Assert.Equal(databaseValue.LastUpdatedOn, item.LastUpdatedOn);
                Assert.Equal(databaseValue.Origin.DisplayAddress, item.Origin.DisplayAddress);
                Assert.Equal(databaseValue.Origin.Coordinates.Latitude, item.Origin.Lat);
                Assert.Equal(databaseValue.Origin.Coordinates.Longitude, item.Origin.Lng);
                Assert.Equal(databaseValue.OriginPhoto, item.OriginPhoto);
                Assert.Equal(databaseValue.RequestInsulation, item.RequestInsulation);
                Assert.Equal(databaseValue.RequestSignature, item.RequestSignature);

                foreach (var specialInstruction in item.SpecialInstructions)
                {
                    Assert.True(databaseValue.SpecialInstructions
                        .Any(x => x.Description == specialInstruction.Description && x.IsCompleted == specialInstruction.IsCompleted));
                }
            }
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override FindWorkQuery Build()
        {
            return new FindWorkQuery
            {
                ExcludeUserName = this.Faker.Person.UserName,
                Page = 1,
                PageSize = 10
            };
        }
    }
}