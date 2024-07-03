using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ViewModelInteractionDemo
{
    public class DetailPOCOViewModel:ISupportParameter
    {
        public virtual string DetailInfo { get; set; }

        object _Parameter;
        object ISupportParameter.Parameter
        {
            get { return _Parameter; }
            set
            {
                _Parameter = value;
                DetailInfo = string.Format("Detail for {0} item", _Parameter);
            }
        }
    }
}
