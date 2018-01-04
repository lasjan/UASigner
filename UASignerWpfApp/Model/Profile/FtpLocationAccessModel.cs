using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model.Validation;
using UASigner.WpfApp.Validation;
namespace UASigner.WpfApp.Model
{
    class FtpLocationAccessModel:LocationAccessModel
    {
        string address;
        public string Address
        {
            get { return address; }
            set 
            { 
                this.address = value;
                this.DisplayName = String.Format("{0}[{1}]", this.Address, this.Port);
                OnPropertyChanged();
                OnPropertyChanged("DisplayName");  
            }
        }
        int? port;
        public int? Port
        {
            get { return port; }
            set 
            { 
                this.port = value; OnPropertyChanged();
                this.DisplayName = String.Format("{0}[{1}]", this.Address, this.Port);
                OnPropertyChanged();
                OnPropertyChanged("DisplayName");  
            
            }
        }
        public FtpLocationAccessModel()
            : base()
        {
            this.validators.Add("Port", new List<IPropertyValidator<NotifyModelBase>> { 
                new StringNotEmptyValidator<NotifyModelBase>()
            });
            this.validators.Add("Address", new List<IPropertyValidator<NotifyModelBase>> { 
                new StringNotEmptyValidator<NotifyModelBase>()
            });

        }
        protected override List<string> ToValidateProperties()
        {
            List<string> l = new List<string> { "Address","Port" };
            l.AddRange(base.ToValidateProperties());
            return l;
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
