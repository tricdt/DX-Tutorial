using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TreeListDemo {
    public class DataAnnotationsElement4 {

        [Display(Name = "Id", AutoGenerateField = false), Editable(false)]
        public int ID { get; set; }

        [Display(Name = "Parent Id", AutoGenerateField = false), Editable(false)]
        public int ParentID { get; set; }
        
        [Display(Name = "Category", GroupName = "Product Details"), Editable(false)]
        public CategoryData ProductCategory { get; set; }

        [Display(Name = "Name", GroupName = "Product Details"), Editable(false)]
        public string ProductName { get; set; }

        [Display(Name = "Customer", GroupName = "Order Details/Main"), Editable(false)]
        public string CustomerName { get; set; }

        [Display(Name = "Date", GroupName = "Order Details/Main")]
        public DateTime OrderDate { get; set; }

        [Display(GroupName = "Order Details/Status")]
        public int Quantity { get; set; }

        [Display(GroupName = "Order Details/Status"), DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Display(Name = "Is ready", GroupName = "Order Details/Status")]
        public bool IsReady { get; set; }

        public override string ToString() {
            return "Nested Bands Layout";
        }
    }
}
