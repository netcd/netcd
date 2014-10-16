// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEtcdClient.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the IEtcdClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IEtcdClient type.
    /// </summary>
    public interface IEtcdClient : IDictionary<string, object>
    {
        /// <summary>
        /// Gets the advanced.
        /// </summary>
        IAdvancedEtcdClient Advanced { get; }
    }
}