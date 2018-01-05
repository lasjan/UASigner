using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using UASigner.WpfApp.Model;
namespace UASigner.WpfApp.Controls
{
    /// <summary>
    /// Interaction logic for CertViewer.xaml
    /// </summary>
    public partial class CertViewer : UserControl
    {
        public static readonly DependencyProperty CertificateCollectionProperty = DependencyProperty.Register("CertificateCollection",
            typeof(ObservableCollection<CertificateModel>),
            typeof(CertViewer),
            new PropertyMetadata(null));

        public static readonly DependencyProperty TestProperty = DependencyProperty.Register("Test",
            typeof(string),
            typeof(CertViewer),
            new PropertyMetadata("elo"));


        public ObservableCollection<CertificateModel> CertificateCollection
        {
            get { return (ObservableCollection<CertificateModel>)GetValue(CertificateCollectionProperty); }
            set { SetValue(CertificateCollectionProperty, value); }
        }
        public string Test
        {
            get { return (string)GetValue(TestProperty); }
            set { SetValue(TestProperty, value); }
        }
        public CertViewer()
        {
            InitializeComponent();
        }
    }
}
