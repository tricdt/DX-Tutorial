using DevExpress.Mvvm.POCO;
using System.Windows;
using DevExpress.Mvvm.DataAnnotations;

namespace RibbonDemo {
    [POCOViewModel]
    public class PaintPanelViewModel : PanelViewModel {
        protected PaintPanelViewModel() { }
        public static PaintPanelViewModel Create(string caption, Point location, RibbonMergingViewModel parentViewModel) {
            PaintPanelViewModel instance = ViewModelSource.Create(() => new PaintPanelViewModel());
            instance.Caption = caption;
            instance.Location = location;
            instance.ParentViewModel = parentViewModel;
            return instance;
        }
    }
}
