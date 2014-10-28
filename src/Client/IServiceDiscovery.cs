// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceDiscovery.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the IServiceDiscovery type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IServiceDiscovery type.
    /// </summary>
    public interface IServiceDiscovery
    {
        /// <summary>
        /// Discover a named service.
        /// </summary>
        /// <param name="serviceName">
        /// The service name.
        /// </param>
        /// <returns>
        /// The found service.
        /// </returns>
        IEnumerable<string> Discover(string serviceName);

        /// <summary>
        /// Announce a new instance for a given service.
        /// </summary>
        /// <param name="serviceName">
        /// The service name.
        /// </param>
        /// <param name="instanceName">
        /// The instance name.
        /// </param>
        /// <param name="instanceAddress">
        /// The instance address.
        /// </param>
        void Announce(string serviceName, string instanceName, string instanceAddress);

        /// <summary>
        /// Announce a new instance for a given service.
        /// </summary>
        /// <param name="serviceName">
        /// The service name.
        /// </param>
        /// <param name="instanceName">
        /// The instance name.
        /// </param>
        /// <param name="instanceAddress">
        /// The instance address.
        /// </param>
        /// <param name="timeToLive">
        /// The time to live.
        /// </param>
        void Announce(string serviceName, string instanceName, string instanceAddress, TimeSpan timeToLive);

        /// <summary>
        /// Retire an instance for a given service.
        /// </summary>
        /// <param name="serviceName">
        /// The service name.
        /// </param>
        /// <param name="instanceName">
        /// The instance name.
        /// </param>
        void Retire(string serviceName, string instanceName);
    }
}