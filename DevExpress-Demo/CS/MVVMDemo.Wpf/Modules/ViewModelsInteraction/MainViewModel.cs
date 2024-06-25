using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace MVVMDemo.ViewModelsInteraction {
    public class MainViewModel : ViewModelBase {
        [Command]
        public void ShowDetail(string item) {
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
