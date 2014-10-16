// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PutRequest.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the PutRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced
{
    /// <summary>
    /// Defines the PutRequest type.
    /// </summary>
    public class PutRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PutRequest"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        public PutRequest(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PutRequest"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="timeToLive">
        /// The time to live.
        /// </param>
        public PutRequest(string key, string value, int timeToLive)
        {
            Key = key;
            Value = value;
            TimeToLive = timeToLive;
        }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets the time to live.
        /// </summary>
        public int TimeToLive { get; private set; }
    }
}