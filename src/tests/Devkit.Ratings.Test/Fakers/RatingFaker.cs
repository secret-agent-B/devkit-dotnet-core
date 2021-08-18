// -----------------------------------------------------------------------
// <copyright file="RatingFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Test.Fakers
{
    using System;
    using Bogus;
    using Devkit.Ratings.Data.Models;
    using Devkit.Test;

    /// <summary>
    /// RatingFaker class is the instance faker for Rating model.
    /// </summary>
    public class RatingFaker : FakerBase<Rating>
    {
        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        public override Rating Generate()
        {
            var author = new Faker().Person;
            var receiver = new Faker().Person;

            this.Faker
                .RuleFor(x => x.Value, f => f.Random.Int(1, 5))
                .RuleFor(x => x.AuthorUserName, f => author.UserName)
                .RuleFor(x => x.ProductId, f => f.Random.Hexadecimal(24, string.Empty))
                .RuleFor(x => x.ReceiverUserName, receiver.UserName)
                .RuleFor(x => x.Summary, f => f.Rant.Review())
                .RuleFor(x => x.TransactionId, f => f.Random.Hexadecimal(24, string.Empty))
                .RuleFor(x => x.CreatedOn, f => f.Date.Recent(f.Random.Int(1, 5)))
                .RuleFor(x => x.LastUpdatedOn, (f, r) =>
                  {
                      if (f.Random.Bool())
                      {
                          return f.Date.Between(r.CreatedOn, DateTime.UtcNow);
                      }

                      return null;
                  });

            return this.Faker.Generate();
        }
    }
}