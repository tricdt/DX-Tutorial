using DevExpress.DemoData.Models.Vehicles;
#if DXCORE3
using Microsoft.EntityFrameworkCore;
#else
using System.Data.Entity;
#endif
using System.Collections.Generic;

using System.Linq;
using VehicleModel = DevExpress.DemoData.Models.Vehicles.Model;

namespace DevExpress.DemoData {
    public class VehiclesData {
        VehiclesContext context = new VehiclesContext();
        IEnumerable<VehicleModel> models;
        public IEnumerable<VehicleModel> Models {
            get {
                if(models == null) {
                    context.Models.Load();
                    context.Trademarks.Load();
                    models = context.Models.Local.ToList();
                }
                return models;
            }
        }
    }
}
