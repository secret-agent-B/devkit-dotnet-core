// -----------------------------------------------------------------------
// <copyright file="Unit_CreateTransactionValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Business.Transactions.Commands.CreateTransaction
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Logistics.Store.API.Business.Transactions.Commands.CreateTransaction;
    using Xunit;

    /// <summary>
    /// The Unit_CreateTransactionValidator tests the CreateTransactionValidator.
    /// </summary>
    public class Unit_CreateTransactionValidator : UnitTestBase<CreateTransactionValidator>
    {
        /// <summary>
        /// Fails if account identifier is null or empty.
        /// </summary>
        [Fact(DisplayName = "Fails if account identifier is null or empty")]
        public void Fail_if_account_id_is_null_or_empty()
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.UserName, string.Empty);
            validator.ShouldHaveValidationErrorFor(x => x.UserName, new string(' ', this.Faker.Random.Int(1, 10)));
        }

        /// <summary>
        /// Fails if currency is invalid.
        /// </summary>
        [Fact(DisplayName = "Fails if currency is invalid")]
        public void Fail_if_currency_is_invalid()
        {
            var validator = this.Build();

            validator.ShouldHaveValidationErrorFor(x => x.Currency, string.Empty);
            validator.ShouldHaveValidationErrorFor(x => x.Currency, this.Faker.Random.String(4));
        }

        /// <summary>
        /// Passes if account identifier is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if account identifier is valid")]
        public void Pass_if_account_id_is_valid()
        {
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.UserName, this.Faker.Person.Email);
        }

        /// <summary>
        /// Passes if currency is valid.
        /// </summary>
        [Fact(DisplayName = "Passes if currency is valid")]
        public void Pass_if_currency_is_valid()
        {
            var validator = this.Build();

            validator.ShouldNotHaveValidationErrorFor(x => x.Currency, "php");
            validator.ShouldNotHaveValidationErrorFor(x => x.Currency, "usd");
        }
    }
}