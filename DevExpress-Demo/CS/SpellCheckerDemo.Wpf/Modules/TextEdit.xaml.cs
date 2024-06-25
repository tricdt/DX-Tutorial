using System.Collections.Generic;
using System.Windows;

namespace SpellCheckerDemo {
    public partial class TextEdit : SpellCheckerDemoModule {
        public TextEdit() {
            InitializeComponent();
        }
        protected override List<FrameworkElement> CheckingElements { get { return new List<FrameworkElement>() { textEdit }; } }
    }
}
