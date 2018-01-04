using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model.Validation;
using UASigner.WpfApp.Validation;
namespace UASigner.WpfApp.Model
{
    class PkFileInfoModel:PkInfoModel
    {
        string location;
        public string Location 
        {
            get { return location; }
            set { this.location = value; OnPropertyChanged(); }
        }
        protected override List<string> ToValidateProperties()
        {
            List<string> l = new List<string> { "Location" };
            l.AddRange(base.ToValidateProperties());
            return l;
        }

        public PkFileInfoModel()
            : base()
        {
            this.validators.Add("Location", new List<IPropertyValidator<NotifyModelBase>> { 
                new StringNotEmptyValidator<NotifyModelBase>(),
                new FileNotExistsValidator<NotifyModelBase>()
            
            });

        }
    }
}
