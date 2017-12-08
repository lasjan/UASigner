using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization;
namespace UASigner.Profiles.Utils
{
    public class Serialization
    {
        public static T DeserializeToObject<T>(string xml)
        {

            XmlSerializer xser = new XmlSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    sw.Write(xml);
                    sw.Flush();
                    ms.Position = 0;
                    return (T)xser.Deserialize(ms);
                }
            }
        }
        public static T DeserializeToObjectWithDataContract<T>(string xml)
        {
            DataContractSerializer  dcser = new DataContractSerializer(typeof(T));

            using (MemoryStream ms = new MemoryStream())
            {
                using (StreamWriter sw = new StreamWriter(ms))
                {
                    sw.Write(xml);
                    sw.Flush();
                    ms.Position = 0;

                    return (T)dcser.ReadObject(ms);
                }
            }
        }

        public static string SerializeFromObject<T>(T o)
        {
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            XmlSerializer xser = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                xser.Serialize(ms, o, ns);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);

                return sr.ReadToEnd();
            }
        }
    }
}
