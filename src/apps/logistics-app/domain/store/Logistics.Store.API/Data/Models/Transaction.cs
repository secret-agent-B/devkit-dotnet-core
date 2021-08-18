// -----------------------------------------------------------------------
// <copyright file="Transaction.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Data.Models
{
    using System.Collections.Generic;
    using Devkit.Data;
    using Logistics.Store.API.Constant;

    /// <summary>
    /// The transaction class.
    /// </summary>
    public class Transaction : DocumentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transaction"/> class.
        /// </summary>
        public Transaction()
        {
            this.Coupons = new List<string>();
            this.LineItems = new List<LineItem>();
        }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string AccountId { get; set; }

        /// <summary>
        /// Gets the coupons.
        /// </summary>
        /// <value>
        /// The coupons.
        /// </value>
        public List<string> Coupons { get; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<LineItem> LineItems { get; }

        /// <summary>
        /// Gets or sets the type of the payment.
        /// </summary>
        /// <value>
        /// The type of the payment.
        /// </value>
        public PaymentTypes PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public double TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the transaction status.
        /// </summary>
        /// <value>
        /// The transaction status.
        /// </value>
        public TransactionStatus TransactionStatus { get; set; }
    }
}