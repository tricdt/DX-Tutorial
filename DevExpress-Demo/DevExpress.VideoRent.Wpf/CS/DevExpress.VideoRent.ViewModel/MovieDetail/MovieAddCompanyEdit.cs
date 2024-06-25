using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieAddCompanyEdit : AddVRObjectEdit<MovieCompany> {
        CompanyEditData companyEditData;

        public MovieAddCompanyEdit(MovieAddCompanyEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public CompanyEditData CompanyEditData {
            get { return companyEditData; }
            private set { SetValue<CompanyEditData>("CompanyEditData", ref companyEditData, value, true); }
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            CompanyEditData = new CompanyEditData(AddVRObjectEditObject.VideoRentObject.Session);
        }
        protected override void DisposeManaged() {
            CompanyEditData = null;
            base.DisposeManaged();
        }
    }
}
