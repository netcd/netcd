// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteKeyRequest.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the DeleteKeyRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced.Requests
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the DeleteKeyRequest type.
    /// </summary>
    public class DeleteKeyRequest
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Get the parameters.
        /// </summary>
        /// <returns>
        /// The parameters.
        /// </returns>
        public Dictionary<string, object> GetParameters()
        {
            var parameters = new Dictionary<string, object>();

            return parameters;
        }
    }
}