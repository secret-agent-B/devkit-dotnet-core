// -----------------------------------------------------------------------
// <copyright file="Intg_UpdateVehicle.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.Test.Business.Vehicles.Commands.UpdateVehicle
{
    using System;
    using System.Threading.Tasks;
    using Bogus;
    using Bogus.Extensions.UnitedKingdom;
    using Devkit.Test;
    using Logistics.Vehicles.API;
    using Logistics.Vehicles.API.Business.Vehicles.Commands.UpdateVehicle;
    using Logistics.Vehicles.API.Business.ViewModels;
    using Logistics.Vehicles.API.Data;
    using Logistics.Vehicles.Test.Fakers;
    using Xunit;

    /// <summary>
    /// The Intg_UpdateVehicle is the integration test for UpdateVehicleCommand.
    /// </summary>
    public class Intg_UpdateVehicle : IntegrationTestBase<UpdateVehicleCommand, Startup>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_UpdateVehicle"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_UpdateVehicle(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to update a new vehicle.
        /// </summary>
        [Fact(DisplayName = "Should be able to update a new vehicle")]
        public async Task Should_be_able_to_update_a_new_vehicle()
        {
            var command = this.Build();
            var response = await this.PutAsync<VehicleVM>("/vehicles", command);

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(command.Id, response.Payload.Id);
            Assert.Equal(command.Manufacturer, response.Payload.Manufacturer);
            Assert.Equal(command.Model, response.Payload.Model);
            Assert.Equal(command.OwnerUserName, response.Payload.OwnerUserName);
            Assert.Equal(command.Photo, response.Payload.Photo);
            Assert.Equal(command.PlateNumber, response.Payload.PlateNumber);
            Assert.Equal(command.VIN, response.Payload.VIN);
            Assert.Equal(command.Year, response.Payload.Year);
            Assert.NotNull(response.Payload.CreatedOn);
            Assert.NotNull(response.Payload.LastUpdatedOn);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override UpdateVehicleCommand Build()
        {
            var commandFaker = new Faker<UpdateVehicleCommand>()
                .RuleFor(x => x.Id, f => f.PickRandom(this.Repository.All<Vehicle>()).Id)
                .RuleFor(x => x.Manufacturer, this.Faker.Vehicle.Manufacturer())
                .RuleFor(x => x.Model, this.Faker.Vehicle.Model())
                .RuleFor(x => x.OwnerUserName, this.Faker.Random.Hexadecimal(24, string.Empty))
                .RuleFor(x => x.Photo, "image/jpeg;base64,R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==")
                .RuleFor(x => x.PlateNumber, this.Faker.Vehicle.GbRegistrationPlate(this.Faker.Date.Past(4), DateTime.Now))
                .RuleFor(x => x.VIN, this.Faker.Vehicle.Vin())
                .RuleFor(x => x.Year, DateTime.Now.Year);

            return commandFaker.Generate();
        }

        /// <summary>
        /// Seeds the database.
        /// </summary>
        protected override void SeedDatabase()
        {
            this.Repository.AddRangeWithAudit(new VehicleFaker().Generate(10));
        }
    }
}