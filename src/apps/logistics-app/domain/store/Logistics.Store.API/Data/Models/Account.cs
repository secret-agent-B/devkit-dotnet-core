// -----------------------------------------------------------------------
// <copyright file="Account.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Data.Models
{
    using Devkit.Data;
    using Logistics.Store.API.Constant;

    /// <summary>
    /// Account class that tracks user credits.
    /// </summary>
    /// <seealso cref="DocumentBase" />
    public class Account : DocumentBase
    {
        /// <summary>
        /// Gets or sets the credits.
        /// </summary>
        /// <value>
        /// The credits.
        /// </value>
        public double AvailableCredits { get; set; }

        /// <summary>
        /// Gets or sets the account status.
        /// </summary>
        /// <value>
        /// The account status.
        /// </value>
        public AccountStatuses Status { get; set; }

        /// <summary>
        /// Gets or sets the owner identifier.
        /// </summary>
        /// <value>
        /// The owner identifier.
        /// </value>
        public string UserName { get; set; }
    }
}