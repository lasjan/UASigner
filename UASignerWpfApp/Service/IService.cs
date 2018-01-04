using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Service
{
    public interface IService:IObservable<ServiceMessage>
    {
        void Start();
        void Stop();
    }
}
