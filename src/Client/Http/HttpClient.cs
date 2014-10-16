// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpClient.cs" company="netcd">
//   Copyright © netcd 2014
// </copyright>
// <summary>
//   Defines the HttpClient type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace netcd.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Web;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines the HttpClient type.
    /// </summary>
    public class HttpClient
    {
        public static T Get<T>(Uri url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            request.AllowAutoRedirect = true;
            request.Timeout = 30000; // 30 seconds

            try
            {
                var response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    // TODO: Figure out what to throw.
                    throw new HttpException("Not found!");
                }

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    // TODO: Figure out what to throw.
                    throw new HttpException("Got invalid response");
                }

                var json = ReadResponse(response);

                var data = JsonConvert.DeserializeObject<T>(json);

                return data;
            }
            catch (WebException)
            {
                // TODO: Figure out what to throw.
                throw;
            }
        }

        public static T Put<T>(Uri url, Dictionary<string, object> values)
        {
            var formData = GetFormData(values);

            var dataBytes = Encoding.ASCII.GetBytes(formData);

            var request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "PUT";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = dataBytes.Length;
            request.UserAgent = "netcd";

            using (var stream = request.GetRequestStream())
            {
                stream.Write(dataBytes, 0, dataBytes.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var json = ReadResponse(response);

            var data = JsonConvert.DeserializeObject<T>(json);

            return data;
        }

        private static string GetFormData(Dictionary<string, object> values)
        {
            var dataCollection = HttpUtility.ParseQueryString(string.Empty);

            foreach (var value in values)
            {
                dataCollection.Add(value.Key, value.Value.ToString());
            }

            var data = dataCollection.ToString();

            return data;
        }

        private static string ReadResponse(HttpWebResponse response)
        {
            string value;

            using (var stream = response.GetResponseStream())
            {
                if (stream == null)
                {
                    return null;
                }

                using (var reader = new StreamReader(stream))
                {
                    value = reader.ReadToEnd();
                }
            }

            return value;
        }
    }
}