using System;
using System.Collections.Generic;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;

namespace DevExpress.VideoRent.ViewModel {
    public class CompaniesListObject : VRObjectsListObject<Company>, ICompaniesEditObjectParent, ICompanyMoviesEditObjectParent {
        Dictionary<Guid, CompanyMoviesEditObject> companyMoviesEditObjects = new Dictionary<Guid, CompanyMoviesEditObject>();

        public CompaniesListObject(Session session) : base(session) { }
        public override AllObjects<Company> GetVideoRentObjects() {
            return new AllObjects<Company>(Session, null, new SortProperty("[MoviesCount]", SortingDirection.Descending), new SortProperty("[Name]", SortingDirection.Ascending));
        }
        public Company GetVideoRentObject(Guid videoRentObjectOid) {
            return SessionHelper.GetObjectByKey<Company>(videoRentObjectOid, Session);
        }
        internal CompaniesEditObject CompaniesEditObject { get { return (CompaniesEditObject)ListEditObject; } }
        internal CompaniesViewOptionsEditObject CompaniesViewOptionsEditObject { get { return (CompaniesViewOptionsEditObject)ViewOptionsEditObject; } }
        protected override EditableSubobject CreateListEditObject() {
            return new CompaniesEditObject(this);
        }
        protected override EditableSubobject CreateViewOptionsEditObject() {
            return new CompaniesViewOptionsEditObject(this);
        }
        #region Subobjects
        internal CompanyMoviesEditObject GetCompanyMoviesEditObject(Guid companyOid) {
            CompanyMoviesEditObject editObject;
            if(!companyMoviesEditObjects.TryGetValue(companyOid, out editObject)) {
                editObject = new CompanyMoviesEditObject(this, companyOid);
                companyMoviesEditObjects.Add(companyOid, editObject);
            }
            return editObject;
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> list = new List<EditableSubobject>(base.Subobjects);
                foreach(CompanyMoviesEditObject companyMoviesEditObject in companyMoviesEditObjects.Values)
                    list.Add(companyMoviesEditObject);
                return list;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            CompanyMoviesEditObject companyMoviesEditObject = editableSubobject as CompanyMoviesEditObject;
            if(companyMoviesEditObject != null) {
                if(!companyMoviesEditObjects.ContainsKey(companyMoviesEditObject.VideoRentObjectOid)) return false;
                companyMoviesEditObjects.Remove(companyMoviesEditObject.VideoRentObjectOid);
                return true;
            }
            return false;
        }
        #endregion
    }
}
