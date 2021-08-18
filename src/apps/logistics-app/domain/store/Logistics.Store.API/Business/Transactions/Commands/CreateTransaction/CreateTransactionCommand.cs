// -----------------------------------------------------------------------
// <copyright file="CreateTransactionCommand.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Transactions.Commands.CreateTransaction
{
    using System.Collections.Generic;
    using Devkit.Patterns.CQRS.Command;
    using Logistics.Store.API.Business.ViewModels;

    /// <summary>
    /// The CreateTransactionCommand is fired when the user commits to pay for a product.
    /// </summary>
    public class CreateTransactionCommand : CommandRequestBase<TransactionVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateTransactionCommand"/> class.
        /// </summary>
        public CreateTransactionCommand()
        {
            this.Coupons = new List<string>();
            this.OrderedProducts = new Dictionary<string, int>();
        }

        /// <summary>
        /// Gets the coupons.
        /// </summary>
        /// <value>
        /// The coupons.
        /// </value>
        public List<string> Coupons { get; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public string Currency { get; set; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public Dictionary<string, int> OrderedProducts { get; }

        /// <summary>
        /// Gets or sets the type of the payment.
        /// </summary>
        /// <value>
        /// The type of the payment.
        /// </value>
        public string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}