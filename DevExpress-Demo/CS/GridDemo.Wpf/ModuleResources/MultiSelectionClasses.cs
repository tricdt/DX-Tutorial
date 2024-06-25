using DevExpress.DemoData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Data;

namespace GridDemo {
    public struct Range {
        public string Text { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public override string ToString() {
            return Text;
        }
    }
    public class ProductIdToProductNameConverter : IValueConverter {   
        static Dictionary<long, Product> products;

        static Dictionary<long, Product> Products {
            get {
                if(products == null)
                    products = NWindContext.Create().Products.ToDictionary(p => p.ProductID);
                return products;                
            }
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {    
            long id = (long)value;
            if(Products.ContainsKey(id)) {
                Product product = Products[id];
                return product != null ? product.ProductName : string.Empty;
            }     
            return string.Empty;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}
