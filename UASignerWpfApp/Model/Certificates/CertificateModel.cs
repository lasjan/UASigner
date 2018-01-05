using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
namespace UASigner.WpfApp.Model
{
    public class CertificateModel:NotifyModelBase
    {
        string subject;
        public string Subject
        {
            get { return subject; }
            set { subject = value; OnPropertyChanged(); }
        }
        string cn;
        public string CN
        {
            get { return cn ; }
            set { cn = value; OnPropertyChanged(); }
        }
        string simpleName;
        public string SimpleName
        {
            get { return simpleName; }
            set { simpleName = value; OnPropertyChanged(); }
        }

        public static CertificateModel CreateInstance(X509Certificate2 certificate)
        {
              return new CertificateModel { Subject = certificate.Subject, SimpleName = certificate.GetNameInfo(X509NameType.SimpleName, false) };
       
        }


    }
}
