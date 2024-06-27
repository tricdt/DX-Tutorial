using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;


namespace MyDX_Demo.Module.MVVM_Demo.ViewModelBaseDemo
{
    public class DataErrorInfoViewModel:ViewModelBase, IDataErrorInfo
    {
        [Required(ErrorMessage = "Please enter the user name.")]
        [StringLength(100, MinimumLength = 5)]
        public virtual string UserName { get; set; }
        string IDataErrorInfo.this[string columnName]
        {
            get
            {
                return IDataErrorInfoHelper.GetErrorText(this, columnName);
            }
        }
        string IDataErrorInfo.Error
        {
            get { return null; }
        }
        [Command]
        public void Register()
        {
            MessageBox.Show("Registered");
        }
        public bool CanRegister()
        {
            return !IDataErrorInfoHelper.HasErrors(this);
        }
    }
}
