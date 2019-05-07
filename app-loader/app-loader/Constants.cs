using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLoader
{
    public static class Constants
    {
        public static string AppName = "UniversalLauncher";

        public static string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string AppRoot => Path.Combine(AppData, AppName);
    }
}
