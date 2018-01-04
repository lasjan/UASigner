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

            this.serviceConfig = new Service.Configuration.ServiceConfiguration();

            if (parsedConfig.ServiceConfig != null)
            {
                if (parsedConfig.ServiceConfig.Parameters != null)
                {
                    if (parsedConfig.ServiceConfig.Parameters.ContainsKey("idleTime"))
                    {
                        this.serviceConfig.SetIdleTime(Int32.Parse(parsedConfig.ServiceConfig.Parameters["idleTime"]));
                    }
                }
            }
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

                if (inputAccess.Type.Equals("ftp", StringComparison.InvariantCultureIgnoreCase))
                {
                    var address = inputAccess.Parameters["address"];
                    var port = inputAccess.Parameters["port"];
                    var user = inputAccess.Parameters["user"];
                    var password = inputAccess.Parameters["password"];

                    FtpAccess inFtpAccess = new FtpAccess(address, Int32.Parse(port), user, password);
                    profile.InLocationAccess = inFtpAccess;
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
                    if (outputAccess.Type.Equals("ftp", StringComparison.InvariantCultureIgnoreCase))
                    {
                        var address = outputAccess.Parameters["address"];
                        var port = outputAccess.Parameters["port"];
                        var user = outputAccess.Parameters["user"];
                        var password = outputAccess.Parameters["password"];

                        FtpAccess outFtpAccess = new FtpAccess(address, Int32.Parse(port), user, password);
                        outAccess.Add(outFtpAccess);
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

                bool addTS = false;
                Boolean.TryParse(cfProile.SignContext.AddContentTimeStamp, out addTS);
                signProfile.AddContentTimeStamp = addTS;
                
                bool addCert = false;
                Boolean.TryParse(cfProile.SignContext.AddCertificate, out addCert);
                signProfile.AddCertificate = addCert;

                bool addVD = false;
                Boolean.TryParse(cfProile.SignContext.AddValidationData, out addVD);
                signProfile.AddValidationData = addVD;

                SignatureLevel signatureLevel = SignatureLevel.CADES_BASELINE_B;

                Enum.TryParse<SignatureLevel>(cfProile.SignContext.SignatureProfile, out signatureLevel);
                signProfile.Level = signatureLevel;

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

            if (this.serviceConfig != null)
            {
                CFServiceConfig cfServiceConfig = new CFServiceConfig();
                cfServiceConfig.Parameters = new SerializableDictionary<string, string>();

                foreach (var p in this.serviceConfig.ConfigDict)
                {
                    cfServiceConfig.Parameters.Add(p.Key, p.Value);
                }

                cfConfig.ServiceConfig = cfServiceConfig;
            }
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
                if (profile.InLocationAccess is FtpAccess)
                {
                    var d = (profile.InLocationAccess as FtpAccess);
                    SerializableDictionary<string, string> parameters = new SerializableDictionary<string, string> { };
                    parameters.Add("address", d.Address);
                    parameters.Add("port", d.Port.ToString());
                    parameters.Add("user", d.UserName);
                    parameters.Add("password", d.Password);
                    CFLocationAccess inLocation = new CFLocationAccess { Type = "Ftp", Parameters = parameters };
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
                    if (outAccess is FtpAccess)
                    {
                        var d = (outAccess as FtpAccess);
                        SerializableDictionary<string, string> parameters = new SerializableDictionary<string, string> { };
                        parameters.Add("address", d.Address);
                        parameters.Add("port", d.Port.ToString());
                        parameters.Add("user", d.UserName);
                        parameters.Add("password", d.Password);
                        CFLocationAccess outLocation = new CFLocationAccess { Type = "Ftp", Parameters = parameters };
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
                cfProfile.SignContext.SignatureProfile = profile.SignProfile.Level.ToString();
                cfProfile.SignContext.AddValidationData = profile.SignProfile.AddValidationData.ToString();
                cfProfile.SignContext.AddContentTimeStamp = profile.SignProfile.AddContentTimeStamp.ToString();
                cfProfile.SignContext.AddCertificate = profile.SignProfile.AddCertificate.ToString();

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

        public override void Validate()
        {
            this.profiles.ForEach(x => x.Validate());
            this.pkInfos.ForEach(x => x.Validate());
        }
    }
}
