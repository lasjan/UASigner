using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp.Display
{
    public interface IDisplayableFactory
    {
        IDisplayable CreateDisplayable(object o);
    }
}
