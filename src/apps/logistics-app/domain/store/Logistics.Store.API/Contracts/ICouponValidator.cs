// -----------------------------------------------------------------------
// <copyright file="ICouponValidator.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Contracts
{
    /// <summary>
    /// Key code validator for adding credits.
    /// </summary>
    public interface ICouponValidator
    {
        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="code">The coupon code.</param>
        /// <param name="amount">The amount.</param>
        /// <returns>
        ///   <c>true</c> if the specified key is valid; otherwise, <c>false</c>.
        /// </returns>
        bool IsValid(string code, double amount);
    }
}