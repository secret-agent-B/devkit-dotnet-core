// -----------------------------------------------------------------------
// <copyright file="Intg_AddVehicle.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.Test.Business.Vehicles.Commands.AddVehicle
{
    using System;
    using System.Threading.Tasks;
    using Bogus;
    using Bogus.Extensions.UnitedKingdom;
    using Devkit.Test;
    using Logistics.Vehicles.API;
    using Logistics.Vehicles.API.Business.Vehicles.Commands.AddVehicle;
    using Logistics.Vehicles.API.Business.ViewModels;
    using Xunit;

    /// <summary>
    /// The Intg_AddVehicle is the integration test for AddVehicleCommand.
    /// </summary>
    public class Intg_AddVehicle : IntegrationTestBase<AddVehicleCommand, Startup>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_AddVehicle"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_AddVehicle(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
        }

        /// <summary>
        /// Should be able to add a new vehicle.
        /// </summary>
        [Fact(DisplayName = "Should be able to add a new vehicle")]
        public async Task Should_be_able_to_add_a_new_vehicle()
        {
            var command = this.Build();
            var response = await this.PostAsync<VehicleVM>("/vehicles", command);

            Assert.True(response.IsSuccessfulStatusCode);
            Assert.Equal(command.Manufacturer, response.Payload.Manufacturer);
            Assert.Equal(command.Model, response.Payload.Model);
            Assert.Equal(command.OwnerUserName, response.Payload.OwnerUserName);
            Assert.Equal(command.Photo, response.Payload.Photo);
            Assert.Equal(command.PlateNumber, response.Payload.PlateNumber);
            Assert.Equal(command.VIN, response.Payload.VIN);
            Assert.Equal(command.Year, response.Payload.Year);
            Assert.Equal(null, response.Payload.LastUpdatedOn);
            Assert.NotEmpty(response.Payload.Id);
            Assert.NotEqual(DateTime.MinValue, response.Payload.CreatedOn);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override AddVehicleCommand Build()
        {
            var commandFaker = new Faker<AddVehicleCommand>()
                .RuleFor(x => x.Manufacturer, this.Faker.Vehicle.Manufacturer())
                .RuleFor(x => x.Model, this.Faker.Vehicle.Model())
                .RuleFor(x => x.OwnerUserName, this.Faker.Random.Hexadecimal(24, string.Empty))
                .RuleFor(x => x.Photo, "R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==")
                .RuleFor(x => x.PlateNumber, this.Faker.Vehicle.GbRegistrationPlate(this.Faker.Date.Past(4), DateTime.Now))
                .RuleFor(x => x.VIN, this.Faker.Vehicle.Vin())
                .RuleFor(x => x.Year, DateTime.Now.Year);

            return commandFaker.Generate();
        }
    }
}