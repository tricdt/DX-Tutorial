using System;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.DemoBase;
using System.Linq;

namespace SpreadsheetDemo.Tests {
    public class SpreadsheetCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool CanRunModule(Type moduleType) {
#if !DXCORE3
            if (moduleType == typeof(BindingToDataSource))
                return false;
#endif
            return base.CanRunModule(moduleType);
        }
    }
    public class SpreadsheetDemoModulesAccessor : DemoModulesAccessor<SpreadsheetDemoModule> {
        public SpreadsheetDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseSpreadsheetDemoTestingFixture : BaseDemoTestingFixture {
        protected SpreadsheetDemoModulesAccessor ModuleAccessor { get; private set; }
        public BaseSpreadsheetDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract SpreadsheetDemoModulesAccessor GetModulesAccessor();
    }
}
