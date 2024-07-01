using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class WindowServiceDetailViewModel
    {
        public string CustomerName { get; set; }

        public void Register()
        {
            ICurrentWindowService service = this.GetRequiredService<ICurrentWindowService>();
            service.Close();
            MessageBox.Show("Registered");
        }
        public bool CanRegister()
        {
            return !string.IsNullOrEmpty(CustomerName);
        }

        public static WindowServiceDetailViewModel Create()
        {
            return ViewModelSource.Create(() => new WindowServiceDetailViewModel());
        }
        protected WindowServiceDetailViewModel()
        {

        }
    }
}
