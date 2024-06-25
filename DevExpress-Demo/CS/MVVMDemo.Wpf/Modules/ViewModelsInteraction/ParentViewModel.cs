using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace MVVMDemo.ViewModelsInteraction {
    public class ParentViewModel : ViewModelBase {
        [Command]
        public void ShowDetail() {
            IDialogService service = ServiceContainer.GetService<IDialogService>();
            service.ShowDialog(
                dialogButtons: MessageButton.OK,
                title: "Detail", 
                documentType: null, 
                parameter: null, 
                parentViewModel: this);
        }
    }
}
