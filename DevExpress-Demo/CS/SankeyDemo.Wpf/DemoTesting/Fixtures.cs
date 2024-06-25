using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DemoTesting;
using System;

namespace SankeyDemo.Tests {
    public class SankeyCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            return false;
        }
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return false;
        }
    }
}
