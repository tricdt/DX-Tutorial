using DevExpress.DemoData.Models.Vehicles;
using System.Linq;

namespace GridDemo {
    public partial class CardView : GridDemoModule {
        public CardView() {
            InitializeComponent();
            grid.ItemsSource = new VehiclesContext().Models.ToList();
        }
    }
}
