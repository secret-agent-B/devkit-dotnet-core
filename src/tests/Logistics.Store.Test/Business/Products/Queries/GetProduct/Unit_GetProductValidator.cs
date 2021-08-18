// -----------------------------------------------------------------------
// <copyright file="Unit_GetProductValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Products.Queries.GetProduct
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Store.API.Business.Products.Queries.GetProduct;
    using Xunit;

    /// <summary>
    /// Unit_GetProductValidator class is the unit test for the GetProductValidator.
    /// </summary>
    public class Unit_GetProductValidator : UnitTestBase<GetProductValidator>
    {
        /// <summary>
        /// Fails if identifier is invalid.
        /// </summary>
        [Theory(DisplayName = "Fails if identifier is invalid")]
        [InlineData("")]
        [InlineData("123456789012345678901234567890")] // 30 chars
        [InlineData("12345678901234567890123")] // 23 chars
        public void Fail_if_id_is_invalid(string id)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Id, id);
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