// -----------------------------------------------------------------------
// <copyright file="GetTransactionRatingQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Queries.GetTransactionRating
{
    using Devkit.Patterns.CQRS.Query;
    using Devkit.Ratings.Business.ViewModels;

    /// <summary>
    /// The query to get the transaction rating.
    /// </summary>
    public class GetTransactionRatingQuery : QueryRequestBase<RatingVM>
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        /// The transaction identifier.
        /// </value>
        public string TransactionId { get; set; }
    }
}