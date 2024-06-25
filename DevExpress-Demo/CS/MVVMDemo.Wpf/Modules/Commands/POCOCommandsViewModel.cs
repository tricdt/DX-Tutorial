using System.Windows;

namespace MVVMDemo.Commands {
    public class POCOCommandsViewModel {
        public virtual bool CanExecuteSaveCommand { get; set; }
        public void Save() {
            MessageBox.Show("Action: Save");
        }
        public bool CanSave() {
            return CanExecuteSaveCommand;
        }

        public virtual string FileName { get; set; }
        public void Open(string fileName) {
            MessageBox.Show(string.Format("Action: Open {0}", fileName));
        }
        public bool CanOpen(string fileName) {
            return !string.IsNullOrEmpty(fileName);
        }

        protected POCOCommandsViewModel() {
            CanExecuteSaveCommand = true;
        }
    }
}
