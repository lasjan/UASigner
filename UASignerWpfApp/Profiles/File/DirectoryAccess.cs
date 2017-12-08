using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace UASigner.Profiles
{
    public class DirectoryAccess:LocationAccess
    {

        DirectoryInfo directoryInfo;
        string fileMask ="xml";

        public string DirectoryPath
        {
            get { return directoryInfo.FullName; }
        }
        public string FileMask
        {
            get { return fileMask; }
        }
        public DirectoryAccess(string directoryPath, string fileMask)
        {
            this.directoryInfo = new DirectoryInfo(directoryPath);
            this.fileMask = fileMask;
        }
        public override IEnumerable<IDocument> GetDocuments()
        {
            foreach (var fileInfo in this.directoryInfo.GetFiles())
            {
                FileDocument fd = new FileDocument(fileInfo.FullName);
                yield return fd;
            }
        }

        public override void PutDocuments(IEnumerable<IDocument> documents)
        {
            foreach (var document in documents)
            {
                using(MemoryStream ms = new MemoryStream())
                {
                    document.DocumentStream.CopyTo(ms);
                    FileDocument fd = new FileDocument(directoryInfo.FullName + "\\" + document.DocumentName);
                    var bytes = ms.ToArray();
                    fd.DocumentStream.Write(bytes, 0, bytes.Length);
                }
            }
        }
        public override string ToString()
        {
            return String.Format("[{0}{1}]", this.DirectoryPath, this.fileMask);
        }
    }
}
