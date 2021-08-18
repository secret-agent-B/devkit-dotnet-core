// -----------------------------------------------------------------------
// <copyright file="Unit_GetMyOrdersValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Orders.Queries.GetMyOrders
{
    using Devkit.Patterns;
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Orders.Queries.GetMyOrders;
    using Logistics.Orders.API.Constants;
    using Xunit;

    /// <summary>
    /// The Unit_GetMyOrdersValidator is the unit test for GetMyOrdersValidator.
    /// </summary>
    /// <seealso cref="UnitTestBase{GetMyOrdersValidator}" />
    public class Unit_GetMyOrdersValidator : UnitTestBase<GetMyOrdersValidator>
    {
        /// <summary>
        /// Fails if client user name is empty.
        /// </summary>
        [Fact(DisplayName = "Fails if client user name is empty")]
        public void Fail_if_client_user_name_is_empty()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.ClientUserName, string.Empty);
        }

        /// <summary>
        /// Passes if client user name is given.
        /// </summary>
        /// <param name="clientUserName">Name of the client user.</param>
        [Theory(DisplayName = "Passes if client user name is given")]
        [InlineData("driver.name")]
        [InlineData("myusername")]
        [InlineData("my-test-username")]
        public void Pass_if_client_user_name_is_given(string clientUserName)
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.ClientUserName, clientUserName);
        }

        /// <summary>
        /// Passes if status is a valid order status.
        /// </summary>
        [Fact(DisplayName = "Passes if status is a valid order status")]
        public void Pass_if_status_is_a_valid_order_status()
        {
            var validator = this.Build();
            var orderStatuses = EnumerationBase.GetAll<StatusCode>();

            foreach (var orderStatus in orderStatuses)
            {
                if (orderStatus == StatusCode.Unknown)
                {
                    continue;
                }

                validator.ShouldNotHaveValidationErrorFor(x => x.Status, orderStatus);
            }
        }
    }
}