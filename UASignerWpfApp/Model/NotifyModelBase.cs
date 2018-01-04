using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UASigner.WpfApp.Model.Validation;
using UASigner.WpfApp.Validation;
namespace UASigner.WpfApp.Model
{
    public class NotifyModelBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected Dictionary<string, List<string>> errors = new Dictionary<string, List<string>>();
        protected Dictionary<string, List<IPropertyValidator<NotifyModelBase>>> validators = new Dictionary<string, List<IPropertyValidator<NotifyModelBase>>>();
        protected bool isValid = false;
        bool validationReady = false;
        public bool ValidationReady 
        {
            get
            {
                return validationReady;
            }
            set
            {
                validationReady = value;
                foreach (var prop in this.GetType().GetProperties())
                {
                    if (prop != null)
                    {
                        var propValue = prop.GetValue(this);
                        if (propValue != null && propValue is NotifyModelBase)
                        {
                            ((NotifyModelBase)propValue).ValidationReady = true;
                        }
                    }
                }
            }
        }
       
        public bool IsValid 
        {
            get
            {
                bool valid = this.errors.Count == 0;
                foreach (var toV in ToValidateProperties())
                {
                    var p = this.GetType().GetProperty(toV);
                    if (p != null)
                    {
                        var v = p.GetValue(this);
                        if (v != null && v is NotifyModelBase)
                        {
                            valid &= (v as NotifyModelBase).IsValid;
                        }

                    }
                }
                return valid;
            }
        }
        protected NotifyModelBase()
        {
            ValidationReady = false;
        }
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                ValidateProperty(propertyName);
            }
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        protected void OnErrorChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            { 
                DataErrorsChangedEventArgs e = new DataErrorsChangedEventArgs(propertyName);
                ErrorsChanged(this, e);
            }
        }
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (!String.IsNullOrEmpty(propertyName) && this.errors.ContainsKey(propertyName))
            {
                return this.errors[propertyName];
            }
            return null;
        }

        public bool HasErrors
        {
            get { return ValidationReady && errors.Count!=0; }
        }

        public void ValidateAll()
        {
            var toV = ToValidateProperties();
            if (toV != null)
            {
                toV.ForEach(x => ValidateProperty(x));
            }
        }
        protected virtual List<string> ToValidateProperties()
        {
            return new List<string>();
        }
        public virtual void ValidateProperty (string propertyName)
        {
            if (errors.ContainsKey(propertyName))
            {
                errors.Remove(propertyName);
            }
            if (this.validators.ContainsKey(propertyName))
            {
                var v = validators[propertyName];
                if (v != null)
                {
                    v.ForEach(val =>
                    {

                        ValidationResult<NotifyModelBase> valRes = val.Validate(this, propertyName);

                        if (valRes != null)
                        {
                            if (!valRes.State)
                            {
                                if (!this.errors.ContainsKey(propertyName))
                                {
                                    this.errors.Add(propertyName, new List<string> { valRes.Message });
                                }
                                else
                                {
                                    this.errors[propertyName].Add(valRes.Message);
                                }

                            }
                        }
                    });
                }
            }

            //if (this.errors.ContainsKey(propertyName) && this.errors[propertyName] != null && this.errors[propertyName].Count != 0)
            {
                OnErrorChanged(propertyName);
            }

            object propValue = null;
            try
            {
                propValue = this.GetType().GetProperty(propertyName).GetValue(this);
                if (propValue is NotifyModelBase)
                {
                    ((NotifyModelBase)propValue).ValidateAll();
                }
            }
            catch { }
        }
    }
}
