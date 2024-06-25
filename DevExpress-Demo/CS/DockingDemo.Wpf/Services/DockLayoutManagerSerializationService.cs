using System;
using System.Linq;
using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Docking;

namespace DockingDemo {
    public interface IDockLayoutManagerSerializationService {
        void Serialize(object path);
        void Deserialize(object path);
    }
    public class DockLayoutManagerSerializationService : ServiceBase, IDockLayoutManagerSerializationService {
        DockLayoutManager DockLayoutManager { get { return AssociatedObject as DockLayoutManager; } }

        void IDockLayoutManagerSerializationService.Deserialize(object path) {
            DXSerializer.Deserialize(AssociatedObject, path, AssociatedObject.GetType().Name, null);
        }

        void IDockLayoutManagerSerializationService.Serialize(object path) {
            DXSerializer.Serialize(AssociatedObject, path, AssociatedObject.GetType().Name, null);
        }
    }
}
