using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace UASigner.Profiles.Configuration
{
    [XmlRoot("Configuration")]
    public class CFConfig
    {
        [XmlArray("Profiles")]
        [XmlArrayItem("Profile",Type=typeof(CFProfile))]
        public List<CFProfile> Profiles { get; set; }


        [XmlArray("PKInfos")]
        [XmlArrayItem("PKInfo", typeof(CFPKInfo))]
        public List<CFPKInfo> CFPKInfos { get; set; }

        [XmlArray("TSInfos")]
        [XmlArrayItem("TSInfo", typeof(CFTSInfo))]
        public List<CFTSInfo> CFTSInfos { get; set; }

        [XmlElement("GlobalCertPath")]
        public string CertPath { get; set; }

        [XmlElement("Service")]
        public CFServiceConfig ServiceConfig { get; set; }
        
    }
}
