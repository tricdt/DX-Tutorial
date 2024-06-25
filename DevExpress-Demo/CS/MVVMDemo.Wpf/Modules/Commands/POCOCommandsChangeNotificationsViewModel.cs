using DevExpress.Mvvm.POCO;
using System.Windows;

namespace MVVMDemo.Commands {
    public class POCOCommandsChangeNotificationsViewModel {
        public virtual bool CanExecuteSaveCommand { get; set; }

        #region !
        protected void OnCanExecuteSaveCommandChanged() {
            this.RaiseCanExecuteChanged(x => x.Save());
        }
        #endregion

        public void Save() {
            MessageBox.Show("Action: Save");
        }
        public bool CanSave() {
            return CanExecuteSaveCommand;
        }

        protected POCOCommandsChangeNotificationsViewModel() {
            CanExecuteSaveCommand = true;
        }
    }
}
