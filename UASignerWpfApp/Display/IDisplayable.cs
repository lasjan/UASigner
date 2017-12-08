using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UASigner.WpfApp
{
    public interface IDisplayable
    {
         string Icon { get; }
         int Id { get;  }
         string DispName { get; }
         string ToolTip { get;  }
    }
}
