// -----------------------------------------------------------------------
// <copyright file="UpdateException.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Data
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using MongoDB.Driver;

    /// <summary>
    /// Mongo Db update exception.
    /// </summary>
    /// <seealso cref="MongoException" />
    public class UpdateException : Exception
    {
        /// <summary>
        /// Error message.
        /// </summary>
        private const string errorMessage = "Failed to update record.";

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class.
        /// </summary>
        public UpdateException()
            : base(errorMessage)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public UpdateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public UpdateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}