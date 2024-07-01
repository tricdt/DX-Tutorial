using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using MyDX_Demo.Module.MVVM_Demo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ServicesDemo.NavigationService
{
    public class DetailViewModel:ISupportParameter
    {
        public virtual PersonInfo Person { get; set; }

        object _Parameter;
        object ISupportParameter.Parameter
        {
            get { return _Parameter; }
            set
            {
                _Parameter = value;
                Person = (PersonInfo)_Parameter;
            }
        }

        INavigationService NavigationService { get { return this.GetRequiredService<INavigationService>(); } }
        public void Back()
        {
            NavigationService.GoBack();
        }
    }
}
