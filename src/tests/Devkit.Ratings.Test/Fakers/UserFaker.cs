// -----------------------------------------------------------------------
// <copyright file="UserFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Test.Fakers
{
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using Devkit.Ratings.Data.Models;
    using Devkit.Test;

    /// <summary>
    /// UserFaker class is the instance generator of User model.
    /// </summary>
    public class UserFaker : FakerBase<User>
    {
        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        public override User Generate()
        {
            var user = new Faker().Person;
            var ratingFaker = new RatingFaker();

            const int ratingsToGen = 5;
            var ratingsGiven = ratingFaker.Generate(ratingsToGen);
            var ratingsReceived = ratingFaker.Generate(ratingsToGen);

            for (var i = 0; i < ratingsToGen; i++)
            {
                ratingsReceived[i].ReceiverUserName = user.UserName;
                ratingsGiven[i].AuthorUserName = user.UserName;
            }

            this.Faker
                .RuleFor(x => x.UserName, user.UserName)
                .RuleFor(x => x.LastReceivedRatings, new Queue<Rating>(ratingsReceived))
                .RuleFor(x => x.LastGivenRatings, new Queue<Rating>(ratingsGiven))
                .RuleFor(x => x.AverageRating, ratingsReceived.Average(x => x.Value))
                .RuleFor(x => x.RatingsReceived, f => f.Random.Int(ratingsToGen, 20))
                .RuleFor(x => x.Id, f => f.Random.Hexadecimal(24, string.Empty));

            return this.Faker.Generate();
        }
    }
}