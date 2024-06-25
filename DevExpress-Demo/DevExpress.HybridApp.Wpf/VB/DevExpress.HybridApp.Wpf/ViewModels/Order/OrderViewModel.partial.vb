Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Partial Class OrderViewModel

        Public Overrides Sub OnLoaded()
            MyBase.OnLoaded()
            GetRequiredService(Of IReportService)().ShowReport(SalesInvoice(Entity))
        End Sub
    End Class
End Namespace
