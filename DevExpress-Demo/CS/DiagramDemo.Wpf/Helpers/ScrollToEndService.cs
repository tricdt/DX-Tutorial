using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using DevExpress.Mvvm.UI;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Editors;

namespace DiagramDemo {
    public interface IScrollToEndService {
        void ScrollToEnd();
    }
    public class ScrollToEndService : ServiceBase, IScrollToEndService {
        public static readonly DependencyProperty ScrollViewerProperty = DependencyProperty.Register("ScrollViewer", typeof(ScrollViewer), typeof(ScrollToEndService), new PropertyMetadata(null));
        public ScrollViewer ScrollViewer {
            get { return (ScrollViewer)GetValue(ScrollViewerProperty); }
            set { SetValue(ScrollViewerProperty, value); }
        }
        ScrollViewer ActualScrollViewer { get { return ScrollViewer ?? AssociatedObject as ScrollViewer; } }

        void IScrollToEndService.ScrollToEnd() {
            var scrollViewer = ActualScrollViewer;
            if(scrollViewer == null) return;
            scrollViewer.ScrollToEnd();
        }
    }
}
