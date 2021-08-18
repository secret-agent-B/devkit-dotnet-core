// -----------------------------------------------------------------------
// <copyright file="CancelWorkValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.CancelWork
{
    using FluentValidation;

    /// <summary>
    /// The CancelWorkValidator is the validator for CancelWorkCommand.
    /// </summary>
    /// <seealso cref="AbstractValidator{CancelWorkCommand}" />
    public class CancelWorkValidator : AbstractValidator<CancelWorkCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelWorkValidator"/> class.
        /// </summary>
        public CancelWorkValidator()
        {
            this.RuleFor(x => x.Comment)
                .NotNull()
                .NotEmpty()
                .MinimumLength(25);

            this.RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Id)
                .Length(24);
        }
    }
}