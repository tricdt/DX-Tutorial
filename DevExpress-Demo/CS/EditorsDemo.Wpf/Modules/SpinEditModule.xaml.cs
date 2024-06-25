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
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors;
using System.Windows.Markup;
using EditorsDemo.Utils;

namespace EditorsDemo {
    public partial class SpinEditModule : EditorsDemoModule {
        public SpinEditModule() {
            InitializeComponent();
            InitSources();
        }
        void InitSources() {
            List<SpinStyle> styles = new List<SpinStyle>();
            styles.Add(SpinStyle.Vertical);
            styles.Add(SpinStyle.Horizontal);
            cboSpinStyle.ItemsSource = styles;
        }
        void cboSpinStyle_EditValueChanged(object sender, EditValueChangedEventArgs e) {
            ((SpinButtonInfo)editor.ActualButtons[0]).SpinStyle = (SpinStyle)e.NewValue;
        }
    }
}
