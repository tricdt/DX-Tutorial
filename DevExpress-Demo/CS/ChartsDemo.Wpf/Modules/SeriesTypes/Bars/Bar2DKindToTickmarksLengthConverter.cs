using System;
using System.Globalization;
using DevExpress.Xpf.Charts;

namespace ChartsDemo {
    class Bar2DKindToTickmarksLengthConverter : ForwardOnlyValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            Bar2DKind bar2DKind = value as Bar2DKind;
            if (bar2DKind != null) {
                switch (bar2DKind.Name) {
                    case "Glass Cylinder":
                        return 18;
                    case "Quasi-3D Bar":
                        return 9;
                }
            }
            return 5;
        }
    }
}
