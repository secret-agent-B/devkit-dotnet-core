// -----------------------------------------------------------------------
// <copyright file="CalculateCostValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Queries.CalculateCost
{
    using FluentValidation;

    /// <summary>
    /// The CalculateCostValidator is the validator for CalculateCostQuery.
    /// </summary>
    public class CalculateCostValidator : AbstractValidator<CalculateCostQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateCostValidator" /> class.
        /// </summary>
        public CalculateCostValidator()
        {
            this.RuleFor(x => x.DistanceInKm)
                .GreaterThan(0);
        }
    }
}