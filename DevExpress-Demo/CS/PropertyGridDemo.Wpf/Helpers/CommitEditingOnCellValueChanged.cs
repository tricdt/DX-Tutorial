using DevExpress.Xpf.Grid;
using DevExpress.Mvvm.UI.Interactivity;

namespace PropertyGridDemo {
    public class CommitEditingOnCellValueChanged : Behavior<GridViewBase> {
        protected override void OnAttached() {
            base.OnAttached();
            this.AssociatedObject.CellValueChanged += AssociatedObject_CellValueChanged;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            this.AssociatedObject.CellValueChanged -= AssociatedObject_CellValueChanged;
        }
        void AssociatedObject_CellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e) {
            var view = (DataViewBase)sender;
            view.CommitEditing();
        }
    }
}
