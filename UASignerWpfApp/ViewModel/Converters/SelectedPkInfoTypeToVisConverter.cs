using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using UASigner.WpfApp.Model;
using UASigner.Profiles;
namespace UASigner.WpfApp.ViewModel
{
    class SelectedPkInfoTypeToVisConverter:IMultiValueConverter
    {
        System.Windows.Visibility defVis = System.Windows.Visibility.Collapsed;
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var selected = ((UASigner.WpfApp.ViewModel.AddPkInfoViewModel)(value)).SelectedPkType;
            if (parameter is PK_LOCATION)
            { 
                if(((PK_LOCATION)parameter) == selected.Location)
                {
                    return System.Windows.Visibility.Visible;
                }
            }

            return defVis;
            
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return "";

        }

        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (values != null && values.Length > 0)
            {
                if (values[0] != null)
                {
                    var selected = values[0] as PkTypeModel;
                    if (parameter is PK_LOCATION)
                    {
                        if (((PK_LOCATION)parameter) == selected.Location)
                        {
                            return System.Windows.Visibility.Visible;
                        }
                    }
                }
            }
            return defVis;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
