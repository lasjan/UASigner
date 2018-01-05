using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Exceptions;
namespace UASigner.Profiles.Exceptions
{
    public class ConfigurationException : Exception, IVisualException
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
        public ConfigurationException(Exception innerEx,int code, string exception, params string[] parameters)
            : base(exception,innerEx)
        {
            this.code = code;
            this.parameters = parameters;
        }
    }
    /*
     Codes Lookup 
     * 1 - fodler nie istnieje
     * 2 - backup tylko na lokalny storze
     * 101 - pk info z aliasem juz istnieje
     * 102 - haslo sie nie zgadza
     
     */
}
