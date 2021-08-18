// -----------------------------------------------------------------------
// <copyright file="ChatRConfigurationFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Test.Fakers
{
    using Bogus;
    using Devkit.ChatR.Configurations;
    using Devkit.Test;
    using Microsoft.Extensions.Options;
    using Moq;

    /// <summary>
    /// ChatR configuration faker.
    /// </summary>
    internal class ChatRConfigurationFaker : FakerBase<IOptions<ChatRConfiguration>>
    {
        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        public override IOptions<ChatRConfiguration> Generate()
        {
            var faker = new Faker();
            var mock = new Mock<IOptions<ChatRConfiguration>>();

            mock
                .Setup(x => x.Value)
                .Returns(new ChatRConfiguration
                {
                    SystemName = "Snappy",
                    SupportPhoneNumber = faker.Person.Phone,
                    SystemRole = "System",
                    WelcomeMessage = faker.Rant.Review(),
                    SupportEmail = faker.Person.Email
                });

            return mock.Object;
        }
    }
}