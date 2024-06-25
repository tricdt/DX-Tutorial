using DevExpress.Xpf.DemoBase.DemoTesting;
using System.Threading;
using System.Windows.Threading;
using System;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using System.Windows;
using System.Globalization;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace PropertyGridDemo.Tests {
    public class PropertyGridCheckAllDemosFixture : CheckAllDemosFixture {
    }
    public class PropertyGridDemoModulesAccessor : DemoModulesAccessor<PropertyGridDemoModule> {
        public PropertyGridDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BasePropertyGridDemoTestingFixture : BaseDemoTestingFixture {
        protected PropertyGridDemoModulesAccessor ModuleAccessor { get; private set; }
        public BasePropertyGridDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract PropertyGridDemoModulesAccessor GetModulesAccessor();
    }
}
