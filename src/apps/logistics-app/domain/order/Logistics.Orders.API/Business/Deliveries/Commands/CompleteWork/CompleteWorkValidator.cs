// -----------------------------------------------------------------------
// <copyright file="CompleteWorkValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.CompleteWork
{
    using Devkit.Patterns.Extensions;
    using FluentValidation;

    /// <summary>
    /// The CompleteWorkValidator is the validator for CompleteWorkCommand.
    /// </summary>
    /// <seealso cref="AbstractValidator{CancelWorkCommand}" />
    public class CompleteWorkValidator : AbstractValidator<CompleteWorkCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompleteWorkValidator"/> class.
        /// </summary>
        public CompleteWorkValidator()
        {
            this.RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Id)
                .Length(24);

            this.RuleFor(x => x.Photo)
                .NotNull()
                .NotEmpty()
                .ValidBase64Image();
        }
    }
}