// -----------------------------------------------------------------------
// <copyright file="Unit_GetReceivedRatingValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Test.Business.Ratings.Queries.GetReceivedRatings
{
    using System;
    using Devkit.Ratings.Business.Ratings.Queries.GetReceivedRatings;
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using MongoDB.Driver.Core.Authentication;
    using NUnit.Framework;

    /// <summary>
    /// Unit_GetReceivedRatingValidator class is the unit test for GetReceivedRatingValidator.
    /// </summary>
    public class Unit_GetReceivedRatingValidator : UnitTestBase<GetReceivedRatingsValidator>
    {
        /// <summary>
        /// Fails if date ranges do not agree.
        /// </summary>
        [TestCase(TestName = "Fails if date ranges do not agree")]
        public void Fail_if_date_ranges_do_not_agree()
        {
            var validator = this.Build();
            var query = new GetReceivedRatingsQuery
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
            var result = validator.TestValidate(new GetReceivedRatingsQuery());

            result.ShouldHaveValidationErrorFor(x => x.ReceiverUserName);
        }

        /// <summary>
        /// Passes if date ranges agree.
        /// </summary>
        [TestCase(TestName = "Passes if date ranges agree")]
        public void Pass_if_date_ranges_agree()
        {
            var validator = this.Build();
            var query = new GetReceivedRatingsQuery
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
        public void Pass_if_receiver_username_is_valid(string username)
        {
            var validator = this.Build();
            var query = new GetReceivedRatingsQuery
            {
                ReceiverUserName = username
            };
            var result = validator.TestValidate(query);

            result.ShouldNotHaveValidationErrorFor(x => x.ReceiverUserName);
        }
    }
}