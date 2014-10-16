// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PutResponse.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the EtcdPutResponse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the EtcdPutResponse type.
    /// </summary>
    public class PutResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PutResponse"/> class.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        /// <param name="previousNode">
        /// The previous node.
        /// </param>
        public PutResponse(string action, Node node, Node previousNode)
        {
            Action = action;
            Node = node;
            PreviousNode = previousNode;
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// Gets the node.
        /// </summary>
        public Node Node { get; private set; }

        /// <summary>
        /// Gets the previous node.
        /// </summary>
        [JsonProperty("prevNode")]
        public Node PreviousNode { get; private set; }
    }
}