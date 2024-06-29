using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDX_Demo.Module.MVVM_Demo.POCOViewModelsDemo
{
    public class CommandsViewModel
    {
        public virtual bool CanExecuteSaveCommand { get; set; }
        public void Save()
        {
            MessageBox.Show("Action: Save");
        }
        public bool CanSave()
        {
            return CanExecuteSaveCommand;
        }

        public virtual string FileName { get; set; }
        public void Open(string fileName)
        {
            MessageBox.Show(string.Format("Action: Open {0}", fileName));
        }
        public bool CanOpen(string fileName)
        {
            return !string.IsNullOrEmpty(fileName);
        }

        protected CommandsViewModel()
        {
            CanExecuteSaveCommand = true;
        }
    }
}
