using System.Windows;

namespace SankeyDemo {
    public partial class Interaction : SankeyDemoModule {
        public Interaction() {
            InitializeComponent();
        }
        void map_SizeChanged(object sender, SizeChangedEventArgs e) {
            map.ZoomToFitLayerItems(0);
        }
        void mapLayer_Loaded(object sender, RoutedEventArgs e) {
            map.ZoomToFitLayerItems(0);
        }
        void gridTable_Loaded(object sender, RoutedEventArgs e) {
            gridTable.BestFitColumns();
        }
    }
}
