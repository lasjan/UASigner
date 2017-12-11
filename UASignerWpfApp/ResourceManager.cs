using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UASigner.WpfApp.Display;
using System.Globalization;
using UASigner.Profiles.Exceptions;
namespace UASigner.WpfApp
{
    public class ResourceManager
    {
        static List<CultureFlag> cultureFlags;
        public static string ResourceNs { get; set; }

        public static List<DisplayableAccessType> GetLocationTypes()
        {
            List<DisplayableAccessType> list = new List<DisplayableAccessType>();
            list.Add(new DisplayableAccessType{AccessType = Profiles.AccessType.Directory, DispName = GetStringResource("cblistitem_directory","directory")});
            list.Add(new DisplayableAccessType { AccessType = Profiles.AccessType.Ftp, DispName = GetStringResource("cblistitem_ftp", "Ftp") });
            return list;
        }
        public static List<CultureFlag> GetCultureFlags()
        {
            if (cultureFlags == null)
            {
                cultureFlags = LoadFlags();
            }
            return cultureFlags;
        }
        static List<CultureFlag> LoadFlags()
        {
            var list = new List<CultureFlag> { };

            CultureFlag t1 = new CultureFlag { Culture = "uk-UA", Icon = @"/Resources/Images/ua_flag_ico.png" };
            CultureFlag t2 = new CultureFlag { Culture = "en-US", Icon = @"/Resources/Images/uk_flag_ico.png" };
            CultureFlag t3 = new CultureFlag { Culture = "pl-PL", Icon = @"/Resources/Images/pl_flag_ico.png" };
            list.Add(t1);
            list.Add(t2);
            list.Add(t3);
            return list;
        }
        public static void SetCulture(string culture)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(culture);
            CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(culture);
        }

        static string GetStringResource(string name,string defaultName)
        { 
            var rm  = new System.Resources.ResourceManager(ResourceNs, System.Reflection.Assembly.GetExecutingAssembly());
            return rm.GetString(name) ?? defaultName;
        }
        public static string BuildExceptionMessage(Exception ex)
        {
            string msg = ex.Message;
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager(ResourceNs, System.Reflection.Assembly.GetExecutingAssembly());
            if (ex is ConfigurationException)
            {
                try
                {
                    msg = String.Format(rm.GetString((ex as ConfigurationException).DictEntryKey), (ex as ConfigurationException).Parameters);
                }
                catch { }
                
            }
            return msg;
        }
        public static System.Resources.ResourceManager GetResourceManager()
        { 
            return  new System.Resources.ResourceManager(ResourceNs, System.Reflection.Assembly.GetExecutingAssembly());
        }
    }
}
