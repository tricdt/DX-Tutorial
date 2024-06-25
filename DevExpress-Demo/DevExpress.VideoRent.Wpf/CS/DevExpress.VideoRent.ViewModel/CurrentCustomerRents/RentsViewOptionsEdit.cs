using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class RentsViewOptionsEdit : ModuleObjectEdit {
        public RentsViewOptionsEdit(RentsViewOptionsEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) { }
        public RentsViewOptionsEditObject RentsViewOptionsEditObject { get { return (RentsViewOptionsEditObject)EditObject; } }
    }
}
