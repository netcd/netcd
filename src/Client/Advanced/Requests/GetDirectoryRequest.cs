// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetDirectoryRequest.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the GetDirectoryRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced.Requests
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the GetDirectoryRequest type.
    /// </summary>
    public class GetDirectoryRequest
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether recursive.
        /// </summary>
        public bool Recursive { get; set; }

        /// <summary>
        /// Get the parameters.
        /// </summary>
        /// <returns>
        /// The parameters.
        /// </returns>
        public Dictionary<string, object> GetParameters()
        {
            var parameters = new Dictionary<string, object>();

            if (Recursive)
            {
                parameters.Add("recursive", "true");
            }

            return parameters;
        }
    }
}