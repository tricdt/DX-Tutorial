Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO

Namespace DevExpress.DevAV.ViewModels

    Partial Class CustomerCollectionViewModel
        Implements ISupportFiltering(Of Customer)

        Const title As String = "Edit Customers"

        Const text As String = "You can easily create custom edit forms using the 40+ controls that ship as part of the DevExpress Data Editors Library. To see what you can build, activate the Employees module."

        Private viewSettingsField As ViewSettingsViewModel

        Public ReadOnly Property ViewSettings As ViewSettingsViewModel
            Get
                If viewSettingsField Is Nothing Then viewSettingsField = ViewSettingsViewModel.Create(CollectionViewKind.ListView, Me)
                Return viewSettingsField
            End Get
        End Property

        Public Sub ShowSalesMap(ByVal customer As Customer)
            Dim mapViewModel As CustomerMapViewModel = ViewModelSource.Create(Function() New CustomerMapViewModel())
            Log("OutlookInspiredApp: View Sales Map")
            GetRequiredService(Of IDocumentManagerService)().CreateDocument("CitiesMapView", mapViewModel, customer.Id, Me).Show()
        End Sub

        Public Function CanShowSalesMap(ByVal customer As Customer) As Boolean
            Return customer IsNot Nothing
        End Function

        Public Sub PrintCustomerProfile()
            ShowReport(CusomerProfile(SelectedEntity), "Profile")
        End Sub

        Public Function CanPrintCustomerProfile() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        Public Sub PrintDirectory()
            ShowReport(CustomerContactsDirectory(SelectedEntity), "ContactsDirectory")
        End Sub

        Public Function CanPrintDirectory() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        Public Sub PrintOrderDetailReport()
            ShowReport(CustomerSalesDetailReport(QueriesHelper.GetCustomerSaleDetails(SelectedEntity.Id, CreateUnitOfWork().OrderItems)), "SalesDetail")
        End Sub

        Public Function CanPrintOrderDetailReport() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        Public Sub PrintOrderSummaryReport()
            ShowReport(CustomerSalesSummaryReport(QueriesHelper.GetCustomerSaleOrderItemDetails(SelectedEntity.Id, CreateUnitOfWork().OrderItems)), "SalesSummary")
        End Sub

        Public Function CanPrintOrderSummaryReport() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        Public Sub PrintLocationsDirectory()
            ShowReport(CustomerLocationsDirectory(Entities), "LocationDirectory")
        End Sub

        Protected Overrides Sub OnSelectedEntityChanged()
            MyBase.OnSelectedEntityChanged()
            SetDefaultReport(CusomerProfile(SelectedEntity))
        End Sub

        Protected Overrides Sub OnEntitiesAssigned(ByVal getSelectedEntityCallback As Func(Of Customer))
            MyBase.OnEntitiesAssigned(getSelectedEntityCallback)
            If Entities.Any() AndAlso SelectedEntity Is Nothing Then SelectedEntity = Entities.OrderBy(Function(x) x.Name).FirstOrDefault()
        End Sub

        Private Sub ShowReport(ByVal reportInfo As IReportInfo, ByVal reportId As String)
            ExportService.ShowReport(reportInfo)
            PrintService.ShowReport(reportInfo)
            Log(String.Format("OutlookInspiredApp: Create Report : Customers: {0}", reportId))
        End Sub

        Private Sub SetDefaultReport(ByVal reportInfo As IReportInfo)
            If IsInDesignMode() Then Return
            ExportService.SetDefaultReport(reportInfo)
            PrintService.SetDefaultReport(reportInfo)
        End Sub

        Private ReadOnly Property PrintService As IReportService
            Get
                Return GetRequiredService(Of IReportService)("PrintService")
            End Get
        End Property

        Private ReadOnly Property ExportService As IReportService
            Get
                Return GetRequiredService(Of IReportService)("ExportService")
            End Get
        End Property

        Public Overrides Sub Edit(ByVal entity As Customer)
            ShowCustomerEditForm()
        End Sub

        Public Overrides Sub [New]()
            ShowCustomerEditForm()
        End Sub

        Private Sub ShowCustomerEditForm()
            MessageBoxService.Show(text, title, MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK)
        End Sub

        Public Sub CreateCustomFilter()
            Call Messenger.Default.Send(New CreateCustomFilterMessage(Of Customer)())
        End Sub

        Public Sub ShowAnalysis()
            Dim documentManagerService As IDocumentManagerService = GetRequiredService(Of IDocumentManagerService)("AnalysisWindowedDocumentUIService")
            documentManagerService.CreateDocument("CustomerAnalysis", Nothing, Me).Show()
        End Sub

        Public Overrides Sub Delete(ByVal entity As Customer)
            MessageBoxService.ShowMessage("To ensure data integrity, the Customers module doesn't allow records to be deleted. Record deletion is supported by the Employees module.", "Delete Customer", MessageButton.OK)
        End Sub

#Region "ISupportFiltering"
        Private Property ISupportFiltering_FilterExpression As Expression(Of Func(Of Customer, Boolean)) Implements ISupportFiltering(Of Customer).FilterExpression
            Get
                Return FilterExpression
            End Get

            Set(ByVal value As Expression(Of Func(Of Customer, Boolean)))
                FilterExpression = value
            End Set
        End Property
#End Region
    End Class
End Namespace
