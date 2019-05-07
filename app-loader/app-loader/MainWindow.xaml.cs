using AppLoader.Info;
using AppLoader.Store;
using System.Collections.Generic;
using System.Windows;

namespace AppLoader
{
    public class MainViewModel
    {
        public List<AppInfo> MyApps { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel
            {
                MyApps = AppStore.Instance.GetAllApps()
            };
        }
    }
}
