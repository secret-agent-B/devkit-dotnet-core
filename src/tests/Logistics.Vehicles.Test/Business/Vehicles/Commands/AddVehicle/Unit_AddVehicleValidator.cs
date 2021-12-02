// -----------------------------------------------------------------------
// <copyright file="Unit_AddVehicleValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.Test.Business.Vehicles.Commands.AddVehicle
{
    using System;
    using Bogus.Extensions.UnitedKingdom;
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Vehicles.API.Business.Vehicles.Commands.AddVehicle;
    using Xunit;

    /// <summary>
    /// The Unit_AddVehicleValidator is the unit test for AddVehicleValidator.
    /// </summary>
    public class Unit_AddVehicleValidator : UnitTestBase<AddVehicleValidator>
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
            var validator = this.Build()
                .TestValidate(
                    new AddVehicleCommand { 
                        Manufacturer = manufacturer 
                    });
            
            validator.ShouldHaveValidationErrorFor(x => x.Manufacturer);
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
            var validator = this.Build()
                .TestValidate(
                    new AddVehicleCommand { 
                        Model = model
                    });

            validator.ShouldHaveValidationErrorFor(x => x.Model);
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
            var validator = this.Build()
                .TestValidate(
                    new AddVehicleCommand { 
                        OwnerUserName = ownerId
                    });

            validator.ShouldHaveValidationErrorFor(x => x.OwnerUserName);
        }

        /// <summary>
        /// Fails for invalid photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        [Theory(DisplayName = "Fails for invalid photo")]
        [InlineData("not-a-base-64-string")]
        public void Fail_for_invalid_photo(string photo)
        {
            var validator = this.Build()
                .TestValidate(
                    new AddVehicleCommand { 
                        Photo = photo
                    });

            validator.ShouldHaveValidationErrorFor(x => x.Photo);
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
            var validator = this.Build()
                .TestValidate(
                    new AddVehicleCommand { 
                        PlateNumber = plateNumber
                    });

            validator.ShouldHaveValidationErrorFor(x => x.PlateNumber);
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
            var validator = this.Build()
                .TestValidate(
                    new AddVehicleCommand { 
                        VIN = vin
                    });

            validator.ShouldHaveValidationErrorFor(x => x.VIN);
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
            var validator = this.Build()
                .TestValidate(
                    new AddVehicleCommand { 
                        Year = year
                    });

            validator.ShouldHaveValidationErrorFor(x => x.Year);
        }

        /// <summary>
        /// Failses for when year is two years from now.
        /// </summary>
        [Fact(DisplayName = "Fails for when year is two years from now")]
        public void Fails_for_when_year_is_two_years_from_now()
        {
            var validator = this.Build()
                .TestValidate(
                    new AddVehicleCommand { 
                        Year = DateTime.Now.Year + 2
                    });

            validator.ShouldHaveValidationErrorFor(x => x.Year);
        }

        /// <summary>
        /// Passeses if the command is valid.
        /// </summary>
        [Fact(DisplayName = "Passeses if the command is valid")]
        public void Passes_if_the_command_is_valid()
        {
            var validator = this.Build()
                .TestValidate(
                    new AddVehicleCommand { 
                        Manufacturer = this.Faker.Vehicle.Manufacturer(),
                        Model = this.Faker.Vehicle.Model(),
                        OwnerUserName = this.Faker.Random.Hexadecimal(24, string.Empty),
                        Photo = "image/jpeg;base64,R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==",
                        PlateNumber = this.Faker.Vehicle.GbRegistrationPlate(this.Faker.Date.Past(4), DateTime.Now),
                        VIN = this.Faker.Vehicle.Vin(),
                        Year = DateTime.Now.Year
                    });

            validator.ShouldNotHaveValidationErrorFor(x => x.Manufacturer);
            validator.ShouldNotHaveValidationErrorFor(x => x.Model);
            validator.ShouldNotHaveValidationErrorFor(x => x.OwnerUserName);
            validator.ShouldNotHaveValidationErrorFor(x => x.Photo);
            validator.ShouldNotHaveValidationErrorFor(x => x.PlateNumber);
            validator.ShouldNotHaveValidationErrorFor(x => x.VIN);
            validator.ShouldNotHaveValidationErrorFor(x => x.Year);
        }
    }
}