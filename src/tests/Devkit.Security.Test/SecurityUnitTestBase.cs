// -----------------------------------------------------------------------
// <copyright file="SecurityUnitTestBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Test
{
    using Devkit.Communication.FileStore.Fakes.Consumers;
    using Devkit.Test;
    using MassTransit.Testing;

    /// <summary>
    /// SecurityUnitTestBase class is the security unit test base.
    /// </summary>
    public class SecurityUnitTestBase<TSystemUnderTest> : UnitTestBase<TSystemUnderTest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityUnitTestBase{TSystemUnderTest}"/> class.
        /// </summary>
        protected SecurityUnitTestBase()
        {
            this.TestHarness = new InMemoryTestHarness();
            this.TestHarness.Consumer<FakeUploadBase64ImageConsumer>();
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="SecurityUnitTestBase{TSystemUnderTest}"/> class.
        /// </summary>
        ~SecurityUnitTestBase()
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
        }
    }
}