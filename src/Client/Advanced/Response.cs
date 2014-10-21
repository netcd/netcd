// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Response.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the Response type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using RestSharp;

    /// <summary>
    /// Defines the Response type.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Response"/> class.
        /// </summary>
        public Response()
        {
            Headers = new Headers();
        }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Gets or sets the node.
        /// </summary>
        public Node Node { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public int? ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the cause.
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public int? Index { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the prev node.
        /// </summary>
        public Node PrevNode { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        public Headers Headers { get; set; }

        /// <summary>
        /// Populate the headers.
        /// </summary>
        /// <param name="headers">
        /// The headers.
        /// </param>
        /// <returns>
        /// The response with populated headers.
        /// </returns>
        public Response PopulateHeaders(IList<Parameter> headers)
        {
            PopulateEtcdIndexHeader(headers, this);

            PopulateRaftIndexHeader(headers, this);

            PopulateRaftTermHeader(headers, this);

            return this;
        }

        /// <summary>
        /// Populate the raft term header.
        /// </summary>
        /// <param name="headers">
        /// The headers collection.
        /// </param>
        /// <param name="response">
        /// The response to populate.
        /// </param>
        private static void PopulateRaftTermHeader(IEnumerable<Parameter> headers, Response response)
        {
            PopulateHeader(headers, "X-Raft-Term", header => { response.Headers.RaftTerm = int.Parse(header.Value.ToString()); });
        }

        /// <summary>
        /// Populate the raft index header.
        /// </summary>
        /// <param name="headers">
        /// The headers collection.
        /// </param>
        /// <param name="response">
        /// The response to populate.
        /// </param>
        private static void PopulateRaftIndexHeader(IEnumerable<Parameter> headers, Response response)
        {
            PopulateHeader(headers, "X-Raft-Index", header => { response.Headers.RaftIndex = int.Parse(header.Value.ToString()); });
        }

        /// <summary>
        /// Populate the etcd index header.
        /// </summary>
        /// <param name="headers">
        /// The headers collection.
        /// </param>
        /// <param name="response">
        /// The response to populate.
        /// </param>
        private static void PopulateEtcdIndexHeader(IEnumerable<Parameter> headers, Response response)
        {
            PopulateHeader(headers, "X-Etcd-Index", header => { response.Headers.EtcdIndex = int.Parse(header.Value.ToString()); });
        }

        /// <summary>
        /// Find a header with a given name.
        /// </summary>
        /// <param name="headers">
        /// The headers collection.
        /// </param>
        /// <param name="name">
        /// The name of the header to find.
        /// </param>
        /// <returns>
        /// The found header.
        /// </returns>
        private static Parameter FindHeader(IEnumerable<Parameter> headers, string name)
        {
            return headers.FirstOrDefault(h => h.Name.Equals(name));
        }

        /// <summary>
        /// Execute a callback function when a header is found.
        /// </summary>
        /// <param name="headers">
        /// The headers collection.
        /// </param>
        /// <param name="name">
        /// The name of the header to find.
        /// </param>
        /// <param name="callback">
        /// The function to call when a header is found.
        /// </param>
        private static void PopulateHeader(IEnumerable<Parameter> headers, string name, Action<Parameter> callback)
        {
            var header = FindHeader(headers, name);

            if (header == null)
            {
                return;
            }

            callback(header);
        }
    }
}