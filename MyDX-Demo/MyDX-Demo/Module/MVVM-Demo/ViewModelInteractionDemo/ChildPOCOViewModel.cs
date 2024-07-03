using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ViewModelInteractionDemo
{
    public class ChildPOCOViewModel
    {
        IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }

        public void ShowMessage()
        {
            MessageBoxService.ShowMessage("Showing message using service from parent view model.", "Message", MessageButton.OK, MessageIcon.Exclamation);
        }
    }
}
