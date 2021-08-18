// -----------------------------------------------------------------------
// <copyright file="OrdersUnitTestBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test
{
    using Devkit.Communication.FileStore.Fakes.Consumers;
    using Devkit.Communication.Security.Fakes.Consumers;
    using Devkit.Test;
    using Logistics.Communication.Store.Fakes.Consumers;
    using Logistics.Orders.Test.Fakers;
    using MassTransit.Testing;

    /// <summary>
    /// Orders unit test base.
    /// </summary>
    public abstract class OrdersUnitTestBase<TSystemUnderTest> : UnitTestBase<TSystemUnderTest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrdersUnitTestBase{TSystemUnderTest}"/> class.
        /// </summary>
        protected OrdersUnitTestBase()
        {
            this.TestHarness = new InMemoryTestHarness();
            this.TestHarness.Consumer<FakeGetUserConsumer>();
            this.TestHarness.Consumer<FakeGetUsersConsumer>();
            this.TestHarness.Consumer<FakeGetAccountConsumer>();
            this.TestHarness.Consumer<FakeUploadBase64ImageConsumer>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="OrdersUnitTestBase{TSystemUnderTest}"/> class.
        /// </summary>
        ~OrdersUnitTestBase()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets the test harness.
        /// </summary>
        /// <value>
        /// The test harness.
        /// </value>
        protected InMemoryTestHarness TestHarness { get; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            this.TestHarness.Stop();
            this.TestHarness.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Seeds the database.
        /// </summary>
        protected override void SeedDatabase()
        {
            this.Repository.AddRangeWithAudit(new OrderFaker().Generate(10));
        }
    }
}