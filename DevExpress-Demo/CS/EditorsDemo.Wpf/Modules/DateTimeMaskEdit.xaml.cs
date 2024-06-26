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
	
	
	
	public partial class DateTimeMaskEdit : EditorsDemoModule {
		public DateTimeMaskEdit() {
			InitializeComponent();
            Loaded += new RoutedEventHandler(DateTimeMaskEdit_Loaded);
		}
        void DateTimeMaskEdit_Loaded(object sender, RoutedEventArgs e) {
            full.Focus();
        }
		private void EditorGotFocus(object sender, RoutedEventArgs e) {
			mask.FocusedEditor = sender as TextEdit;
		}
	}
}
