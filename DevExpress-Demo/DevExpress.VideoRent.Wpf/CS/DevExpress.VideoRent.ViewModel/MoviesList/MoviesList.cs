using System;
using System.Collections.Generic;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MoviesList : VRObjectsList<Movie> {
        MoviePicturesEdit moviePicturesEdit;

        public MoviesList(MoviesListObject editObject)
            : base(editObject) {
            ListEdit = new MoviesEdit(VRObjectsListObject.MoviesEditObject, this);
        }
        public new MoviesListObject VRObjectsListObject { get { return (MoviesListObject)EditObject; } }
        public MoviesEdit MoviesEdit { get { return (MoviesEdit)ListEdit; } }
        public MoviesViewOptionsEdit MoviesViewOptionsEdit { get { return (MoviesViewOptionsEdit)ViewOptionsEdit; } }
        #region Edits
        public MoviePicturesEdit MoviePicturesEdit {
            get { return moviePicturesEdit; }
            set { SetValue<MoviePicturesEdit>("MoviePicturesEdit", ref moviePicturesEdit, value, true); }
        }
        protected override IEnumerable<ModuleObjectEdit> ModuleObjectEdits {
            get {
                List<ModuleObjectEdit> baseEdits = new List<ModuleObjectEdit>();
                if(MoviePicturesEdit != null)
                    baseEdits.Add(MoviePicturesEdit);
                baseEdits.AddRange(base.ModuleObjectEdits);
                return baseEdits;
            }
        }
        #endregion
        protected override void OnListEditCurrentRecordChanged(object sender, EventArgs e) {
            base.OnListEditCurrentRecordChanged(sender, e);
            MoviePicturesEdit = MoviesEdit.CurrentRecord == null ? null : CreateMoviePicturesEdit();
        }
        protected override ModuleObjectDetailBase OpenDetailOverride(Guid? vroOid, object parameter) {
            return ModulesManager.Current.OpenModuleObjectDetail(new MovieDetailObject(VRObjectsListObject.MoviesEditObject.VideoRentObjects.Session, vroOid), true);
        }
        protected override ModuleObjectEdit CreateViewOptionsEditOverride() {
            return new MoviesViewOptionsEdit(VRObjectsListObject.MoviesViewOptionsEditObject, this);
        }
        MoviePicturesEdit CreateMoviePicturesEdit() {
            MoviePicturesEditObject editObject = VRObjectsListObject.GetMoviePicturesEditObject(MoviesEdit.CurrentRecord.Oid);
            return MoviePicturesEdit != null && MoviePicturesEdit.EditObject == editObject ? MoviePicturesEdit : new MoviePicturesEdit(editObject, this);
        }
    }
}
