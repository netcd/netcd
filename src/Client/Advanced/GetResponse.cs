// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GetResponse.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the GetResponse type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Advanced
{
    /// <summary>
    /// Defines the GetResponse type.
    /// </summary>
    public class GetResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetResponse"/> class.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="node">
        /// The node.
        /// </param>
        public GetResponse(string action, Node node)
        {
            Action = action;
            Node = node;
        }

        /// <summary>
        /// Gets the action.
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// Gets the node.
        /// </summary>
        public Node Node { get; private set; }
    }
}