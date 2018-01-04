using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.ViewModel;
namespace UASigner.WpfApp.ViewModel.Controls
{
    public class FileSelectorViewModel:ViewModelBase
    {
        string filePath;

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; OnPropertyChanged(); }
        }
    }
}
