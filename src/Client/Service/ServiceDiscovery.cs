// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceDiscovery.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the ServiceDiscovery type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Service
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the ServiceDiscovery type.
    /// </summary>
    public class ServiceDiscovery : IServiceDiscovery
    {
        /// <summary>
        /// The advanced etcd client.
        /// </summary>
        private readonly IAdvancedEtcdClient _advancedEtcdClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDiscovery"/> class.
        /// </summary>
        /// <param name="advancedEtcdClient">
        /// The advanced etcd client.
        /// </param>
        public ServiceDiscovery(IAdvancedEtcdClient advancedEtcdClient)
        {
            _advancedEtcdClient = advancedEtcdClient;
        }

        /// <summary>
        /// Discover a named service.
        /// </summary>
        /// <param name="serviceName">
        /// The service name.
        /// </param>
        /// <returns>
        /// The found service.
        /// </returns>
        public IEnumerable<string> Discover(string serviceName)
        {
            throw new System.NotImplementedException();
        }

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
        public void Announce(string serviceName, string instanceName, string instanceAddress)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Retire an instance for a given service.
        /// </summary>
        /// <param name="serviceName">
        /// The service name.
        /// </param>
        /// <param name="instanceName">
        /// The instance name.
        /// </param>
        public void Retire(string serviceName, string instanceName)
        {
            throw new System.NotImplementedException();
        }
    }
}