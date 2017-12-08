using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
namespace UASigner.Profiles.Configuration
{
    public class SerializableDictionary<TKey, TValue>
    : Dictionary<string, string>, IXmlSerializable 
    {
        #region IXmlSerializable Members
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(System.Xml.XmlReader reader)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                string key = reader.GetAttribute(0);
                reader.MoveToContent();
                string value = reader.ReadElementContentAsString();
                this.Add(key, value);

            }
            reader.Read();
            //reader.ReadEndElement();
        }

        public void WriteXml(System.Xml.XmlWriter writer)
        {
            XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
            XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

            foreach (string key in this.Keys)
            {
                writer.WriteStartElement("P");
                writer.WriteAttributeString("k", key);
                writer.WriteString(this[key]);
                writer.WriteEndElement();

            }
        }
        #endregion
    }
}
