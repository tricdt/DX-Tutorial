using DevExpress.Xpf.DemoBase.DemoTesting;
using System.Threading;
using System.Windows.Threading;
using System;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using System.Windows;
using System.Globalization;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace DialogsDemo.Tests {
    public class DialogsCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            return  theme != Theme.HybridApp;
        }
    }
    public class DialogsDemoModulesAccessor : DemoModulesAccessor<DialogsDemoModule> {
        public DialogsDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseDialogsDemoTestingFixture : BaseDemoTestingFixture {
        protected DialogsDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseDialogsDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract DialogsDemoModulesAccessor GetModulesAccessor();
    }
}
