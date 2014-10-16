// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Node.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the Node type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines the Node type.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        /// <param name="createdIndex">
        /// The created index.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="modifiedIndex">
        /// The modified index.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="expiration">
        /// The expiration.
        /// </param>
        /// <param name="timeToLive">
        /// The time to live.
        /// </param>
        public Node(int createdIndex, string key, int modifiedIndex, string value, DateTime expiration, int timeToLive)
        {
            TimeToLive = timeToLive;
            Expiration = expiration;
            CreatedIndex = createdIndex;
            Key = key;
            ModifiedIndex = modifiedIndex;
            Value = value;
        }

        /// <summary>
        /// Gets the created index.
        /// </summary>
        public int CreatedIndex { get; private set; }

        /// <summary>
        /// Gets the key.
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Gets the modified index.
        /// </summary>
        public int ModifiedIndex { get; private set; }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets the expiration.
        /// </summary>
        public DateTime Expiration { get; private set; }

        /// <summary>
        /// Gets the time to live.
        /// </summary>
        [JsonProperty("ttl")]
        public int TimeToLive { get; private set; }
    }
}