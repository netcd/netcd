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
    using System.Collections.Generic;

    /// <summary>
    /// Defines the Node type.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// Gets or sets the created index.
        /// </summary>
        public int CreatedIndex { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the modified index.
        /// </summary>
        public int ModifiedIndex { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the expiration.
        /// </summary>
        public DateTime? Expiration { get; set; }

        /// <summary>
        /// Gets or sets the time to live.
        /// </summary>
        public int? Ttl { get; set; }

        /// <summary>
        /// Gets or sets the nodes.
        /// </summary>
        public List<Node> Nodes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether dir.
        /// </summary>
        public bool Dir { get; set; }
    }
}