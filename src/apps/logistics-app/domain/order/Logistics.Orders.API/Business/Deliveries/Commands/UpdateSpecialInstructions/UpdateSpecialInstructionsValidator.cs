// -----------------------------------------------------------------------
// <copyright file="UpdateWorkValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Commands.UpdateSpecialInstructions
{
    using FluentValidation;

    /// <summary>
    /// Update delivery validator.
    /// </summary>
    /// <seealso cref="AbstractValidator{UpdateDeliveryCommand}" />
    public class UpdateSpecialInstructionsValidator : AbstractValidator<UpdateSpecialInstructionsCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSpecialInstructionsValidator"/> class.
        /// </summary>
        public UpdateSpecialInstructionsValidator()
        {
            this.RuleFor(x => x.OrderId)
                .Length(24);

            this.RuleFor(x => x.UserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.SpecialInstructions)
                .NotNull()
                .NotEmpty();
        }
    }
}