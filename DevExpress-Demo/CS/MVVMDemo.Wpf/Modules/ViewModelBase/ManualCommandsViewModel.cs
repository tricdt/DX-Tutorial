using DevExpress.Mvvm;
using System.Windows;
using System.Windows.Input;

namespace MVVMDemo.ViewModelBaseDemo {
    public class ManualCommandsViewModel : ViewModelBase {
        public ICommand SaveCommand { get; private set; }
        public ICommand OpenCommand { get; private set; }

        #region !
        public ManualCommandsViewModel() {
            CanExecuteSaveCommand = true;

            SaveCommand = new DelegateCommand(
                () => MessageBox.Show("Action: Save"),
                () => CanExecuteSaveCommand);

            OpenCommand = new DelegateCommand<string>(
                fileName => MessageBox.Show(string.Format("Action: Open {0}", FileName)),
                fileName => !string.IsNullOrEmpty(fileName));
        } 
        #endregion

        public bool CanExecuteSaveCommand {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public string FileName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

    }
}
