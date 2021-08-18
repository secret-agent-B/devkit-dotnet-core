// -----------------------------------------------------------------------
// <copyright file="ResponseBase.cs" company="RyanAd">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.Patterns.CQRS
{
    using System.Collections.Generic;
    using System.Linq;
    using Devkit.Patterns.CQRS.Contracts;
    using Newtonsoft.Json;

    /// <summary>
    /// The output base class.
    /// </summary>
    public class ResponseBase : IResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseBase"/> class.
        /// </summary>
        public ResponseBase()
        {
            this.Exceptions = new Dictionary<string, IList<string>>();
        }

        /// <summary>
        /// Gets the exceptions.
        /// </summary>
        /// <value>
        /// The exceptions.
        /// </value>
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public IDictionary<string, IList<string>> Exceptions { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is successful.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is successful; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public bool IsSuccessful
        {
            get
            {
                return !this.Exceptions.Any();
            }
        }

        /// <summary>
        /// Adds the exception.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="errorMessage">The error message.</param>
        public void AddException(string key, string errorMessage)
        {
            if (this.Exceptions.TryGetValue(key, out var values))
            {
                if (values.Contains(errorMessage))
                {
                    return;
                }

                values.Add(errorMessage);
            }

            this.Exceptions.Add(key, new List<string> { errorMessage });
        }
    }
}