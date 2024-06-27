using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ViewModelBaseDemo
{
    public class LoginViewModel:ViewModelBase
    {
        public string UserName
        {
            get { return GetValue<string>(); }
            set { SetValue(value); }
        }
        public string Status
        {
            get { return GetValue<string>(); }
            private set { SetValue(value); }
        }

        [Command]
        public void Login()
        {
            Status = "User: " + UserName;
        }
        public bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName);
        }
    }
}
