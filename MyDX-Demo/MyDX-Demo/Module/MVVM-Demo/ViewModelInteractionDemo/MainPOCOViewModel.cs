using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDX_Demo.Module.MVVM_Demo.ViewModelInteractionDemo
{
    public class MainPOCOViewModel
    {
        public void ShowDetail(string item)
        {
            IDialogService service = this.GetRequiredService<IDialogService>();
            service.ShowDialog(
                dialogButtons: MessageButton.OK,
                title: "Detail",
                documentType: null,
                parameter: item,
                parentViewModel: this);
        }
    }
}
