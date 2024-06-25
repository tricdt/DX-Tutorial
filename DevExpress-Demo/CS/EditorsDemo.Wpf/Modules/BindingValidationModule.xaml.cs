using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Windows;
using System.Windows.Data;

namespace EditorsDemo {
    [CodeFile("ViewModels/ValidationViewModelBase.(cs)")]
    [CodeFile("ViewModels/BindingValidationViewModel.(cs)")]
    public partial class BindingValidationModule : EditorsDemoModule {
        public BindingValidationModule() {
            InitializeComponent();
            DataContext = new BindingValidationViewModel();
        }
        void OnMailChanged(object sender, RoutedEventArgs e) {
            BindingExpression expression = BindingOperations.GetBindingExpression(txtConfirmMail, BaseEdit.EditValueProperty);
            if(expression != null)
                expression.UpdateTarget();
        }
        void OnConfirmMailChanged(object sender, RoutedEventArgs e) {
            BindingExpression expression = BindingOperations.GetBindingExpression(txtMail, BaseEdit.EditValueProperty);
            if(expression != null)
                expression.UpdateTarget();
        }
        void OnCardTypeChanged(object sender, RoutedEventArgs e) {
            BindingExpression expression = BindingOperations.GetBindingExpression(txtCardNumber, BaseEdit.EditValueProperty);
            if(expression != null)
                expression.UpdateTarget();
        }
        void OnClick(object sender, RoutedEventArgs e) {
            MessageBox.Show("Thank you!", "Joined", MessageBoxButton.OK);
        }
    }
}
