// -----------------------------------------------------------------------
// <copyright file="Unit_UpdateOrderValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Commands.UpdateOrder
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Orders.Commands.UpdateOrder;
    using Logistics.Orders.Test.Fakers;
    using Xunit;

    /// <summary>
    /// Unit test for UpdateOrder.
    /// </summary>
    public class Unit_UpdateOrder : UnitTestBase<UpdateOrderValidator>
    {
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
        /// Passes if identifier is valid.
        /// </summary>
        [Fact(DisplayName = "Fails if identifier is invalid")]
        public void Pass_if_id_is_valid()
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.Id, this.Faker.Random.Hexadecimal(24, string.Empty));
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
        protected override UpdateOrderValidator Build()
        {
            return new UpdateOrderValidator(new DeliveryOptionsFaker().Generate());
        }
    }
}