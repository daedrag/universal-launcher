using AppLoader.Info;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace AppLoader.Store
{
    public class AppStore
    {
        private List<AppInfo> _allApps;
        private List<AppInfo> _localApps;

        private AppStore()
        {
            using (var reader = new StreamReader("AppStore.xml"))
            {
                var deserializer = new XmlSerializer(typeof(List<AppInfo>), new XmlRootAttribute("Apps"));
                this._allApps = (List<AppInfo>)deserializer.Deserialize(reader);
            }

            this._localApps = new List<AppInfo>();
            foreach (var appPath in Directory.GetDirectories(Constants.AppRoot))
            {
                foreach (var versionPath in Directory.GetDirectories(appPath))
                {
                    var versionDir = new DirectoryInfo(versionPath).Name;
                    if (!Version.TryParse(versionDir, out var appVersion)) continue;

                    var versionManifest = Path.Combine(appPath, $"{versionDir}.manifest");
                    if (!File.Exists(versionManifest)) continue;

                    try
                    {
                        using (var reader = new StreamReader(versionManifest))
                        {
                            var deserializer = new XmlSerializer(typeof(AppInfo));
                            var appInfo = (AppInfo)deserializer.Deserialize(reader);
                            this._localApps.Add(appInfo);
                        }
                    }
                    catch (Exception)
                    {
                        // ignore
                    }
                }
            }
        }

        public static AppStore Instance { get; } = new AppStore();

        public List<AppInfo> GetAllApps()
        {
            return _allApps.ToList();
        }

        public List<AppInfo> GetInstalledApps()
        {
            return _localApps.ToList();
        }
    }
}
