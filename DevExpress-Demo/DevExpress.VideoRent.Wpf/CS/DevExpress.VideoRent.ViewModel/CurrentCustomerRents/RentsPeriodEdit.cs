using DevExpress.VideoRent.ViewModel.ViewModelBase;

namespace DevExpress.VideoRent.ViewModel {
    public class RentsPeriodEdit : ModuleObjectEdit {
        RentsPeriodEditData rentsPeriodEditData;

        public RentsPeriodEdit(RentsPeriodEditObject editObject, ModuleObjectDetail detail)
            : base(editObject, detail) {
            RentsPeriodEditData = new RentsPeriodEditData();
        }
        public RentsPeriodEditObject RentsViewOptionsEditObject { get { return (RentsPeriodEditObject)EditObject; } }
        public RentsPeriodEditData RentsPeriodEditData {
            get { return rentsPeriodEditData; }
            set { SetValue<RentsPeriodEditData>("RentsPeriodEditData", ref rentsPeriodEditData, value); }
        }
    }
}
