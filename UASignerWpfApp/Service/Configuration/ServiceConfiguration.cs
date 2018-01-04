using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Service.Configuration
{
    public class ServiceConfiguration
    {
        public Dictionary<string, string> ConfigDict {  get; private set; }
   
        public event EventHandler ConfigurationChanged;

        public ServiceConfiguration()
        {
            ConfigDict = new Dictionary<string, string>();
        }
        public int GetIdleTime()
        {
            if (ConfigDict.ContainsKey("idleTime"))
            {
                return Int32.Parse(ConfigDict["idleTime"]);
            }
            return 1000; 
        }
        public void SetIdleTime(int idleTimeInMs)
        {
            if (!ConfigDict.ContainsKey("idleTime"))
            {
                ConfigDict["idleTime"] = String.Empty;
            }
            ConfigDict["idleTime"] = idleTimeInMs.ToString();

            OnConfigurationChanged();
        }

        private void OnConfigurationChanged()
        {
            if (ConfigurationChanged != null)
            {
                ConfigurationChanged(this, null);
            }
        }
    }
}
