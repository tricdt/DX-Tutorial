using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.DXEventDemo {
    public class BindingEventToMethodViewModel : BindableBase {
        public string State {
            get { return GetValue<string>(); }
            private set { SetValue(value); }
        }
        public void Initialize() {
            State = "Initialized";
        }
    }
}
