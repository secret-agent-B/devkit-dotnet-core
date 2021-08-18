// -----------------------------------------------------------------------
// <copyright file="AccountFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Fakers
{
    using System;
    using Devkit.Test;
    using Logistics.Store.API.Constant;
    using Logistics.Store.API.Data.Models;

    /// <summary>
    /// The AccountFaker creates instances of an account used for testing.
    /// </summary>
    public class AccountFaker : FakerBase<Account>
    {
        /// <summary>
        /// Generates the specified count.
        /// </summary>
        /// <returns>
        /// A list of T.
        /// </returns>
        public override Account Generate()
        {
            this.Faker
                .RuleFor(x => x.Status, f => f.PickRandom<AccountStatuses>())
                .RuleFor(x => x.AvailableCredits, f => Math.Round(f.Random.Double(1.00, 1000.00), 2, MidpointRounding.AwayFromZero))
                .RuleFor(x => x.UserName, f => f.Person.Email);

            return this.Faker.Generate();
        }
    }
}