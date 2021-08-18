// -----------------------------------------------------------------------
// <copyright file="CreateTransactionCommandFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.Test.Fakers
{
    using Devkit.Test;
    using Logistics.Store.API.Business.Transactions.Commands.CreateTransaction;
    using Logistics.Store.API.Constant;

    /// <summary>
    /// The CreateTransactionFaker generates instances of a CreateTransactionCommand used for testing.
    /// </summary>
    public class CreateTransactionCommandFaker : FakerBase<CreateTransactionCommand>
    {
        /// <summary>
        /// Generates the specified count.
        /// </summary>
        /// <returns>
        /// A list of T.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override CreateTransactionCommand Generate()
        {
            this.Faker
                .RuleFor(x => x.UserName, f => f.Person.Email)
                .RuleFor(x => x.Currency, f => f.Finance.Currency().Code)
                .RuleFor(x => x.PaymentType, f => f.PickRandom<PaymentTypes>().ToString());

            return this.Faker.Generate();
        }
    }
}