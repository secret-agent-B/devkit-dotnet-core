// -----------------------------------------------------------------------
// <copyright file="DistanceVM.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Business.ViewModels
{
    using System;
    using Logistics.Orders.API.Data.Models;

    /// <summary>
    /// Distance view model contract.
    /// </summary>
    public class DistanceVM
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DistanceVM"/> class.
        /// </summary>
        public DistanceVM()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DistanceVM"/> class.
        /// </summary>
        /// <param name="distance">The distance.</param>
        public DistanceVM(Distance distance)
        {
            if (distance == null)
            {
                throw new ArgumentNullException(nameof(distance));
            }

            this.Text = distance.Text;
            this.Value = distance.Value;
            this.Time = distance.Time;
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value { get; set; }
    }
}