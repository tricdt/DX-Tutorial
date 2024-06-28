using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ViewModelInteractionDemo
{
    public class MainViewModel:ViewModelBase
    {
        [Command]
        public void ShowDetail(string item)
        {
            IDialogService service = ServiceContainer.GetService<IDialogService>();
            service.ShowDialog(
                dialogButtons: MessageButton.OK,
                title: "Detail",
                documentType: null,
                parameter: item,
                parentViewModel: this);
        }
    }
}
