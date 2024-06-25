using System;
using System.Collections.Generic;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public interface IMovieItemsEditObjectParent {
        Movie GetVideoRentObject(Guid videoRentObjectOid);
    }
    public sealed class MovieItemsEditObject : EditableSubobject {
        Guid movieOid;
        Movie movie;

        public MovieItemsEditObject(EditableObject parent, Guid movieOid)
            : base(parent) {
            this.movieOid = movieOid;
            Update();
        }
        public IMovieItemsEditObjectParent MovieItemsEditObjectParent { get { return (IMovieItemsEditObjectParent)Parent; } }
        public Guid MovieOid {
            get { return movieOid; }
            private set { SetValue<Guid>("MovieOid", ref movieOid, value); }
        }
        public Movie Movie {
            get { return movie; }
            private set { SetValue<Movie>("Movie", ref movie, value); }
        }
        protected override void UpdateOverride() {
            Movie = MovieItemsEditObjectParent.GetVideoRentObject(movieOid);
        }
        protected override void DisposeManaged() {
            Movie = null;
            base.DisposeManaged();
        }
    }
}
