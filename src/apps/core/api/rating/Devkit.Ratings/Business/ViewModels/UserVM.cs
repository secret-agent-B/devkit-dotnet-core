// -----------------------------------------------------------------------
// <copyright file="UserVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.ViewModels
{
    using System.Collections.Generic;
    using Devkit.Patterns.CQRS;

    /// <summary>
    /// The response class that summarizes the users rating activities.
    /// </summary>
    public class UserVM : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserVM" /> class.
        /// </summary>
        public UserVM()
        {
            this.LastGivenRatings = new Queue<RatingVM>();
            this.LastReceivedRatings = new Queue<RatingVM>();
        }

        /// <summary>
        /// Gets or sets the average rating.
        /// </summary>
        /// <value>
        /// The average rating.
        /// </value>
        public double AverageRating { get; set; }

        /// <summary>
        /// Gets or sets the last given ratings.
        /// </summary>
        /// <value>
        /// The last given ratings.
        /// </value>
        public Queue<RatingVM> LastGivenRatings { get; set; }

        /// <summary>
        /// Gets or sets the most recent ratings.
        /// </summary>
        /// <value>
        /// The most recent ratings.
        /// </value>
        public Queue<RatingVM> LastReceivedRatings { get; set; }

        /// <summary>
        /// Gets or sets the total number of ratings received from other users.
        /// </summary>
        /// <value>
        /// The total ratings.
        /// </value>
        public int RatingsReceived { get; set; }

        /// <summary>
        /// Gets or sets the total rating.
        /// </summary>
        /// <value>
        /// The total rating.
        /// </value>
        public int TotalValue { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}