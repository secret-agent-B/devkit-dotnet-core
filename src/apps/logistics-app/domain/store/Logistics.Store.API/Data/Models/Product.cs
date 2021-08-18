// -----------------------------------------------------------------------
// <copyright file="Product.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Data.Models
{
    using System;
    using System.Collections.Generic;
    using Devkit.Data;

    /// <summary>
    /// The product information class.
    /// </summary>
    public class Product : DocumentBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            this.Highlights = new List<string>();
        }

        /// <summary>
        /// Gets or sets the active date.
        /// </summary>
        /// <value>
        /// The active date.
        /// </value>
        public DateTime ActiveDate { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        /// <value>
        /// The code.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets the highlights.
        /// </summary>
        /// <value>
        /// The highlights.
        /// </value>
        public List<string> Highlights { get; }

        /// <summary>
        /// Gets or sets the inactive date.
        /// </summary>
        /// <value>
        /// The inactive date.
        /// </value>
        public DateTime InactiveDate { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public double PricePerUnit { get; set; }
    }
}