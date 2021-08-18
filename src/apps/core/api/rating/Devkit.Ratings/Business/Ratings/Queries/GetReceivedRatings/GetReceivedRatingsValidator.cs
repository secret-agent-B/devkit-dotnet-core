// -----------------------------------------------------------------------
// <copyright file="GetReceivedRatingsValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Queries.GetReceivedRatings
{
    using System;
    using FluentValidation;

    /// <summary>
    /// Validator for GetReceivedRatingsQuery.
    /// </summary>
    public class GetReceivedRatingsValidator : AbstractValidator<GetReceivedRatingsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetReceivedRatingsValidator"/> class.
        /// </summary>
        public GetReceivedRatingsValidator()
        {
            this.RuleFor(x => x.ReceiverUserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.EndDate)
                .LessThanOrEqualTo(DateTime.UtcNow);

            this.RuleFor(x => x.StartDate)
                .LessThan(query => query.EndDate);
        }
    }
}