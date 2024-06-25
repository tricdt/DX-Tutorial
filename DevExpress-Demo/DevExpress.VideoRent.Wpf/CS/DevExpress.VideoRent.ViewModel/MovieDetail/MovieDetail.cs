using System;
using System.Collections.Generic;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MovieDetail : VRObjectDetail<Movie> {
        MovieEdit movieEdit;
        MovieAddCompanyEdit movieAddCompanyEdit;
        MovieAddArtistEdit movieAddArtistEdit;
        MovieItemsEdit movieItemsEdit;
        MovieAddItemsEdit movieAddItemsEdit;

        public MovieDetail(MovieDetailObject editObject)
            : base(editObject) {
            MovieEdit = new MovieEdit(VRObjectDetailEditObject.MovieEditObject, this);
        }
        public new MovieDetailObject VRObjectDetailEditObject { get { return (MovieDetailObject)EditObject; } }
        #region Edits
        public MovieEdit MovieEdit {
            get { return movieEdit; }
            private set { SetValue<MovieEdit>("MovieEdit", ref movieEdit, value); }
        }
        public MovieAddCompanyEdit MovieAddCompanyEdit {
            get { return movieAddCompanyEdit; }
            private set { SetValue<MovieAddCompanyEdit>("MovieAddCompanyEdit", ref movieAddCompanyEdit, value); }
        }
        public MovieAddArtistEdit MovieAddArtistEdit {
            get { return movieAddArtistEdit; }
            private set { SetValue<MovieAddArtistEdit>("MovieAddArtistEdit", ref movieAddArtistEdit, value); }
        }
        public MovieItemsEdit MovieItemsEdit {
            get { return movieItemsEdit; }
            private set { SetValue<MovieItemsEdit>("MovieItemsEdit", ref movieItemsEdit, value); }
        }
        public MovieAddItemsEdit MovieAddItemsEdit {
            get { return movieAddItemsEdit; }
            private set { SetValue<MovieAddItemsEdit>("MovieAddItemsEdit", ref movieAddItemsEdit, value); }
        }
        protected override IEnumerable<ModuleObjectEdit> ModuleObjectEdits {
            get {
                List<ModuleObjectEdit> list = new List<ModuleObjectEdit>(base.ModuleObjectEdits);
                if(MovieAddCompanyEdit != null)
                    list.Add(MovieAddCompanyEdit);
                if(MovieAddArtistEdit != null)
                    list.Add(MovieAddArtistEdit);
                if(MovieEdit != null)
                    list.Add(MovieEdit);
                if(MovieItemsEdit != null)
                    list.Add(MovieItemsEdit);
                if(MovieAddItemsEdit != null)
                    list.Add(MovieAddItemsEdit);
                return list;
            }
        }
        #endregion
        public MovieAddCompanyEdit AddCompany() {
            MovieAddCompanyEdit = new MovieAddCompanyEdit(VRObjectDetailEditObject.MovieCompanyEditObject, this);
            MovieAddCompanyEdit.AfterDispose += OnMovieAddCompanyEditAfterDispose;
            return MovieAddCompanyEdit;
        }
        public MovieAddArtistEdit AddArtist() {
            MovieAddArtistEdit = new MovieAddArtistEdit(VRObjectDetailEditObject.MovieArtistEditObject, this);
            MovieAddArtistEdit.AfterDispose += OnMovieAddArtistEditAfterDispose;
            return MovieAddArtistEdit;
        }
        public MovieAddItemsEdit AddItems() {
            MovieAddItemsEdit = new MovieAddItemsEdit(VRObjectDetailEditObject.MovieAddItemsEditObject, this);
            MovieAddItemsEdit.AfterDispose += OnMovieAddItemsEditAfterDispose;
            return MovieAddItemsEdit;
        }
        public MovieItemsEdit OpenItems() {
            MovieItemsEdit = new MovieItemsEdit(VRObjectDetailEditObject.MovieItemsEditObject, this);
            MovieItemsEdit.AfterDispose += OnMovieItemsEditAfterDispose;
            return MovieItemsEdit;
        }
        protected override void OnEditObjectReloaded(object sender, EventArgs e) {
            base.OnEditObjectReloaded(sender, e);
            TitleDraft = VRObjectDetailEditObject.WasCreatedNewObject ? ConstStrings.Get("NewMovie") : VRObjectDetailEditObject.MovieEditObject.VideoRentObject.Title;
        }
        void OnMovieAddCompanyEditAfterDispose(object sender, EventArgs e) {
            MovieAddCompanyEdit = null;
        }
        void OnMovieAddArtistEditAfterDispose(object sender, EventArgs e) {
            MovieAddArtistEdit = null;
        }
        void OnMovieItemsEditAfterDispose(object sender, EventArgs e) {
            MovieItemsEdit = null;
        }
        void OnMovieAddItemsEditAfterDispose(object sender, EventArgs e) {
            MovieAddItemsEdit = null;
        }
        #region Commands
        public Action<object> CommandAddCompany { get { return DoCommandAddCompany; } }
        void DoCommandAddCompany(object p) { AddCompany(); }
        public Action<object> CommandAddArtist { get { return DoCommandAddArtist; } }
        void DoCommandAddArtist(object p) { AddArtist(); }
        public Action<object> CommandManageItems { get { return DoCommandManageItems; } }
        void DoCommandManageItems(object p) { OpenItems(); }
        public Action<object> CommandAddItems { get { return DoCommandAddItems; } }
        void DoCommandAddItems(object p) { AddItems(); }
        #endregion
    }
}
