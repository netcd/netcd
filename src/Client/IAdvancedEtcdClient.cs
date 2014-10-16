// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAdvancedEtcdClient.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the IAdvancedEtcdClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd
{
    using netcd.Advanced;

    /// <summary>
    /// Defines the IAdvancedEtcdClient type.
    /// </summary>
    public interface IAdvancedEtcdClient
    {
        /// <summary>
        /// Put a value into the etcd cluster.
        /// </summary>
        /// <param name="request">
        /// The put request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        PutResponse Put(PutRequest request);

        /// <summary>
        /// Get a value from the etcd cluster.
        /// </summary>
        /// <param name="request">
        /// The get request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        GetResponse Get(GetRequest request);

        /// <summary>
        /// Delete a value from the etcd cluster.
        /// </summary>
        /// <param name="request">
        /// The delete request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        DeleteResponse Delete(DeleteRequest request);
    }
}