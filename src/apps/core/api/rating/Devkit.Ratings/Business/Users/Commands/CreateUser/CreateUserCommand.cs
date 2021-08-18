// -----------------------------------------------------------------------
// <copyright file="CreateUserCommand.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Ratings.Business.Users.Commands.CreateUser
{
    using Devkit.Patterns.CQRS.Command;
    using Devkit.Ratings.Business.ViewModels;

    /// <summary>
    /// Creates a user record.
    /// </summary>
    public class CreateUserCommand : CommandRequestBase<UserVM>
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }
    }
}