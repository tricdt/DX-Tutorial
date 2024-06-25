using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using System.Windows;

namespace MVVMDemo.Services {
    public class WindowServiceViewModel {
        public void ShowRegistrationForm() {
            WindowServiceDetailViewModel detailViewModel = WindowServiceDetailViewModel.Create();

            IWindowService service = this.GetRequiredService<IWindowService>();
            service.Title = "Registration Form";
            service.Show(viewModel: detailViewModel);
        }
    }
}
