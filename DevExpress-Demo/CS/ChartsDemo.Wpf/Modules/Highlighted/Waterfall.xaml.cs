using System.Collections.Generic;
using System.Xml.Serialization;
using DevExpress.Data.Filtering;
using DevExpress.Xpf.DemoBase;

namespace ChartsDemo {
    [CodeFile("Modules/Highlighted/Waterfall.xaml"),
     CodeFile("Modules/Highlighted/Waterfall.xaml.(cs)"),
     NoAutogeneratedCodeFiles]
    public partial class WaterfallDemo : ChartsDemoModule {
        public WaterfallDemo() {
            InitializeComponent();
            DataContext = CsvReader.ReadCarbonData("carbon.csv");
            aggregatedDataItem.Tag = new BinaryOperator("Factor", "Imbalance", BinaryOperatorType.Equal);
            detailedDataItem.Tag = new BinaryOperator("Factor", "Imbalance", BinaryOperatorType.NotEqual);
        }
    }
}
