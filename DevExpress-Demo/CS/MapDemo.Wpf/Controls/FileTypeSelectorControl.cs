using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Map;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Data;

namespace MapDemo {
    public class ShapesExporterControl : VisibleControl {
        readonly ShapesExporterViewModel viewModel;

        public static readonly DependencyProperty ExportingLayerProperty = DependencyProperty.Register("ExportingLayer",
            typeof(VectorLayer), typeof(ShapesExporterControl), new PropertyMetadata(null));
        public static readonly DependencyProperty PressedProperty = DependencyProperty.Register("Pressed",
            typeof(bool), typeof(ShapesExporterControl), new PropertyMetadata(OnButtonPressed));
        
        static void OnButtonPressed(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ShapesExporterControl control = d as ShapesExporterControl;
            if(control == null || !control.viewModel.Pressed)
                return;
            string fileType = (string)control.viewModel.SelectedFileType;
            string format = GetFileFormat(fileType);
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Empty;
            saveFileDialog.Filter = string.Format("{0} files (*.{1})|*.{1}", format.ToUpper(), format);
            bool? dialogResult = saveFileDialog.ShowDialog();
            if(!dialogResult.HasValue || !dialogResult.Value)
                return;
            switch(fileType) {
                case ".shp-file": control.ExportingLayer.ExportToShp(saveFileDialog.FileName, new ShpExportOptions() { ExportToDbf = true, ShapeType = ShapeType.Polygon }); break;
                case ".kml-file": control.ExportingLayer.ExportToKml(saveFileDialog.FileName); break;
                case ".svg-file": control.ExportingLayer.ExportToSvg(saveFileDialog.FileName); break;
            }
            ThemedMessageBox.Show("Info", string.Format("Shapes successfully exported to {0} file", saveFileDialog.FileName), MessageBoxButton.OK, MessageBoxImage.Information);
        }
        static string GetFileFormat(string fileType) {
            switch(fileType) {
                case ".shp-file": return "shp";
                case ".kml-file": return "kml";
                case ".svg-file": return "svg";
            }
            return string.Empty;
        }
        
        public VectorLayer ExportingLayer {
            get { return (VectorLayer)GetValue(ExportingLayerProperty); }
            set { SetValue(ExportingLayerProperty, value); }
        }
        
        public ShapesExporterControl() {
            DefaultStyleKey = typeof(ShapesExporterControl);
            this.viewModel = new ShapesExporterViewModel();
            DataContext = viewModel;
            BindingOperations.SetBinding(this, PressedProperty, new Binding("Pressed"));
        }
    }

    public class ShapesExporterViewModel : DependencyObject {
        const string DefaultFileType = ".shp-file";

        public static readonly DependencyProperty PressedProperty = DependencyProperty.Register("Pressed",
            typeof(bool), typeof(ShapesExporterViewModel), new PropertyMetadata(false));
        public static readonly DependencyProperty PopupOpenedProperty = DependencyProperty.Register("PopupOpened",
            typeof(bool), typeof(ShapesExporterViewModel), new PropertyMetadata(false));
        public static readonly DependencyProperty SelectedFileTypeProperty = DependencyProperty.Register("SelectedFileType",
            typeof(object), typeof(ShapesExporterViewModel), new PropertyMetadata(DefaultFileType, null, CoerceFileTypeChanged));

        static object CoerceFileTypeChanged(DependencyObject d, object baseValue) {
            ShapesExporterViewModel viewmodel = d as ShapesExporterViewModel;
            if(viewmodel == null)
                return DefaultFileType;
            ListBoxEditItem item = baseValue as ListBoxEditItem;
            return item == null ? viewmodel.SelectedFileType : item.Content;
        }

        public bool Pressed {
            get { return (bool)GetValue(PressedProperty); }
            set { SetValue(PressedProperty, value); }
        }
        public bool PopupOpened {
            get { return (bool)GetValue(PopupOpenedProperty); }
            set { SetValue(PopupOpenedProperty, value); }
        }
        public object SelectedFileType {
            get { return (object)GetValue(SelectedFileTypeProperty); }
            set { SetValue(SelectedFileTypeProperty, value); }
        }
    }
}
