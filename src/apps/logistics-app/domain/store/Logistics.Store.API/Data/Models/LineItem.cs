// -----------------------------------------------------------------------
// <copyright file="LineItem.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Data.Models
{
    /// <summary>
    /// Represents a product that is bought within a transaction.
    /// </summary>
    public class LineItem
    {
        /// <summary>
        /// Gets or sets the price per unit.
        /// </summary>
        /// <value>
        /// The price per unit.
        /// </value>
        public double PricePerUnit { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public double UnitTotalAmount { get; set; }
    }
}