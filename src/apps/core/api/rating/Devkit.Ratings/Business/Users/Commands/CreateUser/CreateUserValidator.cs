// -----------------------------------------------------------------------
// <copyright file="CreateUserValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Users.Commands.CreateUser
{
    using FluentValidation;

    /// <summary>
    /// The validator for CreateUserCommand.
    /// </summary>
    /// <seealso cref="AbstractValidator{CreateUserCommand}" />
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserValidator"/> class.
        /// </summary>
        public CreateUserValidator()
        {
            this.RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();
        }
    }
}