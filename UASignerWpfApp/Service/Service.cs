using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Providers;
using UASigner.Service.Configuration;
namespace UASigner.Service
{
    public abstract class Service:IService
    {
        ServiceConfiguration config;
        IProfileProvider profileProvider;
        private List<IObserver<ServiceMessage>> observers;

        public Service(ServiceConfiguration config, IProfileProvider profileProvider)
        {
            this.config = config;
            this.profileProvider = profileProvider;
            Init();

        }
        public virtual void Init()
        {}
       
        public IDisposable Subscribe(IObserver<ServiceMessage> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<ServiceMessage>> _observers;
            private IObserver<ServiceMessage> _observer;

            public Unsubscriber(List<IObserver<ServiceMessage>> observers, IObserver<ServiceMessage> observer)
            {
                this._observers = observers;
                this._observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                    _observers.Remove(_observer);
            }
        }

        public abstract void Start();
        public abstract void Stop();
    }
}
