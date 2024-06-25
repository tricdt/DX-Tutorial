using System;
using DevExpress.Xpf.Core;
using ProductsDemo.Modules;

namespace ProductsDemo.Controls {
    public partial class EditTaskWindow : ThemedWindow {
        public EditTaskWindow() {
            InitializeComponent();
        }

        void ThemedWindow_Closed(object sender, EventArgs e) {
            ((EditTaskViewModel)DataContext).Close();
        }
    }
}
