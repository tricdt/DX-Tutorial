using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.DemoBase;
using DevExpress.Xpf.TreeMap;

namespace TreeMapDemo {
    [CodeFile("Modules/Sunburst/Interaction.xaml")]
    [CodeFile("Modules/Sunburst/Interaction.xaml.(cs)")]
    [NoAutogeneratedCodeFiles]
    public partial class SunburstInteractionDemo : TreeMapDemoModule {
        public SunburstInteractionDemo() {
            InitializeComponent();
        }
    }


    [POCOViewModel]
    public class SunburstInteractionDemoViewModel {
        public static SunburstInteractionDemoViewModel Create() {
            return ViewModelSource.Create(() => new SunburstInteractionDemoViewModel());
        }

        public virtual List<SaleItem> DataSource { get; protected set; }
        public virtual IList SelectedItems { get; set; }
        public virtual SaleItem SelectedItemOnChart { get; set; }

        protected SunburstInteractionDemoViewModel() {
            DataSource = SaleItem.GetProductsByCompanies();
            SelectedItems = DataSource;
        }

        protected void OnSelectedItemsChanged() {
            if (SelectedItems == null || SelectedItems.Count == 0)
                SelectedItems = DataSource;
        }
    }


    public class SaleItem {
        static Dictionary<string, List<string>> _categorizedProducts;
        static Dictionary<string, List<string>> CategorizedProducts {
            get {
                if (_categorizedProducts == null) {
                    _categorizedProducts = new Dictionary<string, List<string>>();
                    _categorizedProducts["Cameras"] = new List<string>() { "Camera", "Camcorder", "Binoculars", "Flash", "Tripod" };
                    _categorizedProducts["Cell Phones"] = new List<string>() { "Smartphone", "Sim Card" };
                    _categorizedProducts["Computers"] = new List<string>() { "Desktop", "Laptop", "Tablet", "Printer" };
                }
                return _categorizedProducts;
            }
        }
        static readonly PaletteBase palette = new Office2016Palette();
        static readonly Dictionary<string, Color> CompanyColors = new Dictionary<string, Color> {
            {"North", palette[0]},
            {"South", palette[1] },
            {"West", palette[2] },
            {"East", palette[3] }
        };

        public static readonly string[] Companies = new string[] { "North", "South", "West", "East" };

        public static List<SaleItem> GetProductsByCompanies() {
            Random rnd = new Random(1);
            List<SaleItem> items = new List<SaleItem>();
            foreach (string company in Companies) {
                foreach (string category in CategorizedProducts.Keys) {
                    foreach (string product in CategorizedProducts[category]) {
                        int income = rnd.Next(20, 100);
                        int revenue = income + rnd.Next(20, 50);
                        items.Add(new SaleItem() {
                            Company = company,
                            Product = product,
                            Income = income,
                            Revenue = revenue,
                            Category = category,
                            Color = CompanyColors[company]
                        });
                    }
                }
            }
            return items;
        }

        public string Product { get; set; }
        public string Company { get; set; }
        public double Income { get; set; }
        public double Revenue { get; set; }
        public string Category { get; set; }
        public Color Color { get; set; }
    }

    class SelectedItemsConverter : MarkupExtension, IValueConverter {
        public IList FullDataSource { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value == null)
                return FullDataSource;
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }

    class SalesColorizer : SunburstColorizerBase {
        public override Brush GetItemBrush(ISunburstSectorInfo item) {
            object sourceObject = item.SourceObject;
            SaleItem saleItem = sourceObject is IList ? (SaleItem)((IList)sourceObject)[0] : (SaleItem)sourceObject;
            SolidColorBrush brush = new SolidColorBrush(saleItem.Color);
            brush.Freeze();
            return brush;
        }
        protected override TreeMapDependencyObject CreateObject() {
            return new SalesColorizer();
        }
    }

    class HighlightConverter : MarkupExtension, IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            return values[0] == values[1];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            return this;
        }
    }
}
