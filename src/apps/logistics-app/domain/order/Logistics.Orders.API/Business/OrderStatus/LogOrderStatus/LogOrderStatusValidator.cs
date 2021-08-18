// -----------------------------------------------------------------------
// <copyright file="UpdateOrderStatusValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.OrderStatus.LogOrderStatus
{
    using FluentValidation;

    /// <summary>
    /// The UpdateOrderStatusValidator validates the UpdateStatusCommand.
    /// </summary>
    public class LogOrderStatusValidator : AbstractValidator<LogOrderStatusCommand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogOrderStatusValidator"/> class.
        /// </summary>
        public LogOrderStatusValidator()
        {
            this.RuleFor(x => x.OrderId)
                .Length(24);

            this.RuleFor(x => x.Code)
                .NotNull()
                .NotEmpty();
        }
    }
}