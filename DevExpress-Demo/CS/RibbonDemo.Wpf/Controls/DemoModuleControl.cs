using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.Ribbon;
using System.Windows.Media;
using DevExpress.Xpf.Core;
using DevExpress.Mvvm;
using System.Linq;

namespace RibbonDemo {
    public class RibbonDemoModule : DemoModule {
        public static readonly DependencyProperty RibbonProperty;

        static RibbonDemoModule() {
            Type ownerType = typeof(RibbonDemoModule);
            RibbonProperty = DependencyPropertyManager.Register("Ribbon", typeof(RibbonControl), ownerType, new FrameworkPropertyMetadata(null));
        }
        public RibbonControl Ribbon {
            get { return (RibbonControl)GetValue(RibbonProperty); }
            set { SetValue(RibbonProperty, value); }
        }

        protected virtual bool NeedChangeEditorsTheme { get { return false; } }
        protected override void ShowPopupContent() {
            base.ShowPopupContent();
            if(Ribbon != null)
                Ribbon.AllowKeyTips = true;
        }
        protected override void HidePopupContent() {
            if(Ribbon != null) {
                Ribbon.AllowKeyTips = false;
                NavigationTree.ExitMenuMode(false, false);
                Ribbon.CloseApplicationMenu();
            }
            base.HidePopupContent();
        }
    }
}
