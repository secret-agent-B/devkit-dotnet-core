// -----------------------------------------------------------------------
// <copyright file="SpecialInstructionStatus.cs" company="RyanAd" createdOn="06-20-2020 12:25 PM" updatedOn="06-20-2020 12:28 PM" >
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Constants
{
    /// <summary>
    /// The delivery status.
    /// </summary>
    public enum SpecialInstructionStatus
    {
        /// <summary>
        /// The status when the package has been picked up by the driver.
        /// </summary>
        Incomplete = 0,

        /// <summary>
        /// The delivery and payment has been cleared.
        /// </summary>
        Completed = 1
    }
}