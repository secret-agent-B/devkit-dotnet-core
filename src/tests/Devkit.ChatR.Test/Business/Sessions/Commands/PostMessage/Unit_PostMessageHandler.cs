// -----------------------------------------------------------------------
// <copyright file="Unit_PostMessageHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Test.Business.Sessions.Commands.PostMessage
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.ChatR.Business.Sessions.Commands.PostMessage;
    using Devkit.ChatR.Business.ViewModels;
    using Devkit.ChatR.Test.Fakers;
    using Devkit.Test;
    using Moq;
    using NUnit.Framework;
    using StackExchange.Redis;
    using StackExchange.Redis.Extensions.Core.Abstractions;

    /// <summary>
    /// Unit test for PostMessageHandler.
    /// </summary>
    public class Unit_PostMessageHandler : UnitTestBase<(PostMessageCommand command, PostMessageHandler handler)>
    {
        /// <summary>
        /// Should be able to post a message to existing session.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [TestCase(TestName = "Should be able to post a message to existing session")]
        public async Task Should_be_able_to_post_a_message_to_existing_session()
        {
            var (command, handler) = this.Build();
            var response = await handler.Handle(command, CancellationToken.None);

            Assert.That(response.IsSuccessful);
            Assert.That(response.IsDeleted, Is.False);
            Assert.That(response.Id, Is.Not.Empty);
            Assert.That(response.Timestamp, Is.Not.Null);
            Assert.Equals(command.UserName, response.AuthorUserName);
            Assert.Equals(command.Message, response.Message);
        }

        /// <summary>
        /// Should fail if non participant tries to post to session.
        /// </summary>
        [TestCase(TestName = "Should fail if non participant tries to post to session")]
        public async Task Should_fail_if_non_participant_tries_to_post_to_session()
        {
            var (command, handler) = this.Build();
            command.SessionId = $"{command.SessionId}_INVALID";

            var response = await handler.Handle(command, CancellationToken.None);

            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Exceptions.ContainsKey(nameof(command.SessionId)));
        }

        /// <summary>
        /// Should fail to post a message to invalid session identifier.
        /// </summary>
        [TestCase(TestName = "Should fail to post a message to invalid session identifier")]
        public async Task Should_fail_to_post_a_message_to_invalid_session_id()
        {
            var (command, handler) = this.Build();
            command.UserName = $"{command.UserName}_INVALID";

            var response = await handler.Handle(command, CancellationToken.None);

            Assert.That(response.IsSuccessful, Is.False);
            Assert.That(response.Exceptions.ContainsKey(nameof(command.UserName)));
        }

        /// <summary>
        /// Builds this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        protected override (PostMessageCommand command, PostMessageHandler handler) Build()
        {
            var client = new ParticipantVM
            {
                Role = "client",
                UserName = this.Faker.Person.Email,
                PhoneNumber = this.Faker.Person.Phone
            };

            var driver = new ParticipantVM
            {
                Role = "driver",
                UserName = this.Faker.Person.Email,
                PhoneNumber = this.Faker.Person.Phone
            };

            var session = new SessionVM
            {
                Id = $"{this.Faker.Random.Hexadecimal(24, string.Empty)}_{driver.UserName}",
                Topic = this.Faker.Commerce.Product(),
                Messages = new List<MessageVM>(),
                Participants = new List<ParticipantVM> { client, driver }
            };

            var command = new PostMessageCommand
            {
                UserName = driver.UserName,
                Message = this.Faker.Rant.Review(),
                SessionId = session.Id
            };

            var mockRedisCache = new Mock<IRedisClient>();

            mockRedisCache
                .Setup(x => x.Db0.ExistsAsync(It.IsAny<string>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync((string key, CommandFlags commandFlags) => session.Id == key);

            mockRedisCache
                .Setup(x => x.Db0.GetAsync<SessionVM>(It.IsAny<string>(), It.IsAny<CommandFlags>()))
                .ReturnsAsync(session);

            mockRedisCache
                .Setup(x => x.Db0.ReplaceAsync(
                    It.IsAny<string>(),
                    It.IsAny<SessionVM>(),
                    It.IsAny<When>(),
                    It.IsAny<CommandFlags>()))
                .ReturnsAsync((string key, SessionVM newSession, When when, CommandFlags commandFlags) =>
                {
                    session = newSession;
                    return true;
                });

            var handler = new PostMessageHandler(this.Repository, mockRedisCache.Object, new ChatRConfigurationFaker().Generate());

            return (command, handler);
        }
    }
}