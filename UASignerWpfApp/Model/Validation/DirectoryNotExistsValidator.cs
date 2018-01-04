using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using UASigner.WpfApp.Validation;
namespace UASigner.WpfApp.Model.Validation
{
    public class DirectoryNotExistsValidator<T> : IPropertyValidator<T>
    {
        T toValidate;
        string propName;
        bool valid = true;
        public ValidationResult<T> Validate(T toValidate, string propertyName)
        {
            this.toValidate = toValidate;
            this.propName = propertyName;

            ValidationResult<T> vr = new ValidationResult<T>();

            vr.AcceptValidator(this);

            return vr;
        }

        public bool CalculateState()
        {
            string propValue = null;
            try
            {
                propValue = toValidate.GetType().GetProperty(propName).GetValue(toValidate).ToString();
            }
            catch { }

            valid = Directory.Exists(propValue);

            return valid;
        }

        public string CreateMessage()
        {
            if (valid) { return null; }

            return String.Format("Directory under {0} does not exists", propName);
        }
    }
}
