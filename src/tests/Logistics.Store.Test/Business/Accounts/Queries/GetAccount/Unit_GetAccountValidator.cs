// -----------------------------------------------------------------------
// <copyright file="Unit_GetAccountValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Accounts.Queries.GetAccount
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Store.API.Business.Accounts.Queries;
    using Xunit;

    /// <summary>
    /// The unit test for the GetAccountValidator.
    /// </summary>
    public class Unit_GetAccountValidator : UnitTestBase<GetAccountValidator>
    {
        /// <summary>
        /// Fails if username is invalid.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        [Theory(DisplayName = "Fails if username is invalid")]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("@!@#.com")]
        [InlineData("1234567890")]
        public void Fail_if_username_is_invalid(string userName)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.UserName, userName);
        }

        /// <summary>
        /// Passes if username is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if username is valid")]
        public void Pass_if_username_is_valid()
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.UserName, this.Faker.Person.Email);
            validator.ShouldNotHaveValidationErrorFor(x => x.UserName, "a@x.com");
        }
    }
}