﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using UASigner.Profiles;
using UASigner.WpfApp.Display;
using UASigner.Profiles.Providers;
using UASigner.Profiles.Configuration;
using UASigner.WpfApp.SubWindows;
using UASigner.WpfApp.Providers;
using UASigner.WpfApp.Model;
namespace UASigner.WpfApp
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //List<ILocationDisplay> list = new List<ILocationDisplay>();
        IGenericProviderAction<IProfile> profileProvider;
        IGenericProviderAction<PKInfo> pkInfoProvider;
        IModelGenericProviderAction<PkInfoModel> pkInfoModelprovider;
        Configuration config;
        public MainWindow()
        {
            InitializeComponent();
            Init();  
        }

        void Init()
        {
            this.config = ConfigurationManager.GetConfiguration();
            this.config.Validate();
            profileProvider = new ProfileProvider(config);
            pkInfoProvider = new PkInfoProvider(config);
            pkInfoProvider.AddObserver(profileProvider);

            pkInfoModelprovider = new ModelPkInfoProvider(pkInfoProvider);

            var test = pkInfoModelprovider.Get().ToList();
            
            //var firstPk = pkInfoProvider.Get().First();
            //firstPk.Alias = firstPk.Alias + "_changed";
            //pkInfoProvider.Edit(firstPk);

            var pppp = profileProvider.Get();
            ComboBox_Language.ItemsSource = ResourceManager.GetCultureFlags();
            ComboBox_Language.SelectedValuePath = "Culture";

            var o = new UASigner.WpfApp.View.AddPkInfoView();

            var vm = new UASigner.WpfApp.ViewModel.AddPkInfoViewModel(pkInfoModelprovider, null);

            o.DataContext = vm;
            o.Show();
            Display();
        }

        void addProfile_MyEvent(object sender, EventArgs e)
        {
            Display();
            this.YourListBox.Items.Refresh();
        }

        private void Display()
        {
            var profiles = profileProvider.Get();
            var pkinfos = pkInfoProvider.Get();
            List<IDisplayable> displayableProfiles = new List<IDisplayable>();
            foreach (var profile in profiles)
            {
                DisplayableProfile displayableProfile = new DisplayableProfile(profile);
                displayableProfiles.Add(displayableProfile);
            }
            List<IDisplayable> displayablePkInfos= new List<IDisplayable>();

            foreach (var pkinfo in pkinfos)
            {
                DisplayablePkInfo displayablePkInfo = new DisplayablePkInfo(pkinfo);
                displayablePkInfos.Add(displayablePkInfo);
            }

            this.PKListBox.ItemsSource = displayablePkInfos;
            this.YourListBox.ItemsSource = displayableProfiles;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddEditProfileWindow addProfile = new AddEditProfileWindow(null);
            addProfile.MyEvent += addProfile_MyEvent;
            addProfile.Show();

        }

        void addProfile_MyEvent(object sender, AddEditProfileEventArgs args)
        {
            var profileToAddEdit = args.ProfileToAdd;
            if (args.WorkMode == WorkMode.Add)
            {
                profileProvider.Add(profileToAddEdit);
            }
            if (args.WorkMode == WorkMode.Edit)
            {
                profileProvider.Edit(profileToAddEdit);
            }
            Display();
            this.YourListBox.Items.Refresh();

        }

        private void ButtonAdd_PKInfo_Click(object sender, RoutedEventArgs e)
        {
            AddEditPKInfoWindow addPkInfo = new AddEditPKInfoWindow(null);
            addPkInfo.AddEdit += addPkInfo_AddEdit;
            addPkInfo.Show();
        }

        void addPkInfo_AddEdit(object sender, AddEditPKInfoEventArgs args)
        {
            var pkToAdd = args.PkInfoToAdd;
            pkInfoProvider.Add(pkToAdd);
            Display();
            this.PKListBox.Items.Refresh();
        }


        private void ComboBox_Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var rr = (e.AddedItems[0] as CultureFlag);
                ResourceManager.SetCulture(rr.Culture);

            }
            ((App)Application.Current).OnLanguageChange();
        }

        private void MenuItem_ClickEdit(object sender, RoutedEventArgs e)
        {
            var commandParam = ((System.Windows.Controls.MenuItem)(sender)).CommandParameter;
            int profileId = (int)commandParam;

            var toEdit = this.profileProvider.Get().First(x => x.Id == profileId);

            AddEditProfileWindow addProfile = new AddEditProfileWindow(toEdit);
            addProfile.MyEvent += addProfile_MyEvent;
            addProfile.Show();
        }
        private void MenuItem_ClickRemove(object sender, RoutedEventArgs e)
        {
            var commandParam = ((System.Windows.Controls.MenuItem)(sender)).CommandParameter;
            int profileId = (int)commandParam;

            var toRemove = this.profileProvider.Get().First(x => x.Id == profileId);

            this.profileProvider.Delete(toRemove);

            Display();
        }
      
    }
}
