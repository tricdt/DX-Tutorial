using DevExpress.Mvvm;
using System.Windows;

namespace MVVMDemo.DXEventDemo {
    public class EventArgsViewModel : BindableBase {
        public void ShowPersonDetail(PersonInfo person) {
            if(person != null)
                MessageBox.Show(person.FullName, "Detail info");
        }
    }
}
