using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Bars;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
namespace BarsDemo {
    public class RootView : ContentControl {
        static RootView() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(RootView), new FrameworkPropertyMetadata(typeof(RootView)));
        }
        protected override void OnContentChanged(object oldContent, object newContent) {
            base.OnContentChanged(oldContent, newContent);
            (newContent as DependencyObject)
                .Do(x => BindingOperations.SetBinding(
                    x,
                    MergingProperties.ToolBarMergeStyleProperty,
                    new Binding() {
                        Path = new PropertyPath(MergingProperties.ToolBarMergeStyleProperty),
                        Source = this
                    }));
        }
    }
}
