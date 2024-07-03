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
        public PersonInfo[] Persons { get; private set; }

        public void ShowPersonDetail(EnumMemberInfo person)
        {
            MessageBox.Show("HiHi "+ person);
        }
        protected EnumItemsSourceBehaviorViewModel()
        {
            Persons = PersonInfo.Persons;
        }
        public virtual UserRole SelectedRole { get; set; }
    }
}
