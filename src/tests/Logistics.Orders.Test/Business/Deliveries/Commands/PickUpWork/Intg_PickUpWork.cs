// -----------------------------------------------------------------------
// <copyright file="Intg_PickUpWork.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Business.Deliveries.Commands.PickUpWork
{
    using Devkit.Test;
    using Logistics.Orders.API;
    using Logistics.Orders.API.Business.Deliveries.Commands.PickUpWork;

    /// <summary>
    /// Intg_PickUpWork class is the integration test for PickUpWork module.
    /// </summary>
    public class Intg_PickUpWork : OrdersIntegrationTestBase<(PickUpWorkCommand command, PickUpWorkHandler handler)>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_PickUpWork"/> class.
        /// </summary>
        /// <param name="testFixture">The application test fixture.</param>
        public Intg_PickUpWork(AppTestFixture<Startup> testFixture)
            : base(testFixture)
        {
            // TODO: Create pick up work handler integration test.
        }
    }
}