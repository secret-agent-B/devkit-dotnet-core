// -----------------------------------------------------------------------
// <copyright file="CancelOrderValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Commands.CancelOrder
{
    using FluentValidation;

    /// <summary>
    /// The CancelOrderValidator is the validator for CancelOrderCommand.
    /// </summary>
    /// <seealso cref="AbstractValidator{CancelOrderCommand}" />
    public class CancelOrderValidator : AbstractValidator<CancelOrderCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CancelOrderValidator"/> class.
        /// </summary>
        public CancelOrderValidator()
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