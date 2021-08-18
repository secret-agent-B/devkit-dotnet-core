// -----------------------------------------------------------------------
// <copyright file="GetMyProfileQuery.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Business.Users.Queries.GetMyProfile
{
    using Devkit.Patterns.CQRS.Query;
    using Devkit.Security.Business.ViewModels;

    /// <summary>
    /// The query for getting user information.
    /// </summary>
    public class GetMyProfileQuery : QueryRequestBase<UserVM>
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