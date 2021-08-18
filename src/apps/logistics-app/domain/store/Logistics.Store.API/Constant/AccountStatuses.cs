// -----------------------------------------------------------------------
// <copyright file="AccountStatuses.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Constant
{
    using System;

    /// <summary>
    /// The account status enumeration.
    /// </summary>
    [Flags]
    public enum AccountStatuses
    {
        /// <summary>
        /// The 'none' account status.
        /// </summary>
        None = 0,

        /// <summary>
        /// The active account status.
        /// </summary>
        Active = 1,

        /// <summary>
        /// The inactive account status.
        /// </summary>
        InActive = 2,

        /// <summary>
        /// The disabled account status.
        /// </summary>
        Disabled = 3
    }
}