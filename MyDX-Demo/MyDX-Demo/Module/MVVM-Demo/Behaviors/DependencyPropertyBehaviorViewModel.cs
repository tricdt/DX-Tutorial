using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDX_Demo.Module.MVVM_Demo.Behaviors
{
    public class DependencyPropertyBehaviorViewModel
    {
        public virtual string SelectedText { get; set; }
        public void ShowSelectedText()
        {
            MessageBox.Show(SelectedText);
        }
        public bool CanShowSelectedText()
        {
            return !string.IsNullOrEmpty(SelectedText);
        }
    }
}
