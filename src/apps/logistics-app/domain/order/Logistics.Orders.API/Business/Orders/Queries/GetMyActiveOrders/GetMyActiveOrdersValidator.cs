// -----------------------------------------------------------------------
// <copyright file="GetMyActiveOrdersValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Orders.Queries.GetMyActiveOrders
{
    using FluentValidation;

    /// <summary>
    /// GetMyActiveOrdersValidator class is the validator for GetMyActiveOrdersQuery.
    /// </summary>
    public class GetMyActiveOrdersValidator : AbstractValidator<GetMyActiveOrdersQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMyActiveOrdersValidator"/> class.
        /// </summary>
        public GetMyActiveOrdersValidator()
        {
            this.RuleFor(x => x.ClientUserName)
                .EmailAddress();
        }
    }
}