// -----------------------------------------------------------------------
// <copyright file="Unit_FindWorkValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Queries.FindWork
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Deliveries.Queries.FindWork;
    using Xunit;

    /// <summary>
    /// The Unit_FindWorkValidator is the unit test for FindWorkValidator.
    /// </summary>
    public class Unit_FindWorkValidator : UnitTestBase<FindWorkValidator>
    {
        /// <summary>
        /// Fails if exclude username is invalid.
        /// </summary>
        [Theory(DisplayName = "Fails if exclude username is invalid")]
        [InlineData("")] // empty
        [InlineData("1234")] // too short
        public void Fail_if_exclude_username_is_invalid(string excludeUserName)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.ExcludeUserName, excludeUserName);
        }

        /// <summary>
        /// Fails if page is invalid.
        /// </summary>
        [Fact(DisplayName = "Fails if page is invalid")]
        public void Fail_if_page_is_invalid()
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.Page, 0);
        }

        /// <summary>
        /// Fails if page size is out of the bounds.
        /// </summary>
        [Theory(DisplayName = "Fails if page size is out of the bounds")]
        [InlineData(9)]
        [InlineData(51)]
        public void Fail_if_page_size_is_out_of_bounds(int pageSize)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.PageSize, pageSize);
        }

        /// <summary>
        /// Passes if query is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if query is valid")]
        public void Pass_if_query_is_valid()
        {
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.ExcludeUserName, this.Faker.Person.UserName);
            validator.ShouldNotHaveValidationErrorFor(x => x.Page, this.Faker.Random.Int(1, 10));
            validator.ShouldNotHaveValidationErrorFor(x => x.PageSize, this.Faker.Random.Int(10, 50));
        }
    }
}