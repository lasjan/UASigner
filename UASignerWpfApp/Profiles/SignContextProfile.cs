using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using UASigner.Profiles.Exceptions;
namespace UASigner.Profiles
{
    public class SignContextProfile
    {
       
        public List<PKInfo> KeysInfo { get; set; }
        public string CertificatePath { get; set; }
        public TimeStampServerInfo Tsinfo { get; set; }
        public SignatureLevel Level { get; set; }
        public bool AddContentTimeStamp { get; set; }
        public bool AddCertificate { get; set; }
        public bool AddValidationData { get; set; }
        public SignContextProfile()
        {
            KeysInfo = new List<PKInfo>();
        }

        public virtual void Validate()
        {
            if (!System.IO.Directory.Exists(this.CertificatePath))
            {
                throw new ConfigurationException(1, String.Format("Directory {0} does not exists!", this.CertificatePath), null);
            }
            this.KeysInfo.ForEach(x => x.Validate());
        }
    }
}
