using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Screenshotter
{
    public static class JSONSerializer
    {
        /// <summary>
        /// Serializes an object to JSON
        /// </summary>
        public static string Serialize(object instance)
        {
            var t = instance.GetType();
            var serializer = new DataContractJsonSerializer(t);
            using (var stream = new MemoryStream())
            {
                serializer.WriteObject(stream, instance);
                return Encoding.Default.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// Deserializes an object from JSON
        /// </summary>
        public static T Deserialize<T>(string json) where T : class
        {
            using (var stream = new MemoryStream(Encoding.Default.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return serializer.ReadObject(stream) as T;
            }
        }
    }
}
