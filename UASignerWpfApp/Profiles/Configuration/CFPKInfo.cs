﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace UASigner.Profiles.Configuration
{
     public class CFPKInfo
     {
         [XmlAttribute("Id")]
         public int Id { get; set; }
         [XmlAttribute("Type")]
         public string Type { get; set; }
         [XmlElement("Parameters")]
         public SerializableDictionary<string, string> Parameters { get; set; }
    }
}
