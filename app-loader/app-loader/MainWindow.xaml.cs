using AppLoader.Info;
using AppLoader.Store;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace AppLoader
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private List<AppInfo> _displayApps;
        private bool _isLocalAppView;

        public MainViewModel()
        {
            this._displayApps = new List<AppInfo>();
            this._isLocalAppView = true;
        }

        public List<AppInfo> DisplayApps
        {
            get { return this._displayApps; }
            set { this._displayApps = value; OnPropertyChanged(); }
        }

        public bool IsLocalAppView
        {
            get { return this._isLocalAppView; }
            set { this._isLocalAppView = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        public MainWindow()
        {
            InitializeComponent();

            this._viewModel = new MainViewModel();
            OnMyAppsTabClicked(this, null);
            MyAppsRadioBtn.IsChecked = true;
            AllAppsRadioBtn.IsChecked = false;

            DataContext = this._viewModel;
        }

        private void OnMyAppsTabClicked(object sender, RoutedEventArgs e)
        {
            this._viewModel.DisplayApps = AppStore.Instance.GetInstalledApps();
            this._viewModel.IsLocalAppView = true;
        }

        private void OnAllAppsClicked(object sender, RoutedEventArgs e)
        {
            this._viewModel.DisplayApps = AppStore.Instance.GetAllApps();
            this._viewModel.IsLocalAppView = false;
        }
    }
}
