using System;
using System.Collections.Generic;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class CompaniesList : VRObjectsList<Company> {
        CompanyMoviesEdit companyMoviesEdit;

        public CompaniesList(CompaniesListObject editObject)
            : base(editObject) {
            ListEdit = new CompaniesEdit(VRObjectsListObject.CompaniesEditObject, this);
        }
        public new CompaniesListObject VRObjectsListObject { get { return (CompaniesListObject)EditObject; } }
        public CompaniesEdit CompaniesEdit { get { return (CompaniesEdit)ListEdit; } }
        public CompaniesViewOptionsEdit CompaniesViewOptionsEdit { get { return (CompaniesViewOptionsEdit)ViewOptionsEdit; } }
        #region Edits
        public CompanyMoviesEdit CompanyMoviesEdit {
            get { return companyMoviesEdit; }
            set { SetValue<CompanyMoviesEdit>("CompanyMoviesEdit", ref companyMoviesEdit, value, true); }
        }
        protected override IEnumerable<ModuleObjectEdit> ModuleObjectEdits {
            get {
                List<ModuleObjectEdit> baseEdits = new List<ModuleObjectEdit>(base.ModuleObjectEdits);
                if(CompanyMoviesEdit != null)
                    baseEdits.Add(CompanyMoviesEdit);
                return baseEdits;
            }
        }
        #endregion
        protected override void OnListEditCurrentRecordChanged(object sender, EventArgs e) {
            base.OnListEditCurrentRecordChanged(sender, e);
            CompanyMoviesEdit = CompaniesEdit.CurrentRecord == null ? null : new CompanyMoviesEdit(VRObjectsListObject.GetCompanyMoviesEditObject(CompaniesEdit.CurrentRecord.Oid), this);
        }
        protected override ModuleObjectDetailBase OpenDetailOverride(Guid? companyOid, object parameter) {
            return ModulesManager.Current.OpenModuleObjectDetail(new CompanyDetailObject(VRObjectsListObject.CompaniesEditObject.VideoRentObjects.Session, companyOid), true);
        }
        protected override ModuleObjectEdit CreateViewOptionsEditOverride() {
            return new CompaniesViewOptionsEdit(VRObjectsListObject.CompaniesViewOptionsEditObject, this);
        }
    }
}
