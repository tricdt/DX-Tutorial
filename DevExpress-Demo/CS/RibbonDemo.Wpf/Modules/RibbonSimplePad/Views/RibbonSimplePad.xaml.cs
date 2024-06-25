using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Utils;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Ribbon;
using System.ComponentModel;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase;
using System.Windows.Interop;
using DevExpress.Xpf.Utils;
using System.Windows.Navigation;
using DevExpress.Xpf.Grid;

namespace RibbonDemo {

    [CodeFiles(@"Modules/RibbonSimplePad/Views/RibbonSimplePad.xaml;
                 Modules/RibbonSimplePad/Views/RibbonSimplePad.xaml.(cs);
                 Modules/RibbonSimplePad/ViewModels/SimplePadViewModel.(cs);
                 Modules/RibbonSimplePad/Views/BackstageViewPanes.xaml;
                 Modules/RibbonSimplePad/Views/TemplateSelectors.xaml;
                 Modules/RibbonSimplePad/ViewModels/InlineImageViewModel.(cs);
                 Modules/RibbonSimplePad/ViewModels/RecentItemViewModel.(cs);
                 Modules/RibbonSimplePad/Services/BackstageViewService.(cs);
                 Modules/RibbonSimplePad/TemplateSelectors/InlineImageContentTemplateSelector.(cs);
                 Modules/RibbonSimplePad/TemplateSelectors/TemplateSelectorDictionary.(cs);
                 Modules/RibbonSimplePad/TemplateSelectors/TextMarkerStyleTemplateSelector.(cs);
                 Modules/RibbonSimplePad/Views/BackstageViewCommonStyles.xaml")]    
    public partial class RibbonSimplePad : RibbonDemoModule {
        static RibbonSimplePad() { GridControl.AllowInfiniteGridSize = true; }
        public static readonly DependencyProperty FontEditWidthProperty =
            DependencyProperty.Register("FontEditWidth", typeof(double?), typeof(RibbonSimplePad), new FrameworkPropertyMetadata(null));

        public double? FontEditWidth {
            get { return (double)GetValue(FontEditWidthProperty); }
            set { SetValue(FontEditWidthProperty, value); }
        }

        public RibbonSimplePad() {
            InitializeComponent();
            Ribbon = RibbonControl;
            richControl.Document.Blocks.Add(new Paragraph(new Run("Select the image below to show a contextual tab.")));
            richControl.Document.Blocks.Add(new Paragraph(new InlineUIContainer() { Child = new InlineImage(InlineImageViewModel.Create("/RibbonDemo;component/Images/Clipart/caCompClientEnabled.png")) }));
            ModuleLoaded += OnModuleLoaded;
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        void OnLoaded(object sender, RoutedEventArgs e) {
            RibbonControl.SetCurrentValue(RibbonControl.RibbonStyleProperty, GetRibbonStyle());
            RibbonControl.SetCurrentValue(RibbonControl.RibbonTitleBarVisibilityProperty, GetTitleBarVisibility());
            ThemeManager.ThemeChanged += OnThemeChanged;
            OnThemeChanged(null, null);
        }
        protected virtual RibbonTitleBarVisibility GetTitleBarVisibility() {
            return RibbonTitleBarVisibility.Auto;
        }
        protected virtual RibbonStyle GetRibbonStyle() {
            return DevExpress.Xpf.Ribbon.RibbonStyle.Office2019;
        }
        protected override void Hide() {
            RibbonControl.CloseApplicationMenu();
            base.Hide();
        }

        void OnModuleLoaded(object sender, RoutedEventArgs e) {
            if(!this.IsInDesignTool()) {
                richControl.SetFocus();
            }
        }
        void OnUnloaded(object sender, RoutedEventArgs e) {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }
        void OnThemeChanged(DependencyObject sender, ThemeChangedRoutedEventArgs e) {
            if(!Dispatcher.CheckAccess())
                return;

            FontEditWidth = ThemeManager.GetIsTouchEnabled(this) ? 90 : 50;
        }
    }   
}
