using AppLoader.Store;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;

namespace AppLoader
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);
        //    RunThisAsAdmin();
        //    AppRegistry.Instance.WatchProcessEvent();
        //}

        //private static void RunThisAsAdmin()
        //{
        //    if (!IsAdministrator())
        //    {
        //        var exe = Process.GetCurrentProcess().MainModule.FileName;
        //        var startInfo = new ProcessStartInfo(exe)
        //        {
        //            UseShellExecute = true,
        //            Verb = "runas",
        //            WindowStyle = ProcessWindowStyle.Normal,
        //            CreateNoWindow = false
        //        };
        //        Process.Start(startInfo);
        //        Process.GetCurrentProcess().Kill();
        //    }
        //}

        //private static bool IsAdministrator()
        //{
        //    var identity = WindowsIdentity.GetCurrent();
        //    var principal = new WindowsPrincipal(identity);
        //    return principal.IsInRole(WindowsBuiltInRole.Administrator);
        //}
    }
}
