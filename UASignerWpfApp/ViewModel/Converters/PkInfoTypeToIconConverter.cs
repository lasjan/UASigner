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
    class PkInfoTypeToIconConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is PkFileInfoModel)
            {
                return @"/Resources/Images/file_key_ico.png";
            }
            else if (value is PkUsbStickPkInfoModel)
            {
                return @"/Resources/Images/usb_key_ico.png";
            }
            
            return String.Empty ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
