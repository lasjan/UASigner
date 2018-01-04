using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Exceptions;
namespace UASigner.WpfApp.Exceptions
{
    public class ValidateException:Exception,IVisualException
    {
        string[] parameters;
        string dicyEntryKey;
        public string DictEntryKey
        {
            get 
            {
                return dicyEntryKey;
            }
        }
        public string[] Parameters
        {
            get
            {
                return parameters;
            }
        }


        public ValidateException(string dicyEntryKey, string exception, params string[] parameters)
            : base(exception)
        {
            this.parameters = parameters;
            this.dicyEntryKey = dicyEntryKey;
        }
        public ValidateException(Exception innerEx, string dicyEntryKey, string exception, params string[] parameters)
            : base(exception,innerEx)
        {
            this.parameters = parameters;
            this.dicyEntryKey = dicyEntryKey;
        }
    }
}
