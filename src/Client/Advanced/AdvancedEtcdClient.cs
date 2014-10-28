// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AdvancedEtcdClient.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the AdvancedEtcdClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced
{
    using System;
    using System.Collections.Generic;

    using netcd.Advanced.Requests;

    using RestSharp;

    /// <summary>
    /// Defines the AdvancedEtcdClient type.
    /// </summary>
    public class AdvancedEtcdClient : IAdvancedEtcdClient
    {
        /// <summary>
        /// The cluster urls.
        /// </summary>
        private readonly Uri[] _clusterUrls;

        /// <summary>
        /// The client.
        /// </summary>
        private RestClient _client;

        /// <summary>
        /// The current cluster url index.
        /// </summary>
        private int _currentClusterUrlIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdvancedEtcdClient"/> class.
        /// </summary>
        /// <param name="clusterUrls">
        /// The cluster urls.
        /// </param>
        public AdvancedEtcdClient(Uri[] clusterUrls)
        {
            if (clusterUrls == null)
            {
                throw new ArgumentNullException("clusterUrls");
            }

            if (clusterUrls.Length < 1)
            {
                throw new ArgumentException("Must provide at least one url", "clusterUrls");
            }

            _clusterUrls = clusterUrls;

            RenewClient();
        }

        /// <summary>
        /// Set a key.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        public Response SetKey(SetKeyRequest request)
        {
            return Execute(request.Key, Method.PUT, request.GetParameters());
        }

        /// <summary>
        /// Get a key.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        public Response GetKey(GetKeyRequest request)
        {
            return Execute(request.Key, Method.GET, request.GetParameters());
        }

        /// <summary>
        /// Delete a key.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        public Response DeleteKey(DeleteKeyRequest request)
        {
            return Execute(request.Key, Method.DELETE, request.GetParameters());
        }

        /// <summary>
        /// Watch a key.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        public void WatchKey(WatchKeyRequest request)
        {
            ExecuteAsync(request.Key, Method.GET, request.Callback, request.GetParameters());
        }

        /// <summary>
        /// Get a directory.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        public Response GetDirectory(GetDirectoryRequest request)
        {
            return Execute(request.Key, Method.GET, request.GetParameters());
        }

        /// <summary>
        /// Create a directory.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        public Response CreateDirectory(CreateDirectoryRequest request)
        {
            return Execute(request.Key, Method.PUT, request.GetParameters());
        }

        /// <summary>
        /// Delete a directory.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        public Response DeleteDirectory(DeleteDirectoryRequest request)
        {
            return Execute(request.Key, Method.DELETE, request.GetParameters());
        }

        /// <summary>
        /// Watch a directory.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        public void WatchDirectory(WatchDirectoryRequest request)
        {
            ExecuteAsync(request.Key, Method.GET, request.Callback, request.GetParameters());
        }

        /// <summary>
        /// Process a REST response.
        /// </summary>
        /// <param name="response">
        /// The REST response.
        /// </param>
        /// <returns>
        /// The etcd response.
        /// </returns>
        private static Response ProcessResponse(IRestResponse<Response> response)
        {
            if (response == null || response.Data == null)
            {
                return null;
            }

            return response.Data.PopulateHeaders(response.Headers);
        }

        /// <summary>
        /// Renew the client.
        /// </summary>
        private void RenewClient()
        {
            var url = _clusterUrls[_currentClusterUrlIndex].ToString();

            ChooseNextClusterNodeCandidate();

            _client = new RestClient(url);
        }

        /// <summary>
        /// Choose the next cluster node candidate.
        /// </summary>
        private void ChooseNextClusterNodeCandidate()
        {
            _currentClusterUrlIndex += 1;

            if (_currentClusterUrlIndex >= _clusterUrls.Length)
            {
                _currentClusterUrlIndex = 0;
            }
        }

        /// <summary>
        /// Execute a request against etcd.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="method">
        /// The HTTP method.
        /// </param>
        /// <param name="parameters">
        /// The request parameters.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        private Response Execute(string key, Method method, Dictionary<string, object> parameters)
        {
            var request = new RestRequest(string.Format("v2/keys/{0}", key), method);

            request.OnBeforeDeserialization += r => { r.ContentType = "application/json"; };

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    request.AddParameter(parameter.Key, parameter.Value);
                }
            }

            var response = _client.Execute<Response>(request);

            // TODO: Handle exception.
            // TODO: On timeout exception, renew the client.

            return ProcessResponse(response);
        }

        /// <summary>
        /// Execute an async request against etcd.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="method">
        /// The HTTP method.
        /// </param>
        /// <param name="callback">
        /// The function to call when a response is received.
        /// </param>
        /// <param name="parameters">
        /// The request parameters.
        /// </param>
        private void ExecuteAsync(string key, Method method, Action<Response> callback, Dictionary<string, object> parameters = null)
        {
            var url = string.Format("v2/keys/{0}", key);

            var restRequest = new RestRequest(url, method);

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    restRequest.AddParameter(parameter.Key, parameter.Value);
                }
            }

            _client.ExecuteAsync<Response>(restRequest, r => callback(ProcessResponse(r)));
        }
    }
}