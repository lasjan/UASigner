using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Service
{
    public class ServiceMessage
    {
        public ServiceState State
        {
            get;
            set;
        }

        public string Message
        {
            get;
            set;
        }
    }
}
