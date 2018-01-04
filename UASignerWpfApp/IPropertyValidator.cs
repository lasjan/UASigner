using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Validation
{
    public interface IPropertyValidator<T>
    {
        ValidationResult<T> Validate(T toValidate, string propertyName);
        bool CalculateState();
        string CreateMessage();
    }
}
