using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DevExpress.SalesDemo.Wpf {
    public class ApplicationSettings {
        public static ApplicationSettings Default { get { return (ApplicationSettings)Application.Current.Resources["ApplicationSettings"]; } }

        public string MainWindowTitle { get; set; }
        public double MainWindowMinWidth { get; set; }
        public double MainWindowMinHeight { get; set; }
        public double MainWindowWidth { get; set; }
        public double MainWindowHeight { get; set; }
        public Uri MainWindowIcon { get; set; }
        public WindowState MainWindowState { get; set; }
    }
}
