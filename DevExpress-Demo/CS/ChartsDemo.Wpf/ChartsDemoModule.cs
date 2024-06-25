using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DevExpress.Xpf.Charts;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace ChartsDemo {
    public class ChartsDemoModule : DemoModule {
        public static DispatcherTimer CreateTimer() {
            return new DispatcherTimer(DemoTestingHelper.IsTesting ? DispatcherPriority.ApplicationIdle : DispatcherPriority.Background);
        }
        public static readonly DependencyPropertyKey IsModuleLoadedPropertyKey =
            DependencyProperty.RegisterReadOnly("IsModuleLoaded",
                                                typeof(bool),
                                                typeof(ChartsDemoModule),
                                                new PropertyMetadata(false));
        public static readonly DependencyProperty IsModuleLoadedProperty = IsModuleLoadedPropertyKey.DependencyProperty;


        static IEnumerable<T> FindVisualChildren<T>(DependencyObject root)
            where T : DependencyObject {
            if (root != null) {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(root); i++) {
                    DependencyObject child = VisualTreeHelper.GetChild(root, i);
                    if (child != null && child is T) {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child)) {
                        yield return childOfChild;
                    }
                }
            }
        }

        bool runChartsAnimationOnLoad = true;

        public ChartsDemoModule() {
            ModuleLoaded += ChartsDemoModule_ModuleLoaded;
        }
        public virtual void OnStartModule() {

        }
        public virtual void OnStopModule() {

        }
        
        public virtual ChartControl ModuleChartControl { get { return null; } }
        public bool IsModuleLoaded {
            get { return (bool)GetValue(IsModuleLoadedProperty); }
            private set { SetValue(IsModuleLoadedPropertyKey, value); }
        }
        public bool RunChartsAnimationOnLoad {
            get { return runChartsAnimationOnLoad; }
            set { runChartsAnimationOnLoad = value; }
        }
        void ChartsDemoModule_ModuleLoaded(object sender, RoutedEventArgs e) {
            IsModuleLoaded = true;
        }
        protected override void Show() {
            base.Show();
            foreach (ChartControl chart in FindVisualChildren<ChartControl>(this)) {
                if (chart.Palette != null)
                    chart.Palette.ColorCycleLength = 10;
                if (runChartsAnimationOnLoad)
                    chart.Animate();
            }
        }
    }
}
