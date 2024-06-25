using System;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class CompanyEdit : VRObjectEdit<Company> {
        MovieEditData movieEditData;
        CompanyTypeEditData companyTypeEditData;
        CountryEditData countryEditData;
        MovieCompany currentMovie;

        public CompanyEdit(CompanyEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public new CompanyEditObject VRObjectEditObject { get { return (CompanyEditObject)EditObject; } }
        public CompanyTypeEditData CompanyTypeEditData {
            get { return companyTypeEditData; }
            private set { SetValue<CompanyTypeEditData>("CompanyTypeEditData", ref companyTypeEditData, value, true); }
        }
        public MovieEditData MovieEditData {
            get { return movieEditData; }
            private set { SetValue<MovieEditData>("MovieEditData", ref movieEditData, value, true); }
        }
        public CountryEditData CountryEditData {
            get { return countryEditData; }
            private set { SetValue<CountryEditData>("CountryEditData", ref countryEditData, value, true); }
        }
        public MovieCompany CurrentMovie {
            get { return currentMovie; }
            set { SetValue<MovieCompany>("CurrentMovie", ref currentMovie, value); }
        }
        public void DeleteCurrentMovie() {
            DeleteObject(CurrentMovie, string.Format(ConstStrings.Get("DeleteRecord"), CurrentMovie.Movie.Title));
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            MovieEditData = new MovieEditData(VRObjectEditObject.VideoRentObject.Session);
            CompanyTypeEditData = new CompanyTypeEditData(VRObjectEditObject.VideoRentObject.Session);
            CountryEditData = new CountryEditData(VRObjectEditObject.VideoRentObject.Session);
        }
        protected override void DisposeManaged() {
            CountryEditData = null;
            MovieEditData = null;
            CompanyTypeEditData = null;
            base.DisposeManaged();
        }
        #region Commands
        public Action<object> CommandGoToWebSite { get { return DoCommandGoToWebSite; } }
        void DoCommandGoToWebSite(object p) { ObjectHelper.ShowWebSite(VRObjectEditObject.VideoRentObject.WebSite); }
        public Action<object> CommandDeleteCurrentMovie { get { return DoCommandDeleteCurrentMovie; } }
        void DoCommandDeleteCurrentMovie(object p) { DeleteCurrentMovie(); }
        #endregion
    }
}
