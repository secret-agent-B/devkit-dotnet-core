// -----------------------------------------------------------------------
// <copyright file="Unit_GetSubmittedRatingsValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Test.Business.Ratings.Queries.GetSubmittedRatings
{
    using System;
    using Devkit.Ratings.Business.Ratings.Queries.GetSubmittedRatings;
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using NUnit.Framework;

    /// <summary>
    /// Unit_GetSubmittedRatingValidator class is the unit test for GetSubmittedRatingValidator.
    /// </summary>
    public class Unit_GetSubmittedRatingsValidator : UnitTestBase<GetSubmittedRatingsValidator>
    {
        /// <summary>
        /// Fails if date ranges do not agree.
        /// </summary>
        [TestCase(TestName = "Fails if date ranges do not agree")]
        public void Fail_if_date_ranges_do_not_agree()
        {
            var validator = this.Build();
            var query = new GetSubmittedRatingsQuery
            {
                StartDate = DateTime.UtcNow,
                EndDate = this.Faker.Date.Recent(5)
            };
            var result = validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.StartDate);
        }

        /// <summary>
        /// Fails if receiver name is invalid.
        /// </summary>
        [TestCase(TestName = "Fails if receiver name is invalid")]
        public void Fail_if_receiver_name_is_invalid()
        {
            var validator = this.Build();
            var query = new GetSubmittedRatingsQuery
            {
                AuthorUserName = string.Empty
            };
            var result = validator.TestValidate(query);

            result.ShouldHaveValidationErrorFor(x => x.AuthorUserName);
        }

        /// <summary>
        /// Passes if date ranges agree.
        /// </summary>
        [TestCase(TestName = "Passes if date ranges agree")]
        public void Pass_if_date_ranges_agree()
        {
            var validator = this.Build();
            var query = new GetSubmittedRatingsQuery
            {
                StartDate = this.Faker.Date.Recent(5),
                EndDate = DateTime.UtcNow
            };
            var result = validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(x => x.StartDate);
        }

        /// <summary>
        /// Passes if receiver username is valid.
        /// </summary>
        [Theory()]
        [TestCase("x@x.com")]
        [TestCase("radriano")]
        public void Pass_if_receiver_username_is_valid(string userName)
        {
            var validator = this.Build();
            var query = new GetSubmittedRatingsQuery
            {
                AuthorUserName = userName
            };
            var result = validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(x => x.AuthorUserName);
            result.ShouldNotHaveValidationErrorFor(x => x.AuthorUserName);
        }
    }
}