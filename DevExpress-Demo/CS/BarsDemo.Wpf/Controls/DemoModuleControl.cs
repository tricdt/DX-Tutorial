using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Utils;
namespace BarsDemo {
    public class BarsDemoModule : DemoModule {
        public ICommand GoDevExpressCommand {
            get { return (ICommand)GetValue(GoDevExpressCommandProperty); }
            set { SetValue(GoDevExpressCommandProperty, value); }
        }
        public static readonly DependencyProperty GoDevExpressCommandProperty =
            DependencyProperty.Register("GoDevExpressCommand", typeof(ICommand), typeof(BarsDemoModule), new PropertyMetadata(null));
        public ICommand ShowAboutCommand {
            get { return (ICommand)GetValue(ShowAboutCommandProperty); }
            set { SetValue(ShowAboutCommandProperty, value); }
        }
        public static readonly DependencyProperty ShowAboutCommandProperty =
            DependencyProperty.Register("ShowAboutCommand", typeof(ICommand), typeof(BarsDemoModule), new PropertyMetadata(null));
        public static readonly DependencyProperty BarManagerProperty = 
            DependencyPropertyManager.Register("BarManager", typeof(BarManager), typeof(BarsDemoModule), new FrameworkPropertyMetadata(null));
        public BarManager Manager {
            get { return (BarManager)GetValue(BarManagerProperty); }
            set { SetValue(BarManagerProperty, value); }
        }
        static BarsDemoModule() {
            BarNameScope.IsScopeOwnerProperty.OverrideMetadata(typeof(BarsDemoModule), new FrameworkPropertyMetadata(true));
        }
        public BarsDemoModule() {
            if(!this.IsInDesignTool()) {
                Margin = new Thickness(25);
                BorderThickness = new Thickness(1);
            }
            Loaded += BarsDemoModule_Loaded;
            ShowAboutCommand = new DelegateCommand(ShowAboutExecute);
            GoDevExpressCommand = new DelegateCommand(GoDevExpressExecute);
        }
        void BarsDemoModule_Loaded(object sender, RoutedEventArgs e) {
            UpdateBorder();
        }
        void UpdateBorder() {
            if(Theme.MetropolisLightName == ThemeManager.GetThemeName(this))
                BorderBrush = new SolidColorBrush(Colors.DarkGray);
            else {
                Color color = (TextElement.GetForeground(this) as SolidColorBrush).Color;
                BorderBrush = new SolidColorBrush(Color.FromArgb(50, color.R, color.G, color.B));
            }
        }
        void ShowAboutExecute() {
            var demoBase = LayoutHelper.FindLayoutOrVisualParentObject<DemoBaseControl>(this);
            demoBase.ShowAbout();
        }
        void GoDevExpressExecute() {
            Process.Start(new ProcessStartInfo { FileName = "http://www.devexpress.com", UseShellExecute = true });
        }
    }
}
