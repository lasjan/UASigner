using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace UASigner.Profiles
{
    public interface IDocument
    {
        string DocumentName { get; }
        Stream DocumentStream { get; }

    }
}
