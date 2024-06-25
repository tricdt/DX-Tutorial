using DevExpress.Mvvm;

namespace MVVMDemo.ViewModelsInteraction {
    public class DetailPOCOViewModel : ISupportParameter {
        public virtual string DetailInfo { get; set; }

        object _Parameter;
        object ISupportParameter.Parameter {
            get { return _Parameter; }
            set {
                _Parameter = value;
                DetailInfo = string.Format("Detail for {0} item",_Parameter);
            }
        }
    }
}
