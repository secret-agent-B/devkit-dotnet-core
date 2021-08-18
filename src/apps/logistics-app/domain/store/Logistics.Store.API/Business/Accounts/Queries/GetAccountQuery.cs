// -----------------------------------------------------------------------
// <copyright file="GetAccountQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Accounts.Queries
{
    using Devkit.Patterns.CQRS.Query;
    using Logistics.Store.API.Business.ViewModels;

    /// <summary>
    /// The query used to fetch account information.
    /// </summary>
    /// <seealso cref="QueryRequestBase{AccountVM}" />
    public class GetAccountQuery : QueryRequestBase<AccountVM>
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}