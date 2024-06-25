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

namespace EditorsDemo {
	
	
	
	public partial class NumericMaskEdit : EditorsDemoModule {
		public NumericMaskEdit() {
			InitializeComponent();
            Loaded += new RoutedEventHandler(NumericMaskEdit_Loaded);
		}
        void NumericMaskEdit_Loaded(object sender, RoutedEventArgs e) {
            editor.Focus();
        }
		private void EditorGotFocus(object sender, RoutedEventArgs e) {
			mask.FocusedEditor = sender as TextEdit;
		}

	}
}
