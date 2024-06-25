using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Linq;

namespace GridDemo {
    public class ScrollBarAnnotationsModeConverter : BaseValueConverter {
        public override object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            var list = value as IEnumerable;
            if(list == null)
                return ScrollBarAnnotationMode.None;
            ScrollBarAnnotationMode result = ScrollBarAnnotationMode.None;
            foreach(var item in list.OfType<ValueSelectorItem>().Select(x => (ScrollBarAnnotationMode)x.Content))
                result |= item;
            return result;
        }
    }
}
