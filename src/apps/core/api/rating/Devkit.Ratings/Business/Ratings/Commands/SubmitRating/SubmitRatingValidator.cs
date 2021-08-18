// -----------------------------------------------------------------------
// <copyright file="SubmitRatingValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Commands.SubmitRating
{
    using FluentValidation;

    /// <summary>
    /// Validator for SubmitRatingCommand.
    /// </summary>
    /// <seealso cref="AbstractValidator{SubmitRatingCommand}" />
    public class SubmitRatingValidator : AbstractValidator<SubmitRatingCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitRatingValidator"/> class.
        /// </summary>
        public SubmitRatingValidator()
        {
            this.RuleFor(x => x.AuthorUserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.ReceiverUserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Value)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);
        }
    }
}