using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.DXCommandDemo {
    public class CommandParameterViewModel : BindableBase {
        public string FileName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public void Open(string fileName) {
            MessageBox.Show(string.Format("Action: Open {0}", fileName));
        }
        public bool CanOpen(string fileName) {
            return !string.IsNullOrEmpty(fileName);
        }
    }
}
