// -----------------------------------------------------------------------
// <copyright file="Unit_GetMyDeliveriesValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Queries.GetMyDeliveries
{
    using Devkit.Patterns;
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Deliveries.Queries.GetMyDeliveries;
    using Logistics.Orders.API.Constants;
    using Xunit;

    /// <summary>
    /// The Unit_GetMyDeliveriesValidator is the unit test for GetMyDeliveriesValidator.
    /// </summary>
    /// <seealso cref="UnitTestBase{GetMyDeliveriesValidator}" />
    public class Unit_GetMyDeliveriesValidator : UnitTestBase<GetMyDeliveriesValidator>
    {
        /// <summary>
        /// Fails if client user name is empty.
        /// </summary>
        [Fact(DisplayName = "Fails if client user name is empty")]
        public void Fail_if_client_user_name_is_empty()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.DriverUserName, string.Empty);
        }

        /// <summary>
        /// Passes if client user name is given.
        /// </summary>
        /// <param name="clientUserName">Name of the client user.</param>
        [Theory(DisplayName = "Passes if client user name is given")]
        [InlineData("client.name")]
        [InlineData("myusername")]
        [InlineData("my-test-username")]
        public void Pass_if_client_user_name_is_given(string clientUserName)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.DriverUserName, clientUserName);
        }

        /// <summary>
        /// Passes if status is a valid Deliverie status.
        /// </summary>
        [Fact(DisplayName = "Passes if status is a valid Deliverie status")]
        public void Pass_if_status_is_a_valid_delivery_status()
        {
            var validator = this.Build();
            var orderStatusCodes = EnumerationBase.GetAll<StatusCode>();

            foreach (var statusCode in orderStatusCodes)
            {
                if (statusCode == StatusCode.Unknown)
                {
                    continue;
                }

                validator.ShouldNotHaveValidationErrorFor(x => x.Status, statusCode);
            }
        }
    }
}