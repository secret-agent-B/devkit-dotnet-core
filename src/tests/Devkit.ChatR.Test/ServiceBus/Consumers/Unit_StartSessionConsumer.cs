// -----------------------------------------------------------------------
// <copyright file="Unit_StartSessionConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Test.ServiceBus.Consumers
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.ChatR.Business.ViewModels;
    using Devkit.ChatR.ServiceBus.Consumers;
    using Devkit.ChatR.Test.Fakers;
    using Devkit.Communication.ChatR.Messages;
    using Devkit.Test;
    using MassTransit.Testing;
    using Moq;
    using StackExchange.Redis;
    using StackExchange.Redis.Extensions.Core.Abstractions;
    using Xunit;

    /// <summary>
    /// Unit test for start session consumer.
    /// </summary>
    public class Unit_StartSessionConsumer : UnitTestBase<StartSessionConsumer>
    {
        /// <summary>
        /// Should be able to return user information.
        /// </summary>
        [Fact(DisplayName = "Should be able to return user information")]
        public async Task Should_be_able_to_return_user_info()
        {
            using (var testHarness = new InMemoryTestHarness())
            {
                var mockRedisClient = new Mock<IRedisCacheClient>();
                var cache = new Dictionary<string, SessionVM>();

                mockRedisClient
                    .Setup(x => x.Db0.ExistsAsync(
                        It.IsAny<string>(),
                        It.IsAny<CommandFlags>()))
                    .ReturnsAsync(false)
                    .Verifiable();

                mockRedisClient
                    .Setup(x => x.Db0.AddAsync(
                        It.IsAny<string>(),
                        It.IsAny<SessionVM>(),
                        It.IsAny<When>(),
                        It.IsAny<CommandFlags>()))
                    .ReturnsAsync((string key, SessionVM session, When when, CommandFlags commandFlags) =>
                    {
                        cache.Add(key, session);
                        return true;
                    })
                    .Verifiable();

                testHarness.Consumer(() => new StartSessionConsumer(
                    mockRedisClient.Object,
                    new ChatRConfigurationFaker().Generate()));

                await testHarness.Start(CancellationToken.None);

                var id = this.Faker.Random.Hexadecimal(24);

                await testHarness.Bus.Publish<IStartSession>(
                    new
                    {
                        Id = id,
                        Participants = new[]
                        {
                            new
                            {
                                PhoneNumber = this.Faker.Person.Phone,
                                Role = "client",
                                UserName = this.Faker.Person.Email
                            },
                            new
                            {
                                PhoneNumber = this.Faker.Person.Phone,
                                Role = "driver",
                                UserName = this.Faker.Person.Email
                            }
                        },
                        Topic = this.Faker.Commerce.Product()
                    });

                Assert.True(await testHarness.Consumed.Any<IStartSession>());
                Assert.True(cache.ContainsKey(id));

                mockRedisClient.VerifyAll();
            }
        }
    }
}