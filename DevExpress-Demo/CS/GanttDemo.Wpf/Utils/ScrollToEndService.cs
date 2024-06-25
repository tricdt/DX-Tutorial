using DevExpress.Mvvm.UI;
using System.Windows.Controls;

namespace GanttDemo {
    public interface IScrollToEndService {
        void ScrollToEnd();
    }
    public class ScrollToEndService : ServiceBase, IScrollToEndService {
        ScrollViewer ActualScrollViewer { get { return AssociatedObject as ScrollViewer; } }

        void IScrollToEndService.ScrollToEnd() {
            var scrollViewer = ActualScrollViewer;
            if(scrollViewer == null) return;
            scrollViewer.ScrollToEnd();
        }
    }
}
