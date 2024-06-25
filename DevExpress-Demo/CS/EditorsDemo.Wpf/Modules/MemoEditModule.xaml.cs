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
using DevExpress.Xpf.DemoBase.DataClasses;

namespace EditorsDemo {
    public partial class MemoEditModule : EditorsDemoModule {
        public MemoEditModule() {
            InitializeComponent();
            editor.EditValue = @"MemoEdit is a multi-line text editor. In addition to the advanced text input features derived from the TextEdit control, it offers numerous options for multi-line text management.
- Optional ENTER and TAB key processing.
- Customizable visibility for vertical and horizontal scrollbars.
- Optional text word-wrapping.";
            InitSources();
            DataContext = CarsData.DataSource;
        }
        void InitSources() {
            TextWrapping[] wrapping = new TextWrapping[] { TextWrapping.Wrap, TextWrapping.NoWrap, TextWrapping.WrapWithOverflow };
            lbTextWrapping.ItemsSource = wrapping;

            ScrollBarVisibility[] scrollVisibilities = new ScrollBarVisibility[] { ScrollBarVisibility.Auto, ScrollBarVisibility.Disabled, ScrollBarVisibility.Hidden, ScrollBarVisibility.Visible};
            cbHorizontalScroll.ItemsSource = scrollVisibilities;
            cbVerticalScroll.ItemsSource = scrollVisibilities;
        }
    }
}
