using System;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;
#if SL
using DevExpress.Xpf.Drawing;
#else
using System.Drawing;
#endif

namespace DevExpress.VideoRent.ViewModel {
    public class ArtistEdit : VRObjectEdit<Artist> {
        PersonGenderEditData personGenderEditData;
        LanguageEditData languageEditData;
        MovieEditData movieEditData;
        MovieArtistLineEditData movieArtistLineEditData;
        CountryEditData countryEditData;
        MovieArtist currentMovie;
        Picture currentPicture;

        public ArtistEdit(ArtistEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) {
            PersonGenderEditData = new PersonGenderEditData();
        }
        public new ArtistEditObject VRObjectEditObject { get { return (ArtistEditObject)EditObject; } }
        public PersonGenderEditData PersonGenderEditData {
            get { return personGenderEditData; }
            private set { SetValue<PersonGenderEditData>("PersonGenderEditData", ref personGenderEditData, value); }
        }
        public LanguageEditData LanguageEditData {
            get { return languageEditData; }
            private set { SetValue<LanguageEditData>("LanguageEditData", ref languageEditData, value, true); }
        }
        public MovieEditData MovieEditData {
            get { return movieEditData; }
            private set { SetValue<MovieEditData>("MovieEditData", ref movieEditData, value, true); }
        }
        public MovieArtistLineEditData MovieArtistLineEditData {
            get { return movieArtistLineEditData; }
            private set { SetValue<MovieArtistLineEditData>("MovieArtistLineEditData", ref movieArtistLineEditData, value, true); }
        }
        public CountryEditData CountryEditData {
            get { return countryEditData; }
            private set { SetValue<CountryEditData>("CountryEditData", ref countryEditData, value, true); }
        }
        public Picture CurrentPicture {
            get { return currentPicture; }
            set { SetValue<Picture>("CurrentPicture", ref currentPicture, value); }
        }
        public MovieArtist CurrentMovie {
            get { return currentMovie; }
            set { SetValue<MovieArtist>("CurrentMovie", ref currentMovie, value); }
        }
        public void DeleteCurrentMovie() {
            DeleteObject(CurrentMovie, string.Format(ConstStrings.Get("DeleteRecord"), CurrentMovie.Movie.Title));
        }
        public void AddPicture() {
            Image newImage = OpenFileDialog.View.OpenImage();
            if(newImage == null) return;
            VRObjectEditObject.VideoRentObject.AddPicture(newImage);
        }
        public void DeleteCurrentPicture() {
            DeleteObject(CurrentPicture, ConstStrings.Get("DeletePictureMessage"));
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            MovieEditData = new MovieEditData(VRObjectEditObject.VideoRentObject.Session);
            LanguageEditData = new LanguageEditData(VRObjectEditObject.VideoRentObject.Session);
            MovieArtistLineEditData = new MovieArtistLineEditData(VRObjectEditObject.VideoRentObject.Session);
            CountryEditData = new CountryEditData(VRObjectEditObject.VideoRentObject.Session);
        }
        protected override void DisposeManaged() {
            CountryEditData = null;
            MovieEditData = null;
            LanguageEditData = null;
            MovieArtistLineEditData = null;
            base.DisposeManaged();
        }
        #region Commands
        public Action<object> CommandGoToWebSite { get { return DoCommandGoToWebSite; } }
        void DoCommandGoToWebSite(object p) { ObjectHelper.ShowWebSite(VRObjectEditObject.VideoRentObject.Link); }
        public Action<object> CommandAddPicture { get { return DoCommandAddPicture; } }
        void DoCommandAddPicture(object p) { AddPicture(); }
        public Action<object> CommandDeleteCurrentPicture { get { return DoCommandDeleteCurrentPicture; } }
        void DoCommandDeleteCurrentPicture(object p) { DeleteCurrentPicture(); }
        public Action<object> CommandDeleteCurrentMovie { get { return DoCommandDeleteCurrentMovie; } }
        void DoCommandDeleteCurrentMovie(object p) { DeleteCurrentMovie(); }
        #endregion
    }
}
