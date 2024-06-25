using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.DXCommandDemo {
    public class ValuesFromControlsViewModel : BindableBase {
        public void Save() {
            MessageBox.Show("Action: Save");
        }

        public void Open(string fileName) {
            MessageBox.Show(string.Format("Action: Open {0}", fileName));
        }
        public bool CanOpen(string fileName) {
            return !string.IsNullOrEmpty(fileName);
        }
    }
}
