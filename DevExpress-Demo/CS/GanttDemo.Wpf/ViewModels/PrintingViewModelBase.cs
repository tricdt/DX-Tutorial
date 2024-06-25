using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GanttDemo {

    public abstract class PrintingViewModelBase : ViewModelBase {

        public IPrintableControl PrintableControl { get; set; }

        [Command]
        public virtual void ReOpenPreviewTabs() {
            var service = this.GetService<IDocumentManagerService>();
            foreach(var document in service.Documents.ToArray()) {
                OnTabClosed(document.Content);
                document.Close();
            }

            ShowPreviewInNewTab();
        }
        [Command]
        public virtual void ShowPreviewInNewTab() {
            Logger.Log("ShowPreviewInNewTab");
            var service = this.GetService<IDocumentManagerService>();
            var link = new PrintableControlLink(PrintableControl);
            link.CreateDocument(true);
            var doc = service.CreateDocument(link);
            doc.Title = "Print Preview";
            doc.DestroyOnClose = true;
            doc.Show();
        }
        public void OnTabClosed(object tabContent) {
            ((PrintableControlLink)tabContent).Dispose();
        }
    }

    public class PrintableTabBehavior : Behavior<DataViewBase> {

        public IPrintableControl PrintableControl {
            get { return (IPrintableControl)GetValue(PrintableControlProperty); }
            set { SetValue(PrintableControlProperty, value); }
        }

        public static readonly DependencyProperty PrintableControlProperty =
            DependencyProperty.Register("PrintableControl", typeof(IPrintableControl), typeof(PrintableTabBehavior), new PropertyMetadata(null));

        public ICommand ShowPreviewTab {
            get { return (ICommand)GetValue(ShowPreviewTabProperty); }
            set { SetValue(ShowPreviewTabProperty, value); }
        }

        public static readonly DependencyProperty ShowPreviewTabProperty =
            DependencyProperty.Register("ShowPreviewTab", typeof(ICommand), typeof(PrintableTabBehavior), new PropertyMetadata(null));

        protected override void OnAttached() {
            base.OnAttached();
            PrintableControl = AssociatedObject;
            AssociatedObject.Loaded += AssociatedObject_Loaded;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e) {
            Action action = () => {
                if(ShowPreviewTab != null)
                    ShowPreviewTab.Execute(null);
            };
            AssociatedObject.Dispatcher.BeginInvoke(action, System.Windows.Threading.DispatcherPriority.Loaded);
        }

        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.Loaded -= AssociatedObject_Loaded;
            PrintableControl = null;
        }
    }
}
