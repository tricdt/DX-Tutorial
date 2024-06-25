using System;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Docking;
using Microsoft.Win32;

namespace VisualStudioDocking {
    public interface IDockingSerializationDialogService {
        void SaveLayout();
        void LoadLayout();
    }
    public class DockingSerializationDialogService : ServiceBase, IDockingSerializationDialogService {
        const string filter = "Configuration (*.xml)|*.xml|All files (*.*)|*.*";
        public DockLayoutManager DockLayoutManager { get; set; }
        public void LoadLayout() {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = filter };
            var openResult = openFileDialog.ShowDialog();
            if(openResult.HasValue && openResult.Value)
                DockLayoutManager.RestoreLayoutFromXml(openFileDialog.FileName);
        }
        public void SaveLayout() {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = filter };
            var saveResult = saveFileDialog.ShowDialog();
            if(saveResult.HasValue && saveResult.Value)
                DockLayoutManager.SaveLayoutToXml(saveFileDialog.FileName);
        }
        protected override void OnAttached() {
            base.OnAttached();
            DockLayoutManager = AssociatedObject as DockLayoutManager;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            DockLayoutManager = null;
        }
    }
}
