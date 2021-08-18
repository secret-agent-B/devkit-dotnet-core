// -----------------------------------------------------------------------
// <copyright file="GetAccountValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Accounts.Queries
{
    using FluentValidation;

    /// <summary>
    /// The GetAccountQuery validator.
    /// </summary>
    public class GetAccountValidator : AbstractValidator<GetAccountQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAccountValidator"/> class.
        /// </summary>
        public GetAccountValidator()
        {
            this.RuleFor(x => x.UserName)
                .EmailAddress()
                .NotNull()
                .NotEmpty();
        }
    }
}