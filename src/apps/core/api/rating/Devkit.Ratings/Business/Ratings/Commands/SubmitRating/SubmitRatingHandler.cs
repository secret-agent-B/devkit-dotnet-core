// -----------------------------------------------------------------------
// <copyright file="SubmitRatingHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Ratings.Commands.SubmitRating
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Ratings.Business.Users.Commands.CreateUser;
    using Devkit.Ratings.Business.ViewModels;
    using Devkit.Ratings.Data.Models;
    using MediatR;

    /// <summary>
    /// Records the service ratings given by the customer.
    /// </summary>
    public class SubmitRatingHandler : CommandHandlerBase<SubmitRatingCommand, RatingVM>
    {
        /// <summary>
        /// The mediator.
        /// </summary>
        private readonly IMediator _mediator;

        /// <summary>
        /// The rating.
        /// </summary>
        private Rating _rating;

        /// <summary>
        /// Initializes a new instance of the <see cref="SubmitRatingHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="mediator">The mediator.</param>
        public SubmitRatingHandler(IRepository repository, IMediator mediator)
            : base(repository)
        {
            this._mediator = mediator;
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
            this._rating = new Rating
            {
                AuthorUserName = this.Request.AuthorUserName,
                CreatedOn = DateTime.UtcNow,
                ProductId = this.Request.ProductId,
                ReceiverUserName = this.Request.ReceiverUserName,
                Summary = this.Request.Summary,
                TransactionId = this.Request.TransactionId,
                Value = this.Request.Value
            };

            this.Repository.AddWithAudit(this._rating);

            this.Response.AuthorUserName = this._rating.AuthorUserName;
            this.Response.CreatedOn = this._rating.CreatedOn;
            this.Response.ReceiverUserName = this._rating.ReceiverUserName;
            this.Response.Summary = this._rating.Summary;
            this.Response.TransactionId = this._rating.TransactionId;
            this.Response.Value = this._rating.Value;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Posts the processing.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the asynchronous operation.
        /// </returns>
        protected override async Task PostProcessing(CancellationToken cancellationToken)
        {
            await this.UpdateAuthor();
            await this.UpdateReceiver();
        }

        /// <summary>
        /// Initializes the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>A new user.</returns>
        private async Task<User> InitializeUser(string userName)
        {
            var response = await this._mediator.Send(new CreateUserCommand { UserName = userName });
            return new User { UserName = response.UserName };
        }

        /// <summary>
        /// Updates the author.
        /// </summary>
        private async Task UpdateAuthor()
        {
            var author = this.Repository.GetOneOrDefault<User>(x => x.UserName == this.Request.AuthorUserName)
                ?? await this.InitializeUser(this.Request.AuthorUserName);

            if (author.LastGivenRatings.Count >= 5)
            {
                author.LastGivenRatings.Dequeue();
            }

            author.LastGivenRatings.Enqueue(this._rating);

            this.Repository.UpdateWithAudit<User>(
                x => x.UserName == author.UserName,
                builder => builder
                    .Set(x => x.LastGivenRatings, author.LastGivenRatings));
        }

        /// <summary>
        /// Updates the receiver.
        /// </summary>
        private async Task UpdateReceiver()
        {
            var receiver = this.Repository.GetOneOrDefault<User>(x => x.UserName == this.Request.ReceiverUserName)
                ?? await this.InitializeUser(this.Request.AuthorUserName);

            if (receiver.LastGivenRatings.Count >= 5)
            {
                receiver.LastGivenRatings.Dequeue();
            }

            receiver.LastReceivedRatings.Enqueue(this._rating);

            this.Repository.UpdateWithAudit<User>(
                x => x.UserName == receiver.UserName,
                builder => builder
                    .Set(x => x.LastReceivedRatings, receiver.LastReceivedRatings));
        }
    }
}