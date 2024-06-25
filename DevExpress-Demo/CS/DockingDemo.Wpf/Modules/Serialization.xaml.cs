using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Layout.Core;

namespace DockingDemo {
    public partial class Serialization : DockingDemoModule {
        DevExpress.Xpf.Core.IWorkspaceManager wManager;
        Stream memoryStream;
        public Serialization() {
            InitializeComponent();
            wManager = DevExpress.Xpf.Core.WorkspaceManager.GetWorkspaceManager(dockManager);
        }
        protected virtual void SaveLayout() {
            Ref.Dispose(ref memoryStream);
            memoryStream = new MemoryStream();
            dockManager.SaveLayoutToStream(memoryStream);
            deserializeButton.IsEnabled = true;
        }
        void serializeButton_Click(object sender, RoutedEventArgs e) {
            SaveLayout();
        }
        void deserializeButton_Click(object sender, RoutedEventArgs e) {
            if(memoryStream == null) return;
            memoryStream.Seek(0, SeekOrigin.Begin);
            RestoreLayout("UserLayout", memoryStream);
        }
        void loadSampleLayoutButton_Click(object sender, RoutedEventArgs e) {
            Assembly thisExe = typeof(Serialization).Assembly;
            string name = string.Format("{0}.xml", layoutSampleName.SelectedItem.ToString().ToLower());
            using(Stream resourceStream = DevExpress.Utils.AssemblyHelper.GetEmbeddedResourceStream(thisExe, DemoHelper.GetPath("Layouts/", thisExe) + name, true)) {
                RestoreLayout(name, resourceStream);
            }
        }
        void RestoreLayout(string name, Stream stream) {
            if(useWManagerCheck.IsChecked == true) {
                if(transitionComboBox.EditValue != null)
                    wManager.TransitionEffect = (DevExpress.Xpf.Core.TransitionEffect)transitionComboBox.EditValue;
                wManager.LoadWorkspace(name, stream);
                wManager.ApplyWorkspace(name);
            }
            else {
                dockManager.RestoreLayoutFromStream(stream);
            }
        }
    }
}
