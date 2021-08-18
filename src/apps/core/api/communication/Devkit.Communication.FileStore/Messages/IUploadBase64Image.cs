// -----------------------------------------------------------------------
// <copyright file="IUploadBase64Image.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.FileStore.Messages
{
    using Devkit.ServiceBus.Interfaces;

    /// <summary>
    /// The message to upload a base 64 image.
    /// </summary>
    public interface IUploadBase64Image : IRequest
    {
        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        string Contents { get; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        string FileName { get; }

        /// <summary>
        /// Gets the maximum size.
        /// </summary>
        /// <value>
        /// The maximum size.
        /// </value>
        int Size { get; }
    }
}