using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ViewModelBaseDemo
{
    public class ServicesViewModel:ViewModelBase
    {
        public string UserName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } }
        [Command]
        public void Login()
        {
            MessageBoxService.ShowMessage("User: " + UserName, "Login", MessageButton.OK, MessageIcon.Information);
        }
        public bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName);
        }
    }
}
