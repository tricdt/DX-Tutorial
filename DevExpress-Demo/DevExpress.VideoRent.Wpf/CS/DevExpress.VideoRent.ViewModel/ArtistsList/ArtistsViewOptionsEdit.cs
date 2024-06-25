using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class ArtistsViewOptionsEdit : ModuleObjectEdit {
        public ArtistsViewOptionsEdit(ArtistsViewOptionsEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) { }
        public ArtistsViewOptionsEditObject ArtistsViewOptionsEditObject { get { return (ArtistsViewOptionsEditObject)EditObject; } }
    }
}
