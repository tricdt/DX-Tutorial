using System;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase.DemoTesting;

namespace TreeMapDemo.Tests {
    public class TreeMapCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool AllowSwitchToTheTheme(Type moduleType, Theme theme) {
            return false;
        }
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return false; 
        }
    }
}
