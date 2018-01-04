using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Model.Validation;
using UASigner.WpfApp.Validation;
namespace UASigner.WpfApp.Model
{
    class FileLocationAccessModel:LocationAccessModel
    {
        string path;
        public string Path
        {
            get { return path; }
            set 
            {
                this.path = value;
                this.DisplayName = String.Format("{0}[{1}]", this.Path, this.FileMask);
                OnPropertyChanged();
                OnPropertyChanged("DisplayName");  
            }
        }
        string fileMask;
        public string FileMask
        {
            get { return fileMask; }
            set 
            { 
                this.fileMask = value;
                this.DisplayName = String.Format("{0}[{1}]", this.Path, this.FileMask);
                OnPropertyChanged();
                OnPropertyChanged("DisplayName"); 
            }
        }


        public FileLocationAccessModel()
            : base()
        {
            this.validators.Add("Path", new List<IPropertyValidator<NotifyModelBase>> { 
                new StringNotEmptyValidator<NotifyModelBase>(),
                new DirectoryNotExistsValidator<NotifyModelBase>()
            
            });

        }
        protected override List<string> ToValidateProperties()
        {
            List<string> l = new List<string> { "Path" };
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
