using System;
using System.Windows;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Utils.Themes;

namespace EditorsDemo {
	public class EditorsDemoModule : DemoModule {
        protected virtual bool NeedChangeEditorsTheme { get { return false; } }
    }
}
namespace CommonDemo {
    public class CommonDemoModule : EditorsDemo.EditorsDemoModule {
    }
}
