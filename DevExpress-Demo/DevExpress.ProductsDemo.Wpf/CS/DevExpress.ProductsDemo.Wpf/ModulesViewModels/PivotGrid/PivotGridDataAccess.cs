using DevExpress.Xpf.Charts;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ProductsDemo.Modules {
    [ValueConversion(typeof(Decimal), typeof(Int32))]
    public class DecimalConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            decimal date = (int)value;
            return date;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            int date = (int)((decimal)value);
            return date;
        }
    }

    public class SeriesTypeDescriptorRepository {
        public List<SeriesTypeDescriptor> SeriesTypeDescriptors {get; private set;}

        public SeriesTypeDescriptorRepository(){
            SeriesTypeDescriptors = SeriesTypes;
        }

        static readonly Type XYDiagram2DType = typeof(XYDiagram2D);
        static readonly Type XYDiagram3DType = typeof(XYDiagram3D);
        static readonly Type SimpleDiagram3DType = typeof(SimpleDiagram3D);
        static readonly Type SimpleDiagram2DType = typeof(SimpleDiagram2D);
        static readonly Type DefaultSeriesType = typeof(BarStackedSeries2D);

        static List<SeriesTypeDescriptor> seriesTypes;

        static List<SeriesTypeDescriptor> CreateSeriesTypes() {
            List<SeriesTypeDescriptor> seriesTypes = new List<SeriesTypeDescriptor>();
            seriesTypes.Add(new SeriesTypeDescriptor(typeof(AreaFullStackedSeries2D), XYDiagram2DType, "Area Full-Stacked Series 2D"));
            seriesTypes.Add(new SeriesTypeDescriptor(typeof(AreaSeries2D), XYDiagram2DType, "Area Series 2D"));
            seriesTypes.Add(new SeriesTypeDescriptor (typeof(AreaStackedSeries2D), XYDiagram2DType, "Area Stacked Series 2D"));
            seriesTypes.Add(new SeriesTypeDescriptor (typeof(BarFullStackedSeries2D), XYDiagram2DType, "Bar Full-Stacked Series 2D"));
            seriesTypes.Add(new SeriesTypeDescriptor( typeof(BarStackedSeries2D), XYDiagram2DType, "Bar Stacked Series 2D" ));
            seriesTypes.Add(new SeriesTypeDescriptor(typeof(LineSeries2D), XYDiagram2DType, "Line Series 2D"));
            seriesTypes.Add(new SeriesTypeDescriptor (typeof(PointSeries2D), XYDiagram2DType, "Point Series 2D"));

            seriesTypes.Add(new SeriesTypeDescriptor(typeof(AreaSeries3D), XYDiagram3DType, "Area Series 3D" ));
            seriesTypes.Add(new SeriesTypeDescriptor (typeof(AreaStackedSeries3D), XYDiagram3DType, "Area Stacked Series 3D"));
            seriesTypes.Add( new SeriesTypeDescriptor(typeof(AreaFullStackedSeries3D), XYDiagram3DType, "Area Full-Stacked Series 3D"));
            seriesTypes.Add(new SeriesTypeDescriptor (typeof(BarSeries3D), XYDiagram3DType, "Bar Series 3D"));
            seriesTypes.Add(new SeriesTypeDescriptor (typeof(PointSeries3D), XYDiagram3DType, "Point Series 3D"));
            seriesTypes.Add(new SeriesTypeDescriptor (typeof(PieSeries3D), SimpleDiagram3DType, "Pie Series 3D"));

            seriesTypes.Add(new SeriesTypeDescriptor (typeof(PieSeries2D),SimpleDiagram2DType, "Pie Series 2D"));
            seriesTypes.Sort(CompareComboItemsByStringContent);
            return seriesTypes;
        }
        static List<SeriesTypeDescriptor> SeriesTypes {
            get {
                if(seriesTypes == null)
                    seriesTypes = CreateSeriesTypes();
                return seriesTypes;
            }
        }


        static int CompareComboItemsByStringContent(SeriesTypeDescriptor first, SeriesTypeDescriptor second) {
            string firstStr = first.DisplayText as string;
            return firstStr == null ? -1 : firstStr.CompareTo(second.DisplayText);
        }
    }
}
