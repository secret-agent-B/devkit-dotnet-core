// -----------------------------------------------------------------------
// <copyright file="DeliveryOptionsFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.Test.Fakers
{
    using Devkit.Test;
    using Logistics.Orders.API.Options;
    using Microsoft.Extensions.Options;
    using Moq;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Delivery options faker.
    /// </summary>
    /// <seealso cref="FakerBase{DeliveryOptionsFaker}" />
    internal class DeliveryOptionsFaker : FakerBase<IOptions<DeliveryOptions>>
    {
        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <param name="tax">The tax rate.</param>
        /// <returns>
        /// An instance of T.
        /// </returns>
        [SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Design consistency.")]
        public IOptions<DeliveryOptions> Generate(double tax)
        {
            var mock = new Mock<IOptions<DeliveryOptions>>();
            mock.Setup(x => x.Value).Returns(new DeliveryOptions
            {
                BaseCost = 70.0,
                BaseDistanceInKm = 3.0,
                CostPerKm = 7.0,
                SystemFeePercentage = 20.0,
                Tax = tax
            });

            return mock.Object;
        }

        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        public override IOptions<DeliveryOptions> Generate()
        {
            return this.Generate(0);
        }
    }
}