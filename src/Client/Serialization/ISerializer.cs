// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISerializer.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the ISerializer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Serialization
{
    /// <summary>
    /// Defines the ISerializer type.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serialize a value to a string that can be stored in etcd.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The serialized value to be stored in etcd.
        /// </returns>
        string Serialize(object value);
    }
}