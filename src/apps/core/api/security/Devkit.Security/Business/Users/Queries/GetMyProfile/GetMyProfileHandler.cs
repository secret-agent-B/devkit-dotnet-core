// -----------------------------------------------------------------------
// <copyright file="GetUserHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Business.Users.Queries.GetMyProfile
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Query;
    using Devkit.Patterns.Exceptions;
    using Devkit.Security.Business.ViewModels;
    using Devkit.Security.Data.Models;
    using Microsoft.AspNetCore.Identity;

    /// <summary>
    /// Handler for getting user information.
    /// </summary>
    public class GetMyProfileHandler : QueryHandlerBase<GetMyProfileQuery, UserVM>
    {
        /// <summary>
        /// The user manager.
        /// </summary>
        private readonly IUserStore<UserAccount> _userStore;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetMyProfileHandler" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="userStore">The user manager.</param>
        public GetMyProfileHandler(IRepository repository, IUserStore<UserAccount> userStore)
            : base(repository)
        {
            this._userStore = userStore;
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected async override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            var userAccount = await this._userStore.FindByNameAsync(this.Request.UserName, cancellationToken);

            if (userAccount == null)
            {
                throw new NotFoundException(nameof(UserAccount), this.Request.UserName);
            }

            this.Response = new UserVM(userAccount)
            {
                Address1 = userAccount.Profile.Address1,
                Address2 = userAccount.Profile.Address2,
                City = userAccount.Profile.City,
                Province = userAccount.Profile.Province,
                Country = userAccount.Profile.Country,
                ZipCode = userAccount.Profile.ZipCode,
                SelfieId = userAccount.Profile.SelfieId,
                IdentificationCards = userAccount.Profile.IdentificationCards
                    .Select(x =>
                        new IdentificationCardVM
                        {
                            ImageId = x.ImageId,
                            Number = x.Number,
                            Type = x.Type
                        })
                    .ToList()
            };
        }
    }
}