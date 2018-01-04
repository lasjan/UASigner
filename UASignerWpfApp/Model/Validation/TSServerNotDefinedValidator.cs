using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Model.Validation
{
    public class TSServerNotDefinedValidator<T>:PropertyNotNullValidator<T>
    {
        public override string CreateMessage()
        {
            if (valid) { return null; }

            return "Time stamp server has to be defined when using T,C,XL profiles or adding ContentTimeStamp";
        }
    }
}
