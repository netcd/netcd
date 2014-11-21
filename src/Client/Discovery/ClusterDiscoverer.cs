// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClusterDiscoverer.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the ClusterDiscoverer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Discovery
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using netcd.Advanced;

    using RestSharp;

    /// <summary>
    /// Defines the ClusterDiscoverer type.
    /// </summary>
    public class ClusterDiscoverer
    {
        /// <summary>
        /// Discover an etcd cluster.
        /// </summary>
        /// <param name="discoveryUrl">
        /// The discovery service url.
        /// </param>
        /// <returns>
        /// The cluster addresses.
        /// </returns>
        public static IEnumerable<string> Discover(string discoveryUrl)
        {
            return Discover(new Uri(discoveryUrl));
        }

        /// <summary>
        /// Discover an etcd cluster.
        /// </summary>
        /// <param name="discoveryUrl">
        /// The discovery service url.
        /// </param>
        /// <returns>
        /// The cluster addresses.
        /// </returns>
        public static IEnumerable<string> Discover(Uri discoveryUrl)
        {
            var baseUrl = discoveryUrl.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped);

            var client = new RestClient(baseUrl);

            var request = new RestRequest(discoveryUrl.PathAndQuery, Method.GET);

            var response = client.Execute<Response>(request);

            var servers = response.Data.Node.Nodes.Select(x => x.Value).ToArray();

            foreach (var server in servers)
            {
                try
                {
                    return FetchEtcdEndpoints(new Uri(server));
                }
                catch (Exception)
                {
                    // TODO: Log this and handle it!
                }
            }

            return new string[0];
        }

        /// <summary>
        /// Fetch the etcd endpoints.
        /// </summary>
        /// <param name="machineUrl">
        /// The machine url.
        /// </param>
        /// <returns>
        /// The etcd endpoints.
        /// </returns>
        private static IEnumerable<string> FetchEtcdEndpoints(Uri machineUrl)
        {
            var client = new RestClient(machineUrl);

            var request = new RestRequest("v2/admin/machines", Method.GET);

            var response = client.Execute<List<MachineResponse>>(request);

            return response.Data.Select(x => x.ClientURL).ToArray();
        }

        /// <summary>
        /// Defines the ClusterDiscoverer type.
        /// </summary>
        public class MachineResponse
        {
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets the state.
            /// </summary>
            public string State { get; private set; }

            /// <summary>
            /// Gets or sets the client url.
            /// </summary>
            public string ClientURL { get; set; }

            /// <summary>
            /// Gets or sets the peer url.
            /// </summary>
            public string PeerURL { get; set; }
        }
    }
}