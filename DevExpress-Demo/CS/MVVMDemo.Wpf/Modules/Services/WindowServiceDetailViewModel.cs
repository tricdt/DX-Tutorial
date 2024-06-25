using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace MVVMDemo.Services {
    public class WindowServiceDetailViewModel {
        public string CustomerName { get; set; }

        public void Register() {
            ICurrentWindowService service = this.GetRequiredService<ICurrentWindowService>();
            service.Close();
            MessageBox.Show("Registered");
        }
        public bool CanRegister() {
            return !string.IsNullOrEmpty(CustomerName);
        }

        public static WindowServiceDetailViewModel Create() {
            return ViewModelSource.Create(() => new WindowServiceDetailViewModel());
        }
        protected WindowServiceDetailViewModel() {

        }
    }
}
