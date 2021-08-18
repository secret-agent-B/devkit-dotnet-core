// -----------------------------------------------------------------------
// <copyright file="GetProductValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.Products.Queries.GetProduct
{
    using FluentValidation;

    /// <summary>
    /// Validator for GetProductQuery.
    /// </summary>
    /// <seealso cref="AbstractValidator{GetProductQuery}" />
    public class GetProductValidator : AbstractValidator<GetProductQuery>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetProductValidator"/> class.
        /// </summary>
        public GetProductValidator()
        {
            this.RuleFor(x => x.Id)
                .Length(24);
        }
    }
}