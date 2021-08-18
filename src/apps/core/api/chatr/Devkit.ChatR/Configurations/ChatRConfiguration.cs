// -----------------------------------------------------------------------
// <copyright file="ChatRConfiguration.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Configurations
{
    /// <summary>
    /// The ChatR configuration.
    /// </summary>
    public class ChatRConfiguration
    {
        /// <summary>
        /// The configuration section.
        /// </summary>
        public const string Section = "ChatRConfiguration";

        /// <summary>
        /// Gets or sets the support email.
        /// </summary>
        /// <value>
        /// The support email.
        /// </value>
        public string SupportEmail { get; set; }

        /// <summary>
        /// Gets or sets the support phone number.
        /// </summary>
        /// <value>
        /// The system phone number.
        /// </value>
        public string SupportPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of the system.
        /// </summary>
        /// <value>
        /// The name of the system.
        /// </value>
        public string SystemName { get; set; }

        /// <summary>
        /// Gets or sets the system role.
        /// </summary>
        /// <value>
        /// The system role.
        /// </value>
        public string SystemRole { get; set; }

        /// <summary>
        /// Gets or sets the welcome message.
        /// </summary>
        /// <value>
        /// The welcome message.
        /// </value>
        public string WelcomeMessage { get; set; }
    }
}