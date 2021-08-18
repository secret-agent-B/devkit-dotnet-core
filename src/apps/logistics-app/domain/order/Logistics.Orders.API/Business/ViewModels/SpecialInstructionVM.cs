// -----------------------------------------------------------------------
// <copyright file="SpecialInstructionVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.ViewModels
{
    using System.Diagnostics.CodeAnalysis;
    using Logistics.Orders.API.Data.Models;

    /// <summary>
    /// Special instruction view model contract.
    /// </summary>
    public class SpecialInstructionVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialInstructionVM"/> class.
        /// </summary>
        public SpecialInstructionVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialInstructionVM" /> class.
        /// </summary>
        /// <param name="specialInstruction">The special instruction.</param>
        public SpecialInstructionVM([NotNull] SpecialInstruction specialInstruction)
        {
            this.Description = specialInstruction.Description;
            this.IsCompleted = specialInstruction.IsCompleted;
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether if the wish has been completed.
        /// </summary>
        /// <value>
        /// The is completed status.
        /// </value>
        public bool IsCompleted { get; set; }
    }
}