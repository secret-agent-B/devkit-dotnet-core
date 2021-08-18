// -----------------------------------------------------------------------
// <copyright file="UserStoreFaker.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Test.Fakers
{
    using System.Threading;
    using Devkit.Security.Data.Models;
    using Devkit.Test;
    using Microsoft.AspNetCore.Identity;
    using Moq;

    /// <summary>
    /// UserStoreFaker class is the user store faker.
    /// </summary>
    public class UserStoreFaker : FakerBase<IUserStore<UserAccount>>
    {
        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns>
        /// An instance of T.
        /// </returns>
        public override IUserStore<UserAccount> Generate()
        {
            var mock = new Mock<IUserStore<UserAccount>>();

            mock
                // FindByNameAsync setup
                .Setup(x => x.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((string userName, CancellationToken token) =>
                {
                    var userAccount = new UserAccountFaker().Generate();

                    userAccount.UserName = userName;
                    userAccount.Email = userName;
                    userAccount.NormalizedUserName = userName.ToUpper();
                    userAccount.NormalizedEmail = userName.ToUpper();

                    return userAccount;
                });

            return mock.Object;
        }
    }
}