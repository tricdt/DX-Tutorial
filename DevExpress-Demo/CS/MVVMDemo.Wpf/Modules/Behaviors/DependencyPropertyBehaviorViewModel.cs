using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace MVVMDemo.Behaviors {
    public class DependencyPropertyBehaviorViewModel {
        public virtual string SelectedText { get; set; }
        public void ShowSelectedText() {
            MessageBox.Show(SelectedText);
        }
        public bool CanShowSelectedText() {
            return !string.IsNullOrEmpty(SelectedText);
        }
    }
}
