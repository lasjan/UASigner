using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using UASigner.WpfApp.Model.Validation;
using UASigner.WpfApp.Validation;
namespace UASigner.WpfApp.Model
{
    public class PkInfoModel : NotifyModelBase, IModelItem, ICheckBoxWrappable
    {
        string alias;
        string password;
        public bool IsValidating = false;

        public int? Id { get; set; }
        public string Alias
        {
            get { return alias; }
            set { this.alias = value; OnPropertyChanged(); }
        }
        public string Password
        {
            get { return password; }
            set { this.password = value; OnPropertyChanged(); }
        }

        public PkInfoModel()
            : base()
        {
            this.validators.Add("Alias", new List<IPropertyValidator<NotifyModelBase>> { new StringNotEmptyValidator<NotifyModelBase>() });
            this.validators.Add("Password", new List<IPropertyValidator<NotifyModelBase>> { new StringNotEmptyValidator<NotifyModelBase>() });
        }
        protected override List<string> ToValidateProperties()
        {
            return new List<string> { "Alias", "Password" };
        }

        public string ExposedDisplayName
        {
            get { return Alias; }
        }
    }
}
