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
    using netcd.Advanced.Requests;

    /// <summary>
    /// Defines the IAdvancedEtcdClient type.
    /// </summary>
    public interface IAdvancedEtcdClient
    {
        /// <summary>
        /// Set a key.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        Response SetKey(SetKeyRequest request);

        /// <summary>
        /// Get a key.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        Response GetKey(GetKeyRequest request);

        /// <summary>
        /// Delete a key.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        Response DeleteKey(DeleteKeyRequest request);

        /// <summary>
        /// Watch a key.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        void WatchKey(WatchKeyRequest request);

        /// <summary>
        /// Get a directory.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        Response GetDirectory(GetDirectoryRequest request);

        /// <summary>
        /// Create a directory.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        Response CreateDirectory(CreateDirectoryRequest request);

        /// <summary>
        /// Delete a directory.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        Response DeleteDirectory(DeleteDirectoryRequest request);

        /// <summary>
        /// Watch a directory.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        void WatchDirectory(WatchDirectoryRequest request);
    }
}