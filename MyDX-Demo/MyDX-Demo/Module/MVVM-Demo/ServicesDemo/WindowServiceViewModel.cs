using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class WindowServiceViewModel
    {
        public void ShowRegistrationForm()
        {
            WindowServiceDetailViewModel detailViewModel = WindowServiceDetailViewModel.Create();

            IWindowService service = this.GetRequiredService<IWindowService>();
            service.Title = "Registration Form";
            service.Show(viewModel: detailViewModel);
        }
    }
}
