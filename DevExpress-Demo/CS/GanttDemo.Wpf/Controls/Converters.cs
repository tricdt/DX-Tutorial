using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GanttDemo {
    public class TabItemEventArgsToDataConverter : EventArgsConverterBase<TabControlTabRemovedEventArgs> {
        protected override object Convert(object sender, TabControlTabRemovedEventArgs args) {
            return ((DXTabItem)args.Item).DataContext;
        }
    }
}
