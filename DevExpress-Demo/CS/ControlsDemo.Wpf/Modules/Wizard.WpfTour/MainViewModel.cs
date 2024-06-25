using System.Collections.ObjectModel;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;

namespace ControlsDemo.Wizard.WpfTour {
    public class MainViewModel {
        WizardViewModel WizardViewModel;
        public void LaunchWizard() {
            WizardViewModel = ViewModelSource.Create<WizardViewModel>();
            WizardViewModel.SetParentViewModel(this);
            this.GetRequiredService<IDialogService>().ShowDialog(MessageButton.OKCancel, "Wizard Control Feature Tour", WizardViewModel);
        }
    }
}
