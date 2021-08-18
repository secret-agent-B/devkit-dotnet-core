// -----------------------------------------------------------------------
// <copyright file="Unit_OrderDistanceValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Validators
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Validators;
    using Xunit;

    /// <summary>
    /// The Unit_OrderDistanceValidator tests the OrderDistanceValidator.
    /// </summary>
    public class Unit_OrderDistanceValidator : UnitTestBase<OrderDistanceValidator>
    {
        /// <summary>
        /// Fails if text is not empty.
        /// </summary>
        [Fact(DisplayName = "Fails if text is not empty")]
        public void Fail_if_text_is_not_empty()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Text, string.Empty);
        }

        /// <summary>
        /// Fails if value is less than 0.
        /// </summary>
        /// <param name="value">The value.</param>
        [Theory(DisplayName = "Fails if value is less than 0")]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Fail_if_value_is_less_than_0(int value)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Value, value);
        }

        /// <summary>
        /// Passes if text is not empty.
        /// </summary>
        /// <param name="text">The text.</param>
        [Theory(DisplayName = "Passes if text is not empty")]
        [InlineData("5.0mi")]
        [InlineData("1.0mi")]
        public void Pass_if_text_is_not_empty(string text)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.Text, text);
        }

        /// <summary>
        /// Passes if value is greater than 0.
        /// </summary>
        /// <param name="value">The value.</param>
        [Theory(DisplayName = "Passes if value is greater than 0")]
        [InlineData(1)]
        [InlineData(100)]
        public void Pass_if_value_is_greater_than_0(int value)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.Value, value);
        }
    }
}