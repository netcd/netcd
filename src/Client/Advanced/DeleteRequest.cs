// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteRequest.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the DeleteRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced
{
    /// <summary>
    /// Defines the DeleteRequest type.
    /// </summary>
    public class DeleteRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteRequest"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public DeleteRequest(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; private set; }
    }
}