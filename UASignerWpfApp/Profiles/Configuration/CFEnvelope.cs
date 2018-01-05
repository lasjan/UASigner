using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace UASigner.Profiles.Configuration
{
    public class CFEnvelope
    {
        [XmlAttribute("Type")]
        public string Type { get; set; }
    }
}
