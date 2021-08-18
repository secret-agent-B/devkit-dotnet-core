// -----------------------------------------------------------------------
// <copyright file="GetTransactionRatingValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Queries.GetTransactionRating
{
    using FluentValidation;

    /// <summary>
    /// The validator for the GetTransactionRatingQuery.
    /// </summary>
    public class GetTransactionRatingValidator : AbstractValidator<GetTransactionRatingQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactionRatingValidator" /> class.
        /// </summary>
        public GetTransactionRatingValidator()
        {
            this.RuleFor(x => x.TransactionId)
                .NotNull()
                .NotEmpty();
        }
    }
}