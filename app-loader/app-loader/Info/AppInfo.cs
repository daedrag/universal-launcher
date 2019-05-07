using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace AppLoader.Info
{
    public enum AppType
    {
        EXE,
        URL,
        STATIC_WEB
    }

    [XmlType("App")]
    public class AppInfo : INotifyPropertyChanged
    {
        private string _name;
        private string _version;
        private Version _versionObj;
        private string _displayName;
        private string _description;
        private DateTime _lastUpdated;
        private string _iconPath;
        private string _downloadPath;

        public AppInfo()
        {
        }

        public string Id => $"{Name}-{Version}";

        [XmlElement("StartupLocation")]
        public string StartUpLocation { get; set; }

        [XmlElement("EntryFilePath")]
        public string EntryFilePath { get; set; }

        [XmlElement("Arguments")]
        public string Arguments { get; set; }

        [XmlElement("AppType")]
        public AppType AppType { get; set; }

        [XmlElement("Name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public Version VersionObj
        {
            get { return _versionObj; }
        }

        [XmlElement("Version")]
        public String Version
        {
            get { return _version; }
            set
            {
                _version = value;
                _versionObj = new Version(string.IsNullOrWhiteSpace(_version) ? "0.0.0" : _version);
                OnPropertyChanged();
            }
        }

        [XmlElement("DisplayName")]
        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; OnPropertyChanged(); }
        }

        [XmlElement("Description")]
        public string Description
        {
            get { return _description; }
            set { _description = value; OnPropertyChanged(); }
        }

        [XmlElement("ReleaseDate")]
        public DateTime ReleaseDate
        {
            get { return _lastUpdated; }
            set { _lastUpdated = value; OnPropertyChanged(); }
        }

        [XmlElement("IconPath")]
        public string IconPath
        {
            get { return _iconPath; }
            set { _iconPath = value; OnPropertyChanged(); }
        }

        [XmlElement("DownloadPath")]
        public string DownloadPath
        {
            get { return _downloadPath; }
            set { _downloadPath = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
