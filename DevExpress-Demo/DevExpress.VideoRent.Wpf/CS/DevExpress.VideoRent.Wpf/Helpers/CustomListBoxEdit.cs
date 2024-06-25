using System;
using DevExpress.Xpf.Editors;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class CustomListBoxEdit : ListBoxEdit {
        protected override void OnIsReadOnlyChanged() {
            base.OnIsReadOnlyChanged();
            if(IsReadOnly) {
                IsHitTestVisible = false;
            } else {
                IsHitTestVisible = true;
            }
        }
    }
}
