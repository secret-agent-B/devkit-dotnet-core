// -----------------------------------------------------------------------
// <copyright file="GetSessionValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ChatR.Business.Sessions.Queries.GetSession
{
    using FluentValidation;

    /// <summary>
    /// Validator for GetSessionQuery.
    /// </summary>
    /// <seealso cref="AbstractValidator{GetSessionQuery}" />
    public class GetSessionValidator : AbstractValidator<GetSessionQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetSessionValidator"/> class.
        /// </summary>
        public GetSessionValidator()
        {
            this.RuleFor(x => x.SessionId)
                .NotEmpty()
                .NotNull();
        }
    }
}