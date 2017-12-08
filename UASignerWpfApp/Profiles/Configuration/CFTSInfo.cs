using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
namespace UASigner.Profiles.Configuration
{
    public class CFTSInfo
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        [XmlElement("Url")]
        public string Url {get;set;}
        [XmlElement("Port")]
        public int Port { get; set; }
    }
}
