using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class CompaniesEdit : VRObjectsEdit<Company> {
        CountryEditData countryEditData;
        CompanyTypeEditData companyTypeEditData;
        GridUIOptions gridUIOptions;

        public CompaniesEdit(CompaniesEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) {
            LayoutManager.Current.Subscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
        }
        public new CompaniesEditObject VRObjectsEditObject { get { return (CompaniesEditObject)EditObject; } }
        public CountryEditData CountryEditData {
            get { return countryEditData; }
            set { SetValue<CountryEditData>("CountryEditData", ref countryEditData, value); }
        }
        public CompanyTypeEditData CompanyTypeEditData {
            get { return companyTypeEditData; }
            set { SetValue<CompanyTypeEditData>("CompanyTypeEditData", ref companyTypeEditData, value); }
        }
        public GridUIOptions GridUIOptions {
            get { return gridUIOptions; }
            set { SetValue<GridUIOptions>("GridUIOptions", ref gridUIOptions, value); }
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            CountryEditData = new CountryEditData(VRObjectsEditObject.VideoRentObjects.Session);
            CompanyTypeEditData = new CompanyTypeEditData(VRObjectsEditObject.VideoRentObjects.Session);
        }
        protected override void DisposeManaged() {
            CountryEditData = null;
            CompanyTypeEditData = null;
            base.DisposeManaged();
        }
        void OnLayoutManagerAfterLoad(object sender, EventArgs e) {
            ViewModelLayoutData layoutData = ViewModelLayoutData.GetLayoutData();
            GridUIOptions = layoutData.CompaniesEditGridUIOptions;
        }
        void OnLayoutManagerBeforeSave(object sender, EventArgs e) { }
        #region Commands
        public Action<object> CommandGoToWebSite { get { return DoCommandGoToWebSite; } }
        void DoCommandGoToWebSite(object p) { ObjectHelper.ShowWebSite(CurrentRecord.WebSite); }
        #endregion
    }

}
