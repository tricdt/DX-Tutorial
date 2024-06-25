using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class CompaniesViewOptionsEdit : ModuleObjectEdit {
        public CompaniesViewOptionsEdit(CompaniesViewOptionsEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) { }
        public CompaniesViewOptionsEditObject CompaniesViewOptionsEditObject { get { return (CompaniesViewOptionsEditObject)EditObject; } }
    }
}
