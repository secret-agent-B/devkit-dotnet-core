// -----------------------------------------------------------------------
// <copyright file="Unit_CalculateCostValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Queries.CalculateCost
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Orders.Queries.CalculateCost;
    using Xunit;

    /// <summary>
    /// The Unit_CalculateCostValidator is the unit test for CalculateCostValidator.
    /// </summary>
    public class Unit_CalculateCostValidator : UnitTestBase<CalculateCostValidator>
    {
        /// <summary>
        /// Fails if distance is invalid.
        /// </summary>
        /// <param name="distanceInKm">The distance in km.</param>
        [Theory(DisplayName = "Fails if distance is invalid")]
        [InlineData(-1.0)]
        [InlineData(0)]
        public void Fail_if_distance_is_invalid(double distanceInKm)
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.DistanceInKm, distanceInKm);
        }

        /// <summary>
        /// Passes if distance is valid.
        /// </summary>
        /// <param name="distanceInKm">The distance in km.</param>
        [Theory(DisplayName = "Passes if distance is valid")]
        [InlineData(5.0)]
        [InlineData(4.0)]
        [InlineData(3.0)]
        [InlineData(2.0)]
        [InlineData(1.0)]
        [InlineData(.1)]
        public void Pass_if_distance_is_valid(double distanceInKm)
        {
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.DistanceInKm, distanceInKm);
        }
    }
}