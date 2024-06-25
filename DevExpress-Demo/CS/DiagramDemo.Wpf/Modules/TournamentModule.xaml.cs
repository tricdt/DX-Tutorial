using System.Windows;
using DevExpress.Diagram.Core;
using DevExpress.Diagram.Core.Routing;
using DevExpress.Diagram.Demos;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Diagram;

namespace DiagramDemo {
    [CodeFile("Data/TournamentData.(cs)")]
    public partial class TournamentModule : DiagramDemoModule {
        public TournamentModule() {
            InitializeComponent();
            diagramControl.Controller.RegisterRoutingStrategy(ConnectorType.RightAngle, new RightAngleRoutingStrategy() { ItemMargin = (int)this.diagramControl.TreeLayoutHorizontalSpacing / 2 });
            DataContext = new TournamentViewModel();
        }
        void diagramControl_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e) {
            var item = this.diagramControl.CalcHitItem(el => e.GetPosition(el));
            var clickedItem = item as DiagramItem;
            if(clickedItem != null && !(clickedItem is DiagramContainer)) {
                var commandContainer = clickedItem.ParentItem as DiagramContainer;
                if(commandContainer != null) {
                    var resultShape = commandContainer.Items[2] as DiagramShape;
                    this.diagramControl.SelectItem(resultShape);
                    this.diagramControl.Commands.Execute(DiagramCommandsBase.EditCommand);
                }
            }
        }
        void diagramControl_ItemContentChanged(object sender, DiagramItemContentChangedEventArgs e) {
            int newValue = 0;
            if(!int.TryParse(e.NewValue, out newValue)) {
                MessageBox.Show("The value should be a number.", "Invalid value");
                var shape = e.Item as DiagramShape;
                if(shape != null)
                    shape.Content = e.OldValue;
            }
        }
    }
}
