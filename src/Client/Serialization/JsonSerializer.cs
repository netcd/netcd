// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JsonSerializer.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the JsonSerializer type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Serialization
{
    using Newtonsoft.Json;

    /// <summary>
    /// Defines the JsonSerializer type.
    /// </summary>
    public class JsonSerializer : ISerializer, IDeserializer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JsonSerializer"/> class.
        /// </summary>
        /// <param name="settings">
        /// The settings.
        /// </param>
        public JsonSerializer(JsonSerializerSettings settings)
        {
            Settings = settings;
        }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        public JsonSerializerSettings Settings { get; private set; }

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
            var json = JsonConvert.SerializeObject(value, Settings);

            return json;
        }

        /// <summary>
        /// Deserialize a value from etcd.
        /// </summary>
        /// <param name="json">
        /// The value from etcd.
        /// </param>
        /// <returns>
        /// The deserialized value.
        /// </returns>
        public object Deserialize(string json)
        {
            var value = JsonConvert.DeserializeObject(JsonConvert.DeserializeObject<string>(json), Settings);

            return value;
        }
    }
}