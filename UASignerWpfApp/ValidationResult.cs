using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Validation
{
    public class ValidationResult<T>
    {
        public bool State { get; private set; }
        public string Message { get; private set; }

        public void AcceptValidator(IPropertyValidator<T> validator)
        {
            this.State = validator.CalculateState();
            this.Message = validator.CreateMessage();
        }
    }
}
