// -----------------------------------------------------------------------
// <copyright file="Unit_SubmitRatingHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Test.Business.Ratings.Commands.SubmitRating
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Ratings.Business.Ratings.Commands.SubmitRating;
    using Devkit.Ratings.Business.Users.Commands.CreateUser;
    using Devkit.Ratings.Business.ViewModels;
    using Devkit.Ratings.Data.Models;
    using Devkit.Test;
    using MediatR;
    using Moq;
    using Xunit;

    /// <summary>
    /// Unit_SubmitRatingHandler class is the unit test for SubmitRatingHandler.
    /// </summary>
    public class Unit_SubmitRatingHandler : UnitTestBase<(SubmitRatingHandler handler, SubmitRatingCommand command)>
    {
        /// <summary>
        /// Should be able to submit rating to another user.
        /// </summary>
        [Fact(DisplayName = "Should be able to submit rating to another user")]
        public async Task Should_be_able_to_submit_rating_to_another_user()
        {
            var (handler, command) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.True(response.IsSuccessful);
            Assert.Equal(response.Value, command.Value);
            Assert.Equal(response.AuthorUserName, command.AuthorUserName);
            Assert.Equal(response.ReceiverUserName, command.ReceiverUserName);
            Assert.Equal(response.Summary, command.Summary);
            Assert.Equal(response.TransactionId, command.TransactionId);
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>The handler and a command.</returns>
        protected override (SubmitRatingHandler handler, SubmitRatingCommand command) Build()
        {
            var mockMediator = new Mock<IMediator>();
            mockMediator
                .Setup(x => x.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                .Returns((CreateUserCommand cmd, CancellationToken token) =>
                {
                    var user = new User
                    {
                        UserName = cmd.UserName
                    };

                    this.Repository.AddWithAudit(user);

                    return Task.FromResult(new UserVM
                    {
                        LastReceivedRatings = new Queue<RatingVM>(),
                        LastGivenRatings = new Queue<RatingVM>(),
                        UserName = user.UserName,
                        AverageRating = 0,
                        RatingsReceived = 0,
                        TotalValue = 0
                    });
                });

            var handler = new SubmitRatingHandler(this.Repository, mockMediator.Object);

            var command = new SubmitRatingCommand
            {
                Value = this.Faker.Random.Int(1, 5),
                AuthorUserName = this.Faker.Person.Email,
                ReceiverUserName = this.Faker.Person.Email,
                Summary = this.Faker.Rant.Review(),
                TransactionId = this.Faker.Random.Hexadecimal(24, string.Empty)
            };

            return (handler, command);
        }
    }
}