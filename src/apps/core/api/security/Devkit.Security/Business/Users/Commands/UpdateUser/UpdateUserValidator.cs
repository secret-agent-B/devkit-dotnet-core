// -----------------------------------------------------------------------
// <copyright file="UpdateUserValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Business.Users.Commands.UpdateUser
{
    using FluentValidation;
    using Devkit.Patterns.Extensions;

    /// <summary>
    /// UpdateUserValidator class is validator for UpdateUserCommand.
    /// </summary>
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateUserValidator"/> class.
        /// </summary>
        public UpdateUserValidator()
        {
            this.RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            this.RuleFor(x => x.Address1)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5);

            this.RuleFor(x => x.City)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            this.RuleFor(x => x.Province)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            this.RuleFor(x => x.Country)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2);

            this.RuleFor(x => x.ZipCode)
                .NotNull()
                .NotEmpty()
                .MinimumLength(4);

            this.RuleFor(x => x.Photo)
                .ValidBase64Image(true);
        }
    }
}