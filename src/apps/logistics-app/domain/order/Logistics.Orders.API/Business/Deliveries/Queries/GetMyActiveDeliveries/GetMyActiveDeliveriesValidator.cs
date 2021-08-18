// -----------------------------------------------------------------------
// <copyright file="GetMyActiveDeliveriesValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.Deliveries.Queries.GetMyActiveDeliveries
{
    using FluentValidation;

    /// <summary>
    /// Validator for GetMyActiveDeliveries.
    /// </summary>
    public class GetMyActiveDeliveriesValidator : AbstractValidator<GetMyActiveDeliveriesQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetMyActiveDeliveriesValidator"/> class.
        /// </summary>
        public GetMyActiveDeliveriesValidator()
        {
            this.RuleFor(x => x.DriverUserName)
                .EmailAddress();
        }
    }
}