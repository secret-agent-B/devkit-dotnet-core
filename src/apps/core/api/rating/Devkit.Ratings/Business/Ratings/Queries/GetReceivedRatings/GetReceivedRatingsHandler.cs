// -----------------------------------------------------------------------
// <copyright file="GetReceivedRatingsHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Queries.GetReceivedRatings
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS;
    using Devkit.Patterns.CQRS.Query;
    using Devkit.Ratings.Business.ViewModels;
    using Devkit.Ratings.Data.Models;
    using MongoDB.Driver;

    /// <summary>
    /// Get the user's received ratings.
    /// </summary>
    /// <seealso cref="QueryHandlerBase{GetReceivedRatingsQuery, ResponseSet{RatingVM}}" />
    public class GetReceivedRatingsHandler : QueryHandlerBase<GetReceivedRatingsQuery, ResponseSet<RatingVM>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetReceivedRatingsHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public GetReceivedRatingsHandler(IRepository repository)
            : base(repository)
        {
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var ratings = await this.Repository
                .GetCollection<Rating>()
                .Find(
                    new ExpressionFilterDefinition<Rating>(
                        rating
                            => rating.ReceiverUserName == this.Request.ReceiverUserName
                            && rating.CreatedOn >= this.Request.StartDate
                            && rating.CreatedOn <= this.Request.EndDate))
                .Limit(10)
                .ToListAsync(cancellationToken);

            this.Response.Items.AddRange(
                ratings.Select(x =>
                    new RatingVM
                    {
                        Value = x.Value,
                        Id = x.Id,
                        AuthorUserName = x.AuthorUserName,
                        ReceiverUserName = x.ReceiverUserName,
                        CreatedOn = x.CreatedOn,
                        Summary = x.Summary,
                        TransactionId = x.TransactionId,
                        ProductId = x.ProductId
                    }).ToList());
        }
    }
}