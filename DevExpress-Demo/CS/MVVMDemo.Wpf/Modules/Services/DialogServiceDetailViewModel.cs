using DevExpress.Mvvm.POCO;

namespace MVVMDemo.Services {
    public class DialogServiceDetailViewModel {
        public string CustomerName { get; set; }

        public static DialogServiceDetailViewModel Create() {
            return ViewModelSource.Create(() => new DialogServiceDetailViewModel());
        }
        protected DialogServiceDetailViewModel() {

        }
    }
}
