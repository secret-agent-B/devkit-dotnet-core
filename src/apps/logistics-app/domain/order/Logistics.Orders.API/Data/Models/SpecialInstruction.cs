// -----------------------------------------------------------------------
// <copyright file="SpecialInstruction.cs" company="RyanAd" createdOn="06-20-2020 11:56 AM" updatedOn="06-20-2020 12:22 PM" >
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Data.Models
{
    /// <summary>
    /// A wish requested by the client for the driver.
    /// </summary>
    public class SpecialInstruction
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if the wish has been completed.
        /// </summary>
        /// <value>
        /// The is completed status.
        /// </value>
        public bool IsCompleted { get; set; }
    }
}