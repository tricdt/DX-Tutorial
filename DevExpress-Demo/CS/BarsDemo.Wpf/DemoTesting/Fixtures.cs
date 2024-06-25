using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Editors;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace BarsDemo.Tests {
    
    

    
    
    public class BarsCheckAllDemosFixture : CheckAllDemosFixture {
        Type[] skipMemoryLeaksCheckModules = new Type[] {
        };
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return !skipMemoryLeaksCheckModules.Contains(moduleType);
        }
    }
    public class BarsDemoModulesAccessor : DemoModulesAccessor<BarsDemoModule> {
        public BarsDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        public BarManager Manager { get { return DemoModule.Manager; } }
    }
    public abstract class BaseBarsDemoTestingFixture : BaseDemoTestingFixture {
        readonly BarsDemoModulesAccessor modulesAccessor;
        public BaseBarsDemoTestingFixture() {
            modulesAccessor = new BarsDemoModulesAccessor(this);
        }
        public BarManager Manager { get { return modulesAccessor.Manager; } }
    }
    public class CheckDemoOptionsFixture : BaseBarsDemoTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            AddLoadModuleActions(typeof(ItemProperties));
        }
    }
}
