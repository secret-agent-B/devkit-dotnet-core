// -----------------------------------------------------------------------
// <copyright file="TransactionStatus.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Constant
{
    /// <summary>
    /// The possible transaction statuses.
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// The unknown
        /// </summary>
        Unknown = -2,

        /// <summary>
        /// The unknown
        /// </summary>
        InsufficientFunds = -2,

        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Payment request to transfer funds has been sent.
        /// </summary>
        RequestedFunds = 1,

        /// <summary>
        /// Requested funds were paid but funds are waiting to be cleared from the source account/bank.
        /// </summary>
        Pending = 2,

        /// <summary>
        /// Funds have been transferred to the system account.
        /// </summary>
        Complete = 3
    }
}