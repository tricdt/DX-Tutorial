using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.DemoBase.Helpers.TextColorizer;
using System;

namespace MapDemo.Tests {
    public class MapCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            return false;
        }
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return false;
        }
    }
}
