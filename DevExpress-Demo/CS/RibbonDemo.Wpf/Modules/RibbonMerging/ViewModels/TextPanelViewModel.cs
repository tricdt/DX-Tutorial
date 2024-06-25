using DevExpress.Mvvm.POCO;
using System.Windows;
using DevExpress.Mvvm.DataAnnotations;

namespace RibbonDemo {
    [POCOViewModel]
    public class TextPanelViewModel : PanelViewModel {
        protected TextPanelViewModel() { }
        public static TextPanelViewModel Create(string caption, Point location, RibbonMergingViewModel parentViewModel) {
            TextPanelViewModel instance = ViewModelSource.Create(() => new TextPanelViewModel());
            instance.ParentViewModel = parentViewModel;
            instance.Caption = caption;
            instance.Location = location;
            return instance;
        }
    }
}
