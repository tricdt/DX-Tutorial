using System.Windows;
using System.Collections.Generic;

namespace SpellCheckerDemo {
    public partial class TextBox : SpellCheckerDemoModule {
        public TextBox() {
            InitializeComponent();
        }
        protected override List<FrameworkElement> CheckingElements { get { return new List<FrameworkElement>() { tb }; } }
    }
}
