using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace MVVMDemo.Behaviors {
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class ValidationErrorsHostBehaviorViewModel {
        [Required]
        public virtual string FirstName { get; set; }
        [Required]
        public virtual string LastName { get; set; }

        public void Register() {
            MessageBox.Show("Registered");
        }
    }
}
