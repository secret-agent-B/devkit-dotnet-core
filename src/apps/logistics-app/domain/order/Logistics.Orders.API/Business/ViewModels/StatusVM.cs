// -----------------------------------------------------------------------
// <copyright file="StatusVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.ViewModels
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Devkit.Patterns;
    using Logistics.Orders.API.Constants;
    using Logistics.Orders.API.Data.Models;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// The order status view model.
    /// </summary>
    public class StatusVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusVM"/> class.
        /// </summary>
        public StatusVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusVM" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public StatusVM([NotNull] Status status)
        {
            this.Code = EnumerationBase.FromValue<StatusCode>(status.Value).DisplayName;
            this.Value = status.Value;
            this.Comments = status.Comments;
            this.Timestamp = status.Timestamp;
            this.UserName = status.UserName;
        }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public JObject User { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }
    }
}