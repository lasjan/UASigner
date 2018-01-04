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
    class PasswordInvalidValidator<T> : IPropertyValidator<T>
    {
        public ValidationResult<T> Validate(T toValidate, string propertyName)
        {
            throw new NotImplementedException();
        }

        public bool CalculateState()
        {
            throw new NotImplementedException();
        }

        public string CreateMessage()
        {
            throw new NotImplementedException();
        }
    }
}
