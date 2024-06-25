using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Serialization;
using System.IO;
using System.Reflection;
using System.Windows;

namespace BarsDemo {
    public partial class SaveAndRestoreLayout : BarsDemoModule {
        IWorkspaceManager wManager;
        public SaveAndRestoreLayout() {
            InitializeComponent();
            Loaded += OnLoaded;
            DXSerializer.SetSerializationID(this.barManager, "BarManager");
            isolatedStorageSettingsGroup.Visibility = Visibility.Collapsed;
            wManager = WorkspaceManager.GetWorkspaceManager(barManager);
            wManager.TransitionEffect = TransitionEffect.Ripple;
        }

        void OnLoaded(object sender, RoutedEventArgs e) {
            loadLocalStorageButton.IsEnabled = false;
        }
        
        protected virtual void ShowMessageBox(string message) {
            MessageBox.Show(message, "Layout", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        protected virtual void serializeButton_Click(object sender, RoutedEventArgs e) {
            this.stream = new MemoryStream();
            this.barManager.SaveLayoutToStream(this.stream);
            ShowMessageBox("Layout has been saved");
            this.deserializeButton.IsEnabled = stream.Length > 0;
        }
        protected virtual void deserializeButton_Click(object sender, RoutedEventArgs e) {
            if(this.stream == null)
                return;
            MemoryStream memoryStream = new MemoryStream();
            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(memoryStream);
            stream.Seek(0, SeekOrigin.Begin);
            RestoreLayout("UserSaved", stream);
            stream.Dispose();
            stream = memoryStream;
        }
        void RestoreLayout(string name, Stream file) {
            if(useWManagerCheck.IsChecked.HasValue && useWManagerCheck.IsChecked.Value) {
                if(transitionComboBox.SelectedItem != null)
                    wManager.TransitionEffect = (TransitionEffect)transitionComboBox.SelectedItem;
                wManager.LoadWorkspace(name, file);
                wManager.ApplyWorkspace(name);
            }
            else {
                this.barManager.RestoreLayoutFromStream(file);
                ShowMessageBox("Layout has been restored");
            }
        }
        protected virtual void loadSampleLayoutButton_Click(object sender, RoutedEventArgs e) {
            Assembly thisExe = Assembly.GetExecutingAssembly();
            string name = string.Format("BarsDemo.Layouts.{0}.xml", layoutSampleName.SelectedItem.ToString().ToLower());
            Stream file = thisExe.GetManifestResourceStream(name);
            RestoreLayout(name, file);
        }
        protected virtual void saveLocalStorageButton_Click(object sender, RoutedEventArgs e) {
        }
        protected virtual void loadLocalStorageButton_Click(object sender, RoutedEventArgs e) {
        }

        Stream stream;

        private void transitionComboBox_SelectedIndexChanged(object sender, RoutedEventArgs e) {
            if(barManager != null) {
                IWorkspaceManager wm = WorkspaceManager.GetWorkspaceManager(barManager);
                if(wm != null)
                    wm.TransitionEffect = (TransitionEffect)transitionComboBox.SelectedItem;                
            }
        }
    }
}
