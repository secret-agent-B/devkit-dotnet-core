// -----------------------------------------------------------------------
// <copyright file="ImageUploaderExtensions.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.FileStore.Extensions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Devkit.Communication.FileStore.DTOs;
    using Devkit.Communication.FileStore.Messages;
    using Devkit.ServiceBus.Exceptions;
    using MassTransit;

    /// <summary>
    /// FileUploaderExtension class contains the extension methods for upload files.
    /// </summary>
    public static class ImageUploaderExtensions
    {
        /// <summary>
        /// Uploads a base 64 image.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <param name="base64Image">The base64 image.</param>
        /// <param name="cancellationToken">The cancellationToken token.</param>
        /// <param name="imageSize">Size of the image.</param>
        /// <returns>
        /// A <see cref="Task" /> representing the asynchronous operation.
        /// </returns>
        public static async Task<string> UploadBase64Image(this IRequestClient<IUploadBase64Image> client, string base64Image, CancellationToken cancellationToken, int imageSize = 400)
        {
            if (string.IsNullOrEmpty(base64Image))
            {
                return string.Empty;
            }

            var (uploadResponse, exception) = await client.GetResponse<IFileDTO, IConsumerException>(
                new
                {
                    Contents = base64Image,
                    FileName = $"{Guid.NewGuid().ToString()}.img",
                    Size = imageSize
                }, cancellationToken);

            if (uploadResponse.IsCompletedSuccessfully)
            {
                return (await uploadResponse).Message.Id;
            }

            throw new ServiceBusException((await exception).Message.ErrorMessage);
        }
    }
}