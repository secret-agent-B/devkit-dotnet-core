// -----------------------------------------------------------------------
// <copyright file="GetSubmittedRatingsQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Queries.GetSubmittedRatings
{
    using System;
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Devkit.Ratings.Business.ViewModels;

    /// <summary>
    /// The query to get user's submitted ratings.
    /// </summary>
    public class GetSubmittedRatingsQuery : QueryRequestBase<ResponseSet<RatingVM>>
    {
        /// <summary>
        /// Gets or sets the name of the author user.
        /// </summary>
        /// <value>
        /// The name of the author user.
        /// </value>
        public string AuthorUserName { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }
    }
}