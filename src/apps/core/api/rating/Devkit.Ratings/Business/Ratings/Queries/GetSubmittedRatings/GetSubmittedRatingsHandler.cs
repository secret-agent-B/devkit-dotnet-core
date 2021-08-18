// -----------------------------------------------------------------------
// <copyright file="GetSubmittedRatingsHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Queries.GetSubmittedRatings
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
    /// Handler that returns the ratings that were submitted by a user and within the specified date range.
    /// </summary>
    /// <seealso cref="QueryHandlerBase{GetSubmittedRatingsQuery, ResponseSet{RatingVM}}" />
    public class GetSubmittedRatingsHandler : QueryHandlerBase<GetSubmittedRatingsQuery, ResponseSet<RatingVM>>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSubmittedRatingsHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public GetSubmittedRatingsHandler(IRepository repository)
            : base(repository)
        {
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var ratings = await this.Repository
                .GetCollection<Rating>()
                .Find(
                    new ExpressionFilterDefinition<Rating>(
                        rating
                            => rating.AuthorUserName == this.Request.AuthorUserName
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