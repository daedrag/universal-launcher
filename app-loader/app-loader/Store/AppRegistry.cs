using AppLoader.Info;
using System;
using System.Diagnostics;
using System.IO;

namespace AppLoader.Store
{
    public class AppRegistry
    {
        public static AppRegistry Instance { get; } = new AppRegistry();

        private AppRegistry() { }
        
        public bool Start(AppInfo app, ref string error)
        {
            if (!File.Exists(app.EntryFilePath))
            {
                error = $"Cannot find {Path.GetFileName(app.EntryFilePath)}";
                return false;
            }

            switch (app.AppType)
            {
                case AppType.EXE:
                    return StartExe(app, ref error);
                case AppType.URL:
                case AppType.STATIC_WEB:
                default:
                    error = "Unsupported app type";
                    return false;
            }
        }

        protected bool StartExe(AppInfo app, ref string error)
        {
            try
            {
                var proc = Process.Start(app.EntryFilePath, app.Arguments);
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
    }
}
