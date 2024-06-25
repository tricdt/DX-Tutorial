using System;
using System.Linq;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace PdfViewerDemo.Tests {
    public class PdfViewerCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            return moduleType != typeof(MainDemoModule) || theme != Theme.HybridApp;
        }
        protected override bool AllowSwitchToCodeTab(Type moduleType) {
            return false; 
        }
    }
    public class PdfViewerDemoModulesAccessor : DemoModulesAccessor<PdfViewerDemoModule> {
        public PdfViewerDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BasePdfViewerDemoTestingFixture : BaseDemoTestingFixture {
        protected PdfViewerDemoModulesAccessor ModuleAccessor { get; private set; }
        public BasePdfViewerDemoTestingFixture() {
            ModuleAccessor = GetModulesAccessor();
        }
        protected abstract PdfViewerDemoModulesAccessor GetModulesAccessor();
    }
}
