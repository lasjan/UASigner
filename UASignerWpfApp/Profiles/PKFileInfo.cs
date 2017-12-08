using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles
{
    public class PKFileInfo:PKInfo
    {
        public string PkFilePath { get;  set; }
        public PKFileInfo()
        {
            this.PkLocation = PK_LOCATION.FILE;
        }
    }
}
