using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace EditorsDemo.FilterBehavior {
    public class FilterBehaviorCollecitonViewViewModel : ViewModelBase {
        public ICollectionView ProductsView { get; private set; }
        public List<string> Categories { get; private set; }

        public FilterBehaviorCollecitonViewViewModel() {
            var context = new NWindContext();
            var products = context.Products
                .Select(product => new {
                    product.ProductName,
                    product.Category.CategoryName,
                    product.Discontinued,
                    CategoryPicture = product.Category.Icon25,
                })
                .ToList();
            ProductsView = new ListCollectionView(products);
            Categories = context.Categories
                .Select(category => category.CategoryName)
                .ToList();
        }
    }
}
