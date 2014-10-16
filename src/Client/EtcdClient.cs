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
    using System.Web;

    using netcd.Advanced;
    using netcd.Http;
    using netcd.Serialization;

    /// <summary>
    /// Defines the EtcdClient type.
    /// </summary>
    public class EtcdClient : IEtcdClient, IAdvancedEtcdClient
    {
        /// <summary>
        /// The cluster uri.
        /// </summary>
        private readonly Uri _clusterUrl;

        /// <summary>
        /// The serializer.
        /// </summary>
        private readonly ISerializer _serializer;

        /// <summary>
        /// The deserializer.
        /// </summary>
        private readonly IDeserializer _deserializer;

        /// <summary>
        /// Initializes a new instance of the <see cref="EtcdClient"/> class.
        /// </summary>
        /// <param name="clusterUrl">
        /// The cluster url.
        /// </param>
        /// <param name="serializer">
        /// The serializer.
        /// </param>
        /// <param name="deserializer">
        /// The deserializer.
        /// </param>
        public EtcdClient(Uri clusterUrl, ISerializer serializer, IDeserializer deserializer)
        {
            _clusterUrl = clusterUrl;
            _serializer = serializer;
            _deserializer = deserializer;
        }

        /// <summary>
        /// Gets the advanced.
        /// </summary>
        public IAdvancedEtcdClient Advanced
        {
            get { return this; }
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
                object value;

                if (TryGetValue(key, out value))
                {
                    return value;
                }

                throw new KeyNotFoundException();
            }

            set
            {
                var json = _serializer.Serialize(value);

                var request = new PutRequest(key, json);

                Advanced.Put(request);
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

            return new EtcdClient(clusterUrl, serializer, deserializer);
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
            throw new NotSupportedException();
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
            // TODO: Try to fetch the value, if not exists, return false, else return true.
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
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
            try
            {
                var response = Advanced.Get(new GetRequest(key));

                value = _deserializer.Deserialize(response.Node.Value);

                return true;
            }
            catch (KeyNotFoundException)
            {
                value = null;

                return false;
            }
        }

        /// <summary>
        /// Put a value into the etcd cluster.
        /// </summary>
        /// <param name="request">
        /// The put request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        PutResponse IAdvancedEtcdClient.Put(PutRequest request)
        {
            var formData = new Dictionary<string, object>
            {
                { "value", _serializer.Serialize(request.Value) }
            };

            if (request.TimeToLive > 0)
            {
                formData["ttl"] = request.TimeToLive;
            }

            var response = HttpClient.Put<PutResponse>(new Uri(_clusterUrl, string.Format("v2/keys/{0}", request.Key)), formData);

            return response;
        }

        /// <summary>
        /// Get a value from the etcd cluster.
        /// </summary>
        /// <param name="request">
        /// The get request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        GetResponse IAdvancedEtcdClient.Get(GetRequest request)
        {
            try
            {
                var response = HttpClient.Get<GetResponse>(new Uri(_clusterUrl, string.Format("v2/keys/{0}", request.Key)));

                return response;
            }
            catch (HttpException)
            {
                throw new KeyNotFoundException();
            }
        }

        /// <summary>
        /// Delete a value from the etcd cluster.
        /// </summary>
        /// <param name="request">
        /// The delete request.
        /// </param>
        /// <returns>
        /// The response.
        /// </returns>
        DeleteResponse IAdvancedEtcdClient.Delete(DeleteRequest request)
        {
            throw new NotImplementedException();
        }
    }
}