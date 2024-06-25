using DevExpress.DemoData.Models;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GridDemo {
    public class GridSummaryList : List<GridSummaryItem> { }
    public class NameTextControl : Control {
        public static readonly DependencyProperty NameValueProperty =
            DependencyProperty.Register("NameValue", typeof(string), typeof(NameTextControl), new PropertyMetadata(null));
        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register("TextValue", typeof(string), typeof(NameTextControl), new PropertyMetadata(null));
        public string NameValue {
            get { return (string)GetValue(NameValueProperty); }
            set { SetValue(NameValueProperty, value); }
        }
        public string TextValue {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }
        public NameTextControl() {
            this.SetDefaultStyleKey(typeof(NameTextControl));
        }
    }
    public class HintControl : ContentControl {
        public HintControl() {
            this.SetDefaultStyleKey(typeof(HintControl));
        }
    }
    public class AutoSuggestEditInplaceEditingBehavior : Behavior<AutoSuggestEdit> {
        readonly NWindContext context = NWindContext.Create();
        protected override void OnAttached() {
            base.OnAttached();
            AssociatedObject.QuerySubmitted += AssociatedObjectOnQuerySubmitted;
        }
        protected override void OnDetaching() {
            base.OnDetaching();
            AssociatedObject.QuerySubmitted -= AssociatedObjectOnQuerySubmitted;
        }
        void AssociatedObjectOnQuerySubmitted(object sender, AutoSuggestEditQuerySubmittedEventArgs e) {
            AssociatedObject.ItemsSource = string.IsNullOrEmpty(e.Text)
                ? this.context.Products.ToArray()
                : this.context.Products.Where(x => x.ProductName.StartsWith(e.Text)).ToArray();
        }
    }
}
