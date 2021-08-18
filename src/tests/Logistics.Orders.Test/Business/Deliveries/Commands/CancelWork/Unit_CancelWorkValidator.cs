// -----------------------------------------------------------------------
// <copyright file="Unit_AssignWorkValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Commands.CancelWork
{
    using System;
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Orders.API.Business.Deliveries.Commands.CancelWork;
    using Xunit;

    /// <summary>
    /// The Unit_AssignWorkValidator is the unit test for AssignWorkValidator.
    /// </summary>
    public class Unit_CancelWorkValidator : UnitTestBase<CancelWorkValidator>
    {
        /// <summary>
        /// Fails if comment is invalid.
        /// </summary>
        [Fact(DisplayName = "Fails if comment is invalid")]
        public void Fail_if_comment_is_invalid()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Comment, "");
            validator.ShouldHaveValidationErrorFor(x => x.Comment, this.Faker.Random.AlphaNumeric(24));
        }

        /// <summary>
        /// Fails if driver username is invalid.
        /// </summary>
        [Fact(DisplayName = "Fails if driver username is invalid")]
        public void Fail_if_driver_username_is_invalid()
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.UserName, string.Empty);
        }

        /// <summary>
        /// Fails if identifier is invalid.
        /// </summary>
        [Fact(DisplayName = "Fails if identifier is invalid")]
        public void Fail_if_id_is_invalid()
        {
            var validator = this.Build();
            validator.ShouldHaveValidationErrorFor(x => x.Id, string.Empty);
            validator.ShouldHaveValidationErrorFor(x => x.Id, this.Faker.Random.Hexadecimal(23, string.Empty));
        }

        /// <summary>
        /// Passes if command is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if command is valid")]
        public void Pass_if_command_is_valid()
        {
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.UserName, this.Faker.Person.UserName);
            validator.ShouldNotHaveValidationErrorFor(x => x.Id, this.Faker.Random.Hexadecimal(24, string.Empty));
            validator.ShouldNotHaveValidationErrorFor(x => x.Comment, this.Faker.Random.AlphaNumeric(25));
        }

        /// <summary>
        /// Passes if comment is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if comment is valid")]
        public void Pass_if_comment_is_valid()
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.Comment, new String('x', 25));
        }

        /// <summary>
        /// Passes if identifier is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if identifier is valid")]
        public void Pass_if_id_is_valid()
        {
            var validator = this.Build();
            validator.ShouldNotHaveValidationErrorFor(x => x.Comment, this.Faker.Random.AlphaNumeric(25));
        }
    }
}