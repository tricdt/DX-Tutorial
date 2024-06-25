using System;
using System.Windows;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Utils.Themes;

namespace PropertyGridDemo {
	public class PropertyGridDemoModule : DemoModule {
        static PropertyGridDemoModule() {
            Type ownerType = typeof(PropertyGridDemoModule);
        }
		protected virtual bool NeedChangeEditorsTheme { get { return false; } }
	}
}
