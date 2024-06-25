using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.Windows;

namespace MVVMDemo.Commands {
    public class AutoCommandsViewModel : ViewModelBase {
        public bool CanExecuteSaveCommand {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        #region !
        [Command]
        public void Save() {
            MessageBox.Show("Action: Save");
        }
        public bool CanSave() {
            return CanExecuteSaveCommand;
        } 
        #endregion

        public string FileName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        #region !
        [Command]
        public void Open(string fileName) {
            MessageBox.Show(string.Format("Action: Open {0}", fileName));
        }
        public bool CanOpen(string fileName) {
            return !string.IsNullOrEmpty(fileName);
        } 
        #endregion

        public AutoCommandsViewModel() {
            CanExecuteSaveCommand = true;
        }
    }
}
