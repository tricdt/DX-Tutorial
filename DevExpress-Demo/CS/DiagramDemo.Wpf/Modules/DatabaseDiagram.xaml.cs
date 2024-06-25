using DevExpress.Data.Filtering;
using DevExpress.Diagram.Demos;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Diagram;
using DevExpress.Xpf.Diagram.Themes;
using System.Windows;
using System.Windows.Controls;
using ColumnDefinition = DevExpress.Diagram.Demos.ColumnDefinition;

namespace DiagramDemo {
    [CodeFile("ViewModels/DatabaseDiagramViewModel.(cs)")]
    [CodeFile("Data/DatabaseDiagramData.(cs)")]
    public partial class DatabaseDiagram : DiagramDemoModule {
        TableRelationEvaluationOperator evaluationOperator;
        public DatabaseDiagram() {
            evaluationOperator = new TableRelationEvaluationOperator();
            CriteriaOperator.RegisterCustomFunction(evaluationOperator);
            InitializeComponent();
        }
        ~DatabaseDiagram() {
            CriteriaOperator.UnregisterCustomFunction(evaluationOperator);
        }
        void OnItemsGenerated(object sender, DiagramItemsGeneratedEventArgs e) {
            diagramControl.FitToDrawing();
            diagramControl.ZoomFactor = 1d;
        }
        void OnPanelLoaded(object sender, RoutedEventArgs e) {
            var offset = GetPanelBottomRightOffset();
            var location = diagramPanel.GetBounds(diagramPanel).BottomRight;
            location.Offset(-panAndZoomGroup.FloatSize.Width - offset, -panAndZoomGroup.FloatSize.Height - offset);
            panAndZoomGroup.FloatLocation = location;
        }
        double GetPanelBottomRightOffset() {
            var key = new DiagramDesignerControlThemeKeysExtension() { ResourceKey = DiagramDesignerControlThemeKeys.PanZoomBottomRightOffset };
            return (double)diagramControl.FindResource(key);
        }
    }

    public class DatabaseDiagramItemTemplateSelector : DataTemplateSelector {
        public DataTemplate TableTemplate { get; set; }
        public DataTemplate ColumnTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            if(item is TableDefinition)
                return TableTemplate;
            else if(item is ColumnDefinition)
                return ColumnTemplate;
            return base.SelectTemplate(item, container);
        }
    }
}
