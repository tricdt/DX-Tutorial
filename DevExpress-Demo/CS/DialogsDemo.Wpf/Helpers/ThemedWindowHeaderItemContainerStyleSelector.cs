using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;

namespace DialogsDemo {
    public class ThemedWindowHeaderItemContainerStyleSelector : StyleSelector {
        public override Style SelectStyle(object item, DependencyObject container) { return SelectStyle(item as BaseHeaderItemModel, container as HeaderItemControl) ?? base.SelectStyle(item, container); }
        protected Style SelectStyle(BaseHeaderItemModel item, HeaderItemControl container) { return container.TryFindResource(item.ResourceKey) as Style; }
    }
}
