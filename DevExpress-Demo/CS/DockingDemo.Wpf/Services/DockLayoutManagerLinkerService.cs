using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Bars.Native;
using DevExpress.Xpf.Docking;
using System;

namespace DockingDemo {
    public class DockLayoutManagerLinkerService : ServiceBase {
        [ThreadStatic]
        static WeakList<DockLayoutManager> managers = new WeakList<DockLayoutManager>();
        protected override void OnAttached() {
            base.OnAttached();
            DockLayoutManager manager = AssociatedObject as DockLayoutManager;
            if(manager != null && !managers.Contains(manager)) {
                lock(managers) {
                    foreach(var m in managers) {
                        DockLayoutManagerLinker.Link(manager, m);
                    }
                    managers.Add(manager);
                }
            }
        }
        protected override void OnDetaching() {
            DockLayoutManager manager = AssociatedObject as DockLayoutManager;
            if(manager != null && managers.Contains(manager)) {
                lock(managers) {
                    foreach(var m in managers) {
                        DockLayoutManagerLinker.Unlink(manager, m);
                    }
                    managers.Remove(manager);
                }
            }
            base.OnDetaching();
        }
    }
}
