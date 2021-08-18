// -----------------------------------------------------------------------
// <copyright file="Unit_CompleteWorkValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Commands.CompleteWork
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Deliveries.Commands.CompleteWork;
    using Xunit;

    /// <summary>
    /// Complete work validator unit test.
    /// </summary>
    /// <seealso cref="UnitTestBase{CompleteWorkValidator}" />
    public class Unit_CompleteWorkValidator : UnitTestBase<CompleteWorkValidator>
    {
        /// <summary>
        /// Fails if driver username is invalid.
        /// </summary>
        [Fact(DisplayName = "Fails if driver username is invalid")]
        public void Fail_if_driver_username_is_invalid()
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.UserName, string.Empty);
        }

        /// <summary>
        /// Fails if identifier is invalid.
        /// </summary>
        [Fact(DisplayName = "Fails if identifier is invalid")]
        public void Fail_if_id_is_invalid()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Id, string.Empty);
            validator.ShouldHaveValidationErrorFor(x => x.Id, this.Faker.Random.Hexadecimal(23, string.Empty));
        }

        /// <summary>
        /// Fails if item photo is not valid base64 image.
        /// </summary>
        [Theory(DisplayName = "Fails if item photo is not valid base64 image")]
        [InlineData("INVALIDATE_R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==")]
        [InlineData("_R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==")]
        public void Fail_if_item_photo_is_not_valid_base64_image(string itemPhoto)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Photo, itemPhoto);
        }

        /// <summary>
        /// Passes if command is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if command is valid")]
        public void Pass_if_command_is_valid()
        {
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.UserName, this.Faker.Person.UserName);
            validator.ShouldNotHaveValidationErrorFor(x => x.Id, this.Faker.Random.Hexadecimal(24, string.Empty));
        }

        /// <summary>
        /// Pass if photo is valid base64 image.
        /// </summary>
        [Theory(DisplayName = "Pass if photo is valid base64 image")]
        [InlineData("image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=")]
        public void Pass_if_origin_photo_is_a_valid_base64_image(string originPhoto)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.Photo, originPhoto);
        }
    }
}