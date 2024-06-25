using DevExpress.Xpf.DemoBase;

namespace GridDemo {
    public abstract class GridShowcaseBrowserModule : ShowcaseBrowserModule {
        protected virtual bool NeedChangeEditorsTheme { get { return false; } }
        protected virtual void LoadDemoData() {
        }
    }
}
