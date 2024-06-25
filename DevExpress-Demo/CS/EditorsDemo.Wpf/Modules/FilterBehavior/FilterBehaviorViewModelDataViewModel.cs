using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.Core.FilteringUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EditorsDemo.FilterBehavior {
    public class FilterBehaviorViewModelDataViewModel : ViewModelBase {
        public List<ProductInfo> Products {
            get { return GetValue<List<ProductInfo>>(); }
            private set { SetValue(value); }
        }
        public int ProductsCount {
            get { return GetValue<int>(); }
            private set { SetValue(value); }
        }
        public List<string> Categories { get; private set; }

        public FilterBehaviorViewModelDataViewModel() {
            var context = new NWindContext();
            Categories = context.Categories
                .Select(category => category.CategoryName)
                .ToList();
        }

        [Command]
        public void ShowProducts() {
            Products = GetProductsQuery().ToList();
        }

        internal IList<CountedValue> GetCategoryUniqueValues() {
            return GetProductsQueryCore()
                .GroupBy(x => x.CategoryName)
                .Select(x => new { x.Key, Count = x.Count() })
                .ToList()
                .Select(x => new CountedValue(x.Key, x.Count))
                .ToList();
        }

        Expression<Func<ProductInfo, bool>> filterExpression;
        internal void SetFilter(Expression<Func<ProductInfo, bool>> filterExpression) {
            this.filterExpression = filterExpression;
            ProductsCount = GetProductsQuery().Count();
        }

        IQueryable<ProductInfo> GetProductsQuery() {
            if(filterExpression == null)
                return Enumerable.Empty<ProductInfo>().AsQueryable();
            return GetProductsQueryCore().Where(filterExpression);
        }

        static IQueryable<ProductInfo> GetProductsQueryCore() {
            var context = new NWindContext();
            return context.Products
                .Select(product => new ProductInfo {
                    ProductName = product.ProductName,
                    CategoryName = product.Category.CategoryName,
                    Discontinued = product.Discontinued,
                    CategoryPicture = product.Category.Icon25,
                });
        }
    }
}
