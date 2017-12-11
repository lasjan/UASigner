using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.Profiles.Providers;
using UASigner.Profiles.Exceptions;
namespace UASigner.Profiles
{
    public enum PK_LOCATION { FILE};
    public abstract class PKInfo:IItem
    {
        public int? Id { get; set; }
        public string Alias { get; set; }
        public PK_LOCATION PkLocation { get;  set; }
        public string Password { get;  set; }

        public virtual void Validate()
        {
            if (!CheckPass())
            {
                throw new ConfigurationException(102, "Password invalid");
            }
            return;
        }

        protected virtual bool CheckPass()
        {
            return true;
        }
    }
}
