using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.PivotGrid;

namespace PivotGridDemo {
    public partial class SingleTotal : PivotGridDemoModule {
        public static IValueConverter Converter {
            get {
                return new DiscountFormatConverter();
            }
        }
        public SingleTotal() {
            InitializeComponent();
        }
        public class DiscountFormatConverter : IValueConverter {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
                if(value is FieldSummaryType) {
                    FieldSummaryType summaryType = (FieldSummaryType)value;
                    switch(summaryType) {
                        case FieldSummaryType.Sum:
                            return "N";
                        case FieldSummaryType.Max:
                        case FieldSummaryType.Min:
                        case FieldSummaryType.Average:
                        case FieldSummaryType.StdDev:
                        case FieldSummaryType.StdDevp:
                        case FieldSummaryType.Var:
                        case FieldSummaryType.Varp:
                            return "p";
                    }
                }
                return string.Empty;
            }
            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
                throw new NotImplementedException();
            }
        }
    }
}
