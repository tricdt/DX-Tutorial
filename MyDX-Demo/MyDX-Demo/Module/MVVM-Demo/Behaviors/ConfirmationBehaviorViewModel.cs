using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDX_Demo.Module.MVVM_Demo.Behaviors
{
    public class ConfirmationBehaviorViewModel
    {
        public void Register()
        {
            MessageBox.Show("Registered");
        }
    }
}
