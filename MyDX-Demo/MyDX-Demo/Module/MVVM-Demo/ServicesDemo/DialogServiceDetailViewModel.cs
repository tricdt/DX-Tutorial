using DevExpress.Mvvm.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo
{
    public class DialogServiceDetailViewModel
    {
        public string CustomerName { get; set; }

        public static DialogServiceDetailViewModel Create()
        {
            return ViewModelSource.Create(() => new DialogServiceDetailViewModel());
        }
        protected DialogServiceDetailViewModel()
        {

        }
    }
}
