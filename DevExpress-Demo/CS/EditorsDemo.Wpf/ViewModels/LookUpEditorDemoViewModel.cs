using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DevExpress.DemoData;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Xpf.DemoBase.DataClasses;
using DevExpress.Xpf.Grid.LookUp;
using Employee = DevExpress.Xpf.DemoBase.DataClasses.Employee;

namespace EditorsDemo {
    public class LookUpEditorDemoViewModel : EditorsViewModelBase {

        public LookUpEditorDemoViewModel() : base() {
            OrdersSource = new List<OrderDetailsExtended>(GetOrderDetails(DataLoader));
            SelectedOrders = new List<object>() { OrdersSource[0].ProductName, OrdersSource[2].ProductName, OrdersSource[10].ProductName };
        }

        public List<OrderDetailsExtended> OrdersSource { get; private set; }
        public IList<object> SelectedOrders { get; set; }
        public LookUpEdit FocusedEditor {
            get { return GetValue<LookUpEdit>(); }
            set { SetValue(value); }
        }

        IEnumerable<OrderDetailsExtended> GetOrderDetails(NWindDataLoader dataLoader) {
            return ((IEnumerable<OrderDetailsExtended>)dataLoader.OrderDetailsExtended).Distinct(new OrderEqualityComparer());
        }
    }

    public class OrderEqualityComparer : IEqualityComparer<OrderDetailsExtended> {
        public bool Equals(OrderDetailsExtended x, OrderDetailsExtended y) {
            if(x == null && y != null)
                return false;
            if(x != null && y == null)
                return false;
            return x.ProductName == y.ProductName;
        }

        public int GetHashCode(OrderDetailsExtended obj) {
            return obj.ProductName.GetHashCode();
        }
    }
}
