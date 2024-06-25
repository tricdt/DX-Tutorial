using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.ViewModelsInteraction {
    public class ParentPOCOViewModel {
        public void ShowDetail() {
            IDialogService service = this.GetRequiredService<IDialogService>();
            service.ShowDialog(
                dialogButtons: MessageButton.OK,
                title: "Detail", 
                documentType: null, 
                parameter: null, 
                parentViewModel: this);
        }
    }
}
