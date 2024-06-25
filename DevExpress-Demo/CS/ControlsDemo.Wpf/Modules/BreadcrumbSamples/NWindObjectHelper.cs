using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.Controls;

namespace ControlsDemo.BreadcrumbSamples {
    class NWindChildSelector : IChildSelector {
        IEnumerable IChildSelector.SelectChildren(object item) {
            Category category = item as Category;
            if(category != null)
                return category.Products.OrderBy(x => x.ProductName);

            Product product = item as Product;
            if(product != null)
                return product.OrderDetails.OrderBy(x => x.OrderID);

            return null;
        }
    }
    static class NWindObjectHelper {
        static public ImageSource GetCustomImage(object item) {
            if(item is Category)
                return GetImageSource(((Category)item).Icon17);
            if(item is Product)
                return GetImageSource(((Product)item).Category.Icon17);
            if(item is OrderDetailsExtended)
                return GetImageSource(((OrderDetailsExtended)item).Product.Category.Icon17);
            return null;
        }
        static public string GetCustomText(object item) {
            if(item is Category) return ((Category)item).CategoryName;
            if(item is Product) return ((Product)item).ProductName;
            if(item is OrderDetailsExtended) return string.Format("Order ID = {0}", ((OrderDetailsExtended)item).OrderID);
            return null;
        }
        static ImageSource GetImageSource(byte[] icon) {
            var stream = new MemoryStream(icon);
            var imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.StreamSource = stream;
            imageSource.EndInit();
            return imageSource;
        }
    }
}
