using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MyDX_Demo.Module.MVVM_Demo.ViewModelBaseDemo
{
    public class ManualCommandsViewModel:ViewModelBase
    {
        public ICommand SaveCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }
        public bool CanExecuteSaveCommand
        {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public string FileName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public ManualCommandsViewModel()
        {
            CanExecuteSaveCommand = true;

            SaveCommand = new DelegateCommand(
                () => MessageBox.Show("Action: Save"),
                () => CanExecuteSaveCommand);

            OpenCommand = new DelegateCommand<string>(
                fileName => MessageBox.Show(string.Format("Action: Open {0}", FileName)),
                fileName => !string.IsNullOrEmpty(fileName));
        }
    }
}
