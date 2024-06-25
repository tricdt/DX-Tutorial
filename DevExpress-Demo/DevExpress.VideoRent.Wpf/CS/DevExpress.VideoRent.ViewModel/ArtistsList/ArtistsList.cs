using System;
using System.Collections.Generic;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class ArtistsList : VRObjectsList<Artist> {
        ArtistPicturesEdit artistPicturesEdit;

        public ArtistsList(ArtistsListObject editObject)
            : base(editObject) {
            ListEdit = new ArtistsEdit(VRObjectsListObject.ArtistsEditObject, this);
        }
        public new ArtistsListObject VRObjectsListObject { get { return (ArtistsListObject)EditObject; } }
        public ArtistsEdit ArtistsEdit { get { return (ArtistsEdit)ListEdit; } }
        public ArtistsViewOptionsEdit ArtistsViewOptionsEdit { get { return (ArtistsViewOptionsEdit)ViewOptionsEdit; } }
        #region Edits
        public ArtistPicturesEdit ArtistPicturesEdit {
            get { return artistPicturesEdit; }
            set { SetValue<ArtistPicturesEdit>("ArtistPicturesEdit", ref artistPicturesEdit, value, true); }
        }
        protected override IEnumerable<ModuleObjectEdit> ModuleObjectEdits {
            get {
                List<ModuleObjectEdit> baseEdits = new List<ModuleObjectEdit>(base.ModuleObjectEdits);
                if(ArtistPicturesEdit != null)
                    baseEdits.Add(ArtistPicturesEdit);
                return baseEdits;
            }
        }
        #endregion
        protected override void OnListEditCurrentRecordChanged(object sender, EventArgs e) {
            base.OnListEditCurrentRecordChanged(sender, e);
            ArtistPicturesEdit = ArtistsEdit.CurrentRecord == null ? null : CreateArtistPicturesEdit();
        }
        protected override ModuleObjectDetailBase OpenDetailOverride(Guid? vroOid, object parameter) {
            return ModulesManager.Current.OpenModuleObjectDetail(new ArtistDetailObject(VRObjectsListObject.ArtistsEditObject.VideoRentObjects.Session, vroOid), true);
        }
        protected override ModuleObjectEdit CreateViewOptionsEditOverride() {
            return new ArtistsViewOptionsEdit(VRObjectsListObject.ArtistsViewOptionsEditObject, this);
        }
        ArtistPicturesEdit CreateArtistPicturesEdit() {
            ArtistPicturesEditObject editObject = VRObjectsListObject.GetArtistPicturesEditObject(ArtistsEdit.CurrentRecord.Oid);
            return ArtistPicturesEdit != null && ArtistPicturesEdit.EditObject == editObject ? ArtistPicturesEdit : new ArtistPicturesEdit(editObject, this);
        }
    }
}
