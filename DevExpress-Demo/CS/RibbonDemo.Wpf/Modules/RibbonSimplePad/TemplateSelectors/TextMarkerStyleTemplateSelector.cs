using System.Windows;
using System.Windows.Controls;
namespace RibbonDemo {
    public class TextMarkerStyleTemplateSelector : DataTemplateSelector {
        public TemplateSelectorDictionary Dictionary { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            return Dictionary[((TextMarkerStyle)item).ToString()];
        }
    }    
}
