// -----------------------------------------------------------------------
// <copyright file="DeliveryCostValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Validators
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using FluentValidation;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.API.Options;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// The delivery cost validator.
    /// </summary>
    public class DeliveryCostValidator : AbstractValidator<DeliveryCostVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryCostValidator" /> class.
        /// </summary>
        /// <param name="options">The options.</param>
        public DeliveryCostValidator([NotNull] IOptions<DeliveryOptions> options)
        {
            if (options.Value.Tax > 0)
            {
                this.RuleFor(x => x.Tax)
                    .GreaterThan(0);
            }

            this.RuleFor(x => x.DistanceInKm)
                .GreaterThan(0);

            this.RuleFor(x => x.DriverFee)
                .GreaterThan(0);

            this.RuleFor(x => x.SystemFee)
                .GreaterThan(0);

            this.RuleFor(x => x.Total)
                .GreaterThan(0)
                .Must((vm, value) =>
                {
                    var total = vm.DriverFee + vm.SystemFee + vm.Tax;
                    var roundedTotal = Math.Round(total, 2);
                    var result = Math.Abs(roundedTotal - vm.Total) == 0;

                    return result;
                })
                .WithMessage("Total and charges do not agree.");
        }
    }
}