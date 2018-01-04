using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model.Validation;
using UASigner.WpfApp.Validation;
namespace UASigner.WpfApp.Model
{
    class PkUsbStickPkInfoModel:PkInfoModel
    {
        string vendorDllPath;
        int slotId;
        public string VendorDllPath 
        {
            get { return vendorDllPath; }
            set { this.vendorDllPath = value; OnPropertyChanged(); }
        }

        public int SlotId
        {
            get { return slotId; }
            set { this.slotId = value; OnPropertyChanged(); }
        }
        protected override List<string> ToValidateProperties()
        {
            List<string> l = new List<string> { "VendorDllPath" };
            l.AddRange(base.ToValidateProperties());
            return l;
        }
        public PkUsbStickPkInfoModel()
            : base()
        {
            this.validators.Add("VendorDllPath", new List<IPropertyValidator<NotifyModelBase>> { new StringNotEmptyValidator<NotifyModelBase>() });
        }
    }
}
