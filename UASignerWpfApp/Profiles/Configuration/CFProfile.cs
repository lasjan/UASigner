using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace UASigner.Profiles.Configuration
{
    public class CFProfile
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        [XmlElement("InputLocationAccess")]
        public CFLocationAccess InputLocationAccess { get; set; }
        [XmlElement("BackupLocationAccess")]
        public CFLocationAccess BackupLocationAccess { get; set; }
        [XmlArray("OutputLocationsAccess")]
        [XmlArrayItem("OutputLocationAccess", Type = typeof(CFLocationAccess))]
        public List<CFLocationAccess> OutputLocationsAccess  { get; set; }
        [XmlElement("SignContext")]
        public CFSignContext SignContext { get; set; }
        [XmlElement("Envelope")]
        public CFEnvelope Envelope {get;set;}

    }
}
