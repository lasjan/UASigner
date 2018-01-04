using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model.Validation;
using UASigner.WpfApp.Validation;
namespace UASigner.WpfApp.Model
{
    public class RootFolderModel:NotifyModelBase
    {
        string rootPath;
        public string RootPath 
        {
            get
            {
                return rootPath;
            }
            set
            {
                rootPath = value;
                OnPropertyChanged();
            }
        }

        public RootFolderModel():base()
        {
            this.validators.Add("RootPath", new List<IPropertyValidator<NotifyModelBase>> { 
                new StringNotEmptyValidator<NotifyModelBase>(),
                new DirectoryNotExistsValidator<NotifyModelBase>()
            
            });
        }
        protected override List<string> ToValidateProperties()
        {
            List<string> l = new List<string> { "RootPath" };
            l.AddRange(base.ToValidateProperties());
            return l;
        }
    }
}
