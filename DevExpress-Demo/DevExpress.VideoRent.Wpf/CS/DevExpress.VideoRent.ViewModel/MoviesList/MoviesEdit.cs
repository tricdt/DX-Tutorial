using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MoviesEdit : VRObjectsEdit<Movie> {
        MovieGenreEditData movieGenreEditData;
        MovieRatingEditData movieRatingEditData;
        LanguageEditData languageEditData;
        CountryEditData countryEditData;
        GridView gridView;
        GridUIOptions gridUIOptions;
        int currentMoviePicturesCount;

        public MoviesEdit(MoviesEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) {
            LayoutManager.Current.Subscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
            MovieGenreEditData = new MovieGenreEditData();
            MovieRatingEditData = new MovieRatingEditData();
        }
        public new MoviesEditObject VRObjectsEditObject { get { return (MoviesEditObject)EditObject; } }
        public MovieGenreEditData MovieGenreEditData {
            get { return movieGenreEditData; }
            private set { SetValue<MovieGenreEditData>("MovieGenreEditData", ref movieGenreEditData, value); }
        }
        public MovieRatingEditData MovieRatingEditData {
            get { return movieRatingEditData; }
            private set { SetValue<MovieRatingEditData>("MovieRatingEditData", ref movieRatingEditData, value); }
        }
        public CountryEditData CountryEditData {
            get { return countryEditData; }
            private set { SetValue<CountryEditData>("CountryEditData", ref countryEditData, value, true); }
        }
        public LanguageEditData LanguageEditData {
            get { return languageEditData; }
            private set { SetValue<LanguageEditData>("LanguageEditData", ref languageEditData, value, true); }
        }
        public GridView GridView {
            get { return gridView; }
            set { SetValue<GridView>("GridView", ref gridView, value); }
        }
        public GridUIOptions GridUIOptions {
            get { return gridUIOptions; }
            set { SetValue<GridUIOptions>("GridUIOptions", ref gridUIOptions, value); }
        }
        public int CurrentMoviePicturesCount {
            get { return currentMoviePicturesCount; }
            set { SetValue<int>("CurrentMoviePicturesCount", ref currentMoviePicturesCount, value); }
        }
        protected override void RaiseCurrentRecordChangedOverride(Movie oldValue, Movie newValue) {
            base.RaiseCurrentRecordChangedOverride(oldValue, newValue);
            if(oldValue != null)
                oldValue.Pictures.ListChanged -= OnPicturesListChanged;
            if(newValue != null) {
                newValue.Pictures.ListChanged += OnPicturesListChanged;
                CurrentMoviePicturesCount = newValue.Pictures.Count;
            } else {
                currentMoviePicturesCount = 0;
            }
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            CountryEditData = new CountryEditData(VRObjectsEditObject.VideoRentObjects.Session);
            LanguageEditData = new LanguageEditData(VRObjectsEditObject.VideoRentObjects.Session);
        }
        protected override void DisposeManaged() {
            LayoutManager.Current.Unsubscribe(OnLayoutManagerAfterLoad, OnLayoutManagerBeforeSave);
            LanguageEditData = null;
            CountryEditData = null;
            base.DisposeManaged();
        }
        void OnPicturesListChanged(object sender, System.ComponentModel.ListChangedEventArgs e) {
            CurrentMoviePicturesCount = CurrentRecord == null ? 0 : CurrentRecord.Pictures.Count;
        }
        void OnLayoutManagerAfterLoad(object sender, EventArgs e) {
            ViewModelLayoutData layoutData = ViewModelLayoutData.GetLayoutData();
            GridView = layoutData.MoviesEditGridView;
            GridUIOptions = layoutData.MoviesEditGridUIOptions;
        }
        void OnLayoutManagerBeforeSave(object sender, EventArgs e) {
            ViewModelLayoutData layoutData = ViewModelLayoutData.GetLayoutData();
            layoutData.MoviesEditGridView = GridView;
        }
        #region Commands
        public Action<object> CommandGoToWebSite { get { return DoCommandGoToWebSite; } }
        void DoCommandGoToWebSite(object p) { ObjectHelper.ShowWebSite(CurrentRecord.WebSite); }
        #endregion
    }
}
