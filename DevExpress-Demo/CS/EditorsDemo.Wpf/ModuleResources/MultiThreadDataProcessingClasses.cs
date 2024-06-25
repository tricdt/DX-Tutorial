using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using DevExpress.Mvvm.DataAnnotations;

namespace GridDemo {
    [POCOViewModel]
    public class CountItem {
        public virtual string DisplayName { get; set; }
        public virtual int Count { get; set; }
    }
    public class CountItemCollection : List<CountItem> { }
    public class OrderDataListSource : IListSource {
        OrderInfoDataGenerator orderDataGenerator;

        public OrderDataListSource(OrderInfoDataGenerator orderDataGenerator) {
            this.orderDataGenerator = orderDataGenerator;
        }
        public bool ContainsListCollection {
            get { return false; }
        }
        public IList GetList() {
            return orderDataGenerator.GetOrders();
        }
    }

}
