// -----------------------------------------------------------------------
// <copyright file="OrderLocationValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Validators
{
    using FluentValidation;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// Update Order coordinates validator.
    /// </summary>
    /// <seealso cref="AbstractValidator{LocationVM}" />
    public class OrderLocationValidator : AbstractValidator<LocationVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderLocationValidator"/> class.
        /// </summary>
        public OrderLocationValidator()
        {
            this.RuleFor(x => x.DisplayAddress)
                .NotNull();

            this.RuleFor(x => x.Lat)
                .NotNull()
                .LessThanOrEqualTo(90.0)
                .GreaterThanOrEqualTo(-90.0);

            this.RuleFor(x => x.Lng)
                .NotNull()
                .LessThanOrEqualTo(180.0)
                .GreaterThanOrEqualTo(-180.0);
        }
    }
}