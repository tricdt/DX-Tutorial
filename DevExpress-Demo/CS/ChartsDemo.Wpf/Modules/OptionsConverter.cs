using System;
using System.Globalization;
using DevExpress.Xpf.Core;

namespace ChartsDemo {
    class OptionsConverter : ForwardOnlyValueConverter {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return null;
            DXTabItem tabItem = (DXTabItem)value;
            TabItemModule tabModule = (TabItemModule)tabItem.Content;
            if (tabModule.Options == null)
                return null;
            tabModule.Options.DataContext = tabModule.DataContext;
            return tabModule.Options;
        }
    }
}
