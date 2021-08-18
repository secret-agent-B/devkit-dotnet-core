// -----------------------------------------------------------------------
// <copyright file="Unit_DeliveryCostValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Validators
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Validators;
    using Logistics.Orders.API.Business.ViewModels;
    using Logistics.Orders.Test.Fakers;
    using Xunit;

    /// <summary>
    /// The unit test for delivery cost validator.
    /// </summary>
    public class Unit_DeliveryCostValidator : UnitTestBase<DeliveryCostValidator>
    {
        /// <summary>
        /// Fails if distance in km is not greater than 0.
        /// </summary>
        [Theory(DisplayName = "Fails if distanceInKm is not greater than 0")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-.1)]
        public void Fail_if_distance_in_km_is_not_greater_than_0(double distanceInKm)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.DistanceInKm, distanceInKm);
        }

        /// <summary>
        /// Fails if driver fee is not greater than 0.
        /// </summary>
        [Theory(DisplayName = "Fails if driver fee is not greater than 0")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-.1)]
        public void Fail_if_driver_fee_is_not_greater_than_0(double driverFee)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.DriverFee, driverFee);
        }

        /// <summary>
        /// Fails if system fee is not greater than 0.
        /// </summary>
        [Theory(DisplayName = "Fails if system fee is not greater than 0")]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-.1)]
        public void Fail_if_system_fee_is_not_greater_than_0(double systemFee)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.SystemFee, systemFee);
        }

        /// <summary>
        /// Fails if tax value is not greater than 0.
        /// </summary>
        [Fact(DisplayName = "Fails if tax value is not greater than 0")]
        public void Fail_if_tax_value_is_not_greater_than_0()
        {
            var options = new DeliveryOptionsFaker().Generate(10.0);
            var validator = new DeliveryCostValidator(options);

            validator.ShouldHaveValidationErrorFor(x => x.Tax, 0.0);
        }

        /// <summary>
        /// Passes if delivery cost is valid without tax rate.
        /// </summary>
        [Fact(DisplayName = "Passes if delivery cost is valid without tax rate")]
        public void Pass_if_delivery_cost_is_valid_with_tax_rate()
        {
            var options = new DeliveryOptionsFaker().Generate(10.0);
            var validator = new DeliveryCostValidator(options);
            var query = new DeliveryCostVM
            {
                DriverFee = 95.2,
                SystemFee = 23.8,
                Tax = 11.9,
                Total = 130.9
            };

            validator.ShouldNotHaveValidationErrorFor(x => x.SystemFee, query);
            validator.ShouldNotHaveValidationErrorFor(x => x.DriverFee, query);
            validator.ShouldNotHaveValidationErrorFor(x => x.Tax, query);
            validator.ShouldNotHaveValidationErrorFor(x => x.Total, query);
        }

        /// <summary>
        /// Passes if delivery cost is valid without tax rate.
        /// </summary>
        [Fact(DisplayName = "Passes if delivery cost is valid without tax rate")]
        public void Pass_if_delivery_cost_is_valid_without_tax_rate()
        {
            var validator = this.Build();
            var query = new DeliveryCostVM
            {
                DriverFee = 95.2,
                SystemFee = 23.8,
                Tax = 0,
                Total = 119
            };

            validator.ShouldNotHaveValidationErrorFor(x => x.SystemFee, query);
            validator.ShouldNotHaveValidationErrorFor(x => x.DriverFee, query);
            validator.ShouldNotHaveValidationErrorFor(x => x.Tax, query);
            validator.ShouldNotHaveValidationErrorFor(x => x.Total, query);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>A delivery cost validator.</returns>
        protected override DeliveryCostValidator Build()
        {
            return new DeliveryCostValidator(new DeliveryOptionsFaker().Generate());
        }
    }
}