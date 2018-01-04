using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Service
{
    public interface IServiceController
    {
        event EventHandler StopInvoke;
        event EventHandler StartInvoke;
    }
}
