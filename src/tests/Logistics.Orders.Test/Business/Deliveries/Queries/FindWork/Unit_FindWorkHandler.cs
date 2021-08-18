// -----------------------------------------------------------------------
// <copyright file="Unit_FindWorkHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Queries.FindWork
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Logistics.Orders.API.Business.Deliveries.Queries.FindWork;
    using Logistics.Orders.API.Data.Models;
    using Logistics.Orders.API.Options;
    using Logistics.Orders.Test.Fakers;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;
    using Moq;
    using Xunit;

    /// <summary>
    /// The Unit_FindWorkHandler is the unit test for FindWorkHandler.
    /// </summary>
    public class Unit_FindWorkHandler : OrdersUnitTestBase<(FindWorkQuery query, FindWorkHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unit_FindWorkHandler"/> class.
        /// </summary>
        public Unit_FindWorkHandler()
        {
            this.TestHarness.Start().Wait();
        }

        /// <summary>
        /// Should be able to get work list for drivers.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Should be able to get work list for drivers")]
        public async Task Should_be_able_to_get_work_list_for_drivers()
        {
            var (query, handler) = this.Build();
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.True(response.IsSuccessful);

            foreach (var item in response.Items)
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
        protected override (FindWorkQuery query, FindWorkHandler handler) Build()
        {
            var query = new FindWorkQuery
            {
                ExcludeUserName = this.Faker.Person.UserName,
                Page = 1,
                PageSize = 10
            };

            var mockSearchOptions = new Mock<IOptions<SearchWorkOptions>>();
            mockSearchOptions
                .Setup(x => x.Value)
                .Returns(new SearchWorkOptions
                {
                    MaxSearchDistanceInKm = 1000
                });

            return (query, new FindWorkHandler(this.Repository, mockSearchOptions.Object, this.TestHarness.Bus));
        }

        /// <summary>
        /// Seeds the database.
        /// </summary>
        protected override void SeedDatabase()
        {
            var orders = new OrderFaker().Generate(100);

            this.Repository.GetCollection<Order>().Indexes.CreateManyAsync(
                new[] {
                    new CreateIndexModel<Order>(Builders<Order>.IndexKeys.Geo2DSphere(x => x.Origin.Coordinates)),
                    new CreateIndexModel<Order>(Builders<Order>.IndexKeys.Geo2DSphere(x => x.Destination.Coordinates))
                }
            ).Wait();

            this.Repository.AddRangeWithAudit(orders);
        }
    }
}