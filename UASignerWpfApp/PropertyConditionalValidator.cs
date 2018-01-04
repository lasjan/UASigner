using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Validation
{
    public class PropertyConditionalValidator<T>:IPropertyValidator<T>
    {
        T toValidate;
        string propName;

        string conditionalPropertyName;
        List<object> conditionalPropetyValues;
        List<IPropertyValidator<T>> validators;

        List<ValidationResult<T>> vrList = new List<ValidationResult<T>>();

        public PropertyConditionalValidator(string conditionalPropertyName, List<object> conditionalPropetyValues,List<IPropertyValidator<T>> validators)
        {
            this.conditionalPropertyName = conditionalPropertyName;
            this.conditionalPropetyValues = conditionalPropetyValues;
            this.validators = validators;

            if (conditionalPropertyName == null)
            {
                throw new ArgumentNullException("conditionalPropertyName");
            }
            if (validators == null)
            {
                throw new ArgumentNullException("validators");
            }
        }
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
            vrList.Clear();
            object conditionalPropValue = null;
            bool fireValidator = false;
            var pinfo = toValidate.GetType().GetProperty(conditionalPropertyName);
            if (pinfo != null)
            {
                conditionalPropValue = pinfo.GetValue(toValidate);

                fireValidator = conditionalPropetyValues.Any(x => x.Equals(conditionalPropValue));
            }

            if (!fireValidator) return true;

            foreach (var v in validators)
            {
                var vr = v.Validate(toValidate, propName);
                vrList.Add(vr);
            }

            var firstEr = vrList.FirstOrDefault(x=>!x.State);

            if (firstEr == null) return true;

            return firstEr.State;
        }

        public string CreateMessage()
        {
            var firstEr = vrList.FirstOrDefault(x => !x.State);

            if (firstEr != null)
            {
                return firstEr.Message;
            }

            return null;
        }
    }
}
