// -----------------------------------------------------------------------
// <copyright file="User.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Data.Models
{
    using Devkit.Data;
    using System.Collections.Generic;

    /// <summary>
    /// The rating summary of a user.
    /// </summary>
    public class User : DocumentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        public User()
        {
            this.LastGivenRatings = new Queue<Rating>(5);
            this.LastReceivedRatings = new Queue<Rating>(5);
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
        public Queue<Rating> LastGivenRatings { get; set; }

        /// <summary>
        /// Gets or sets the most recent ratings.
        /// </summary>
        /// <value>
        /// The most recent ratings.
        /// </value>
        public Queue<Rating> LastReceivedRatings { get; set; }

        /// <summary>
        /// Gets or sets the total number of ratings received from other users.
        /// </summary>
        /// <value>
        /// The total ratings.
        /// </value>
        public int RatingsReceived { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}