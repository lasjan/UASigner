using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UASigner.WpfApp.Model;
using UASigner.Profiles;
using UASigner.Service;

namespace UASigner.WpfApp.ViewModel
{
    public class ServiceStateToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string param = String.Empty;
            if (parameter != null)
            {
                param = parameter.ToString().ToLower();
            }
            if (value is ServiceMessageModel)
            {
                switch ((value as ServiceMessageModel).State)
                {
                    case ServiceState.Starting:
                        return false;
                    case ServiceState.Stopping:
                        return false;
                    case ServiceState.Started:
                        if (param.Equals("start"))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    case ServiceState.Stopped:
                        if (param.Equals("start"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                }

            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
