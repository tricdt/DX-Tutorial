using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class CompanyMoviesEdit : ModuleObjectEdit {
        public CompanyMoviesEdit(CompanyMoviesEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public CompanyMoviesEditObject CompanyMoviesEditObject { get { return (CompanyMoviesEditObject)EditObject; } }
    }
}
