// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateDirectoryRequest.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the CreateDirectoryRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced.Requests
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the CreateDirectoryRequest type.
    /// </summary>
    public class CreateDirectoryRequest
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the time to live.
        /// </summary>
        public int TimeToLive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether previous exists.
        /// </summary>
        public bool PreviousExists { get; set; }

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
                { "dir", "true" }
            };

            if (TimeToLive > 0)
            {
                parameters.Add("ttl", TimeToLive);
            }

            if (PreviousExists)
            {
                parameters.Add("prevExist", "true");
            }

            return parameters;
        }
    }
}