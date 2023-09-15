// -----------------------------------------------------------------------
// <copyright file="Intg_PostMessage.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Test.Business.Sessions.Commands.PostMessage
{
    using Devkit.ChatR.Business.Sessions.Commands.PostMessage;
    using Devkit.Test;

    /// <summary>
    /// Integration test for PostMessage.
    /// </summary>
    public class Intg_PostMessage : IntegrationTestBase<PostMessageCommand, Startup>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Intg_PostMessage"/> class.
        /// </summary>
        public Intg_PostMessage()
            : base()
        {
        }
    }
}