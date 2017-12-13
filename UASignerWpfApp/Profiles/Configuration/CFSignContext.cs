using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace UASigner.Profiles.Configuration
{
    public class CFSignContext
    {
        [XmlArray("PKInfosId")]
        [XmlArrayItem("PkInfoId", typeof(int))]
        public List<int> CFPKInfosId { get; set; }

        [XmlIgnore]
        public int? TsId
        {
            get { return (int?)(String.IsNullOrEmpty(TSInfoId) ? null : (int?)Int32.Parse(TSInfoId)); }
            set { this.TSInfoId = value == null ? String.Empty : value.ToString(); }
        }
        [XmlElement("TSInfoId")]
        public string TSInfoId { get; set; }

        [XmlElement("CertPath")]
        public string CertPath { get; set; }

        [XmlElement]
        public string SignatureProfile { get; set; }

        [XmlElement]
        public string AddContentTimeStamp { get; set; }

        [XmlElement]
        public string AddCertificate { get; set; }

        [XmlElement]
        public string AddValidationData { get; set; }
    }
}
