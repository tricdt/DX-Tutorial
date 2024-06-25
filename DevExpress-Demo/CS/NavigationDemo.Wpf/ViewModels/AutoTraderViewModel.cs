using System.Collections.Generic;
using System.Linq;
using DevExpress.DemoData.Models.Vehicles;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.Filtering;

namespace NavigationDemo {
    public class AutoTraderViewModel {
        public IEnumerable<Model> Vehicles { get; private set; }
        public IEnumerable<Trademark> Trademarks { get; private set; }
        public IEnumerable<TransmissionType> TransmissionTypes { get; private set; }
        public IEnumerable<BodyStyle> BodyStyles { get; private set; }
        public int[] DoorTypes { get; private set; }

        public AutoTraderViewModel() {
            if(this.IsInDesignMode())
                return;
            var context = new VehiclesContext();
            Vehicles = context.Models.ToList();
            Trademarks = context.Trademarks.ToList();
            TransmissionTypes = context.TransmissionTypes.ToList();
            BodyStyles = context.BodyStyles.ToList();
            DoorTypes = new int[] { 2, 3, 4 };
        }
    }
}
