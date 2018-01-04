using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Exceptions
{
    public interface IVisualException
    {
        string DictEntryKey { get; }
    }
}
