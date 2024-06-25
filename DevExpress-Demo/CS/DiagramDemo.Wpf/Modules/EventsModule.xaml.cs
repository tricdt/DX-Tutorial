using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Grid;

namespace DiagramDemo {
    [CodeFile("ViewModels/DiagramEventsInfo.(cs)")]
    [CodeFile("ViewModels/DiagramEventNode.(cs)")]
    [CodeFile("ViewModels/EventsViewModel.(cs)")]
    [CodeFile("Helpers/ScrollToEndService.(cs)")]
    public partial class EventsModule : DiagramDemoModule {
        public EventsModule() {
            InitializeComponent();
        }
    }
    public class EventNodeTemplateSelector : DataTemplateSelector {
        public DataTemplate EventTemplate { get; set; }
        public DataTemplate OtherTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            var node = ((GridCellData)item).RowData.Row as DiagramEventNode;
            return node != null && node.Kind == DiagramEventNodeKind.EventNode ? EventTemplate : OtherTemplate;
        }
    }
}
