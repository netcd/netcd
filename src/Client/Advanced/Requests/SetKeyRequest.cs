// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SetKeyRequest.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the SetKeyRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced.Requests
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the SetKeyRequest type.
    /// </summary>
    public class SetKeyRequest
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the time to live.
        /// </summary>
        public int TimeToLive { get; set; }

        /// <summary>
        /// Get the parameters.
        /// </summary>
        /// <returns>
        /// The parameters.
        /// </returns>
        public Dictionary<string, object> GetParameters()
        {
            var parameters = new Dictionary<string, object> { { "value", Value } };

            if (TimeToLive > 0)
            {
                parameters.Add("ttl", TimeToLive);
            }

            return parameters;
        }
    }
}