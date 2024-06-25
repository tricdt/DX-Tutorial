using DevExpress.Xpf.Editors;
using System.Windows.Controls;

namespace DockingDemo {
    public class CodeViewPresenter : DevExpress.Xpf.DemoBase.Helpers.Internal.CodeViewPresenter { }
    public class AutoScrollableTextEdit : TextEdit {
        public AutoScrollableTextEdit() {
            EditValueChanged += AutoScrollableTextEdit_EditValueChanged;
        }
        private void AutoScrollableTextEdit_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            Dispatcher.BeginInvoke(new System.Action(() => {
                ScrollToEnd();
            }));
        }
        public void ScrollToEnd() {
            if(EditCore != null)
                ((TextBox)this.EditCore).ScrollToEnd();
        }
    }
}
