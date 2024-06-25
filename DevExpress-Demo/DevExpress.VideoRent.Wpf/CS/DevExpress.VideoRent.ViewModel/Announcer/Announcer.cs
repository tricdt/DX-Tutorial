using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
using DevExpress.Xpo;

namespace DevExpress.VideoRent.ViewModel {
    public class Announcer : ModuleObjectDetailBase {
        Session session = null;
        MoviesListObject moviesListObject;
        ArtistsListObject artistsListObject;
        MovieCategoriesListObject movieCategoriesListObject;
        CompaniesListObject companiesListObject;
        CurrentCustomerRentsDetailObject currentCustomerRentsDetailObject;
        CustomersListObject customersListObject;
        ModuleObjectDetailBase movieDetail;
        ModuleObjectDetailBase artistDetail;
        ModuleObjectDetailBase companyDetail;

        public Announcer(object session)
            : base(session) {
            this.session = session as Session;
        }
        #region Objects Cache
        MoviesListObject MoviesListObject {
            get {
                if(moviesListObject == null)
                    moviesListObject = new MoviesListObject(session);
                return moviesListObject;
            }
        }
        ArtistsListObject ArtistsListObject {
            get {
                if(artistsListObject == null)
                    artistsListObject = new ArtistsListObject(session);
                return artistsListObject;
            }
        }
        MovieCategoriesListObject MovieCategoriesListObject {
            get {
                if(movieCategoriesListObject == null)
                    movieCategoriesListObject = new MovieCategoriesListObject(session);
                return movieCategoriesListObject;
            }
        }
        CompaniesListObject CompaniesListObject {
            get {
                if(companiesListObject == null)
                    companiesListObject = new CompaniesListObject(session);
                return companiesListObject;
            }
        }
        CurrentCustomerRentsDetailObject CurrentCustomerRentsDetailObject {
            get {
                if(currentCustomerRentsDetailObject == null)
                    currentCustomerRentsDetailObject = new CurrentCustomerRentsDetailObject(session);
                return currentCustomerRentsDetailObject;
            }
        }
        CustomersListObject CustomersListObject {
            get {
                if(customersListObject == null)
                    customersListObject = new CustomersListObject(session);
                return customersListObject;
            }
        }
        ModuleObjectDetailBase MovieDetail {
            get {
                if(movieDetail == null)
                    movieDetail = OpenModule(MoviesListObject);
                return movieDetail;
            }
        }
        ModuleObjectDetailBase ArtistDetail {
            get {
                if(artistDetail == null)
                    artistDetail = OpenModule(ArtistsListObject);
                return artistDetail;
            }
        }
        ModuleObjectDetailBase CompanyDetail {
            get {
                if(companyDetail == null)
                    companyDetail = OpenModule(CompaniesListObject);
                return companyDetail;
            }
        }
        #endregion
        ModuleObjectDetailBase OpenModule(EditableObject editObject) {
            return OpenModule(editObject, true, null);
        }
        ModuleObjectDetailBase OpenModule(EditableObject editObject, bool focus, object tag) {
            return ModulesManager.Current.OpenModuleObjectDetail(editObject, focus, tag);
        }
        void OpenFirstRecordInList<T>(ModuleObjectDetailBase moduleBase) where T : VideoRentBaseObject {
            VRObjectsList<T> list = moduleBase as VRObjectsList<T>;
            list.OpenDetail(((VRObjectsEditObject<T>)list.ListEdit.EditObject).VideoRentObjects[0].Oid);
        }
        #region Commands
        public Action<object> CommandOpenMoviesList { get { return DoCommandOpenMoviesList; } }
        void DoCommandOpenMoviesList(object p) {
            OpenModule(MoviesListObject);
        }
        public Action<object> CommandOpenActorsList { get { return DoCommandOpenActorsList; } }
        void DoCommandOpenActorsList(object p) {
            OpenModule(ArtistsListObject);
        }
        public Action<object> CommandOpenMovieCategoriesList { get { return DoCommandOpenMovieCategoriesList; } }
        void DoCommandOpenMovieCategoriesList(object p) {
            OpenModule(MovieCategoriesListObject);
        }
        public Action<object> CommandOpenCompaniesList { get { return DoCommandOpenCompaniesList; } }
        void DoCommandOpenCompaniesList(object p) {
            OpenModule(CompaniesListObject);
        }
        public Action<object> CommandOpenCustomersList { get { return DoCommandOpenCustomersList; } }
        void DoCommandOpenCustomersList(object p) {
            OpenModule(CustomersListObject);
        }
        public Action<object> CommandOpenRentalsList { get { return DoCommandOpenRentalsList; } }
        void DoCommandOpenRentalsList(object p) {
            OpenModule(CurrentCustomerRentsDetailObject);
        }
        public Action<object> CommandOpenMovieDetail { get { return DoCommandOpenMovieDetail; } }
        void DoCommandOpenMovieDetail(object p) {
            OpenFirstRecordInList<Movie>(MovieDetail);
        }
        public Action<object> CommandOpenArtistDetail { get { return DoCommandOpenArtistDetail; } }
        void DoCommandOpenArtistDetail(object p) {
            OpenFirstRecordInList<Artist>(ArtistDetail);
        }
        public Action<object> CommandOpenCompanyDetail { get { return DoCommandOpenCompanyDetail; } }
        void DoCommandOpenCompanyDetail(object p) {
            OpenFirstRecordInList<Company>(CompanyDetail);
        }
        public Action<object> CommandLearnMore { get { return DoCommandLearnMore; } }
        void DoCommandLearnMore(object p) { ObjectHelper.ShowWebSite("http://www.devexpress.com/"); }
        public Action<object> CommandDownload { get { return DoCommandDownload; } }
        void DoCommandDownload(object p) { ObjectHelper.ShowWebSite(AssemblyInfo.DXLinkTrial); }
        public Action<object> CommandBuy { get { return DoCommandBuy; } }
        void DoCommandBuy(object p) { ObjectHelper.ShowWebSite(AssemblyInfo.DXLinkBuyNow); }
        #endregion

    }
}
