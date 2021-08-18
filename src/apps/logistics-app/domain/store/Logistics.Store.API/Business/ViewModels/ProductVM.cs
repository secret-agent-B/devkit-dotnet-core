// -----------------------------------------------------------------------
// <copyright file="ProductVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Store.API.Business.ViewModels
{
    using System.Collections.Generic;
    using Devkit.Patterns.CQRS;
    using Logistics.Store.API.Data.Models;

    /// <summary>
    /// The product view model.
    /// </summary>
    public class ProductVM : ResponseBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductVM"/> class.
        /// </summary>
        public ProductVM()
        {
            this.Highlights = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductVM" /> class.
        /// </summary>
        /// <param name="product">The product.</param>
        public ProductVM(Product product)
        {
            this.Id = product.Id;
            this.Code = product.Code;
            this.Description = product.Description;
            this.Highlights = product.Highlights;
            this.Name = product.Name;
            this.PricePerUnit = product.PricePerUnit;
        }

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
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

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