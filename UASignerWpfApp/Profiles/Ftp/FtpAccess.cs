using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles
{
    public class FtpAccess : LocationAccess
    {
        string address;
        int port;
        string userName;
        string password;


        public string Address {
            get
            {
                return address;
            }
        }
        public int Port
        {
            get
            {
                return port;
            }
        }
        public string UserName
        {
            get
            {
                return userName;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
        }
        public FtpAccess(string address, int port) : this(address, port, null, null) { }

        public FtpAccess(string address, int port, string userName, string password)
        {
            this.address = address;
            this.port = port;
            this.userName = userName;
            this.password = password;
        }
        public override IEnumerable<IDocument> GetDocuments()
        {
            throw new NotImplementedException();
        }

        public override void PutDocuments(IEnumerable<IDocument> documents)
        {
            throw new NotImplementedException();
        }

        public override void RemoveDocument(IDocument document, bool withBackup)
        {
            throw new NotImplementedException();
        }
    }
}
