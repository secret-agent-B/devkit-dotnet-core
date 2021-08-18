// -----------------------------------------------------------------------
// <copyright file="CreateTransactionValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Transactions.Commands.CreateTransaction
{
    using System;
    using FluentValidation;
    using Logistics.Store.API.Constant;

    /// <summary>
    /// The CreateTransactionValidator validates the CreateTransactionCommand.
    /// </summary>
    public class CreateTransactionValidator : AbstractValidator<CreateTransactionCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTransactionValidator"/> class.
        /// </summary>
        public CreateTransactionValidator()
        {
            this.RuleFor(x => x.UserName)
                .EmailAddress()
                .NotNull()
                .NotEmpty();

            this.RuleFor(x => x.Currency)
                .NotNull()
                .NotEmpty()
                .MaximumLength(3);

            this.RuleFor(x => x.PaymentType)
                .Must((paymentType) => Enum.TryParse<PaymentTypes>(paymentType, out _))
                .WithMessage("Invalid payment type.");
        }
    }
}