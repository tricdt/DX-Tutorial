using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Layout.Core;
using DevExpress.Xpf.Docking;
using DockingDemo.ViewModels;

namespace DockingDemo {
    [CodeFile("ViewModels/SerializationViewModel.(cs)")]
    public partial class MVVMSerialization : DockingDemoModule {
        public MVVMSerialization() {
            InitializeComponent();
        }
    }
    public class MVVMSerializationLayoutAdapter : ILayoutAdapter {
        #region ILayoutAdapter Members
        string ILayoutAdapter.Resolve(DockLayoutManager owner, object item) {
            BaseLayoutItem panelHost = owner.GetItem("PanelHost");
            if(panelHost == null) {
                panelHost = new LayoutGroup() { Name = "PanelHost", DestroyOnClosingChildren = false };
                owner.LayoutRoot.Add(panelHost);
            }
            return "PanelHost";
        }
        #endregion
    }
}
