using DevExpress.Xpf.Grid;
using System.Windows;
using System.Windows.Controls;

namespace GanttDemo {
    public partial class Events : GanttDemoModule {
        public Events() {
            InitializeComponent();
        }
    }

    public class EventNodeTemplateSelector : DataTemplateSelector {
        public DataTemplate EventTemplate { get; set; }
        public DataTemplate OtherTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var node = ((GridCellData)item).RowData.Row as GanttEventNode;
            return node != null && node.Kind == GanttEventNodeKind.EventNode ? EventTemplate : OtherTemplate;
        }
    }
}
