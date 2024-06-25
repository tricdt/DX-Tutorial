using DevExpress.Mvvm.DataAnnotations;

namespace MapDemo {
    [POCOViewModel]
    public class ProductGroupViewModel {
        public virtual string Name { get; set; }
        public virtual double Value { get; set; }
        public ProductGroupViewModel(double value, string productName) {
            Value = value;
            Name = productName;
        }
    }
}
