using AppLoader.Store;
using MaterialDesignThemes.Wpf;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AppLoader.Info
{
    /// <summary>
    /// Interaction logic for AppInfoCard.xaml
    /// </summary>
    public partial class RemoteAppInfoCard : UserControl
    {
        private AppInfo _appInfo;
        private System.Drawing.Icon _icon;

        private PackIcon _defaultIcon;

        public RemoteAppInfoCard()
        {
            InitializeComponent();
            _defaultIcon = new PackIcon()
            {
                BorderThickness = new Thickness(0.25),
                Foreground = System.Windows.Media.Brushes.Gray
            };
        }

        private void OnDownloadClicked(object sender, RoutedEventArgs e)
        {
            if (_appInfo == null) return;

            string error = "";
            AppRegistry.Instance.Start(_appInfo, ref error);

            if (!string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(error, "Failed to launch", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UserControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _appInfo = null;
            _icon = null;

            var newAppInfo = e.NewValue as AppInfo;
            if (newAppInfo != null)
            {
                _appInfo = newAppInfo;
                _icon = null;

                if (string.IsNullOrWhiteSpace(_appInfo.IconPath) || !File.Exists(_appInfo.IconPath))
                {
                    if (_appInfo.AppType == AppType.EXE)
                    {
                        _icon = TryGetIconFromExe(_appInfo.EntryFilePath);
                    }
                }
            }

            if (_icon != null)
            {
                iconPlaceholder.Source = ToBitmapImage(_icon.ToBitmap());
            }
            else
            {
                switch (_appInfo.AppType)
                {
                    case AppType.EXE:
                        _defaultIcon.Kind = PackIconKind.PackageVariant;
                        break;
                    case AppType.URL:
                        _defaultIcon.Kind = PackIconKind.Earth;
                        break;
                    case AppType.STATIC_WEB:
                        _defaultIcon.Kind = PackIconKind.Earth;
                        break;
                    default:
                        _defaultIcon.Kind = PackIconKind.PackageVariant;
                        break;
                }
                iconPlaceholder.Source = ToImageSource(_defaultIcon);
            }
        }

        private System.Drawing.Icon TryGetIconFromExe(string exePath)
        {
            try
            {
                var icon = System.Drawing.Icon.ExtractAssociatedIcon(exePath);
                return icon;
            }
            catch (System.Exception)
            {
                return null;
            }
            
        }

        private BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }

        private ImageSource ToImageSource(PackIcon packIcon)
        {
            var geometryDrawing = new GeometryDrawing
            {
                Geometry = Geometry.Parse(packIcon.Data),
                Brush = packIcon.Foreground,
                Pen = new System.Windows.Media.Pen(packIcon.Foreground, packIcon.BorderThickness.Left)
            };

            var drawingGroup = new DrawingGroup { Children = { geometryDrawing } };

            return new DrawingImage { Drawing = drawingGroup };
        }
    }
}
