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
using UASigner.WpfApp.Display;
using UASigner.Profiles.Providers;
using UASigner.Profiles.Configuration;
namespace UASigner.WpfApp.SubWindows
{
    /// <summary>
    /// Interaction logic for AddProfileWindow.xaml
    /// </summary>
    public partial class AddEditProfileWindow : Window
    {
        public event AddEditProfileDelegate MyEvent;
        IProfile profile;
        Configuration cfg = ConfigurationManager.GetConfiguration();
        protected void OnAddEdit(DirectoryProfile newDp)
        {
            if (this.MyEvent != null)
            {
                var ea = new AddEditProfileEventArgs() { ProfileToAdd = newDp };
                this.MyEvent(this, ea);
            }
        }

        public AddEditProfileWindow(IProfile editProfile)
        {
            InitializeComponent();
            if (editProfile != null)
            {
                profile = editProfile;
            }
            Init();
        }
        private void Init()
        {
           
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SignContextProfile context = new SignContextProfile();
            context.KeysInfo.Add(cfg.GetPkInfos().First());
            DirectoryProfile dp3 = new DirectoryProfile
            (
            null, new DirectoryAccess(@"E:\UA\Processor\IN\PARTNER_4", "xml"), new List<DirectoryAccess> { new DirectoryAccess(@"E:\UA\Processor\OUT\PARTNER_4", "xml") }, context
            );
            OnAddEdit(dp3);
            this.Close();
        }

        private void ComboBoxEdit_InAccess_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
