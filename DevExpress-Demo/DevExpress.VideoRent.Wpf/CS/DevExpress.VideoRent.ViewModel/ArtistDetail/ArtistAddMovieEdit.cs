using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class ArtistAddMovieEdit : AddVRObjectEdit<MovieArtist> {
        MovieEditData movieEditData;
        MovieArtistLineEditData movieArtistLineEditData;

        public ArtistAddMovieEdit(ArtistAddMovieEditObject editObject, ModuleObjectDetail detail) : base(editObject, detail) { }
        public MovieEditData MovieEditData {
            get { return movieEditData; }
            private set { SetValue<MovieEditData>("MovieEditData", ref movieEditData, value, true); }
        }
        public MovieArtistLineEditData MovieArtistLineEditData {
            get { return movieArtistLineEditData; }
            private set { SetValue<MovieArtistLineEditData>("MovieArtistLineEditData", ref movieArtistLineEditData, value, true); }
        }
        protected override void OnEditObjectUpdated(object sender, EventArgs e) {
            base.OnEditObjectUpdated(sender, e);
            MovieEditData = new MovieEditData(AddVRObjectEditObject.VideoRentObject.Session);
            MovieArtistLineEditData = new MovieArtistLineEditData(AddVRObjectEditObject.VideoRentObject.Session);
        }
        protected override void DisposeManaged() {
            MovieEditData = null;
            MovieArtistLineEditData = null;
            base.DisposeManaged();
        }
    }
}
