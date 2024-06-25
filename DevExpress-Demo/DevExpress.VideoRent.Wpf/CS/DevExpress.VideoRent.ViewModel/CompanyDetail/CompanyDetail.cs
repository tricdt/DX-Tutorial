using System;
using System.Collections.Generic;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class CompanyDetail : VRObjectDetail<Company> {
        CompanyEdit companyEdit;
        CompanyAddMovieEdit companyAddMovieEdit;

        public CompanyDetail(CompanyDetailObject editObject)
            : base(editObject) {
            CompanyEdit = new CompanyEdit(VRObjectDetailEditObject.CompanyEditObject, this);
        }
        public new CompanyDetailObject VRObjectDetailEditObject { get { return (CompanyDetailObject)EditObject; } }
        #region Edits
        public CompanyEdit CompanyEdit {
            get { return companyEdit; }
            private set { SetValue<CompanyEdit>("CompanyEdit", ref companyEdit, value); }
        }
        public CompanyAddMovieEdit CompanyAddMovieEdit {
            get { return companyAddMovieEdit; }
            private set { SetValue<CompanyAddMovieEdit>("CompanyAddMovieEdit", ref companyAddMovieEdit, value); }
        }
        protected override IEnumerable<ModuleObjectEdit> ModuleObjectEdits {
            get {
                List<ModuleObjectEdit> list = new List<ModuleObjectEdit>(base.ModuleObjectEdits);
                if(CompanyAddMovieEdit != null)
                    list.Add(CompanyAddMovieEdit);
                if(CompanyEdit != null)
                    list.Add(CompanyEdit);
                return list;
            }
        }
        #endregion
        public void AddMovie() {
            CompanyAddMovieEdit = new CompanyAddMovieEdit(VRObjectDetailEditObject.CompanyAddMovieEditObject, this);
            CompanyAddMovieEdit.AfterDispose += OnCompanyAddMovieEditAfterDispose;
        }
        protected override void OnEditObjectReloaded(object sender, EventArgs e) {
            base.OnEditObjectReloaded(sender, e);
            TitleDraft = VRObjectDetailEditObject.WasCreatedNewObject ? ConstStrings.Get("NewCompany") : VRObjectDetailEditObject.CompanyEditObject.VideoRentObject.Name;
        }
        void OnCompanyAddMovieEditAfterDispose(object sender, EventArgs e) {
            CompanyAddMovieEdit = null;
        }
        #region Commands
        public Action<object> CommandAddMovie { get { return DoCommandAddMovie; } }
        void DoCommandAddMovie(object p) { AddMovie(); }
        #endregion
    }
}
