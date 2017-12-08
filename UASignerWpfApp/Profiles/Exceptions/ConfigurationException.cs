using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles.Exceptions
{
    public class ConfigurationException:Exception
    {
        int code;
        string[] parameters;
        public string DictEntryKey
        {
            get 
            {
                return "err_conf_" + code.ToString();
            }
        }
        public string[] Parameters
        {
            get
            {
                return parameters;
            }
        }


        public ConfigurationException(int code, string exception, params string[] parameters)
            : base(exception)
        {
            this.code = code;
            this.parameters = parameters;
        }
    }
    /*
     Codes Lookup 
     0 - pk info z aliasem juz istnieje
     */
}
