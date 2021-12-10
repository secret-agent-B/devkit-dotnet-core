// -----------------------------------------------------------------------
// <copyright file="Base64ImageExtensions.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Communication.FileStore.Extensions
{
    using System;
    using System.Drawing;
    using System.IO;
    using System.Text.RegularExpressions;
    using SixLabors.ImageSharp;
    using SixLabors.ImageSharp.Formats.Png;
    using SixLabors.ImageSharp.Processing;

    /// <summary>
    /// File Base Manager handler.
    /// </summary>
    public static class Base64ImageExtensions
    {
        /// <summary>
        /// Saves file to the server.
        /// </summary>
        /// <param name="dataString">Posted 64 Bit data string.</param>
        /// <param name="size">Image Size.</param>
        /// <returns>bool.</returns>
        public static byte[] ToImage(this string dataString, int size = 0)
        {
            // copied from https://gist.github.com/vbfox/484643
            var base64Data = Regex.Match(dataString, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
            var binData = Convert.FromBase64String(base64Data);

            using var stream = new MemoryStream(binData);
            using var image = Image.Load(stream);

            var newWidth = image.Width;
            var newHeight = image.Height;

            if (size != 0)
            {
                //Check if the Width is greater than its Height.
                if (image.Width > image.Height)
                {
                    newWidth = size;
                    newHeight = image.Height * size / image.Width;
                }
                else
                {
                    newWidth = image.Width * size / image.Height;
                    newHeight = size;
                }
            }

            // Resize image
            image.Mutate(x => x.Resize(newWidth, newHeight));

            using (var responseStream = new MemoryStream())
            {
                image.Save(responseStream, new PngEncoder());
                return responseStream.ToArray();
            }
        }
    }
}