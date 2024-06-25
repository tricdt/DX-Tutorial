using DevExpress.Xpf.DemoBase;
using System.Windows;

namespace EditorsDemo {
    [CodeFile("ViewModels/ValidationViewModelBase.(cs)")]
    [CodeFile("ViewModels/InputValidationViewModel.(cs)")]
    public partial class InputValidationModule : EditorsDemoModule {
        public InputValidationModule() {
            InitializeComponent();
        }
        void OnGotFocus(object sender, RoutedEventArgs e) {
            settings.DataContext = sender;
        }
        void OnCardTypeChanged(object sender, RoutedEventArgs e) {
            if(txtCardNumber != null)
                txtCardNumber.DoValidate();
        }
        void OnMailChanged(object sender, RoutedEventArgs e) {
            if(txtConfirmMail != null)
                txtConfirmMail.DoValidate();
        }
    }
}
