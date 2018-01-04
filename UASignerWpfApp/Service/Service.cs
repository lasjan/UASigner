using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Providers;
using UASigner.Service.Configuration;
using UASigner.Profiles;
using UASigner.Profiles.Configuration;
namespace UASigner.Service
{
    public abstract class Service:IService
    {
        protected UASigner.Profiles.Configuration.Configuration config;
        protected ServiceConfiguration serviceConfig;
        protected IGenericProviderAction<IProfile> profileProvider;
        protected List<IObserver<ServiceMessage>> observers = new List<IObserver<ServiceMessage>>();
        protected IServiceController serviceController;
        public Service(UASigner.Profiles.Configuration.Configuration config, IServiceController serviceController)
        {
            this.config = config;
            this.serviceConfig = config.GetServiceConfiguration();
            this.profileProvider = new ProfileProvider(config);
            this.serviceController = serviceController;
            this.serviceController.StartInvoke += serviceController_StartInvoke;
            this.serviceController.StopInvoke += serviceController_StopInvoke;
            Init();

        }

        void serviceController_StopInvoke(object sender, EventArgs e)
        {
            Stop();
        }

        void serviceController_StartInvoke(object sender, EventArgs e)
        {
            Start();  
        }

        public virtual void Init()
        {
        }
       
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
