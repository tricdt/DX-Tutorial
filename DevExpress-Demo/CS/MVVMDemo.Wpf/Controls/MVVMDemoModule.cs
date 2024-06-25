using System;
using DevExpress.Xpf.DemoBase;
using System.IO;
using DevExpress.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Utils;
using System.Windows.Media.Imaging;

namespace MVVMDemo {
    public abstract class MVVMDemoModule : ShowcaseBrowserModule {
        protected virtual bool NeedChangeEditorsTheme { get { return false; } }
        protected virtual void LoadDemoData() {
        }
    }
}
