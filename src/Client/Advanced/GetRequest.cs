// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetRequest.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the GetRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced
{
    /// <summary>
    /// Defines the GetRequest type.
    /// </summary>
    public class GetRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetRequest"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public GetRequest(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; private set; }
    }
}