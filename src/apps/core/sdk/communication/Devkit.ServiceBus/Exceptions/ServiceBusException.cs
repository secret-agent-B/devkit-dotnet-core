// -----------------------------------------------------------------------
// <copyright file="ServiceBusException.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.ServiceBus.Exceptions
{
    using System;

    /// <summary>
    /// ServiceBusException class is the exception that can be thrown when a service bus exception happened.
    /// </summary>
    public class ServiceBusException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBusException"/> class.
        /// </summary>
        public ServiceBusException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBusException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ServiceBusException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceBusException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ServiceBusException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}