using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Mvvm.POCO;

namespace DialogsDemo {
    public abstract class BaseThemedWindowContentModel {
        public ThemedWindowViewModel RootViewModel { get { return this.GetParentViewModel<ThemedWindowViewModel>(); }}
    }
}
