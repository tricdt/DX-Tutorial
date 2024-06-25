using DevExpress.Mvvm.DataAnnotations;

namespace MVVMDemo.POCOViewModels {
    public class DependentPropertiesViewModel {
        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        [DependsOnProperties("FirstName", "LastName")]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}
