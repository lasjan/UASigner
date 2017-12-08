using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles.Configuration
{
    public static class ConfigurationManager
    {
        static object locker = new object();
        static Configuration config = null;
        static bool inited = false;
        public static void InitWithFile(string fileName)
        {
            config = new ConfigurationFile(fileName);
            inited = true;
        }
        public static void InitWithConnString(string connString)
        {
            throw new Exception("Currently not supported");
        }
        public static Configuration GetConfiguration()
        {
            if(!inited)
            {
                throw new Exception("Not initialized. Use one of Init options");
            }

            return config;
        }
    }
}
