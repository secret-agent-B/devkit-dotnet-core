// -----------------------------------------------------------------------
// <copyright file="Intg_GetVehicles.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.Test.Business.Vehicles.Queries.GetVehicles
{
    using System.Threading.Tasks;
    using Devkit.Patterns.CQRS;
    using Devkit.Test;
    using Logistics.Vehicles.API;
    using Logistics.Vehicles.API.Business.Vehicles.Queries.GetVehicles;
    using Logistics.Vehicles.API.Business.ViewModels;
    using Logistics.Vehicles.API.Data;
    using Logistics.Vehicles.Test.Fakers;
    using Xunit;

    /// <summary>
    /// The Intg_GetVehicles is the integration test for GetVehicles.
    /// </summary>
    public class Intg_GetVehicles : IntegrationTestBase<GetVehiclesQuery, Startup>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_GetVehicles"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_GetVehicles(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to get vehicles by owner username.
        /// </summary>
        /// <returns>A task.</returns>
        [Fact(DisplayName = "Should be able to get vehicles by owner username")]
        public async Task Should_be_able_to_get_vehicles_by_owner_username()
        {
            var query = this.Build();
            var response = await this.GetAsync<ResponseSet<VehicleVM>>($"/vehicles/{query.OwnerUserName}");

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.NotEmpty(response.Payload.Items);

            foreach (var item in response.Payload.Items)
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
        protected override GetVehiclesQuery Build()
        {
            return new GetVehiclesQuery
            {
                OwnerUserName = this.Faker.PickRandom(this.Repository.All<Vehicle>()).OwnerUserName
            };
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