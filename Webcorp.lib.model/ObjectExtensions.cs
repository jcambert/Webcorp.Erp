using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Webcorp.Model
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this T source)
        {
            MemoryStream stream1 = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(stream1, source);

            stream1.Position = 0;
            T result = (T)ser.ReadObject(stream1);

            return result;
        }
    }
}
