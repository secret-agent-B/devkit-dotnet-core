// -----------------------------------------------------------------------
// <copyright file="Unit_UpdateVehicleHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.Test.Business.Vehicles.Commands.UpdateVehicle
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Bogus;
    using Bogus.Extensions.UnitedKingdom;
    using Devkit.Test;
    using Logistics.Vehicles.API.Business.Vehicles.Commands.UpdateVehicle;
    using Logistics.Vehicles.API.Data;
    using Logistics.Vehicles.Test.Fakers;
    using Xunit;

    /// <summary>
    /// The Unit_UpdateVehicleHandler is the unit test for UpdateVehicleHandler.
    /// </summary>
    /// <seealso cref="UnitTestBase{(UpdateVehicleCommand command, UpdateVehicleHandler handler)}" />
    public class Unit_UpdateVehicleHandler : UnitTestBase<(UpdateVehicleCommand command, UpdateVehicleHandler handler)>
    {
        /// <summary>
        /// Should be able to update a vehicle.
        /// </summary>
        [Fact(DisplayName = "Should be able to update a vehicle")]
        public async Task Should_be_able_to_update_a_vehicle()
        {
            var (command, handler) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(command.Id, response.Id);
            Assert.Equal(command.Manufacturer, response.Manufacturer);
            Assert.Equal(command.Model, response.Model);
            Assert.Equal(command.OwnerUserName, response.OwnerUserName);
            Assert.Equal(command.Photo, response.Photo);
            Assert.Equal(command.PlateNumber, response.PlateNumber);
            Assert.Equal(command.VIN, response.VIN);
            Assert.Equal(command.Year, response.Year);
            Assert.NotNull(response.CreatedOn);
            Assert.NotNull(response.LastUpdatedOn);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (UpdateVehicleCommand command, UpdateVehicleHandler handler) Build()
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

            var handler = new UpdateVehicleHandler(this.Repository);

            return (commandFaker.Generate(), handler);
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