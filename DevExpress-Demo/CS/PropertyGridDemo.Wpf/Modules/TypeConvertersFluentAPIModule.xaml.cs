using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Xpf.PropertyGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PropertyGridDemo {
    public partial class TypeConvertersFluentAPIModule : PropertyGridDemoModule {
        public TypeConvertersFluentAPIModule() {
            InitializeComponent();
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata<ProductDescriptorMetadata>();
            DataContext = new ProductDescriptor { Product = new Product { Name = "Macintosh" }, Tags = new string[] { "Apple", "Fruit", "Round" } };
        }
        void PropertyGridControl_OnCustomExpand(object sender, CustomExpandEventArgs args) {
            if (args.IsInitializing) {
                args.IsExpanded = true;
            }
        }
    }

    public class Product {
        public string Name { get; set; }
        public int Count { get; set; }
    }
    public class ProductDescriptor {
        public Product Product { get; set; }
        public string[] Tags { get; set; }
    }
    public class ProductDescriptorMetadata {
        public static void BuildMetadata(MetadataBuilder<ProductDescriptor> builder) {
            builder.Property(x => x.Product)
                .TypeConverter()
                    .ConvertFromRule((string str) => new Product { Name = str, Count = 0 })
                    .ConvertToRule<string>(product => product == null ? null : product.Name)
                    .PropertiesProvider(() => TypeDescriptor.GetProperties(typeof(Product)).Cast<PropertyDescriptor>())
                .EndTypeConverter();

            builder.Property(x => x.Tags)
                .TypeConverter()
                    .ConvertFromRule((string str) => string.IsNullOrEmpty(str) ? null : str.Split(' '))
                    .ConvertToRule<string>(strArray => strArray == null ? null : strArray.Aggregate((sum, element) => sum + " " + element))
                .EndTypeConverter();
        }
    }
}
