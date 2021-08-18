// -----------------------------------------------------------------------
// <copyright file="Unit_PickUpWorkValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Commands.PickUpWork
{
    using System.Linq;
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Deliveries.Commands.PickUpWork;
    using Xunit;

    /// <summary>
    /// Unit_PickUpHandler class is the unit test for PickUpWorkValidator.
    /// </summary>
    public class Unit_PickUpWorkValidator : UnitTestBase<PickUpWorkValidator>
    {
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
        /// Fails if order is missing.
        /// </summary>
        [Fact(DisplayName = "Fails if order is missing")]
        public void Fail_if_order_is_missing()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Id, string.Empty);

            foreach (var count in Enumerable.Range(0, 23))
            {
                validator.ShouldHaveValidationErrorFor(x => x.Id, this.Faker.Random.Hexadecimal(count, string.Empty));
            }
        }

        /// <summary>
        /// Passes if order identifier is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if order identifier is valid")]
        public void Pass_if_order_id_is_valid()
        {
            var validator = this.Build();
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