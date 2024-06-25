using ControlsDemo.Modules;
using DevExpress.Xpf.DemoBase.DemoTesting;
using System;
using System.Linq;

namespace ControlsDemo.Tests {
    public class ControlsCheckAllDemosFixture : CheckAllDemosFixture {
        Type[] skipMemoryLeaksCheckModules = new Type[] { typeof(BreadcrumbSamplesModule) };
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return !skipMemoryLeaksCheckModules.Contains(moduleType);
        }
    }
}
