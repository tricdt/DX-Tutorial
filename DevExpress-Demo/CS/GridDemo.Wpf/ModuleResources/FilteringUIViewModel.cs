using DevExpress.Data.Filtering;
using DevExpress.DemoData.Models.Vehicles;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.Filtering;
using DevExpress.Xpf.Core.FilteringUI;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GridDemo {
    public class FilteringUIViewModel {
        public IEnumerable<Model> Vehicles { get; private set; }
        public IEnumerable<Trademark> Trademarks { get; private set; }
        public IEnumerable<TransmissionType> TransmissionTypes { get; private set; }
        public IEnumerable<BodyStyle> BodyStyles { get; private set; }
        public int[] DoorTypes { get; private set; }
        
        public FilteringUIViewModel() {
            if(this.IsInDesignMode()) return;
            var context = new VehiclesContext();
            Vehicles = context.Models.ToList();
            Trademarks = context.Trademarks.ToList();
            TransmissionTypes = context.TransmissionTypes.ToList();
            BodyStyles = context.BodyStyles.ToList();
            DoorTypes = new int[] { 2, 3, 4 };
        }
    }
}
