Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Partial Class OrderCollectionViewModel

        Const NumberOfAverageOrders As Integer = 200

        Public Overridable Property AverageOrders As List(Of Order)

        Public Overridable Property Sales As List(Of SalesInfo)

        Public Overridable Property SelectedSale As SalesInfo

        Public Sub ShowPrintPreview()
            Dim link = GetRequiredService(Of Common.View.IPrintableControlPreviewService)().GetLink()
            Me.GetRequiredService(Of IDocumentManagerService)("FrameDocumentUIService").CreateDocument("PrintableControlPrintPreview", PrintableControlPreviewViewModel.Create(link), Nothing, Me).Show()
        End Sub

        Protected Overrides Sub OnInitializeInRuntime()
            MyBase.OnInitializeInRuntime()
            Dim unitOfWork = UnitOfWorkFactory.CreateUnitOfWork()
            Sales = QueriesHelper.GetSales(unitOfWork.OrderItems)
            SelectedSale = Sales(0)
            AverageOrders = QueriesHelper.GetAverageOrders(unitOfWork.Orders.ActualOrders(), NumberOfAverageOrders)
        End Sub

        Protected Overrides Sub OnEntitiesAssigned(ByVal getSelectedEntityCallback As Func(Of Order))
            MyBase.OnEntitiesAssigned(getSelectedEntityCallback)
            If SelectedEntity Is Nothing Then SelectedEntity = Entities.FirstOrDefault()
        End Sub
    End Class
End Namespace
