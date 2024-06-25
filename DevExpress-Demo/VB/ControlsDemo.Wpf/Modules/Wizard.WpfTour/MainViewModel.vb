Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace ControlsDemo.Wizard.WpfTour

    Public Class MainViewModel

        Private WizardViewModel As WizardViewModel

        Public Sub LaunchWizard()
            WizardViewModel = ViewModelSource.Create(Of WizardViewModel)()
            WizardViewModel.SetParentViewModel(Me)
            GetRequiredService(Of IDialogService)().ShowDialog(MessageButton.OKCancel, "Wizard Control Feature Tour", WizardViewModel)
        End Sub
    End Class
End Namespace
