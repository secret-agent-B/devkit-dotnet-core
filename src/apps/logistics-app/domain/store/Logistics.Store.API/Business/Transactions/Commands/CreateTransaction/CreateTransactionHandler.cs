// -----------------------------------------------------------------------
// <copyright file="CreateTransactionHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Transactions.Commands.CreateTransaction
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Patterns.Exceptions;
    using Logistics.Store.API.Business.ViewModels;
    using Logistics.Store.API.Constant;
    using Logistics.Store.API.Data.Models;

    /// <summary>
    /// The CreateTransactionHandler handles a CreateTransactionCommand request.
    /// </summary>
    public class CreateTransactionHandler : CommandHandlerBase<CreateTransactionCommand, TransactionVM>
    {
        /// <summary>
        /// The transaction.
        /// </summary>
        private Transaction _transaction;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTransactionHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public CreateTransactionHandler(IRepository repository)
            : base(repository)
        {
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override Task ExecuteAsync(CancellationToken cancellationToken) =>
            Task.Run(() =>
            {
                var account = this.Repository.GetOneOrDefault<Account>(x => x.UserName == this.Request.UserName);

                if (account == null)
                {
                    throw new NotFoundException(nameof(Account), this.Request.UserName);
                }

                this._transaction = new Transaction
                {
                    AccountId = account.Id,
                    PaymentType = Enum.Parse<PaymentTypes>(this.Request.PaymentType),
                    TransactionStatus = TransactionStatus.Pending
                };

                this._transaction.Coupons.AddRange(this.Request.Coupons);

                foreach ((string key, int value) in this.Request.OrderedProducts)
                {
                    var product = this.Repository.GetOneOrDefault<Product>(x => x.Id == key);
                    var unitTotalAmount = product.PricePerUnit * value;

                    this._transaction.LineItems.Add(new LineItem
                    {
                        PricePerUnit = product.PricePerUnit,
                        ProductId = product.Id,
                        Quantity = value,
                        UnitTotalAmount = unitTotalAmount
                    });

                    this._transaction.TotalAmount += unitTotalAmount;
                }

                this.Repository.AddWithAudit(this._transaction);
                this.Response = new TransactionVM(this._transaction, account);
            }, cancellationToken);
    }
}