using DevExpress.Mvvm;

namespace MyDX_Demo.Module.MVVM_Demo.DXBindingDemo
{
    public class BackConversionViewModel
    {
        public virtual bool IsEnabled { get; set; }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        protected BackConversionViewModel()
        {
            FirstName = "Gregory";
            LastName = "Price";
        }
    }
}
