using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles;
using UASigner.Service.Configuration;
namespace UASigner.Profiles.Configuration
{
    public abstract class Configuration
    {
        public event EventHandler ConfigurationChanged;

        protected List<IProfile> profiles;
        protected List<PKInfo> pkInfos;
        protected List<TimeStampServerInfo> tsInfos;
        protected string defaultCertPath;

        protected ServiceConfiguration serviceConfig;
        public string GetDefaultCertPath()
        {
            return defaultCertPath;
        }
        public void SetDefaultCertPath(string certPath)
        {
            this.defaultCertPath = certPath;
            SyncSource();
            OnConfigurationChanged();
        }
        public List<IProfile> GetProfiles()
        {
            return profiles;
        }
        public void SetProfiles(List<IProfile> profiles)
        {
            this.profiles = profiles;
            SyncSource();
            OnConfigurationChanged();
        }

        public List<PKInfo> GetPkInfos()
        {
            return pkInfos;
        }
        public void SetPkInfos(List<PKInfo> pkInfos)
        {
            this.pkInfos = pkInfos;
            SyncSource();
            OnConfigurationChanged();
        }

        public List<TimeStampServerInfo> GetTimeStampServerInfos()
        {
            return tsInfos;
        }
        public void SetTimeStampServerInfos(List<TimeStampServerInfo> tsInfos)
        {
            this.tsInfos = tsInfos;
            SyncSource();
            OnConfigurationChanged();
        }


        private void OnConfigurationChanged()
        {
            if (ConfigurationChanged != null)
            {
                ConfigurationChanged(this, null);
            }
        }

        public ServiceConfiguration GetServiceConfiguration()
        {
            return this.serviceConfig;
        }
        public void SetServiceConfiguration(ServiceConfiguration config)
        {
            this.serviceConfig = config;
            SyncSource();
            OnConfigurationChanged();
        }
        protected abstract void SyncSource();

        public virtual void Validate()
        {
            return;
        }
    }
}
