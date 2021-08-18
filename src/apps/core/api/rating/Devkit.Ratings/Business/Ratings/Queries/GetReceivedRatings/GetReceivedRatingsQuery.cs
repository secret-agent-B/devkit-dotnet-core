// -----------------------------------------------------------------------
// <copyright file="GetProviderRatingsQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Queries.GetReceivedRatings
{
    using System;
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Devkit.Ratings.Business.ViewModels;

    /// <summary>
    /// The query to get ratings received by a user.
    /// </summary>
    public class GetReceivedRatingsQuery : QueryRequestBase<ResponseSet<RatingVM>>
    {
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the receiver user.
        /// </summary>
        /// <value>
        /// The name of the receiver user.
        /// </value>
        public string ReceiverUserName { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }
    }
}