﻿using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace ArangoDBNetStandard.Serialization
{
    /// <summary>
    /// Implements a <see cref="IApiClientSerialization"/> that uses Json.NET.
    /// </summary>
    public class JsonNetApiClientSerialization : ApiClientSerialization
    {
        public override IApiClientSerializationOptions DefaultOptions => new ApiClientSerializationOptions(true, true);

        /// <summary>
        /// Deserializes the JSON structure contained by the specified stream
        /// into an instance of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="stream">The stream containing the JSON structure to deserialize.</param>
        /// <returns></returns>
        public override T DeserializeFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
            {
                return default(T);
            }

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();

                T result = js.Deserialize<T>(jtr);

                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="options"></param>
        /// <returns></returns>

        public override byte[] Serialize<T>(T item, IApiClientSerializationOptions options)
        {
            // When no options passed use the default.
            if(options == null)
            {
                options = DefaultOptions;
            }

            var jsonSettings = new JsonSerializerSettings
            {
                NullValueHandling = options.IgnoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            if (options.UseCamelCasePropertyNames)
            {
                jsonSettings.ContractResolver = new CamelCasePropertyNamesExceptDictionaryContractResolver();
            }

            string json = JsonConvert.SerializeObject(item, jsonSettings);

            return Encoding.UTF8.GetBytes(json);
        }
    }
}
