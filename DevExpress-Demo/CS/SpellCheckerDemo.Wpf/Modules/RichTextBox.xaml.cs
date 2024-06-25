using System.Windows;
using System.Collections.Generic;

namespace SpellCheckerDemo {
    public partial class RichTextBox : SpellCheckerDemoModule {
        public RichTextBox() {
            InitializeComponent();
        }
        protected override List<FrameworkElement> CheckingElements { get { return new List<FrameworkElement>() { richTextBox }; } }
    }
}
