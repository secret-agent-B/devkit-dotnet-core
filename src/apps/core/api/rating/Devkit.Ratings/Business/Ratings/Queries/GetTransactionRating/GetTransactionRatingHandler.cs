// -----------------------------------------------------------------------
// <copyright file="GetTransactionRatingHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Queries.GetTransactionRating
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Query;
    using Devkit.Patterns.Exceptions;
    using Devkit.Ratings.Business.ViewModels;
    using Devkit.Ratings.Data.Models;

    /// <summary>
    /// Handler that returns the rating for a transaction.
    /// </summary>
    /// <seealso cref="RatingVM" />
    public class GetTransactionRatingHandler : QueryHandlerBase<GetTransactionRatingQuery, RatingVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetTransactionRatingHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public GetTransactionRatingHandler(IRepository repository)
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
        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var ratings = this.Repository.GetOneOrDefault<Rating>(x => x.TransactionId == this.Request.TransactionId);

            if (ratings == null)
            {
                throw new NotFoundException(nameof(this.Request.TransactionId), this.Request.TransactionId);
            }

            return Task.FromResult(
                new RatingVM
                {
                    Value = ratings.Value,
                    Id = ratings.Id,
                    AuthorUserName = ratings.AuthorUserName,
                    ReceiverUserName = ratings.ReceiverUserName,
                    CreatedOn = ratings.CreatedOn,
                    Summary = ratings.Summary,
                    TransactionId = ratings.TransactionId,
                    ProductId = ratings.ProductId
                });
        }
    }
}