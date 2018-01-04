using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using UASigner.Profiles.Providers;
using UASigner.Profiles;
using UASigner.Service.Configuration;
namespace UASigner.Service
{
    public class DummyService:Service
    {
        Task t;
        CancellationTokenSource tokenSource;
        CancellationToken cancelationToken;

        public DummyService(ServiceConfiguration config, IServiceController serviceController, IGenericProviderAction<IProfile> profileProvider)
            : base(config,serviceController, profileProvider)
        { }
        private void DoWork()
        {
            t = Task.Factory.StartNew(() =>
            {
                ServiceMessage message = new ServiceMessage { State = ServiceState.Started, Message = "Started" };
                foreach (var observer in observers)
                {
                    observer.OnNext(message);
                }

                while (true)
                {
                    System.Threading.Thread.Sleep(this.config.GetIdleTime());
                    
                    message = new ServiceMessage { State = ServiceState.Started, Message = "Working.." + DateTime.Now.ToString() };
                    foreach (var observer in observers)
                    {
                        observer.OnNext(message);
                    }
                    if (cancelationToken.IsCancellationRequested)
                    {
                       
                        tokenSource.Cancel();
                        break;
                    }

                   
                }

                message = new ServiceMessage { State = ServiceState.Stopped, Message = "Stopped" };
                foreach (var observer in observers)
                {
                    observer.OnNext(message);
                }
            });

        }
        public override void Start()
        {
            ServiceMessage message = new ServiceMessage { State = ServiceState.Starting,  Message ="Starting.."};
            foreach (var observer in observers)
            {
                observer.OnNext(message);
            }

            DoWork();

           
        }

        public override void Stop()
        {
            ServiceMessage message = new ServiceMessage { State = ServiceState.Stopping, Message = "Stopping.." };
            foreach (var observer in observers)
            {
                observer.OnNext(message);
            }

            tokenSource.Cancel();


        }
        public override void Init()
        {
            tokenSource = new CancellationTokenSource();
            cancelationToken = tokenSource.Token;
        }

    }
}
