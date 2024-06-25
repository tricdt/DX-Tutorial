using System;
using System.Windows.Markup;

namespace PivotGridDemo {
    public class Int32Extension : MarkupExtension {
        readonly int value;
        public Int32Extension(int value) {
            this.value = value;
        }
        public override object ProvideValue(IServiceProvider serviceProvider) {
            return value;
        }
    }
}
