using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.POCOViewModelsDemo
{
    public class PropertiesViewModel
    {
        public virtual string UserName { get; set; }
        protected void OnUserNameChanged(string oldValue)
        {
            ChangedStatus = string.Format("Old value: '{0}' New value: '{1}'", oldValue, UserName);
        }
        public virtual string ChangedStatus { get; protected set; }
    }
}
