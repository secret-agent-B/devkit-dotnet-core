// -----------------------------------------------------------------------
// <copyright file="OrderDistanceValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Validators
{
    using FluentValidation;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// Update Order distance validator.
    /// </summary>
    /// <seealso cref="AbstractValidator{DistanceVM}" />
    public class OrderDistanceValidator : AbstractValidator<DistanceVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDistanceValidator"/> class.
        /// </summary>
        public OrderDistanceValidator()
        {
            this.RuleFor(x => x.Text)
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Value)
                .GreaterThan(0);
        }
    }
}