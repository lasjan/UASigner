using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using UASigner.Profiles.Utils;
using System.Text;

namespace UASigner.Profiles.Configuration
{
    
    public class ConfigurationFile:Configuration
    {
        string xmlFileContent;
        string filePath;
        CFConfig parsedConfig;
        public ConfigurationFile(string filePath)
        {
            if(!File.Exists(filePath))
            {
                throw new IOException("File does not exists!");
            }
            this.filePath = filePath;
            this.xmlFileContent = File.ReadAllText(filePath);
            parsedConfig = Serialization.DeserializeToObject<CFConfig>(xmlFileContent);

            BuildConfiguration();
        }

        void BuildConfiguration()
        {
            this.profiles = new List<IProfile>();
            this.pkInfos = new List<PKInfo>();
            this.tsInfos = new List<TimeStampServerInfo>();

            this.defaultCertPath = parsedConfig.CertPath;
            foreach (var cfKeyInfo in parsedConfig.CFPKInfos)
            {
                if (cfKeyInfo.Type.Equals("file",StringComparison.InvariantCultureIgnoreCase))
                {
                    PKFileInfo pkfileInfo = new PKFileInfo();
                    pkfileInfo.Id = cfKeyInfo.Id;
                    var parameteres = cfKeyInfo.Parameters;
                    pkfileInfo.Alias = parameteres["alias"];
                    pkfileInfo.Password =  Encoding.UTF8.GetString(Convert.FromBase64String(parameteres["password"]));
                    pkfileInfo.PkFilePath = parameteres["filePath"];

                    pkInfos.Add(pkfileInfo);
                }
            }
            foreach (var cfTssinfo in parsedConfig.CFTSInfos)
            {
                TimeStampServerInfo tsinfo = new TimeStampServerInfo { Id = cfTssinfo.Id, Url = cfTssinfo.Url, Port = cfTssinfo.Port };
                tsInfos.Add(tsinfo);
            }
            foreach (var cfProile in parsedConfig.Profiles)
            {
                GenericProfile profile = new GenericProfile();
                var id = cfProile.Id;
                profile.Id = id;
                var inputAccess = cfProile.InputLocationAccess;

                if (inputAccess.Type.Equals("directory", StringComparison.InvariantCultureIgnoreCase))
                { 
                    var dirPath = inputAccess.Parameters["dirPath"];
                    var fileMask = inputAccess.Parameters["fileMask"];
                    DirectoryAccess inDirAccess = new DirectoryAccess(dirPath, fileMask);
                    profile.InLocationAccess = inDirAccess;
                    
                }
                List<LocationAccess> outAccess = new List<LocationAccess>();

                var outputAccessList = cfProile.OutputLocationsAccess;
                foreach (var outputAccess in cfProile.OutputLocationsAccess)
                {
                    if (outputAccess.Type.Equals("directory", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var dirPath = outputAccess.Parameters["dirPath"];
                        var fileMask = outputAccess.Parameters["fileMask"];
                        DirectoryAccess outDirAccess = new DirectoryAccess(dirPath, fileMask);
                        outAccess.Add(outDirAccess);
                    }
                }
                profile.OutLocationAccess = outAccess;
                SignContextProfile signProfile = new SignContextProfile() ;
                foreach (var keyId in cfProile.SignContext.CFPKInfosId)
                {
                    var pkinfo = this.pkInfos.Where(x => x.Id == keyId).First();
                    signProfile.KeysInfo.Add(pkinfo);
                }

                if (cfProile.SignContext.TsId != null)
                {
                    if (this.tsInfos.Any(x => ((int)x.Id) == (int)cfProile.SignContext.TsId))
                    {
                        var ts = this.tsInfos.First(x => ((int)x.Id) == (int)cfProile.SignContext.TsId);
                        signProfile.Tsinfo = ts;
                    }
                }

                signProfile.CertificatePath = cfProile.SignContext.CertPath ?? parsedConfig.CertPath;
                profile.SignProfile = signProfile; 
                
                profiles.Add(profile);
            }

            
        }

        protected override void SyncSource()
        {
            CFConfig cfConfig = new CFConfig();
            cfConfig.Profiles = new List<CFProfile>();
            cfConfig.CFPKInfos = new List<CFPKInfo>();
            cfConfig.CFTSInfos = new List<CFTSInfo>();

            cfConfig.CertPath = this.defaultCertPath;
            foreach (var pkInfo in this.pkInfos)
            {
                CFPKInfo cfpPkinfo = new CFPKInfo { Id = (int)pkInfo.Id };

                if (pkInfo is PKFileInfo)
                {
                    var p = (pkInfo as PKFileInfo);
                    cfpPkinfo.Type = "File";
                    SerializableDictionary<string, string> parameters = new SerializableDictionary<string, string> { };
                    parameters.Add("filePath", p.PkFilePath);
                    parameters.Add("alias", p.Alias);
                    parameters.Add("password", Convert.ToBase64String(Encoding.UTF8.GetBytes(p.Password)));
                    cfpPkinfo.Parameters = parameters;                  
                }
                cfConfig.CFPKInfos.Add(cfpPkinfo);
            }
            foreach (var profile in this.profiles)
            {
                CFProfile cfProfile = new CFProfile { Id = (int)profile.Id };

                if (profile.InLocationAccess is DirectoryAccess)
                {
                    var d = (profile.InLocationAccess as DirectoryAccess);
                    SerializableDictionary<string, string> parameters = new SerializableDictionary<string, string> { };
                    parameters.Add("dirPath", d.DirectoryPath);
                    parameters.Add("fileMask", d.FileMask);
                    CFLocationAccess inLocation = new CFLocationAccess { Type = "Directory", Parameters = parameters };
                    cfProfile.InputLocationAccess = inLocation;
                }
                cfProfile.OutputLocationsAccess = new List<CFLocationAccess>();
                foreach (var outAccess in profile.OutLocationAccess)
                {
                    if (outAccess is DirectoryAccess)
                    {
                        var d = (outAccess as DirectoryAccess);
                        SerializableDictionary<string, string> parameters = new SerializableDictionary<string, string> { };
                        parameters.Add("dirPath", d.DirectoryPath);
                        parameters.Add("fileMask", d.FileMask);
                        CFLocationAccess outLocation = new CFLocationAccess { Type = "Directory", Parameters = parameters };
                        cfProfile.OutputLocationsAccess.Add(outLocation);
                    }
                }

                cfProfile.SignContext = new CFSignContext();
                cfProfile.SignContext.CFPKInfosId = new List<int>();
                foreach(var pkInfo in profile.SignProfile.KeysInfo)                  
                {
                    cfProfile.SignContext.CFPKInfosId.Add((int)pkInfo.Id);
                }
                if (profile.SignProfile.Tsinfo != null)
                {
                    cfProfile.SignContext.TsId = profile.SignProfile.Tsinfo.Id;
                }

                cfProfile.SignContext.CertPath = profile.SignProfile.CertificatePath;

                cfConfig.Profiles.Add(cfProfile);
            }
            foreach (var tsInfo in this.tsInfos)
            {
                CFTSInfo cfTsinfo = new CFTSInfo { Id = (int)tsInfo.Id, Url = tsInfo.Url, Port = tsInfo.Port };
                cfConfig.CFTSInfos.Add(cfTsinfo);
            }
            var xml = Serialization.SerializeFromObject<CFConfig>(cfConfig);
            File.WriteAllText(this.filePath, xml);
            this.xmlFileContent = xml;
            parsedConfig = Serialization.DeserializeToObject<CFConfig>(xmlFileContent);
            BuildConfiguration();
        }
    }
}
