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
    public class LocationTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is FileLocationAccessModel)
            {
                return @"/Resources/Images/loc_folder_ico.png";
            }
            else if (value is FtpLocationAccessModel)
            {
                return @"/Resources/Images/loc_ftp_ico.png";
            }

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
