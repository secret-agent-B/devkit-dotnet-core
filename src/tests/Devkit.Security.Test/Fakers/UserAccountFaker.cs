// -----------------------------------------------------------------------
// <copyright file="UserAccountFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Test.Fakers
{
    using Bogus;
    using Devkit.Security.Data.Models;
    using Devkit.Test;

    /// <summary>
    /// UserAccountFaker class is the user accounts faker.
    /// </summary>
    public class UserAccountFaker : FakerBase<UserAccount>
    {
        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        public override UserAccount Generate()
        {
            var faker = new Faker();
            var email = faker.Person.Email;
            var firstName = faker.Person.FirstName;
            var middleName = faker.Person.FirstName;
            var lastName = faker.Person.LastName;
            var fullName = $"{firstName} {middleName} {lastName}";

            this.Faker
                .RuleFor(x => x.UserName, email)
                .RuleFor(x => x.Email, email)
                .RuleFor(x => x.NormalizedUserName, email.ToUpper())
                .RuleFor(x => x.NormalizedEmail, email.ToUpper())
                .RuleFor(x => x.Profile,
                    f => new UserProfile
                    {
                        FirstName = firstName,
                        MiddleName = middleName,
                        LastName = lastName,
                        Address1 = f.Person.Address.Street,
                        Address2 = f.Person.Address.Suite,
                        City = f.Person.Address.City,
                        Province = f.Person.Address.State,
                        Country = "USA",
                        ZipCode = f.Person.Address.ZipCode,
                        FullName = fullName,
                        NormalizedFirstName = firstName.ToUpper(),
                        NormalizedMiddleName = middleName.ToUpper(),
                        NormalizedLastName = lastName.ToUpper(),
                        NormalizedFullName = fullName.ToUpper(),
                        SelfieId = faker.Random.Hexadecimal(24, string.Empty)
                    });

            var userAccount = this.Faker.Generate();

            userAccount.Profile.IdentificationCards.Add(
                new IdentificationCard
                {
                    ImageId = faker.Random.Hexadecimal(24, string.Empty),
                    Number = faker.Random.AlphaNumeric(10),
                    Type = faker.PickRandom("NBI", "DRIVERS_LICENSE", "OTHER")
                });

            return userAccount;
        }
    }
}