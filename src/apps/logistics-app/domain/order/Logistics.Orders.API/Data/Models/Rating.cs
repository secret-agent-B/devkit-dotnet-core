// -----------------------------------------------------------------------
// <copyright file="Rating.cs" company="RyanAd" createdOn="06-20-2020 2:10 PM" updatedOn="06-20-2020 2:58 PM" >
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Logistics.Orders.API.Data.Models
{
    /// <summary>
    /// A delivery rating.
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// Gets or sets the communication.
        /// </summary>
        /// <value>
        /// The communication.
        /// </value>
        public int Communication { get; set; }

        /// <summary>
        /// Gets or sets the cost.
        /// </summary>
        /// <value>
        /// The cost.
        /// </value>
        public int Cost { get; set; }

        /// <summary>
        /// Gets or sets the delivery identifier.
        /// </summary>
        /// <value>
        /// The delivery identifier.
        /// </value>
        public string DeliveryId { get; set; }

        /// <summary>
        /// Gets or sets the handling.
        /// </summary>
        /// <value>
        /// The handling.
        /// </value>
        public int Handling { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the timeliness.
        /// </summary>
        /// <value>
        /// The timeliness.
        /// </value>
        public int Timeliness { get; set; }
    }
}