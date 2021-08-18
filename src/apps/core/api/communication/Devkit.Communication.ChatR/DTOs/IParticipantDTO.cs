// -----------------------------------------------------------------------
// <copyright file="IParticipantDTO.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.ChatR.DTOs
{
    /// <summary>
    /// A ChatR participant.
    /// </summary>
    public interface IParticipantDTO
    {
        /// <summary>
        /// Gets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        string PhoneNumber { get; }

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <value>
        /// The role.
        /// </value>
        public string Role { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        string UserName { get; }
    }
}