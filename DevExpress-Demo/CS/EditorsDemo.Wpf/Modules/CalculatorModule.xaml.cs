using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using System.Windows.Threading;
using DevExpress.Xpf.Core.Native;

namespace EditorsDemo {
    
    
    
    public partial class CalculatorModule : EditorsDemoModule {
        public CalculatorModule() {
            InitializeComponent();
        }
        void CalculatorCustomErrorText(object sender, DevExpress.Xpf.Editors.CalculatorCustomErrorTextEventArgs e) {
            if (cbCustomErrorText.IsChecked == true)
                e.ErrorText += " !!!";
        }
        void ShowCalculatorWindow(object sender, RoutedEventArgs e) {
            Calculator calculator = new Calculator() { ShowBorder = false };
            new ThemedWindow() {Owner = WindowShowHelper.GetOwner(editor), Content = calculator, SizeToContent = SizeToContent.WidthAndHeight, Title = "Calculator", WindowStartupLocation = WindowStartupLocation.CenterOwner}.ShowDialog();
            calculator.Focus();
        }
        void Button_Click(object sender, RoutedEventArgs e) {
            calculator.ClearHistory();
        }
    }

    #region Move to Utils
    public class ObjectList : List<object> {
    }
    #endregion
}
