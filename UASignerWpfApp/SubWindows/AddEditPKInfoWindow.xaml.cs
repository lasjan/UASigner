using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UASigner.Profiles;
using Microsoft.Win32;
using System.IO;
using UASigner.Profiles.Exceptions;
using UASigner.Profiles.Configuration;
using UASigner.Profiles.Providers;
using UASigner.WpfApp;
namespace UASigner.WpfApp.SubWindows
{
    public enum Mode { Edit,Add};
    /// <summary>
    /// Interaction logic for AddEditPKInfoWindow.xaml
    /// </summary>
    public partial class AddEditPKInfoWindow : Window
    {
        Mode workMode = Mode.Add;

        IGenericProviderAction<PKInfo> pkInfoProvider;
        Configuration config;

        public event AddEditPkInfoDelegate AddEdit;

        public AddEditPKInfoWindow(PKInfo pkinfo)
        {
            if (pkinfo != null)
            {
                workMode = Mode.Edit;
            }
            InitializeComponent();
            Init();
        }
        protected void OnAddEdit(PKFileInfo newPk)
        {
            if (this.AddEdit != null)
            {
                var ea = new AddEditPKInfoEventArgs() { PkInfoToAdd = newPk };
                this.AddEdit(this, ea);
            }
        }
        private void Init()
        {
            ((App)Application.Current).OnLanguageChange();
            this.config = ConfigurationManager.GetConfiguration();
            pkInfoProvider = new PkInfoProvider(config);
        }
        private void btn_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                textbox_Location.Text = openFileDialog.FileName;
            }
        }

        private void btn_AddEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PKFileInfo pkFileInfo = new PKFileInfo { Alias = textbox_Alias.Text, PkFilePath = textbox_Location.Text, Password = textbox_Password.Password.ToString() };
                if (this.pkInfoProvider.Get().Any(x => x.Alias.Equals(pkFileInfo.Alias, StringComparison.InvariantCultureIgnoreCase)))
                {
                    throw new ConfigurationException(0, "This alias already exists", pkFileInfo.Alias);
                }
                OnAddEdit(pkFileInfo);

                this.Close();
            }
            catch (Exception ex)
            {
                ExceptionHandler(ex);
            }
        }

        void ExceptionHandler(Exception ex)
        {
            string msg = ResourceManager.BuildExceptionMessage(ex);
            MessageBox.Show(msg, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
