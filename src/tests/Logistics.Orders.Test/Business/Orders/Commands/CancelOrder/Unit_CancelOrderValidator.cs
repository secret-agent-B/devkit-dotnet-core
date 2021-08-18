// -----------------------------------------------------------------------
// <copyright file="Unit_CancelOrderValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Commands.CancelOrder
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Orders.Commands.CancelOrder;
    using Xunit;

    /// <summary>
    /// The unit test for cancel order validator.
    /// </summary>
    public class Unit_CancelOrderValidator : UnitTestBase<CancelOrderValidator>
    {
        /// <summary>
        /// Fails if client username is empty.
        /// </summary>
        [Fact(DisplayName = "Fails if client username is empty")]
        public void Fail_if_client_username_is_empty()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.UserName, string.Empty);
        }

        /// <summary>
        /// Fails if comment is invalid.
        /// </summary>
        [Fact(DisplayName = "Fails if comment is invalid")]
        public void Fail_if_comment_is_invalid()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Comment, "");
            validator.ShouldHaveValidationErrorFor(x => x.Comment, this.Faker.Random.AlphaNumeric(24));
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
        /// Passes if client username is valid.
        /// </summary>
        [Theory(DisplayName = "Passes if client username is valid")]
        [InlineData("client.name")]
        [InlineData("myusername")]
        [InlineData("my-test-username")]
        public void Pass_if_client_username_is_valid(string clientUserName)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.UserName, clientUserName);
        }

        /// <summary>
        /// Passes if comment is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if comment is valid")]
        public void Pass_if_comment_is_valid()
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.Comment, this.Faker.Random.AlphaNumeric(25));
        }

        /// <summary>
        /// Passes if identifier is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if identifier is valid")]
        public void Pass_if_id_is_valid()
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.Id, this.Faker.Random.Hexadecimal(24, string.Empty));
        }
    }
}