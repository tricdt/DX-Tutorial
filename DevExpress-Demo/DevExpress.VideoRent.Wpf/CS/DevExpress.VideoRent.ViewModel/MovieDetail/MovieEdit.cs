using System;
using System.Collections.Generic;
using System.Windows.Media;
using DevExpress.VideoRent.Helpers;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieEdit : VRObjectEdit<Movie> {
        MovieGenreEditData movieGenreEditData;
        MovieRatingEditData movieRatingEditData;
        CountryEditData countryEditData;
        List<MovieFormatInfo> formatTabsItemsSource;
        List<RentalsChartSourceItem> rentalsChartSource;
        List<FormatsChartSourceItem> formatsChartSource;
        CompanyEditData companyEditData;
        MovieArtistLineEditData movieArtistLineEditData;
        ArtistEditData artistEditData;
        LanguageEditData languageEditData;
        MovieCategoryEditData movieCategoryEditData;
        MovieCompany currentCompany;
        MovieArtist currentArtist;
        string lastRentedOn;
        MovieCategoryDetail newCategoryDetail;

        public MovieEdit(MovieEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) {
            MovieGenreEditData = new MovieGenreEditData();
            MovieRatingEditData = new MovieRatingEditData();
        }
        public new MovieEditObject VRObjectEditObject { get { return (MovieEditObject)EditObject; } }
        public CountryEditData CountryEditData {
            get { return countryEditData; }
            private set { SetValue<CountryEditData>("CountryEditData", ref countryEditData, value, true); }
        }
        public CompanyEditData CompanyEditData {
            get { return companyEditData; }
            private set { SetValue<CompanyEditData>("CompanyEditData", ref companyEditData, value, true); }
        }
        public ArtistEditData ArtistEditData {
            get { return artistEditData; }
            private set { SetValue<ArtistEditData>("ArtistEditData", ref artistEditData, value, true); }
        }
        public MovieArtistLineEditData MovieArtistLineEditData {
            get { return movieArtistLineEditData; }
            private set { SetValue<MovieArtistLineEditData>("MovieArtistLineEditData", ref movieArtistLineEditData, value, true); }
        }
        public LanguageEditData LanguageEditData {
            get { return languageEditData; }
            private set { SetValue<LanguageEditData>("LanguageEditData", ref languageEditData, value, true); }
        }
        public MovieCategoryEditData MovieCategoryEditData {
            get { return movieCategoryEditData; }
            private set { SetValue<MovieCategoryEditData>("MovieCategoryEditData", ref movieCategoryEditData, value, true); }
        }
        public MovieGenreEditData MovieGenreEditData {
            get { return movieGenreEditData; }
            private set { SetValue<MovieGenreEditData>("MovieGenreEditData", ref movieGenreEditData, value); }
        }
        public MovieRatingEditData MovieRatingEditData {
            get { return movieRatingEditData; }
            private set { SetValue<MovieRatingEditData>("MovieRatingEditData", ref movieRatingEditData, value); }
        }
        public string LastRentedOn {
            get { return lastRentedOn; }
            private set { SetValue<string>("LastRentedOn", ref lastRentedOn, value); }
        }
        public List<MovieFormatInfo> FormatTabsItemsSource {
            get { return formatTabsItemsSource; }
            private set { SetValue<List<MovieFormatInfo>>("FormatTabsItemsSource", ref formatTabsItemsSource, value); }
        }
        public List<RentalsChartSourceItem> RentalsChartSource {
            get { return rentalsChartSource; }
            private set { SetValue<List<RentalsChartSourceItem>>("RentalsChartSource", ref rentalsChartSource, value); }
        }
        public List<FormatsChartSourceItem> FormatsChartSource {
            get { return formatsChartSource; }
            private set { SetValue<List<FormatsChartSourceItem>>("FormatsChartSource", ref formatsChartSource, value); }
        }
        public MovieCompany CurrentCompany {
            get { return currentCompany; }
            set { SetValue<MovieCompany>("CurrentCompany", ref currentCompany, value); }
        }
        public MovieArtist CurrentArtist {
            get { return currentArtist; }
            set { SetValue<MovieArtist>("CurrentArtist", ref currentArtist, value); }
        }
        public MovieCategoryDetail OpenNewCategoryDialog() {
            if(this.newCategoryDetail != null) return this.newCategoryDetail;
            AllObjects<MovieCategory>.Set.Updated += OnMovieCategorySetUpdated;
            this.newCategoryDetail = (MovieCategoryDetail)ModulesManager.Current.OpenModuleObjectDetail(new MovieCategoryDetailObject(VRObjectEditObject.VideoRentObject.Session, null), true);
            this.newCategoryDetail.AfterDispose += OnNewCategoryDetailAfterDispose;
            return this.newCategoryDetail;
        }
        protected override void OnEditObjectBeforeUpdate(object sender, EventArgs e) {
            VRObjectEditObject.VideoRentObject.Items.ListChanged -= OnItemsListChanged;
            base.OnEditObjectBeforeUpdate(sender, e);
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            CountryEditData = new CountryEditData(VRObjectEditObject.VideoRentObject.Session);
            CompanyEditData = new CompanyEditData(VRObjectEditObject.VideoRentObject.Session);
            ArtistEditData = new ArtistEditData(VRObjectEditObject.VideoRentObject.Session);
            MovieArtistLineEditData = new MovieArtistLineEditData(VRObjectEditObject.VideoRentObject.Session);
            LanguageEditData = new LanguageEditData(VRObjectEditObject.VideoRentObject.Session);
            MovieCategoryEditData = new MovieCategoryEditData(VRObjectEditObject.VideoRentObject.Session);
            VRObjectEditObject.VideoRentObject.Items.ListChanged += OnItemsListChanged;
            UpdateLastRentedDate();
            UpdateFormatTabsSource();
        }
        protected override void DisposeManaged() {
            if(VRObjectEditObject.VideoRentObject != null)
                VRObjectEditObject.VideoRentObject.Items.ListChanged -= OnItemsListChanged;
            CountryEditData = null;
            CompanyEditData = null;
            ArtistEditData = null;
            MovieArtistLineEditData = null;
            LanguageEditData = null;
            MovieCategoryEditData = null;
            base.DisposeManaged();
        }
        void UpdateLastRentedDate() {
            MovieItem.CountInfo countInfo = new MovieItem.CountInfo(VRObjectEditObject.VideoRentObject.Items);
            LastRentedOn = countInfo.LastRentedOn == null ? string.Empty : countInfo.LastRentedOn.Value.ToString();
        }
        void UpdateFormatTabsSource() {
#if SL
            ImagesSourceHelper.Current.RequestDiskIcons(UpdateSources);
#else
            UpdateSources(ImagesSourceHelper.Current.DiskIcons);
#endif
        }
        void UpdateSources(List<ImageSource> discIcons) {
            if(VRObjectEditObject.VideoRentObject == null) return;
            MovieItemFormat[] formats = EnumHelper.GetValues<MovieItemFormat>();
            MovieItemFormat?[] formatsAndAll = new MovieItemFormat?[formats.Length + 1];
            for(int i = 0; i < formats.Length; ++i) formatsAndAll[i] = formats[i];
            formatsAndAll[formatsAndAll.Length - 1] = null;
            MovieItem.CountInfo countInfo = new MovieItem.CountInfo(VRObjectEditObject.VideoRentObject.Items);
            List<MovieFormatInfo> formatInfos = new List<MovieFormatInfo>();
            List<RentalsChartSourceItem> rentals = new List<RentalsChartSourceItem>();
            for(int i = 0; i < formatsAndAll.Length; ++i) {
                MovieCategoryPrice categoryPrice = formatsAndAll[i] == null ? null : VRObjectEditObject.VideoRentObject.Category.GetPrice(formatsAndAll[i].Value);
                ImageSource formatImage = i == formatsAndAll.Length - 1 ? null : discIcons[i];
                MovieFormatInfo info = new MovieFormatInfo(formatsAndAll[i], countInfo, categoryPrice, formatImage);
                formatInfos.Add(info);
                rentals.Add(new RentalsChartSourceItem(info.FormatText, info.FormatDetailsDictionary));
            }
            UpdateSourcesFields(countInfo, formatInfos, rentals);
        }
        void UpdateSourcesFields(MovieItem.CountInfo countInfo, List<MovieFormatInfo> formatInfos, List<RentalsChartSourceItem> rentals) {
            FormatTabsItemsSource = formatInfos;
            RentalsChartSource = rentals;
            UpdateFormatsChartSource(countInfo);
        }
        void UpdateFormatsChartSource(MovieItem.CountInfo countInfo) {
            List<FormatsChartSourceItem> formatsSerieSource = new List<FormatsChartSourceItem>();
            MovieItemFormat[] formats = EnumHelper.GetValues<MovieItemFormat>();
            for(int i = 0; i < formats.Length; i++)
                formatsSerieSource.Add(new FormatsChartSourceItem(EnumTitlesKeeper<MovieItemFormat>.GetTitle(formats[i]), countInfo.WithFormat[i]));
            FormatsChartSource = formatsSerieSource;
        }
        void OnNewCategoryDetailAfterDispose(object sender, EventArgs e) {
            this.newCategoryDetail = null;
            AllObjects<MovieCategory>.Set.Updated -= OnMovieCategorySetUpdated;
        }
        void OnMovieCategorySetUpdated(object sender, EditableObjectEventArgs e) {
            if(this.newCategoryDetail == null || e.UpdatedObject != this.newCategoryDetail.EditObject) return;
            Guid categoryOid = (Guid)e.UpdatedObject.Key;
            VRObjectEditObject.VideoRentObject.Category = SessionHelper.GetObjectByKey<MovieCategory>(categoryOid, VRObjectEditObject.VideoRentObject.Session);
        }
        void OnItemsListChanged(object sender, System.ComponentModel.ListChangedEventArgs e) {
            UpdateFormatTabsSource();
        }
        void DeleteCurrentCompany() {
            DeleteObject(CurrentCompany, string.Format(ConstStrings.Get("DeleteRecord"), CurrentCompany.Company.Name));
        }
        void DeleteCurrentArtist() {
            DeleteObject(CurrentArtist, string.Format(ConstStrings.Get("DeleteRecord"), CurrentArtist.Artist.FullName));
        }
        #region Commands
        public Action<object> CommandGoToWebSite { get { return DoCommandGoToWebSite; } }
        void DoCommandGoToWebSite(object p) { ObjectHelper.ShowWebSite(VRObjectEditObject.VideoRentObject.WebSite); }
        public Action<object> CommandAddNewCategory { get { return DoCommandAddNewCategory; } }
        void DoCommandAddNewCategory(object p) { OpenNewCategoryDialog(); }
        public Action<object> CommandDeleteCurrentCompany { get { return DoCommandDeleteCurrentCompany; } }
        void DoCommandDeleteCurrentCompany(object p) { DeleteCurrentCompany(); }
        public Action<object> CommandDeleteCurrentArtist { get { return DoCommandDeleteCurrentArtist; } }
        void DoCommandDeleteCurrentArtist(object p) { DeleteCurrentArtist(); }
        #endregion
    }
}
