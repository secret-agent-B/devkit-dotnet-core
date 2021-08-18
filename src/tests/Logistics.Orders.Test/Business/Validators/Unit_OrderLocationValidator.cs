// -----------------------------------------------------------------------
// <copyright file="Unit_OrderLocationValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Validators
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Validators;
    using Xunit;

    /// <summary>
    /// The Unit_OrderLocationValidator tests the OrderLocationValidator.
    /// </summary>
    public class Unit_OrderLocationValidator : UnitTestBase<OrderLocationValidator>
    {
        /// <summary>
        /// Fails if LAT is out of range.
        /// </summary>
        /// <param name="lat">The latitude.</param>
        [Theory(DisplayName = "Fails if LAT is out of range")]
        [InlineData(91.0)]
        [InlineData(-91.0)]
        [InlineData(90.1)]
        [InlineData(-90.1)]
        public void Fail_if_lat_is_out_of_range(double lat)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Lat, lat);
        }

        /// <summary>
        /// Fails if LNG is out of range.
        /// </summary>
        /// <param name="lng">The longitude.</param>
        [Theory(DisplayName = "Fails if LNG is out of range")]
        [InlineData(181.0)]
        [InlineData(-181.0)]
        [InlineData(180.1)]
        [InlineData(-180.1)]
        public void Fail_if_lng_is_out_of_range(double lng)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Lng, lng);
        }

        /// <summary>
        /// Passes if lat is out of range.
        /// </summary>
        /// <param name="lat">The latitude.</param>
        [Theory(DisplayName = "Fails if LAT is out of range")]
        [InlineData(90.0)]
        [InlineData(-90.0)]
        public void Pass_if_lat_is_out_of_range(double lat)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.Lat, lat);
        }

        /// <summary>
        /// Passes if LNG is out of range.
        /// </summary>
        /// <param name="lng">The longitude.</param>
        [Theory(DisplayName = "Fails if LNG is out of range")]
        [InlineData(180.0)]
        [InlineData(-180.0)]
        public void Pass_if_lng_is_out_of_range(double lng)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.Lng, lng);
        }
    }
}