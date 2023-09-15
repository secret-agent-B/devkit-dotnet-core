// -----------------------------------------------------------------------
// <copyright file="Unit_GetMyProfileValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Test.CQRS.Users.Queries.GetMyProfile
{
    using Devkit.Security.Business.Users.Queries.GetMyProfile;
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    /// <summary>
    /// Unit_GetMyProfileValidator class is the unit test for GetMyProfileValidator.
    /// </summary>
    public class Unit_GetMyProfileValidator : UnitTestBase<GetMyProfileValidator>
    {
        /// <summary>
        /// Fails if username is invalid.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        [Theory()]
        [TestCase("")]
        [TestCase("1")]
        [TestCase("@!@#.com")]
        [TestCase("1234567890")]
        public void Fail_if_username_is_invalid(string userName)
        {
            var validator = this.Build();
            var model = new GetMyProfileQuery
            {
                UserName = userName,
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserName);
        }

        /// <summary>
        /// Passes if username is valid.
        /// </summary>
        [Theory()]
        [TestCase("a@x.com")]
        [TestCase("radriano@microsoft.com")]
        public void Pass_if_username_is_valid()
        {
            var validator = this.Build();
            var model = new GetMyProfileQuery
            {
                UserName = this.Faker.Person.Email
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserName);
        }
    }
}