using DevExpress.Mvvm;
using MyDX_Demo.Module.MVVM_Demo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDX_Demo.Module.MVVM_Demo.Behaviors
{
    public class EnumItemsSourceBehaviorViewModel
    {
        public void Edit(EnumMemberInfo person)
        {
            MessageBox.Show("Xin chao " + person.Name);
        }
    }
}
