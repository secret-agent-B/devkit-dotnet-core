// -----------------------------------------------------------------------
// <copyright file="Unit_AddVehicleHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.Test.Business.Vehicles.Commands.AddVehicle
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Bogus;
    using Bogus.Extensions.UnitedKingdom;
    using Devkit.Test;
    using Logistics.Vehicles.API.Business.Vehicles.Commands.AddVehicle;
    using Xunit;

    /// <summary>
    /// The Unit_AddVehicleHandler is the unit test for AddVehicleHandler.
    /// </summary>
    /// <seealso cref="UnitTestBase{(AddVehicleCommand command, AddVehicleHandler handler)}" />
    public class Unit_AddVehicleHandler : UnitTestBase<(AddVehicleCommand command, AddVehicleHandler handler)>
    {
        /// <summary>
        /// Should be able to add a vehicle.
        /// </summary>
        [Fact(DisplayName = "Should be able to add a vehicle")]
        public async Task Should_be_able_to_add_a_vehicle()
        {
            var (command, handler) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(command.Manufacturer, response.Manufacturer);
            Assert.Equal(command.Model, response.Model);
            Assert.Equal(command.OwnerUserName, response.OwnerUserName);
            Assert.Equal(command.Photo, response.Photo);
            Assert.Equal(command.PlateNumber, response.PlateNumber);
            Assert.Equal(command.VIN, response.VIN);
            Assert.Equal(command.Year, response.Year);
            Assert.Equal(null, response.LastUpdatedOn);
            Assert.NotEmpty(response.Id);
            Assert.NotEqual(DateTime.MinValue, response.CreatedOn);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (AddVehicleCommand command, AddVehicleHandler handler) Build()
        {
            var commandFaker = new Faker<AddVehicleCommand>()
                .RuleFor(x => x.Manufacturer, this.Faker.Vehicle.Manufacturer())
                .RuleFor(x => x.Model, this.Faker.Vehicle.Model())
                .RuleFor(x => x.OwnerUserName, this.Faker.Random.Hexadecimal(24, string.Empty))
                .RuleFor(x => x.Photo, "image/jpeg;base64,R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==")
                .RuleFor(x => x.PlateNumber, this.Faker.Vehicle.GbRegistrationPlate(this.Faker.Date.Past(4), DateTime.Now))
                .RuleFor(x => x.VIN, this.Faker.Vehicle.Vin())
                .RuleFor(x => x.Year, DateTime.Now.Year);

            var handler = new AddVehicleHandler(this.Repository);

            return (commandFaker.Generate(), handler);
        }
    }
}