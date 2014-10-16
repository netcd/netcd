// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultSerializer.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the DefaultSerializer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Serialization
{
    /// <summary>
    /// Defines the DefaultSerializer type.
    /// </summary>
    /// <remarks>
    /// The default serializer only supports strings.
    /// </remarks>
    public class DefaultSerializer : ISerializer, IDeserializer
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
        public string Serialize(object value)
        {
            return value == null ? string.Empty : value.ToString();
        }

        /// <summary>
        /// Deserialize a value from etcd.
        /// </summary>
        /// <param name="value">
        /// The value from etcd.
        /// </param>
        /// <returns>
        /// The deserialized value.
        /// </returns>
        public object Deserialize(string value)
        {
            return value;
        }
    }
}