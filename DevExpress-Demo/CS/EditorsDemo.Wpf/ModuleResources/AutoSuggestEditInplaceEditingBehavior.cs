using System;
using System.Linq;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Editors;

namespace EditorsDemo.ModuleResources {
    public class AutoSuggestEditInplaceEditingBehavior : Behavior<AutoSuggestEdit>{
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
