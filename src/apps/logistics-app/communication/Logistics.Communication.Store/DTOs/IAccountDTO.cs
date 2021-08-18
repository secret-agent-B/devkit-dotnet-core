// -----------------------------------------------------------------------
// <copyright file="IAccountDTO.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Store.DTOs
{
    /// <summary>
    /// IAccount interface is the contract for a user account response.
    /// </summary>
    public interface IAccountDTO
    {
        /// <summary>
        /// Gets the available credits.
        /// </summary>
        /// <value>
        /// The available credits.
        /// </value>
        double AvailableCredits { get; }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        string Status { get; }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; }
    }
}