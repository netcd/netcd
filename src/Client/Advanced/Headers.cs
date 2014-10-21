// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Headers.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the Headers type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced
{
    /// <summary>
    /// Defines the Headers type.
    /// </summary>
    public class Headers
    {
        /// <summary>
        /// Gets or sets the etcd index.
        /// </summary>
        public int EtcdIndex { get; set; }

        /// <summary>
        /// Gets or sets the raft index.
        /// </summary>
        public int RaftIndex { get; set; }

        /// <summary>
        /// Gets or sets the raft term.
        /// </summary>
        public int RaftTerm { get; set; }
    }
}