using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Exceptions;
namespace UASigner.Profiles
{
    public class PKFileInfo:PKInfo
    {
        public string PkFilePath { get;  set; }

        public PKFileInfo()
        {
            this.PkLocation = PK_LOCATION.FILE;
        }
        public override void Validate()
        {
            if (!System.IO.File.Exists(this.PkFilePath))
            {
                throw new ConfigurationException(1, String.Format("File {0} does not exists!", this.PkFilePath), null);
            }

            base.Validate();
        }
    }
}
