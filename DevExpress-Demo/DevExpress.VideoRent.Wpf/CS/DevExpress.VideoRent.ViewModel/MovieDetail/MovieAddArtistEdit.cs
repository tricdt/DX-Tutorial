using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieAddArtistEdit : AddVRObjectEdit<MovieArtist> {
        ArtistEditData artistEditData;
        MovieArtistLineEditData movieArtistLineEditData;

        public MovieAddArtistEdit(MovieAddArtistEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public ArtistEditData ArtistEditData {
            get { return artistEditData; }
            private set { SetValue<ArtistEditData>("ArtistEditData", ref artistEditData, value, true); }
        }
        public MovieArtistLineEditData MovieArtistLineEditData {
            get { return movieArtistLineEditData; }
            private set { SetValue<MovieArtistLineEditData>("MovieArtistLineEditData", ref movieArtistLineEditData, value, true); }
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            ArtistEditData = new ArtistEditData(AddVRObjectEditObject.VideoRentObject.Session);
            MovieArtistLineEditData = new MovieArtistLineEditData(AddVRObjectEditObject.VideoRentObject.Session);
        }
        protected override void DisposeManaged() {
            ArtistEditData = null;
            MovieArtistLineEditData = null;
            base.DisposeManaged();
        }
    }

}
