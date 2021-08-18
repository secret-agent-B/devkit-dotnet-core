// -----------------------------------------------------------------------
// <copyright file="TransactionVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.ViewModels
{
    using System.Diagnostics.CodeAnalysis;
    using Devkit.Patterns.CQRS;
    using Logistics.Store.API.Data.Models;

    /// <summary>
    /// The TransactionVM is a view model that is returned to the caller after executing a module that adds or updates a Transaction object.
    /// </summary>
    public class TransactionVM : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionVM"/> class.
        /// </summary>
        public TransactionVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionVM" /> class.
        /// </summary>
        /// <param name="transaction">The transaction.</param>
        /// <param name="account">The account.</param>
        public TransactionVM([NotNull] Transaction transaction, Account account)
        {
            this.Id = transaction.Id;
            this.Account = new AccountVM(account);
            this.Status = transaction.TransactionStatus.ToString().ToLower();
            this.TotalAmount = transaction.TotalAmount;
        }

        /// <summary>
        /// Gets or sets the account.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public AccountVM Account { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public double TotalAmount { get; set; }
    }
}