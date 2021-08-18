// -----------------------------------------------------------------------
// <copyright file="DeliveryCostFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Fakers
{
    using System;
    using Bogus;
    using Devkit.Test;
    using Logistics.Orders.API.Business.ViewModels;

    /// <summary>
    /// The delivery const faker.
    /// </summary>
    /// <seealso cref="FakerBase{Order}" />
    public class DeliveryCostVMFaker : FakerBase<DeliveryCostVM>
    {
        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public override DeliveryCostVM Generate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generates the specified distance.
        /// </summary>
        /// <param name="distance">The distance.</param>
        /// <returns>The delivery cost.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Design consistency.")]
        public DeliveryCostVM Generate(double distance)
        {
            var faker = new Faker();
            var totalCost = Math.Round(faker.Random.Double(100, 200), 2);
            var systemFee = Math.Round(faker.Random.Double(10, 25), 2);
            var driverFee = totalCost - (0 + systemFee);

            return new DeliveryCostVM
            {
                DriverFee = driverFee,
                SystemFee = systemFee,
                Total = totalCost,
                DistanceInKm = distance,
                Tax = 0
            };
        }
    }
}