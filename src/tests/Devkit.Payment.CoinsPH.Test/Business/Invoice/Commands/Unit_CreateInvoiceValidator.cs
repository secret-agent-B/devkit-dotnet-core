// -----------------------------------------------------------------------
// <copyright file="Unit_CreateInvoiceValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Payment.CoinsPH.Test.Business.Invoice.Commands
{
    using Devkit.Test;
    using FluentValidation.TestHelper;
    using Devkit.Payment.CoinsPH.Business.Invoice.Commands.CreateInvoice;
    using NUnit.Framework;
    using System.Transactions;

    /// <summary>
    /// The Unit_CreateInvoiceValidator is CreateInvoiceValidator's unit test class.
    /// </summary>
    public class Unit_CreateInvoiceValidator : UnitTestBase<CreateInvoiceValidator>
    {
        /// <summary>
        /// Should fail if amount is invalid.
        /// </summary>
        /// <param name="amount">The amount.</param>
        [Theory()]
        [TestCase(0.00)]
        [TestCase(-1.00)]
        public void Should_fail_if_amount_is_invalid(double amount)
        {
            var validator = this.Build();
            var model = new CreateInvoiceCommand
            {
                Amount = amount
            };
            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Amount);
        }

        /// <summary>
        /// Should fail if amount is invalid.
        /// </summary>
        /// <param name="currency">The currency.</param>
        [Theory()]
        [TestCase("")]
        [TestCase(null)]
        public void Should_fail_if_currency_is_invalid(string currency)
        {
            var validator = this.Build();
            var model = new CreateInvoiceCommand
            {
                Currency = currency
            };
            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.Currency);
        }

        /// <summary>
        /// Should fail if transaction identifier is missing.
        /// </summary>
        /// <param name="transactionId">The transaction identifier.</param>
        [Theory()]
        [TestCase("")]
        [TestCase(null)]
        public void Should_fail_if_transaction_id_is_missing(string transactionId)
        {
            var validator = this.Build();
            var model = new CreateInvoiceCommand
            {
                TransactionId = transactionId
            };
            var result = validator.TestValidate(model);

            result.ShouldHaveValidationErrorFor(x => x.TransactionId);
        }

        /// <summary>
        /// Should pass if command is valid.
        /// </summary>
        [TestCase(TestName = "Should pass if command is valid")]
        public void Should_pass_if_command_is_valid()
        {
            var command = new CreateInvoiceCommandFaker().Generate();
            var validator = this.Build();
            var result = validator.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(x => x.Amount);
            result.ShouldNotHaveValidationErrorFor(x => x.Currency);
            result.ShouldNotHaveValidationErrorFor(x => x.TransactionId);
        }
    }
}