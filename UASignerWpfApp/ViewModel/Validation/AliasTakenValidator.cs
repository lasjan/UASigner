using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Validation;
using UASigner.WpfApp.Providers;
using UASigner.WpfApp.Model;
namespace UASigner.WpfApp.ViewModel.Validation
{
    class AliasTakenValidator<T> : IPropertyValidator<T>
    {
        T toValidate;
        string propName;
        string alias;
        bool valid = true;
        IModelGenericProviderAction<PkInfoModel> modelPkInfoProvider;

        public AliasTakenValidator(IModelGenericProviderAction<PkInfoModel> modelPkInfoProvider)
        {
            this.modelPkInfoProvider = modelPkInfoProvider;
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
            object propValue = null;
            try
            {
                propValue = toValidate.GetType().GetProperty(propName).GetValue(toValidate);
            }
            catch { }
            if (propValue!=null && propValue is PkInfoModel)
            {
                alias = (propValue as PkInfoModel).Alias;
                valid = !modelPkInfoProvider.Get().Any(x => x.Alias == alias);
            }

 
            return valid;
        }

        public string CreateMessage()
        {
            if (valid) { return null; }

            return String.Format("Alias {0} is already taken", alias);
        }
    
    }
}
