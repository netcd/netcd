// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EtcdClient.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the EtcdClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using netcd.Advanced;
    using netcd.Advanced.Requests;
    using netcd.Serialization;
    using netcd.Service;

    /// <summary>
    /// Defines the EtcdClient type.
    /// </summary>
    public class EtcdClient : IEtcdClient
    {
        /// <summary>
        /// The serializer.
        /// </summary>
        private readonly ISerializer _serializer;

        /// <summary>
        /// The deserializer.
        /// </summary>
        private readonly IDeserializer _deserializer;

        /// <summary>
        /// The advanced etcd client.
        /// </summary>
        private readonly IAdvancedEtcdClient _advancedEtcdClient;

        /// <summary>
        /// The service discovery.
        /// </summary>
        private readonly IServiceDiscovery _serviceDiscovery;

        /// <summary>
        /// Initializes a new instance of the <see cref="EtcdClient"/> class.
        /// </summary>
        /// <param name="clusterUrls">
        /// The cluster urls.
        /// </param>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        /// <param name="deserializer">
        /// The deserializer.
        /// </param>
        public EtcdClient(Uri[] clusterUrls, ISerializer serializer, IDeserializer deserializer)
        {
            _serializer = serializer;
            _deserializer = deserializer;

            _advancedEtcdClient = new AdvancedEtcdClient(clusterUrls);
            _serviceDiscovery = new ServiceDiscovery(_advancedEtcdClient);
        }

        /// <summary>
        /// Gets the advanced.
        /// </summary>
        public IAdvancedEtcdClient Advanced
        {
            get { return _advancedEtcdClient; }
        }

        /// <summary>
        /// Gets the service.
        /// </summary>
        public IServiceDiscovery Service
        {
            get { return _serviceDiscovery; }
        }

        /// <summary>
        /// Gets a value indicating whether the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/> is
        /// read-only.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/> is
        /// read-only; otherwise, <c>false</c>.
        /// </returns>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the number of elements contained in the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        int ICollection<KeyValuePair<string, object>>.Count
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// containing the keys of the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// containing the keys of the object that implements
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        ICollection<string> IDictionary<string, object>.Keys
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets an <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// containing the values in the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.Generic.ICollection`1"/>
        /// containing the values in the object that implements
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        ICollection<object> IDictionary<string, object>.Values
        {
            get { throw new NotSupportedException(); }
        }

        /// <summary>
        /// Gets or sets the element with the specified key.
        /// </summary>
        /// <returns>
        /// The element with the specified key.
        /// </returns>
        /// <param name="key">
        /// The key of the element to get or set.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">
        /// The property is retrieved and <paramref name="key"/> is not found.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The property is set and the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/> is
        /// read-only.
        /// </exception>
        public object this[string key]
        {
            get
            {
                var response = _advancedEtcdClient.GetKey(new GetKeyRequest { Key = key });

                if (response == null)
                {
                    throw new KeyNotFoundException();
                }

                if (response.Node.Dir)
                {
                    throw new NotSupportedException("Use the advanced interface to access directories");
                }

                return _deserializer.Deserialize(response.Node.Value);
            }

            set
            {
                var request = new SetKeyRequest
                {
                    Key = key,
                    Value = _serializer.Serialize(value)
                };

                _advancedEtcdClient.SetKey(request);
            }
        }

        /// <summary>
        /// Create a new default etcd client.
        /// </summary>
        /// <param name="clusterUrl">
        /// The cluster endpoint url.
        /// </param>
        /// <returns>
        /// The created etcd client.
        /// </returns>
        public static IEtcdClient CreateDefault(string clusterUrl)
        {
            return CreateDefault(new Uri(clusterUrl));
        }

        /// <summary>
        /// Create a new default etcd client.
        /// </summary>
        /// <param name="clusterUrl">
        /// The cluster endpoint url.
        /// </param>
        /// <returns>
        /// The created etcd client.
        /// </returns>
        public static IEtcdClient CreateDefault(Uri clusterUrl)
        {
            var serializer = new DefaultSerializer();
            var deserializer = new DefaultSerializer();

            return new EtcdClient(new[] { clusterUrl }, serializer, deserializer);
        }

        /// <summary>
        /// Discover an etcd cluster via a discovery service.
        /// </summary>
        /// <param name="discoveryUrl">
        /// The discovery service url.
        /// </param>
        /// <returns>
        /// The configured client.
        /// </returns>
        public static IEtcdClient Discover(string discoveryUrl)
        {
            var serializer = new DefaultSerializer();
            var deserializer = new DefaultSerializer();

            return Discover(discoveryUrl, serializer, deserializer);
        }

        /// <summary>
        /// Discover an etcd cluster via a discovery service.
        /// </summary>
        /// <param name="discoveryUrl">
        /// The discovery service url.
        /// </param>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        /// <param name="deserializer">
        /// The deserializer.
        /// </param>
        /// <returns>
        /// The configured client.
        /// </returns>
        public static IEtcdClient Discover(string discoveryUrl, ISerializer serializer, IDeserializer deserializer)
        {
            var addresses = Discovery.ClusterDiscoverer.Discover(discoveryUrl);

            return new EtcdClient(addresses.Select(x => new Uri(x)).ToArray(), serializer, deserializer);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/>
        /// that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        IEnumerator<KeyValuePair<string, object>> IEnumerable<KeyValuePair<string, object>>.GetEnumerator()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that
        /// can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Removes all items from the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is
        /// read-only.
        /// </exception>
        void ICollection<KeyValuePair<string, object>>.Clear()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Determines whether the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/> contains
        /// a specific value.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <paramref name="item"/> is found in the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <param name="item">
        /// The object to locate in the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        bool ICollection<KeyValuePair<string, object>>.Contains(KeyValuePair<string, object> item)
        {
            var response = _advancedEtcdClient.GetKey(new GetKeyRequest { Key = item.Key });

            if (response == null || response.ErrorCode == 100 || response.Node == null)
            {
                return false;
            }

            var fetchedValue = _deserializer.Deserialize(response.Node.Value);

            return Equals(item.Value, fetchedValue);
        }

        /// <summary>
        /// Copies the elements of the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/> to an
        /// <see cref="T:System.Array"/>, starting at a particular
        /// <see cref="T:System.Array"/> index.
        /// </summary>
        /// <param name="array">
        /// The one-dimensional <see cref="T:System.Array"/> that is the
        /// destination of the elements copied from
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// The <see cref="T:System.Array"/> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">
        /// The zero-based index in <paramref name="array"/> at which copying
        /// begins.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="array"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        /// <paramref name="arrayIndex"/> is less than 0.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// The number of elements in the source
        /// <see cref="T:System.Collections.Generic.ICollection`1"/> is
        /// greater than the available space from
        /// <paramref name="arrayIndex"/> to the end of the destination
        /// <paramref name="array"/>.
        /// </exception>
        void ICollection<KeyValuePair<string, object>>.CopyTo(
            KeyValuePair<string, object>[] array,
            int arrayIndex)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Determines whether the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>
        /// contains an element with the specified key.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>
        /// contains an element with the key; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="key">
        /// The key to locate in the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        public bool ContainsKey(string key)
        {
            var response = _advancedEtcdClient.GetKey(new GetKeyRequest { Key = key });

            return response != null && response.ErrorCode != 100 && response.Node != null;
        }

        /// <summary>
        /// Adds an element with the provided key and value to the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <param name="key">
        /// The object to use as the key of the element to add.
        /// </param>
        /// <param name="value">
        /// The object to use as the value of the element to add.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        /// An element with the same key already exists in the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IDictionary`2"/> is
        /// read-only.
        /// </exception>
        public void Add(string key, object value)
        {
            var serializedValue = _serializer.Serialize(value);

            Advanced.SetKey(new SetKeyRequest { Key = key, Value = serializedValue });
        }

        /// <summary>
        /// Adds an item to the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <param name="item">
        /// The object to add to the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is
        /// read-only.
        /// </exception>
        public void Add(KeyValuePair<string, object> item)
        {
            Add(item.Key, item.Value);
        }

        /// <summary>
        /// Removes the element with the specified key from the
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </summary>
        /// <returns>
        /// true if the element is successfully removed; otherwise, false.
        /// This method also returns false if <paramref name="key"/> was not
        /// found in the original
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/>.
        /// </returns>
        /// <param name="key">
        /// The key of the element to remove.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.IDictionary`2"/> is
        /// read-only.
        /// </exception>
        public bool Remove(string key)
        {
            var response = _advancedEtcdClient.DeleteKey(new DeleteKeyRequest { Key = key });

            return response != null && response.ErrorCode != 100 && response.Node != null;
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </summary>
        /// <returns>
        /// <c>true</c> if <paramref name="item"/> was successfully removed
        /// from the <see cref="T:System.Collections.Generic.ICollection`1"/>;
        /// otherwise, <c>false</c>. This method also returns <c>false</c> if
        /// <paramref name="item"/> is not found in the original
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </returns>
        /// <param name="item">
        /// The object to remove from the
        /// <see cref="T:System.Collections.Generic.ICollection`1"/>.
        /// </param><exception cref="T:System.NotSupportedException">
        /// The <see cref="T:System.Collections.Generic.ICollection`1"/> is
        /// read-only.
        /// </exception>
        bool ICollection<KeyValuePair<string, object>>.Remove(KeyValuePair<string, object> item)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the object that implements
        /// <see cref="T:System.Collections.Generic.IDictionary`2"/> contains
        /// an element with the specified key; otherwise, <c>false</c>.
        /// </returns>
        /// <param name="key">
        /// The key whose value to get.
        /// </param>
        /// <param name="value">
        /// When this method returns, the value associated with the specified
        /// key, if the key is found; otherwise, the default value for the
        /// type of the <paramref name="value"/> parameter. This parameter is
        /// passed uninitialized.
        /// </param>
        /// <exception cref="T:System.ArgumentNullException">
        /// <paramref name="key"/> is null.
        /// </exception>
        public bool TryGetValue(string key, out object value)
        {
            var response = _advancedEtcdClient.GetKey(new GetKeyRequest { Key = key });

            if (response == null || response.ErrorCode == 100 || response.Node == null)
            {
                value = default(object);

                return false;
            }

            var deserializedValue = _deserializer.Deserialize(response.Node.Value);

            value = deserializedValue;

            return true;
        }
    }
}