// -----------------------------------------------------------------------
// <copyright file="CompleteWorkValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.PickUpWork
{
    using Devkit.Patterns.Extensions;
    using FluentValidation;

    /// <summary>
    /// The CancelWorkValidator is the validator for CancelWorkCommand.
    /// </summary>
    /// <seealso cref="AbstractValidator{CancelWorkCommand}" />
    public class PickUpWorkValidator : AbstractValidator<PickUpWorkCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PickUpWorkValidator"/> class.
        /// </summary>
        public PickUpWorkValidator()
        {
            this.RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Id)
                .Length(24);

            // TODO: Needs UI implementation
            this.RuleFor(x => x.Photo)
                .NotNull()
                .NotEmpty()
                .ValidBase64Image();
        }
    }
}