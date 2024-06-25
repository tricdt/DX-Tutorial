using DevExpress.Mvvm.UI;
using System.Windows;
namespace RibbonDemo {
    public interface ICloseWindowService {
        void Close();
    }
    public class CloseWindowService : ServiceBase, ICloseWindowService {
        public void Close() {
            if((AssociatedObject is Window))
                ((Window)AssociatedObject).Close();
        }
    }
}
