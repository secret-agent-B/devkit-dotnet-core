// -----------------------------------------------------------------------
// <copyright file="Unit_GetVehiclesHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.Test.Business.Vehicles.Queries.GetVehicles
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Test;
    using Logistics.Vehicles.API.Business.Vehicles.Queries.GetVehicles;
    using Logistics.Vehicles.API.Data;
    using Logistics.Vehicles.Test.Fakers;
    using Xunit;

    /// <summary>
    /// The Unit_GetVehiclesHandler is the unit test for GetVehiclesHandler.
    /// </summary>
    public class Unit_GetVehiclesHandler : UnitTestBase<(GetVehiclesQuery query, GetVehiclesHandler handler)>
    {
        /// <summary>
        /// Should be able to get vehicles by owner username.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Should be able to get vehicles by owner username")]
        public async Task Should_be_able_to_get_vehicles_by_owner_username()
        {
            var (query, handler) = this.Build();
            var response = await handler.Handle(query, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.NotEmpty(response.Items);

            foreach (var item in response.Items)
            {
                Assert.Equal(query.OwnerUserName, item.OwnerUserName);
            }
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (GetVehiclesQuery query, GetVehiclesHandler handler) Build()
        {
            var query = new GetVehiclesQuery
            {
                OwnerUserName = this.Faker.PickRandom(this.Repository.All<Vehicle>()).OwnerUserName
            };

            return (query, new GetVehiclesHandler(this.Repository));
        }

        /// <summary>
        /// Seeds the database.
        /// </summary>
        protected override void SeedDatabase()
        {
            this.Repository.Add<Vehicle>(new VehicleFaker().Generate(10));
        }
    }
}