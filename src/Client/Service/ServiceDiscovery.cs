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
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using netcd.Advanced.Requests;

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
            var request = new GetDirectoryRequest
            {
                Key = serviceName
            };

            var response = _advancedEtcdClient.GetDirectory(request);

            if (response == null || response.Node == null || response.Node.Nodes == null)
            {
                return new string[0];
            }

            var addresses = response.Node.Nodes.Select(x => x.Value);

            return addresses.ToArray();
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
            Announce(serviceName, instanceName, instanceAddress, TimeSpan.FromSeconds(0));
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
        /// <param name="timeToLive">
        /// The time to live.
        /// </param>
        public void Announce(string serviceName, string instanceName, string instanceAddress, TimeSpan timeToLive)
        {
            var request = new SetKeyRequest
            {
                Key = string.Format("{0}/{1}", serviceName, instanceName),
                Value = instanceAddress,
                TimeToLive = (int)timeToLive.TotalSeconds
            };

            _advancedEtcdClient.SetKey(request);
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
            var request = new DeleteKeyRequest
            {
                Key = string.Format("{0}/{1}", serviceName, instanceName)
            };

            _advancedEtcdClient.DeleteKey(request);
        }
    }
}