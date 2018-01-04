using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Configuration;
namespace UASigner.Profiles.Providers
{
    public class RootFolderProvider :IObservable, IGenericPropertyProvider<string> 
    {
        UASigner.Profiles.Configuration.Configuration config;
        List<IObserver> observers;
        public RootFolderProvider(UASigner.Profiles.Configuration.Configuration config)
        {
            this.config = config;
            observers = new List<IObserver>();
        }
        public string Get()
        {
            return config.GetDefaultCertPath();
        }

        public void Set(string property)
        {
            config.SetDefaultCertPath(property);
            observers.ForEach(x => x.Observe(ActionType.Edit, this));
        }

        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }
    }
}
