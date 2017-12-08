using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
namespace UASigner.Profiles
{
    public class SignContextProfile
    {
        public bool DoContentTimestamp
        {
            get { return Tsinfo != null; }
        }
        public List<PKInfo> KeysInfo { get; set; }
        public string CertificatePath { get; set; }
        public TimeStampServerInfo Tsinfo { get; set; }
        public SignatureLevel Level { get; set; }

        public SignContextProfile()
        {
            KeysInfo = new List<PKInfo>();
        }
    }
}
