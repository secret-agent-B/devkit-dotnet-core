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
    using Xunit;

    /// <summary>
    /// Unit_GetReceivedRatingValidator class is the unit test for GetReceivedRatingValidator.
    /// </summary>
    public class Unit_GetReceivedRatingValidator : UnitTestBase<GetReceivedRatingsValidator>
    {
        /// <summary>
        /// Fails if date ranges do not agree.
        /// </summary>
        [Fact(DisplayName = "Fails if date ranges do not agree")]
        public void Fail_if_date_ranges_do_not_agree()
        {
            var validator = this.Build();

            var query = new GetReceivedRatingsQuery
            {
                StartDate = DateTime.UtcNow,
                EndDate = this.Faker.Date.Recent(5)
            };

            validator.ShouldHaveValidationErrorFor(x => x.StartDate, query);
        }

        /// <summary>
        /// Fails if receiver name is invalid.
        /// </summary>
        [Fact(DisplayName = "Fails if receiver name is invalid")]
        public void Fail_if_receiver_name_is_invalid()
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.ReceiverUserName, string.Empty);
        }

        /// <summary>
        /// Passes if date ranges agree.
        /// </summary>
        [Fact(DisplayName = "Passes if date ranges agree")]
        public void Pass_if_date_ranges_agree()
        {
            var validator = this.Build();

            var query = new GetReceivedRatingsQuery
            {
                StartDate = this.Faker.Date.Recent(5),
                EndDate = DateTime.UtcNow
            };

            validator.ShouldNotHaveValidationErrorFor(x => x.StartDate, query);
        }

        /// <summary>
        /// Passes if receiver username is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if receiver username is valid")]
        public void Pass_if_receiver_username_is_valid()
        {
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.ReceiverUserName, this.Faker.Person.Email);
            validator.ShouldNotHaveValidationErrorFor(x => x.ReceiverUserName, this.Faker.Person.UserName);
        }
    }
}