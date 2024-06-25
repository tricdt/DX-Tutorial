using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.DXCommandDemo {
    public class BindingCommandToMethodsViewModel : BindableBase {
        public bool CanExecuteSaveCommand {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }
        public void Save() {
            MessageBox.Show("Action: Save");
        }

        public string FileName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public void Open(string fileName) {
            MessageBox.Show(string.Format("Action: Open {0}", fileName));
        }

        public BindingCommandToMethodsViewModel() {
            CanExecuteSaveCommand = true;
        }

    }
}
