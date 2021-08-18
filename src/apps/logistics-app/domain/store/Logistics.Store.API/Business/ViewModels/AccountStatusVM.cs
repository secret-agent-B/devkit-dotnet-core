// -----------------------------------------------------------------------
// <copyright file="AccountStatusVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.ViewModels
{
    using Logistics.Store.API.Constant;

    /// <summary>
    /// The account status view model.
    /// </summary>
    public class AccountStatusVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountStatusVM"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public AccountStatusVM(AccountStatuses status)
        {
            this.Code = status.ToString().ToLower();
            this.Value = (int)status;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }
    }
}