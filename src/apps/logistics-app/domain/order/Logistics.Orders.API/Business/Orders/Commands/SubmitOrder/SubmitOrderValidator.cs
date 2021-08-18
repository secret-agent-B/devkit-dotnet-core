// -----------------------------------------------------------------------
// <copyright file="SubmitOrderValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Commands.SubmitOrder
{
    using Devkit.Patterns.Extensions;
    using FluentValidation;
    using Logistics.Orders.API.Business.Validators;
    using Logistics.Orders.API.Options;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Create Order validator.
    /// </summary>
    public class SubmitOrderValidator : AbstractValidator<SubmitOrderCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitOrderValidator" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public SubmitOrderValidator(IOptions<DeliveryOptions> options)
        {
            this.RuleFor(x => x.ClientUserName)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.RecipientName)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5);

            this.RuleFor(x => x.RecipientPhone)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.EstimatedItemWeight)
                .GreaterThan(0.0);

            this.RuleFor(x => x.ItemName)
                .MinimumLength(3)
                .MaximumLength(50)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.ItemPhoto)
                .NotNull()
                .NotEmpty()
                .ValidBase64Image();

            this.RuleFor(x => x.OriginPhoto)
                .NotNull()
                .NotEmpty()
                .ValidBase64Image();

            this.RuleFor(x => x.Cost)
                .SetValidator(new DeliveryCostValidator(options));

            this.RuleFor(x => x.Destination)
                .SetValidator(new OrderLocationValidator());

            this.RuleFor(x => x.Origin)
                .SetValidator(new OrderLocationValidator());

            this.RuleFor(x => x.EstimatedDistance)
                .SetValidator(new OrderDistanceValidator());
        }
    }
}