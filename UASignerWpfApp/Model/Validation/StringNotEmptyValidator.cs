using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Validation;
namespace UASigner.WpfApp.Model.Validation
{
    public class StringNotEmptyValidator<T> : IPropertyValidator<T>
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
            object propValue = null;
            try
            {
                propValue = toValidate.GetType().GetProperty(propName).GetValue(toValidate).ToString();
            }
            catch { }
            string propValueS = Convert.ToString(propValue, null);
            valid = !String.IsNullOrEmpty(propValueS);
 
            return valid;
        }

        public string CreateMessage()
        {
            if (valid) { return null; }

            return String.Format("{0} cant be empty",propName);
        }
    }
}
