using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if DXCORE3
using Microsoft.EntityFrameworkCore;
#else
using System.Data.Entity;
#endif

namespace EditorsDemo.FilterBehavior {
    public class FilterBehaviorListBoxEditViewModel : ViewModelBase {
        public IList<Product> Products { get; private set; }
        public List<string> Categories { get; private set; }

        public FilterBehaviorListBoxEditViewModel() {
            var context = new NWindContext();
            Products = context.Products
                .Include(x => x.Category)
                .ToList();
            Categories = context.Categories
                .Select(category => category.CategoryName)
                .ToList();
        }
    }
}
