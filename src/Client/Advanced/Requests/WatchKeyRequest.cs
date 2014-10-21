// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WatchKeyRequest.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the WatchKeyRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced.Requests
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the WatchKeyRequest type.
    /// </summary>
    public class WatchKeyRequest
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the callback.
        /// </summary>
        public Action<Response> Callback { get; set; }

        /// <summary>
        /// Get the parameters.
        /// </summary>
        /// <returns>
        /// The parameters.
        /// </returns>
        public Dictionary<string, object> GetParameters()
        {
            var parameters = new Dictionary<string, object>
            {
                { "wait", "true" }
            };

            return parameters;
        }
    }
}