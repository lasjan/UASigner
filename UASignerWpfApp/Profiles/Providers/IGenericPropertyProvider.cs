using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.Profiles.Providers
{
    public interface IGenericPropertyProvider <T>
    {
        T Get();
        void Set(T property);
    }
}
