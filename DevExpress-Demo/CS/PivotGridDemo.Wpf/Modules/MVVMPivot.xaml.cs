using DevExpress.Xpf.DemoBase;
using System.Windows;
using System.Windows.Controls;

namespace PivotGridDemo {
    [CodeFile("ViewModels/MVVMPivotViewModel.(cs)")]
    public partial class MVVMPivot : PivotGridDemoModule {
        public MVVMPivot() {
            InitializeComponent();
        }
    }
    #region TemplateSelector
    public class FormatConditionGeneratorTemplateSelector : DataTemplateSelector {
        public DataTemplate DataBarTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var formatConditionItem = item as FormatConditionItem;
            if(formatConditionItem != null) {
                switch(formatConditionItem.Type) {
                    case FormatConditionType.DataBar:
                        return DataBarTemplate;
                }
            }
            return base.SelectTemplate(item, container);
        }
    }
    #endregion
}
