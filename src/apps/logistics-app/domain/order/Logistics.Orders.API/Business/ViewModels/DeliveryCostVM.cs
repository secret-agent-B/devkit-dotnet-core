// -----------------------------------------------------------------------
// <copyright file="DeliveryCostVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.ViewModels
{
    using Logistics.Orders.API.Data.Models;
    using Devkit.Patterns.CQRS;

    /// <summary>
    /// The DeliveryCostVM is the view model contains total cost and breakdown of the delivery fee.
    /// </summary>
    public class DeliveryCostVM : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryCostVM"/> class.
        /// </summary>
        public DeliveryCostVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryCostVM"/> class.
        /// </summary>
        /// <param name="deliveryCost">The delivery cost.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public DeliveryCostVM(DeliveryCost deliveryCost)
        {
            this.DriverFee = deliveryCost.DriverFee;
            this.DistanceInKm = deliveryCost.DistanceInKm;
            this.SystemFee = deliveryCost.SystemFee;
            this.Tax = deliveryCost.Tax;
            this.Total = deliveryCost.Total;
        }

        /// <summary>
        /// Gets or sets the distance in km.
        /// </summary>
        /// <value>
        /// The distance in km.
        /// </value>
        public double DistanceInKm { get; set; }

        /// <summary>
        /// Gets or sets the drivers fee.
        /// </summary>
        /// <value>
        /// The drivers fee.
        /// </value>
        public double DriverFee { get; set; }

        /// <summary>
        /// Gets or sets the system fee.
        /// </summary>
        /// <value>
        /// The system fee.
        /// </value>
        public double SystemFee { get; set; }

        /// <summary>
        /// Gets or sets the tax.
        /// </summary>
        /// <value>
        /// The tax.
        /// </value>
        public double Tax { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        /// <value>
        /// The total amount.
        /// </value>
        public double Total { get; set; }
    }
}