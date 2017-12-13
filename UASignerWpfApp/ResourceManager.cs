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

        public static List<DisplayableCadesProfile> GetCadesProfiles()
        {
            List<DisplayableCadesProfile> list = new List<DisplayableCadesProfile>();
            list.Add(new DisplayableCadesProfile { Id = 0, SignLevel = Profiles.SignatureLevel.CADES_BASELINE_B, DispName = GetStringResource("cblistitem_cadesB", "CAdES B"),
                                                   ToolTip = GetStringResource("toolitip_cadesB", "CAdES B desc")
            });
            list.Add(new DisplayableCadesProfile
            {
                Id = 1,
                SignLevel = Profiles.SignatureLevel.CADES_BASELINE_T,
                DispName = GetStringResource("cblistitem_cadesT", "CAdES T"),
                ToolTip = GetStringResource("toolitip_cadesT", "CAdES T desc")
            });
            list.Add(new DisplayableCadesProfile
            {
                Id = 2,
                SignLevel = Profiles.SignatureLevel.CADES_101733_C,
                DispName = GetStringResource("cblistitem_cadesC", "CAdES C"),
                ToolTip = GetStringResource("toolitip_cadesC", "CAdES C desc")
            });
            list.Add(new DisplayableCadesProfile
            {
                Id = 3,
                SignLevel = Profiles.SignatureLevel.CADES_101733_X_LONG,
                DispName = GetStringResource("cblistitem_cadesXL", "CAdES XL"),
                ToolTip = GetStringResource("toolitip_cadesXL", "CAdES XL desc")
            });

            return list;
        }
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
