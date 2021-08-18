// -----------------------------------------------------------------------
// <copyright file="GetSubmittedRatingsValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Queries.GetSubmittedRatings
{
    using System;
    using FluentValidation;

    /// <summary>
    /// Get submitted ratings validator.
    /// </summary>
    /// <seealso cref="AbstractValidator{GetSubmittedRatingsQuery}" />
    public class GetSubmittedRatingsValidator : AbstractValidator<GetSubmittedRatingsQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSubmittedRatingsValidator"/> class.
        /// </summary>
        public GetSubmittedRatingsValidator()
        {
            this.RuleFor(x => x.AuthorUserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.EndDate)
                .LessThanOrEqualTo(DateTime.UtcNow);

            this.RuleFor(x => x.StartDate)
                .LessThan(query => query.EndDate);
        }
    }
}