using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ViewModelInteractionDemo
{
    public class DetailViewModel:ViewModelBase
    {
        public string DetailInfo
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        protected override void OnParameterChanged(object parameter)
        {
            base.OnParameterChanged(parameter);
            DetailInfo = string.Format("Detail for {0} item", parameter);
        }
    }
}
