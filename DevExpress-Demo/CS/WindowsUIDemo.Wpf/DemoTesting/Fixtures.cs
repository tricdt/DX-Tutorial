using DevExpress.Mvvm.UI;
using DevExpress.Xpf.DemoBase.DemoTesting;
using System;
using System.Linq;

namespace WindowsUIDemo.Tests {
    public class WindowsUICheckAllDemosFixture : CheckAllDemosFixture {
        Type[] skipMemoryLeaksCheckModules = new Type[] { };
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return !skipMemoryLeaksCheckModules.Contains(moduleType);
        }
    }
}
