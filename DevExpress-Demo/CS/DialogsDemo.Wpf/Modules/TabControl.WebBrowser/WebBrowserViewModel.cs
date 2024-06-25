using CommonDemo.Helpers;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace CommonDemo.TabControl.WebBrowser {
    public class WebBrowserMainViewModel {
        public virtual ObservableCollection<TabViewModel> Tabs { get; protected set; }
        public virtual ObservableCollection<SpeedDialViewModel> SpeedDials { get; protected set; }

        public static WebBrowserMainViewModel Create() {
            return ViewModelSource.Create(() => new WebBrowserMainViewModel());
        }
        protected WebBrowserMainViewModel() {
            Tabs = new ObservableCollection<TabViewModel>();
            Tabs.Add(TabViewModel.CreateNewTabViewModel());

            SpeedDials = new ObservableCollection<SpeedDialViewModel>();
            SpeedDials.Add(new SpeedDialViewModel(ImagesHelper.GetWebIcon("Microsoft.png"), new Uri("http://www.microsoft.com", UriKind.Absolute), "Microsoft"));
            SpeedDials.Add(new SpeedDialViewModel(ImagesHelper.GetWebIcon("Google.png"), new Uri("http://www.google.com", UriKind.Absolute), "Google"));
            SpeedDials.Add(new SpeedDialViewModel(ImagesHelper.GetWebIcon("Devexpress.png"), new Uri("http://www.devexpress.com", UriKind.Absolute), "DevExpress"));
            SpeedDials.Add(new SpeedDialViewModel(ImagesHelper.GetWebIcon("VisualStudio.png"), new Uri("http://www.visualstudio.com", UriKind.Absolute), "Visual Studio"));
            SpeedDials.Add(new SpeedDialViewModel(ImagesHelper.GetWebIcon("Stackoverflow.png"), new Uri("http://www.stackoverflow.com", UriKind.Absolute), "Stackoverflow"));
            SpeedDials.Add(new SpeedDialViewModel(ImagesHelper.GetWebIcon("Facebook.png"), new Uri("http://www.facebook.com", UriKind.Absolute), "Facebook"));
            SpeedDials.Add(new SpeedDialViewModel(ImagesHelper.GetWebIcon("Twitter.png"), new Uri("http://www.twitter.com", UriKind.Absolute), "Twitter"));
            SpeedDials.Add(new SpeedDialViewModel(ImagesHelper.GetWebIcon("Youtube.png"), new Uri("http://www.youtube.com", UriKind.Absolute), "Youtube"));
            SpeedDials.Add(new SpeedDialViewModel(ImagesHelper.GetWebIcon("Amazon.png"), new Uri("http://www.amazon.com", UriKind.Absolute), "Amazon"));
        }
        public void AddNewTab(TabControlTabAddingEventArgs e) {
            e.Item = TabViewModel.CreateNewTabViewModel();
        }
    }

    public class TabViewModel {
        public virtual bool IsNewTab { get; protected set; }
        public virtual string Title { get; protected set; }
        public virtual Uri Url { get; protected set; }

        public static TabViewModel CreateNewTabViewModel() {
            return ViewModelSource.Create(() => new TabViewModel());
        }
        protected TabViewModel() {
            IsNewTab = true;
            Title = "Speed Dial";
        }
        public void LoadSpeedDial(SpeedDialViewModel speedDial) {
            IsNewTab = false;
            Title = speedDial.Title;
            Url = speedDial.Url;
        }
    }
    public class SpeedDialViewModel {
        public ImageSource Icon { get; private set; }
        public Uri Url { get; private set; }
        public string Title { get; private set; }
        public SpeedDialViewModel(ImageSource icon, Uri url, string title) {
            Icon = icon;
            Url = url;
            Title = title;
        }
    }
}
