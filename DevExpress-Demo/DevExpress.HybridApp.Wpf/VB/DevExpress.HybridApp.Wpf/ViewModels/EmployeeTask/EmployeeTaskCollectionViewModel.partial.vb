Imports System
Imports System.Linq.Expressions
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Partial Class EmployeeTaskCollectionViewModel
        Implements ISupportFiltering(Of EmployeeTask), IFilterTreeViewModelContainer(Of EmployeeTask, Long)

        Public Sub ShowPrintPreview()
            Dim link = GetRequiredService(Of Common.View.IPrintableControlPreviewService)().GetLink()
            Me.GetRequiredService(Of IDocumentManagerService)("FrameDocumentUIService").CreateDocument("PrintableControlPrintPreview", PrintableControlPreviewViewModel.Create(link), Nothing, Me).Show()
        End Sub

        Public Overridable Property FilterTreeViewModel As FilterTreeViewModel(Of EmployeeTask, Long) Implements IFilterTreeViewModelContainer(Of EmployeeTask, Long).FilterTreeViewModel

#Region "ISupportFiltering"
        Private Property ISupportFiltering_FilterExpression As Expression(Of Func(Of EmployeeTask, Boolean)) Implements ISupportFiltering(Of EmployeeTask).FilterExpression
            Get
                Return FilterExpression
            End Get

            Set(ByVal value As Expression(Of Func(Of EmployeeTask, Boolean)))
                FilterExpression = value
            End Set
        End Property
#End Region
    End Class
End Namespace
