using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using DevExpress.Data;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;

namespace WindowsUIDemo {
    [CodeFile("ViewModels/NotificationsViewModel.(cs)")]
    public partial class Notifications : WindowsUIDemoModule {
        public Notifications() {
            InitializeComponent();
#if CLICKONCE
            useWin8Notifications.IsChecked = false;
            useWin8Notifications.IsEnabled = false;
#endif
        }

        public static string ApplicationID {
            get { return string.Format("Components_{0}_Demo_Center_{0}", AssemblyInfo.VersionShort.Replace(".", "_")); }
        }
        public static string ApplicationName {
            get { return "Notifications"; }
        }

        NotificationsViewModel ViewModel { get { return (NotificationsViewModel)DataContext; } }
        bool IsShortcutCreated { get; set; }

        MemoryStream PatchBackground(MemoryStream stream, Color color) {
            string doc = Encoding.UTF8.GetString(stream.ToArray());
            Regex rx = new Regex("color=\"#(.*?)\"");
            var matches = rx.Matches(doc);
            if(matches.Count > 0) {
                string strColor = matches[0].Groups[1].ToString();
                doc = doc.Replace(strColor, color.ToString().Substring(3));
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(doc));
        }
        void OnModuleLoaded(object sender, RoutedEventArgs e) {
            ActivatorLogger = ViewModel.LogService;
            TryCreateApplicationShortcut();
        }
        void OnModuleUnloaded(object sender, RoutedEventArgs e) {
            ActivatorLogger = null;
            TryRemoveApplicationShortcut();
        }
        void TryCreateApplicationShortcut() {
            try {
                IsShortcutCreated = true;
                ShellHelper.TryCreateShortcut(ApplicationID, ApplicationName, null, typeof(UIDemoNotificationActivator));
            } catch {
                ActivatorLogger.LogLine("Cannot create application shortcut. Native\r\nnotifications are not available.");
                IsShortcutCreated = false;
                useWin8Notifications.IsChecked = false;
                useWin8Notifications.IsEnabled = false;
            }
        }
        void TryRemoveApplicationShortcut() {
            if(IsShortcutCreated)
                ShellHelper.TryRemoveShortcut(ApplicationName);
        }

        static ILogService ActivatorLogger;
        public static void SendActivatorMessage(string arguments) {
            if(ActivatorLogger != null) {
                ActivatorLogger.LogLine("Activator invoked! Notification id = " + arguments);
            }
        }
    }
    [Guid("45FD942D-1AD5-48E7-B139-4E1FB9F1F060"), ComVisible(true)]
    public class UIDemoNotificationActivator : ToastNotificationActivator {
        public override void OnActivate(string arguments, Dictionary<string, string> data) {
            Application.Current.Dispatcher.Invoke(() => {
                Notifications.SendActivatorMessage(arguments);
            });
        }
    }
}
