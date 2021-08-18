// -----------------------------------------------------------------------
// <copyright file="Unit_GetMyActiveDeliveries.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Queries.GetMyActiveDeliveries
{
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Deliveries.Queries.GetMyActiveDeliveries;
    using Xunit;

    /// <summary>
    /// Unit_GetMyActiveDeliveries class is the unit test for GetMyActiveDeliveriesValidator.
    /// </summary>
    public class Unit_GetMyActiveDeliveriesValidator : OrdersUnitTestBase<GetMyActiveDeliveriesValidator>
    {
        /// <summary>
        /// Fails if username is invalid.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        [Theory(DisplayName = "Fails if username is invalid")]
        [InlineData("")]
        [InlineData("1")]
        [InlineData("@!@#.com")]
        [InlineData("1234567890")]
        public void Fail_if_username_is_invalid(string userName)
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.DriverUserName, userName);
        }

        /// <summary>
        /// Passes if username is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if username is valid")]
        public void Pass_if_username_is_valid()
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.DriverUserName, this.Faker.Person.Email);
            validator.ShouldNotHaveValidationErrorFor(x => x.DriverUserName, "a@x.com");
        }
    }
}