// -----------------------------------------------------------------------
// <copyright file="GetAccountHandler.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Accounts.Queries
{
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Data.Interfaces;
    using Devkit.Patterns.CQRS.Query;
    using Devkit.Patterns.Exceptions;
    using Logistics.Store.API.Business.ViewModels;
    using Logistics.Store.API.Data.Models;

    /// <summary>
    /// The GetAccountQuery handler.
    /// </summary>
    public class GetAccountHandler : QueryHandlerBase<GetAccountQuery, AccountVM>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountHandler"/> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public GetAccountHandler(IRepository repository)
            : base(repository)
        {
        }

        /// <summary>
        /// The code that is executed to perform the command or query.
        /// </summary>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <returns>
        /// A task.
        /// </returns>
        protected override Task ExecuteAsync(CancellationToken cancellationToken) =>
            Task.Run(() =>
            {
                var account = this.Repository.GetOneOrDefault<Account>(x => x.UserName == this.Request.UserName);

                if (account == null)
                {
                    throw new NotFoundException(nameof(Account), this.Request.UserName);
                }

                this.Response = new AccountVM(account);
            }, cancellationToken);
    }
}