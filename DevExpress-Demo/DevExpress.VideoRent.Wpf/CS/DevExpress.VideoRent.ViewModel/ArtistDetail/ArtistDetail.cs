using System;
using System.Collections.Generic;
using DevExpress.VideoRent.Resources;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class ArtistDetail : VRObjectDetail<Artist> {
        ArtistEdit artistEdit;
        ArtistAddMovieEdit artistAddMovieEdit;

        public ArtistDetail(ArtistDetailObject editObject)
            : base(editObject) {
            ArtistEdit = new ArtistEdit(VRObjectDetailEditObject.ArtistEditObject, this);
        }
        public new ArtistDetailObject VRObjectDetailEditObject { get { return (ArtistDetailObject)EditObject; } }
        #region Edits
        public ArtistEdit ArtistEdit {
            get { return artistEdit; }
            private set { SetValue<ArtistEdit>("ArtistEdit", ref artistEdit, value); }
        }
        public ArtistAddMovieEdit ArtistAddMovieEdit {
            get { return artistAddMovieEdit; }
            private set { SetValue<ArtistAddMovieEdit>("ArtistAddMovieEdit", ref artistAddMovieEdit, value); }
        }
        protected override IEnumerable<ModuleObjectEdit> ModuleObjectEdits {
            get {
                List<ModuleObjectEdit> list = new List<ModuleObjectEdit>(base.ModuleObjectEdits);
                if(ArtistAddMovieEdit != null)
                    list.Add(ArtistAddMovieEdit);
                if(ArtistEdit != null)
                    list.Add(ArtistEdit);
                return list;
            }
        }
        #endregion
        public void AddMovie() {
            ArtistAddMovieEdit = new ArtistAddMovieEdit(VRObjectDetailEditObject.ArtistMovieEditObject, this);
            ArtistAddMovieEdit.AfterDispose += OnArtistAddMovieEditAfterDispose;
        }
        protected override void OnEditObjectReloaded(object sender, EventArgs e) {
            base.OnEditObjectReloaded(sender, e);
            TitleDraft = VRObjectDetailEditObject.WasCreatedNewObject ? ConstStrings.Get("NewActor") : VRObjectDetailEditObject.ArtistEditObject.VideoRentObject.FullName;
        }
        void OnArtistAddMovieEditAfterDispose(object sender, EventArgs e) {
            ArtistAddMovieEdit = null;
        }
        #region Commands
        public Action<object> CommandAddMovie { get { return DoCommandAddMovie; } }
        void DoCommandAddMovie(object p) { AddMovie(); }
        #endregion
    }
}
