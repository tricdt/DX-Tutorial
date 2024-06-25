Imports DevExpress.Mvvm

Namespace DevExpress.DevAV.ViewModels

    Partial Class ProductViewModel

        Public Overrides Sub Delete()
            MessageBoxService.ShowMessage("To ensure data integrity, the Products module doesn't allow records to be deleted. Record deletion is supported by the Employees module.", "Delete Product", MessageButton.OK)
        End Sub
    End Class
End Namespace
