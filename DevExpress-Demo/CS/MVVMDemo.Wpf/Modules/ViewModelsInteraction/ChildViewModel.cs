using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;

namespace MVVMDemo.ViewModelsInteraction {
    public class ChildViewModel : ViewModelBase {
        IMessageBoxService MessageBoxService { get { return ServiceContainer.GetService<IMessageBoxService>(); } }
        [Command]
        public void ShowMessage() {
            MessageBoxService.ShowMessage("Showing message using service from parent view model.", "Message", MessageButton.OK, MessageIcon.Exclamation);
        }
    }
}
