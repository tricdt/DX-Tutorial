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

namespace MVVMDemo.Tests {
    public class MVVMCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            return theme != Theme.HybridApp;
        }
    }
    public class MVVMDemoModulesAccessor : DemoModulesAccessor<MVVMDemoModule> {
        public MVVMDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseMVVMDemoTestingFixture : BaseDemoTestingFixture {
        protected MVVMDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseMVVMDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract MVVMDemoModulesAccessor GetModulesAccessor();
    }
}
