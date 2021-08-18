// -----------------------------------------------------------------------
// <copyright file="ServiceRating.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Data.Models
{
    using Devkit.Data;

    /// <summary>
    /// The service rating entry from a customer to a service or product provider.
    /// </summary>
    public class Rating : DocumentBase
    {
        /// <summary>
        /// Gets or sets the name of the author user.
        /// </summary>
        /// <value>
        /// The name of the author user.
        /// </value>
        public string AuthorUserName { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name of the receiver user.
        /// </summary>
        /// <value>
        /// The name of the receiver user.
        /// </value>
        public string ReceiverUserName { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }
    }
}