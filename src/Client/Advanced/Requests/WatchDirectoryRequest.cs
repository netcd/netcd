// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WatchDirectoryRequest.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the WatchDirectoryRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced.Requests
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the WatchDirectoryRequest type.
    /// </summary>
    public class WatchDirectoryRequest
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