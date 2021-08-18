// -----------------------------------------------------------------------
// <copyright file="ProductFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Fakers
{
    using System;
    using System.Linq;
    using Devkit.Test;
    using Logistics.Store.API.Data.Models;

    /// <summary>
    /// The ProductFaker generates product instances used for testing.
    /// </summary>
    public class ProductFaker : FakerBase<Product>
    {
        /// <summary>
        /// Generates the specified count.
        /// </summary>
        /// <returns>
        /// A list of T.
        /// </returns>
        public override Product Generate()
        {
            this.Faker
                .RuleFor(x => x.Code, f => f.Random.Guid().ToString())
                .RuleFor(x => x.ActiveDate, f => f.Date.Recent(f.Random.Int(1, 10)))
                .RuleFor(x => x.InactiveDate, f => f.Date.Future(5))
                .RuleFor(x => x.Name, f => f.Commerce.ProductName())
                .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
                .RuleFor(x => x.PricePerUnit, f => Math.Round(f.Random.Double(1.00, 100.00), 2, MidpointRounding.AwayFromZero))
                .RuleFor(x => x.Highlights, (f, p) => f.Rant.Reviews(p.Name, f.Random.Int(1, 5)).ToList());

            return this.Faker.Generate();
        }
    }
}