using DevExpress.Xpf.DemoBase;

namespace SchedulingDemo {
    public class SchedulingDemoModule : DemoModule {
        protected override void Clear() {
            base.Clear();
            DataContext = null;
        }
    }
}
