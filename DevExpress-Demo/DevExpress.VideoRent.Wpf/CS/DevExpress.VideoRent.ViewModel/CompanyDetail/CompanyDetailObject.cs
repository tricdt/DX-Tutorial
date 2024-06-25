using System;
using System.Collections.Generic;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public class CompanyDetailObject : VRObjectDetailObject<Company>, ICompanyEditObjectParent, ICompanyAddMovieEditObjectParent {        
        CompanyEditObject companyEditObject;
        CompanyAddMovieEditObject companyMovieEditObject;

        public CompanyDetailObject(Session session, Guid? artistOid) : base(session, artistOid) { }
        #region Subobjects
        internal CompanyEditObject CompanyEditObject {
            get {
                if(companyEditObject == null)
                    companyEditObject = new CompanyEditObject(this, VideoRentObjectOid);
                return companyEditObject;
            }
        }
        internal CompanyAddMovieEditObject CompanyAddMovieEditObject {
            get {
                if(companyMovieEditObject == null)
                    companyMovieEditObject = new CompanyAddMovieEditObject(this, VideoRentObjectOid);
                return companyMovieEditObject;
            }
        }
        internal override IEnumerable<EditableSubobject> Subobjects {
            get {
                List<EditableSubobject> list = new List<EditableSubobject>(base.Subobjects);
                if(companyEditObject != null)
                    list.Add(companyEditObject);
                if(companyMovieEditObject != null)
                    list.Add(companyMovieEditObject);
                return list;
            }
        }
        internal override bool ReleaseSubobject(EditableSubobject editableSubobject) {
            if(base.ReleaseSubobject(editableSubobject)) return true;
            if(editableSubobject == companyMovieEditObject) {
                companyMovieEditObject = null;
                return true;
            }
            if(editableSubobject == companyEditObject) {
                companyEditObject = null;
                return true;
            }
            return false;
        }
        #endregion
        protected override Company CreateNewObjectOverride() {
            return new Company(Session);
        }
    }
}
