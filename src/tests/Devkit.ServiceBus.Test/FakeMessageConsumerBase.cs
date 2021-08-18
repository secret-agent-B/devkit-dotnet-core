// -----------------------------------------------------------------------
// <copyright file="FakeMessageConsumerBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Test
{
    using Bogus;
    using Devkit.ServiceBus;
    using Devkit.ServiceBus.Interfaces;

    /// <summary>
    /// The TestMessageConsumer is a test message consumer.
    /// </summary>
    public abstract class FakeMessageConsumerBase<TMessage> : MessageConsumerBase<TMessage>
        where TMessage : class, IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeMessageConsumerBase{TMessage}" /> class.
        /// </summary>
        protected FakeMessageConsumerBase()
        {
            this.Faker = new Faker();
        }

        /// <summary>
        /// Gets the faker.
        /// </summary>
        /// <value>
        /// The faker.
        /// </value>
        public Faker Faker { get; }
    }
}