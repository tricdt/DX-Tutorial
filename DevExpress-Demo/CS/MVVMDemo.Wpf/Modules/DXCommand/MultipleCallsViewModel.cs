using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.DXCommandDemo {
    public class MultipleCallsViewModel : BindableBase {
        public bool IsReadonly {
            get { return GetValue<bool>(); }
            set { SetValue(value); }
        }

        public string FileName {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }

        public void Open() {
            MessageBox.Show(string.Format("Action: Open {0}", FileName));
        }
        public bool CanOpen() {
            return !string.IsNullOrEmpty(FileName);
        }

        public void Clear() {
            FileName = null;
        }
    }
}
