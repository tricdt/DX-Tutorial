using System.Windows.Controls;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;

namespace ChartsDemo {
    public class TabbedModuleViewModel {
        public static TabbedModuleViewModel Create() {
            return ViewModelSource.Create(() => new TabbedModuleViewModel());
        }

        public virtual DXTabItem SelectedTab { get; set; }

        protected TabbedModuleViewModel() { }
    }


    public class NotCachedTabbedModuleViewModel {
        public static NotCachedTabbedModuleViewModel Create() {
            return ViewModelSource.Create(() => new NotCachedTabbedModuleViewModel());
        }

        public virtual DXTabItem SelectedTab { get; set; }
        public virtual StackPanel Options { get; set; }
    }
}
