using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Validation;

namespace UASigner.WpfApp.Model.Validation
{
    public class NotEmptyListValidator<T,K> : IPropertyValidator<T>
    {
        protected T toValidate;
        protected string propName;
        protected bool valid = true;

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
                propValue = toValidate.GetType().GetProperty(propName).GetValue(toValidate);
                if (propValue == null) { valid = false; }

                if(propValue!=null && propValue is IEnumerable<K>)
                {
                    valid = ((propValue as IEnumerable<K>).Any());

                }
            }
            catch { }

            return valid;
        }

        public virtual string CreateMessage()
        {
            if (valid) { return null; }

            return String.Format("Collection of {0} can't be empty", propName);
        }
    
    }
}
