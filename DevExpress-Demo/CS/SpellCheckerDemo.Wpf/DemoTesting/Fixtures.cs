using DevExpress.Xpf.DemoBase.DemoTesting;

namespace SpellCheckerDemo.Tests {
    public class SpellCheckerDemoModuleAccessor : DemoModulesAccessor<SpellCheckerDemoModule> {
        public SpellCheckerDemoModuleAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
    }
    public abstract class BaseSpellCheckerTestingFixture : BaseDemoTestingFixture {
        readonly SpellCheckerDemoModuleAccessor modulesAccessor;
        public BaseSpellCheckerTestingFixture() {
            this.modulesAccessor = new SpellCheckerDemoModuleAccessor(this);
        }
    }
    public class SpellCheckerCheckAllDemosFixture : CheckAllDemosFixture {
    }
}
