using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UASigner.Profiles.Exceptions;
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
                    using (var ds = document.DocumentStream)
                    {
                        ds.CopyTo(ms);
                        FileDocument fd = new FileDocument(directoryInfo.FullName + "\\" + document.DocumentName);
                        var bytes = ms.ToArray();
                        fd.DocumentStream.Write(bytes, 0, bytes.Length);
                        
                    }
                }
            }
        }
        public override void Validate()
        {
            if (!System.IO.Directory.Exists(this.directoryInfo.FullName))
            {
                throw new ConfigurationException(1, String.Format("Directory {0} does not exists!", this.directoryInfo.FullName), null);
            }
        }
        public override string ToString()
        {
            return String.Format("[{0}{1}]", this.DirectoryPath, this.fileMask);
        }

        public override void RemoveDocument(IDocument document, LocationAccess backupAccess)
        {
            if (backupAccess != null)
            {
                DirectoryAccess backupDirectoryAccress = backupAccess as DirectoryAccess;
                if (backupDirectoryAccress == null)
                {
                    throw new ConfigurationException(2, "Only local storage can be used as backup", null);
                }
                try
                {
                    if (!Directory.Exists(backupDirectoryAccress.DirectoryPath))
                    {
                        Directory.CreateDirectory(backupDirectoryAccress.DirectoryPath);
                    }

                }
                catch { }

                List<IDocument> toMoveList = new List<IDocument> { document };
                backupDirectoryAccress.PutDocuments(toMoveList);
            }
            var toRemove = this.directoryInfo.GetFiles().FirstOrDefault(x => x.Name.Equals(document.DocumentName, StringComparison.InvariantCultureIgnoreCase));
            if (toRemove != null)
            {
                File.Delete(toRemove.FullName);
            }
        }
    }
}
