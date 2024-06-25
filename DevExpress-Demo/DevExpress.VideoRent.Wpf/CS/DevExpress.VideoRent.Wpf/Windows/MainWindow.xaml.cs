using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Utils;
using DevExpress.VideoRent.ViewModel;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.VideoRent.Wpf.Helpers;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpo;
using System.Linq;

namespace DevExpress.VideoRent.Wpf {
    public partial class MainWindow : ThemedWindow {
        UnitOfWork dataSession;

        public MainWindow() {
            Program.Test(this);
            InitializeComponent();
            dataSession = new UnitOfWork(Session.DefaultSession.DataLayer);
            Loaded += OnLoaded;
            Closing += OnClosing;
#if !SL
#endif
        }
        public event EventHandler BeforeClosed;
        void OnClosing(object sender, CancelEventArgs e) {
            e.Cancel = !ModulesManager.Current.CloseAllModuleObjectDetails();
            if(!e.Cancel && BeforeClosed != null)
                BeforeClosed(this, EventArgs.Empty);
        }
        void OnLoaded(object sender, RoutedEventArgs e) {
            WpfViewsManager.Register(Modules, dataSession);
            Activate();
        }
        ImageSource CreateImageSource(Uri uri) {
            if(uri == null) return null;
            try {
                return new BitmapImage(uri);
            } catch {
                return null;
            }
        }
        void OnBbiLayoutOptionsItemClick(object sender, ItemClickEventArgs e) {
            LayoutOptionsWindow layoutOptionsWindow = new LayoutOptionsWindow();
            layoutOptionsWindow.Owner = this;
            layoutOptionsWindow.ShowDialog();
        }
        void OnChangeCurrentCustomerClick(object sender, ItemClickEventArgs e) {
            ModulesManager.Current.OpenModuleObjectDetail(new FindCustomerDetailObject(LayoutManager.Current.Session), false);
        }
        void OnExitButtonClick(object sender, RoutedEventArgs e) {
            Application.Current.MainWindow.Close();
        }
        void OnLogOffButtonClick(object sender, RoutedEventArgs e) {
            // TODO log off
            Application.Current.MainWindow.Close();
        }
        void OnBbiAboutItemClick(object sender, ItemClickEventArgs e) {
            Window aboutWindow = new AboutWindow() { Owner = this };
            aboutWindow.ShowDialog();
        }
        void OnBbiHomeItemClick(object sender, ItemClickEventArgs e) {
            Modules.SelectDemoModule(null);
        }
    }
}
