using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyDX_Demo.Module.MVVM_Demo.ViewModelBaseDemo
{
    public class AutoCommandViewModel:ViewModelBase
    {
        public bool CanExecuteSaveCommand
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        [Command]
        public void Save()
        {
            MessageBox.Show("Action: Save");
        }
        public bool CanSave()
        {
            return CanExecuteSaveCommand;
        }

        public string FileName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        [Command]
        public void Open(string fileName)
        {
            MessageBox.Show(string.Format("Action: Open {0}", fileName));
        }
        public bool CanOpen(string fileName)
        {
            return !string.IsNullOrEmpty(fileName);
        }
        public AutoCommandViewModel()
        {
            CanExecuteSaveCommand = true;
        }
    }
}
