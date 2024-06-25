using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace MVVMDemo.POCOViewModels {
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class DataErrorInfoViewModel {
        [Required(ErrorMessage = "Please enter the user name.")]
        [StringLength(100, MinimumLength = 5)]
        public virtual string UserName { get; set; }

        public void Register() {
            MessageBox.Show("Registered");
        }
        public bool CanRegister() {
            return !IDataErrorInfoHelper.HasErrors(this);
        }
    }
}
