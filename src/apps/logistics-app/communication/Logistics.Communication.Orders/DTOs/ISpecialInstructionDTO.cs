// -----------------------------------------------------------------------
// <copyright file="ISpecialInstructionDTO.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Communication.Orders.DTOs
{
    /// <summary>
    /// A delivery special instruction.
    /// </summary>
    public interface ISpecialInstructionDTO
    {
        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is completed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is completed; otherwise, <c>false</c>.
        /// </value>
        bool IsCompleted { get; }
    }
}