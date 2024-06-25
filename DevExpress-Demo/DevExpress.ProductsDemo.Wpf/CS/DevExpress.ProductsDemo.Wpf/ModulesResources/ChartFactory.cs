using DevExpress.Xpf.Charts;
using DevExpress.Xpf.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsDemo {
    internal static class ChartFactory {
  
        public static Diagram GenerateDiagram(Type seriesType, Type diagramType, bool? showPointsLabels) {
            Series seriesTemplate = CreateSeriesInstance(seriesType);
            Diagram diagram = CreateDiagramBySeriesType(diagramType);
            if(diagram is XYDiagram2D)
                PrepareXYDiagram2D(diagram as XYDiagram2D);
            if(diagram is XYDiagram3D)
                PrepareXYDiagram3D(diagram as XYDiagram3D);
            if(diagram is Diagram3D)
                ((Diagram3D)diagram).RuntimeRotation = true;
            diagram.SeriesDataMember = "Series";
            seriesTemplate.ArgumentDataMember = "Arguments";
            seriesTemplate.ValueDataMember = "Values";
            if(seriesTemplate.Label == null)
                seriesTemplate.Label = new SeriesLabel();
            seriesTemplate.LabelsVisibility = showPointsLabels.Value == true;
            if(seriesTemplate is PieSeries2D || seriesTemplate is PieSeries3D) {
                seriesTemplate.LegendTextPattern = "{A}";
                seriesTemplate.Label.TextPattern = "{A}: {VP:P0}";
            } else {
                seriesTemplate.LegendTextPattern = "{A}: {V}";
                seriesTemplate.ShowInLegend = true;
            }
            diagram.SeriesTemplate = seriesTemplate;
            return diagram;
        }
        static void PrepareXYDiagram2D(XYDiagram2D diagram) {
            if(diagram == null) return;
            diagram.AxisX = new AxisX2D();
            diagram.AxisX.Label = new AxisLabel();
            diagram.AxisX.Label.Staggered = true;
        }
        static void PrepareXYDiagram3D(XYDiagram3D diagram) {
            if(diagram == null) return;
            diagram.AxisX = new AxisX3D();
            diagram.AxisX.Label = new AxisLabel();
            diagram.AxisX.Label.Visible = false;
        }
        public static Series CreateSeriesInstance(Type seriesType) {
            Series series = (Series)Activator.CreateInstance(seriesType);
            ISupportTransparency supportTransparency = series as ISupportTransparency;
            if(supportTransparency != null) {
                bool flag = series is AreaSeries2D;
                flag = flag || series is AreaSeries3D;
                if(flag)
                    supportTransparency.Transparency = 0.4;
                else
                    supportTransparency.Transparency = 0;
            }
            return series;
        }
        static Diagram CreateDiagramBySeriesType(Type diagramType) {
            return (Diagram)Activator.CreateInstance(diagramType);
        }
    }
}
