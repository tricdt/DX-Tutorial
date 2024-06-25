using System;
using System.Collections.Generic;
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
using DevExpress.Xpf.Editors;
using System.ComponentModel;
using System.Text.RegularExpressions;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.Editors.Validation;
using EditorsDemo.Utils;

namespace EditorsDemo {
    [CodeFile("ModuleResources/AutoSuggestEditTemplates.xaml")]
    public partial class AutoSuggestEditModule : EditorsDemoModule {
        readonly NWindContext context = NWindContext.Create();
        public AutoSuggestEditModule() {
            InitializeComponent();
        }
        void Countries_OnQuerySubmitted(object sender, AutoSuggestEditQuerySubmittedEventArgs e) {
            this.countries.ItemsSource = string.IsNullOrEmpty(e.Text)
                ? null
                : this.context.CountriesArray
                      .Where(x => x.StartsWith(e.Text, StringComparison.InvariantCultureIgnoreCase))
                      .Take(10)
                      .ToArray();
        }
        void Products_OnQuerySubmitted(object sender, AutoSuggestEditQuerySubmittedEventArgs e) {
            this.products.ItemsSource = string.IsNullOrEmpty(e.Text)
                ? null
                : this.context.Products.ToList()
                      .Where(x => Regex.IsMatch(x.ProductName, Regex.Escape(e.Text), RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace))
                      .Take(10)
                      .ToArray();
        }
        void LookUp_OnQuerySubmitted(object sender, AutoSuggestEditQuerySubmittedEventArgs e) {
            this.lookup.ItemsSource = string.IsNullOrEmpty(e.Text)
                ? null
                : this.context.Products.ToList().Where(x => ProcessProduct(x, e.Text)).ToArray();
        }
        bool ProcessProduct(Product product, string text) {
            return 
                Regex.IsMatch(product.ProductName, Regex.Escape(text), RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace) ||
                Regex.IsMatch(product.QuantityPerUnit, Regex.Escape(text), RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace) || 
                (product.UnitPrice != null && Regex.IsMatch(product.UnitPrice.ToString(), Regex.Escape(text), RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace));

        }
    }
}
