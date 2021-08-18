// -----------------------------------------------------------------------
// <copyright file="ValidatorExtensions.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.Extensions
{
    using System;
    using FluentValidation;

    /// <summary>
    /// The ValidatorExtensions extends the IRuleBuilderInitial capabilities.
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Validates the base64 image.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilderInitial">The rule builder options.</param>
        /// <param name="isOptional">if set to <c>true</c> [is optional].</param>
        /// <returns>
        /// An instance of a rule builder options.
        /// </returns>
        public static IRuleBuilderInitial<T, string> ValidBase64Image<T>(this IRuleBuilderInitial<T, string> ruleBuilderInitial, bool isOptional = false)
        {
            ruleBuilderInitial.Must(photo => Validate(photo, isOptional))
                .WithMessage("Item photo is not a valid base 64 image");

            return ruleBuilderInitial;
        }

        /// <summary>
        /// Validates the base64 image.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ruleBuilderOptions">The rule builder options.</param>
        /// <param name="isOptional">if set to <c>true</c> [is optional].</param>
        /// <returns>
        /// An instance of a rule builder options.
        /// </returns>
        public static IRuleBuilderOptions<T, string> ValidBase64Image<T>(this IRuleBuilderOptions<T, string> ruleBuilderOptions, bool isOptional = false)
        {
            ruleBuilderOptions.Must(photo => Validate(photo, isOptional))
                .WithMessage("Item photo is not a valid base 64 image");

            return ruleBuilderOptions;
        }

        /// <summary>
        /// Validates the specified photo.
        /// </summary>
        /// <param name="photo">The photo.</param>
        /// <param name="isOptional">if set to <c>true</c> [is optional].</param>
        /// <returns>Returns true if string argument is a base 64 image, otherwise, false.</returns>
        private static bool Validate(string photo, bool isOptional)
        {
            try
            {
                if (string.IsNullOrEmpty(photo) && isOptional)
                {
                    return true;
                }

                if (string.IsNullOrEmpty(photo) && !isOptional)
                {
                    return false;
                }

                const string base64Key = "base64,";

                if (photo.Contains(base64Key))
                {
                    photo = photo.Substring(photo.IndexOf(base64Key, StringComparison.Ordinal) + base64Key.Length);
                }

                byte[] bytes = Convert.FromBase64String(photo);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}