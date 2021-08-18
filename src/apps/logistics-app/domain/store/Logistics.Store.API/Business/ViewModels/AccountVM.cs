// -----------------------------------------------------------------------
// <copyright file="AccountVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.ViewModels
{
    using System.Diagnostics.CodeAnalysis;
    using Devkit.Patterns.CQRS;
    using Logistics.Store.API.Data.Models;

    /// <summary>
    /// The account view model.
    /// </summary>
    public class AccountVM : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountVM"/> class.
        /// </summary>
        public AccountVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccountVM"/> class.
        /// </summary>
        /// <param name="account">The account.</param>
        public AccountVM([NotNull] Account account)
        {
            this.Id = account.Id;
            this.UserName = account.UserName;
            this.AvailableCredits = account.AvailableCredits;
            this.AccountStatus = (int)account.Status;
        }

        /// <summary>
        /// Gets or sets the account status.
        /// </summary>
        /// <value>
        /// The account status.
        /// </value>
        public int AccountStatus { get; set; }

        /// <summary>
        /// Gets or sets the available credits.
        /// </summary>
        /// <value>
        /// The available credits.
        /// </value>
        public double AvailableCredits { get; set; }

        /// <summary>
        /// Gets or sets the account identifier.
        /// </summary>
        /// <value>
        /// The account identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}