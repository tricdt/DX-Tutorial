using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Map;

namespace MapDemo {
    public class MapDemoModule : DemoModule {
        protected override void Hide() {
            foreach(MapControl map in DemoUtils.FindMap(this))
                map.HideToolTip();
            base.Hide();
        }
    }
}
