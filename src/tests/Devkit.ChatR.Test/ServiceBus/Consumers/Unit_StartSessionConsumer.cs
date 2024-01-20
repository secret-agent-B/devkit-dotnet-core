﻿// -----------------------------------------------------------------------
// <copyright file="Unit_StartSessionConsumer.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Test.ServiceBus.Consumers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.ChatR.Business.ViewModels;
    using Devkit.ChatR.ServiceBus.Consumers;
    using Devkit.ChatR.Test.Fakers;
    using Devkit.Communication.ChatR.Messages;
    using Devkit.Test;
    using MassTransit.Futures.Contracts;
    using MassTransit.Testing;
    using Moq;
    using NUnit.Framework;
    using StackExchange.Redis;
    using StackExchange.Redis.Extensions.Core.Abstractions;

    /// <summary>
    /// Unit test for start session consumer.
    /// </summary>
    public class Unit_StartSessionConsumer : UnitTestBase<StartSessionConsumer>
    {
        /// <summary>
        /// Should be able to return user information.
        /// </summary>
        [TestCase(TestName = "Should be able to return user information")]
        public async Task Should_be_able_to_return_user_info()
        {
            using (var testHarness = new InMemoryTestHarness())
            {
                var mockRedisClient = Unit_StartSessionConsumer.GetMockRedisClient();

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

                await Assert.ThatAsync(() => testHarness.Consumed.Any<IStartSession>(), Is.True);

                mockRedisClient.VerifyAll();
            }
        }

        /// <summary>
        /// Creates a mocked IRedisClient.
        /// </summary>
        /// <returns>A mocked IRedisClient.</returns>
        private static Mock<IRedisClient> GetMockRedisClient()
        {
            var mockRedisClient = new Mock<IRedisClient>();
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
                    It.IsAny<CommandFlags>(),
                    It.IsAny<HashSet<string>>()))
                .ReturnsAsync((string key, SessionVM session, When when, CommandFlags commandFlags, HashSet<string> hashset) =>
                {
                    cache.Add(key, session);
                    return true;
                })
                .Verifiable();

            return mockRedisClient;
        }
    }
}