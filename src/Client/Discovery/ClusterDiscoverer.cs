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
            var client = new RestClient();

            var request = new RestRequest(discoveryUrl, Method.GET);

            var response = client.Execute<Response>(request);

            var servers = response.Data.Node.Nodes.Select(x => x.Value).ToArray();

            foreach (var server in servers)
            {
                try
                {
                    return FetchMachines(server);
                }
                catch (Exception)
                {
                    // TODO: Log this and handle it!
                }
            }

            return new string[0];
        }

        private static IEnumerable<string> FetchMachines(string machineUrl)
        {
            var client = new RestClient();

            var url = new Uri(new Uri(machineUrl), "v2/admin/machines");

            var request = new RestRequest(url.ToString(), Method.GET);

            var response = client.Execute<List<MachineResponse>>(request);

            return response.Data.Select(x => x.ClientURL).ToArray();
        }

        public class MachineResponse
        {
            public string Name { get; set; }

            public string State { get; private set; }

            public string ClientURL { get; set; }

            public string PeerURL { get; set; }
        }
    }
}