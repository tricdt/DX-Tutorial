using DevExpress.Xpf.Scheduling;
using SchedulingDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace SchedulingDemo {
    public class ResourceNameToIdConverter : MarkupExtension, IValueConverter {
        IList<string> resources;

        public ResourceNameToIdConverter() {
            resources = WorkData.Calendars.Select(x => x.Name).ToList();            
        }

        public override object ProvideValue(IServiceProvider serviceProvider) { return this; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            IEnumerable<long> ids = value as IEnumerable<long>;
            if(ids != null) {
                List<string> names = ids.Select(x => resources[(int)x]).ToList();
                return names;
            }
            return new List<string>() { TimeRegionsDemoViewModel.AnyResource };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {            
            List<object> names = value as List<object>;
            if (names != null && (string)names[0] != TimeRegionsDemoViewModel.AnyResource) {
                IEnumerable<long> ids = names.Select(x => WorkData.Calendars.Where(v => v.Name == (string)x).FirstOrDefault().Id);
                return ids;
            }

            return null;
        }
    }
}
