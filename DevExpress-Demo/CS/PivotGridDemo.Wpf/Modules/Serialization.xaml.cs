using System.IO;
using System.Reflection;
using System.Windows;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.PivotGrid;
using DevExpress.Xpf.Editors;

namespace PivotGridDemo {
    public partial class Serialization : PivotGridDemoModule {
        MemoryStream currentLayoutStream;

        public Serialization() {
            InitializeComponent();
        }

        private void LoadSampleButton_Click(object sender, RoutedEventArgs e) {
            var item = (ComboBoxEditItem)layoutSamplesComboBox.SelectedItem;
            var stream = (Stream)item.Tag;
            RestoreLayout(stream);
            pivotGrid.BestFit(FieldArea.RowArea, true, false);
            pivotGrid.BestFitColumn(pivotGrid.ColumnCount - 1);
        }

        private void SaveLayoutButton_Click(object sender, RoutedEventArgs e) {
            currentLayoutStream = SaveLayout();
            restoreLayoutButton.IsEnabled = true;
        }

        private void RestoreLayoutButton_Click(object sender, RoutedEventArgs e) {
            RestoreLayout(currentLayoutStream);
        }


        void PivotGridDemoModule_Loaded(object sender, RoutedEventArgs e) {
            AddLayoutSampleItem("Original", SaveLayout());
            AddLayoutSampleItem("Brief view", GetResourceStream("BriefView"));
            AddLayoutSampleItem("Full view", GetResourceStream("FullView"));
            layoutSamplesComboBox.SelectedIndex = 0;
        }
        void AddLayoutSampleItem(string name, Stream stream) {
            layoutSamplesComboBox.Items.Add(new ComboBoxEditItem() { Content = name, Tag = stream });
        }
        Stream GetResourceStream(string name) {
            Assembly assembly = typeof(Serialization).Assembly;
            var resourcePath = DemoHelper.GetPath("PivotGridDemo.Data.LayoutSamples.", assembly) + name + ".xml";
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);
        }

        MemoryStream SaveLayout() {
            MemoryStream stream = new MemoryStream();
            pivotGrid.SaveLayoutToStream(stream);
            return stream;
        }
        void RestoreLayout(Stream stream) {
            if(stream == null)
                return;
            stream.Position = 0;
            pivotGrid.RestoreLayoutFromStream(stream);
        }
    }
}
