using System;
using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class MoviesViewOptionsEdit : ModuleObjectEdit {
        public MoviesViewOptionsEdit(MoviesViewOptionsEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) { }
        public MoviesViewOptionsEditObject MoviesViewOptionsEditObject { get { return (MoviesViewOptionsEditObject)EditObject; } }
    }
}
