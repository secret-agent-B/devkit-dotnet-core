// -----------------------------------------------------------------------
// <copyright file="GetUserValidation.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Security.Business.Users.Queries.GetMyProfile
{
    using FluentValidation;

    /// <summary>
    /// The validation for getting user information.
    /// </summary>
    public class GetMyProfileValidator : AbstractValidator<GetMyProfileQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMyProfileValidator"/> class.
        /// </summary>
        public GetMyProfileValidator()
        {
            this.RuleFor(x => x.UserName)
                .EmailAddress();
        }
    }
}