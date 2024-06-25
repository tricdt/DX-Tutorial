using DevExpress.Mvvm;

namespace MVVMDemo.Services.DocumentManager {
    public class DetailViewModel : ISupportParameter {
        public virtual PersonInfo Person { get; set; }

        object _Parameter;
        object ISupportParameter.Parameter {
            get { return _Parameter; }
            set {
                _Parameter = value;
                Person = (PersonInfo)_Parameter;
            }
        }
    }
}
