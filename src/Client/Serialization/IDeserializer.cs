// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDeserializer.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the IDeserializer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Serialization
{
    /// <summary>
    /// Defines the IDeserializer type.
    /// </summary>
    public interface IDeserializer
    {
        /// <summary>
        /// Deserialize a value from etcd.
        /// </summary>
        /// <param name="value">
        /// The value from etcd.
        /// </param>
        /// <returns>
        /// The deserialized value.
        /// </returns>
         object Deserialize(string value);
    }
}