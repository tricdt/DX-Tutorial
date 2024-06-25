using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class CustomersViewOptionsEdit : ModuleObjectEdit {
        public CustomersViewOptionsEdit(CustomersViewOptionsEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) { }
        public CustomersViewOptionsEditObject CustomersViewOptionsEditObject { get { return (CustomersViewOptionsEditObject)EditObject; } }
    }
}
