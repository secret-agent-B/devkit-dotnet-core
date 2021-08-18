// -----------------------------------------------------------------------
// <copyright file="Unit_SubmitOrderValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Commands.SubmitOrder
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Orders.Commands.SubmitOrder;
    using Logistics.Orders.Test.Fakers;
    using Xunit;

    /// <summary>
    /// Unit test for SubmitOrder.
    /// </summary>
    public class Unit_SubmitOrderValidator : UnitTestBase<SubmitOrderValidator>
    {
        /// <summary>
        /// Fails if client username is empty.
        /// </summary>
        [Fact(DisplayName = "Fails if client username is empty")]
        public void Fail_if_client_username_is_empty()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.ClientUserName, string.Empty);
        }

        /// <summary>
        /// Fails if estimated item weight is invalid.
        /// </summary>
        /// <param name="estItemWeight">The est item weight.</param>
        [Theory(DisplayName = "Fails if estimated item weight is invalid")]
        [InlineData(-1.0)]
        [InlineData(0)]
        public void Fail_if_estimated_item_weight_is_invalid(double estItemWeight)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.EstimatedItemWeight, estItemWeight);
        }

        /// <summary>
        /// Fails if item name is too short.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        [Theory(DisplayName = "Fails if item name is too short")]
        [InlineData("")]
        [InlineData("X")]
        [InlineData("XY")]
        public void Fail_if_item_name_is_too_short(string itemName)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.ItemName, itemName);
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
            validator.ShouldHaveValidationErrorFor(x => x.ItemPhoto, itemPhoto);
        }

        /// <summary>
        /// Fails if origin photo is not valid base64 image.
        /// </summary>
        [Theory(DisplayName = "Fails if origin photo is not valid base64 image")]
        [InlineData("INVALIDATE_R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==")]
        [InlineData("_R0lGODlhAQABAIAAAAAAAAAAACH5BAAAAAAALAAAAAABAAEAAAICTAEAOw==")]
        public void Fail_if_origin_photo_is_not_valid_base64_image(string originPhoto)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.OriginPhoto, originPhoto);
        }

        /// <summary>
        /// Fails if receiver name is too short.
        /// </summary>
        /// <param name="receiverName">Name of the receiver.</param>
        [Theory(DisplayName = "Fails if receiver name is too short")]
        [InlineData("")]
        [InlineData("X")]
        [InlineData("XY")]
        [InlineData("XYZ")]
        [InlineData("XYZA")]
        public void Fail_if_receiver_name_is_too_short(string receiverName)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.RecipientName, receiverName);
        }

        /// <summary>
        /// Passes if client username is valid.
        /// </summary>
        [Theory(DisplayName = "Passes if client username is valid")]
        [InlineData("client.name")]
        [InlineData("myusername")]
        [InlineData("my-test-username")]
        public void Pass_if_client_username_is_valid(string clientUserName)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.ClientUserName, clientUserName);
        }

        /// <summary>
        /// Passes if item name is too short.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        [Theory(DisplayName = "Passes if item name is too short")]
        [InlineData("Pen")]
        [InlineData("Book")]
        [InlineData("Shoes")]
        public void Pass_if_item_name_is_long_enough(string itemName)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.ItemName, itemName);
        }

        /// <summary>
        /// Pass if item photo is a valid base64 image.
        /// </summary>
        [Theory(DisplayName = "Pass if item photo is a valid base64 image")]
        [InlineData("image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=")]
        public void Pass_if_item_photo_is_a_valid_base64_image(string itemPhoto)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.ItemPhoto, itemPhoto);
        }

        /// <summary>
        /// Pass if origin photo is valid base64 image.
        /// </summary>
        [Theory(DisplayName = "Pass if origin photo is valid base64 image")]
        [InlineData("image/jpeg;base64,R0lGODlhAQABAIAAAAUEBAAAACwAAAAAAQABAAACAkQBADs=")]
        public void Pass_if_origin_photo_is_a_valid_base64_image(string originPhoto)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.OriginPhoto, originPhoto);
        }

        /// <summary>
        /// Passes if receiver name is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if receiver name is valid")]
        public void Pass_if_receiver_name_is_valid()
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.RecipientName, this.Faker.Person.FullName);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override SubmitOrderValidator Build()
        {
            return new SubmitOrderValidator(new DeliveryOptionsFaker().Generate());
        }
    }
}