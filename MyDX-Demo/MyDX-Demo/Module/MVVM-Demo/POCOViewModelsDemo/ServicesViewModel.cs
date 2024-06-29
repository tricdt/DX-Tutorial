using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.POCOViewModelsDemo
{
    public class ServicesViewModel
    {
        public virtual string UserName { get; set; }

        #region !
        IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }

        public void Login()
        {
            MessageBoxService.ShowMessage("User: " + UserName, "Login", MessageButton.OK, MessageIcon.Information);
        }
        #endregion

        public bool CanLogin()
        {
            return !string.IsNullOrEmpty(UserName);
        }
    }
}
