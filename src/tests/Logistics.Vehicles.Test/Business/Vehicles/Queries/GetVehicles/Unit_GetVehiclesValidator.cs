// -----------------------------------------------------------------------
// <copyright file="Unit_GetVehiclesValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Vehicles.Test.Business.Vehicles.Queries.GetVehicles
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Vehicles.API.Business.Vehicles.Queries.GetVehicles;
    using Xunit;

    /// <summary>
    /// The Unit_GetVehiclesValidator is the unit test for GetVehiclesValidator.
    /// </summary>
    public class Unit_GetVehiclesValidator : UnitTestBase<GetVehiclesValidator>
    {
        /// <summary>
        /// Fails if owner username is empty.
        /// </summary>
        [Fact(DisplayName = "Fails if owner username is empty")]
        public void Fail_if_owner_username_is_empty()
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.OwnerUserName, string.Empty);
        }

        /// <summary>
        /// Passes if owner username is not empty.
        /// </summary>
        [Fact(DisplayName = "Passes if owner username is not empty")]
        public void Pass_if_owner_username_is_not_empty()
        {
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.OwnerUserName, this.Faker.Person.UserName);
        }
    }
}