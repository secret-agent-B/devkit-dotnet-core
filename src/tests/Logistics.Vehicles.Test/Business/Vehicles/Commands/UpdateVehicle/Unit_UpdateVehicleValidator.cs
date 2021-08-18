// -----------------------------------------------------------------------
// <copyright file="Unit_UpdateVehicleValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.Test.Business.Vehicles.Commands.UpdateVehicle
{
    using System;
    using Bogus.Extensions.UnitedKingdom;
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Vehicles.API.Business.Vehicles.Commands.UpdateVehicle;
    using Xunit;

    /// <summary>
    /// The Unit_UpdateVehicleValidator is the unit test for UpdateVehicleValidator.
    /// </summary>
    public class Unit_UpdateVehicleValidator : UnitTestBase<UpdateVehicleValidator>
    {
        /// <summary>
        /// Fails for invalid manufacturer.
        /// </summary>
        /// <param name="manufacturer">The manufacturer.</param>
        [Theory(DisplayName = "Fails for invalid manufacturer")]
        [InlineData("")]
        [InlineData(null)]
        public void Fail_for_invalid_manufacturer(string manufacturer)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.Manufacturer, manufacturer);
        }

        /// <summary>
        /// Fails for invalid model.
        /// </summary>
        /// <param name="model">The model.</param>
        [Theory(DisplayName = "Fails for invalid model")]
        [InlineData("")]
        [InlineData(null)]
        public void Fail_for_invalid_model(string model)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.Model, model);
        }

        /// <summary>
        /// Fails for invalid ownerId.
        /// </summary>
        /// <param name="ownerId">The owner identifier.</param>
        [Theory(DisplayName = "Fails for invalid ownerId")]
        [InlineData("")]
        [InlineData(null)]
        public void Fail_for_invalid_ownerId(string ownerId)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.OwnerUserName, ownerId);
        }

        /// <summary>
        /// Fails for invalid photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        [Theory(DisplayName = "Fails for invalid photo")]
        [InlineData("not-a-base-64-string")]
        public void Fail_for_invalid_photo(string photo)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.Photo, photo);
        }

        /// <summary>
        /// Fails for invalid plate number.
        /// </summary>
        /// <param name="plateNumber">The plate number.</param>
        [Theory(DisplayName = "Fails for invalid plate number")]
        [InlineData("")]
        [InlineData(null)]
        public void Fail_for_invalid_plate_number(string plateNumber)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.PlateNumber, plateNumber);
        }

        /// <summary>
        /// Fails for invalid vin.
        /// </summary>
        /// <param name="vin">The vin.</param>
        [Theory(DisplayName = "Fails for invalid vin")]
        [InlineData("")]
        [InlineData(null)]
        public void Fail_for_invalid_vin(string vin)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.VIN, vin);
        }

        /// <summary>
        /// Fails for invalid vin.
        /// </summary>
        /// <param name="year">The year.</param>
        [Theory(DisplayName = "Fails for invalid year")]
        [InlineData(0)]
        [InlineData(-1)]
        public void Fail_for_invalid_year(int year)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.Year, year);
        }

        /// <summary>
        /// Fails if identifier is invalid.
        /// </summary>
        public void Fail_if_id_is_invalid()
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.Id, string.Empty);
        }

        /// <summary>
        /// Failses for when year is two years from now.
        /// </summary>
        [Fact(DisplayName = "Fails for when year is two years from now")]
        public void Fails_for_when_year_is_two_years_from_now()
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.Year, DateTime.Now.Year + 2);
        }

        /// <summary>
        /// Passeses if the command is valid.
        /// </summary>
        [Fact(DisplayName = "Passeses if the command is valid")]
        public void Passes_if_the_command_is_valid()
        {
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.Id, this.Faker.Random.Hexadecimal(24, string.Empty));
            validator.ShouldNotHaveValidationErrorFor(x => x.Manufacturer, this.Faker.Vehicle.Manufacturer());
            validator.ShouldNotHaveValidationErrorFor(x => x.Model, this.Faker.Vehicle.Model());
            validator.ShouldNotHaveValidationErrorFor(x => x.OwnerUserName, this.Faker.Random.Hexadecimal(24, string.Empty));
            validator.ShouldNotHaveValidationErrorFor(x => x.Photo, "image/jpeg;base64,R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==");
            validator.ShouldNotHaveValidationErrorFor(x => x.PlateNumber, this.Faker.Vehicle.GbRegistrationPlate(this.Faker.Date.Past(4), DateTime.Now));
            validator.ShouldNotHaveValidationErrorFor(x => x.VIN, this.Faker.Vehicle.Vin());
            validator.ShouldNotHaveValidationErrorFor(x => x.Year, DateTime.Now.Year);
        }
    }
}