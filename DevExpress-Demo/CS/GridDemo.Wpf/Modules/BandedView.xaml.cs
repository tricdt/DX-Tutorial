using DevExpress.DemoData.Models.Vehicles;
using System.Linq;

namespace GridDemo {
    public partial class BandedView : GridDemoModule {
        public BandedView() {
            InitializeComponent();
            grid.ItemsSource = new VehiclesContext().Models.ToList();
        }
    }
}
