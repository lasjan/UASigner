using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model;
using System.Security.Cryptography.X509Certificates;
using System.IO;
namespace UASigner.WpfApp.Utils
{
    public static class Helper
    {
        public static class Fetcher
        {
            public static List<CertificateModel> GetCertficatesFromPath(string path)
            {
                List<CertificateModel> certList = new List<CertificateModel>();
                foreach (var f in Directory.GetFiles(path))
                {
                    FileInfo fi = new FileInfo(f);
                    X509Certificate2 certificate = null;

                    try 
                    {
                        certificate = new X509Certificate2(f);
                    }
                    catch { }

                    if (certificate != null)
                    {
                        certList.Add( CertificateModel.CreateInstance(certificate));
                    }
                }

                return certList;
            }
        }
    }
}
