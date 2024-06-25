using DevExpress.Xpf.Editors;
using System.Windows;
using System.Windows.Controls;

namespace EditorsDemo {
    public class BarCodeSymbologyTemplateSelector : DataTemplateSelector {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container) {
            FrameworkElement element = container as FrameworkElement;
            if(element == null || item == null || !(item is BarCodeStyleSettings))
                return base.SelectTemplate(item, container);
            return (DataTemplate)element.FindResource(((BarCodeStyleSettings)item).GeneratorBase.Name);
        }
    }
}
