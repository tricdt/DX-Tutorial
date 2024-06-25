using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.ViewModelsInteraction {
    public class MainPOCOViewModel {
        public void ShowDetail(string item) {
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
