using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace MVVMDemo.ViewModelsInteraction {
    public class ChildPOCOViewModel {
        IMessageBoxService MessageBoxService { get { return this.GetRequiredService<IMessageBoxService>(); } }

        public void ShowMessage() {
            MessageBoxService.ShowMessage("Showing message using service from parent view model.", "Message", MessageButton.OK, MessageIcon.Exclamation);
        }
    }
}
