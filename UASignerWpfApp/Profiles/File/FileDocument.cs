using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace UASigner.Profiles
{
    public class FileDocument:IDocument
    {
        string filePath;
        public FileDocument(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            this.DocumentName = fi.Name;
            this.filePath = filePath;
        }
        public System.IO.Stream DocumentStream
        {
            get
            {
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
                return fs;
            }
        }

        public string DocumentName
        {
            get;
            set;
        }
    }
}
