// -----------------------------------------------------------------------
// <copyright file="PaymentType.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Constant
{
    using System;

    /// <summary>
    /// Payment type for a product or a service.
    /// </summary>
    [Flags]
    public enum PaymentTypes
    {
        /// <summary>
        /// Unknown payment type.
        /// </summary>
        None = 0,

        /// <summary>
        /// The cash payment type.
        /// </summary>
        Cash = 1,

        /// <summary>
        /// The credit card payment type.
        /// </summary>
        CreditCard = 2,

        /// <summary>
        /// The debit card payment type.
        /// </summary>
        DebitCard = 3,

        /// <summary>
        /// Payment through authorized payment vendors like GCash, Coins.ph, Paymaya, etc.
        /// </summary>
        PaymentVendor = 4,

        /// <summary>
        /// The bank transfer payment type.
        /// </summary>
        BankTransfer = 5,

        /// <summary>
        /// The check payment type.
        /// </summary>
        Check = 6,

        /// <summary>
        /// The coupon payment type.
        /// </summary>
        Coupon = 7
    }
}