using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles
{
    public abstract class LocationAccess
    {
        public abstract IEnumerable<IDocument> GetDocuments();
        public abstract void PutDocuments(IEnumerable<IDocument> documents);
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
