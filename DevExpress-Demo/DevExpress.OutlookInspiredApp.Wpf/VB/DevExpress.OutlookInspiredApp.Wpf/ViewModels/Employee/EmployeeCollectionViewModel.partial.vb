Imports System
Imports System.Linq
Imports System.Linq.Expressions
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Map

Namespace DevExpress.DevAV.ViewModels

    Partial Class EmployeeCollectionViewModel
        Implements ISupportFiltering(Of Employee)

        Friend Shared Function CreateEmployeeMapViewModel(ByVal employee As Employee, ByVal applyDestination As Action(Of Address)) As NavigatorMapViewModel(Of Employee)
            Return NavigatorMapViewModel(Of Employee).Create(employee, AddressHelper.DevAVHomeOffice.ToString(), New GeoPoint(AddressHelper.DevAVHomeOffice.Latitude, AddressHelper.DevAVHomeOffice.Longitude), employee.Address.ToString(), New GeoPoint(employee.Address.Latitude, employee.Address.Longitude), applyDestination)
        End Function

        Private entityPanelViewModelField As EmployeeContactsViewModel

        Private quickLetterField As EmployeeQuickLetterViewModel

        Private viewSettingsField As ViewSettingsViewModel

        Public ReadOnly Property EntityPanelViewModel As EmployeeContactsViewModel
            Get
                If entityPanelViewModelField Is Nothing Then entityPanelViewModelField = EmployeeContactsViewModel.Create()
                Return entityPanelViewModelField
            End Get
        End Property

        Public ReadOnly Property ViewSettings As ViewSettingsViewModel
            Get
                If viewSettingsField Is Nothing Then viewSettingsField = ViewSettingsViewModel.Create(CollectionViewKind.ListView, Me)
                Return viewSettingsField
            End Get
        End Property

        Public ReadOnly Property QuickLetter As EmployeeQuickLetterViewModel
            Get
                If quickLetterField Is Nothing Then quickLetterField = EmployeeQuickLetterViewModel.Create().SetParentViewModel(Me)
                Return quickLetterField
            End Get
        End Property

        Public Overridable Property TableViewSelectedEntity As Employee

        Public Overridable Property CardViewSelectedEntity As Employee

        Public Sub ShowMap()
            Dim mapViewModel = CreateEmployeeMapViewModel(SelectedEntity, Sub(destination)
                SelectedEntity.Address = destination
                Save(SelectedEntity)
            End Sub)
            Log("OutlookInspiredApp: View Employee Map")
            GetRequiredService(Of IDocumentManagerService)().CreateDocument("EmployeeMapView", mapViewModel, Nothing, Me).Show()
        End Sub

        Public Function CanShowMap() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        Public Sub PrintEmployeeProfile()
            ShowReport(EmployeeProfile(SelectedEntity), "Profile")
        End Sub

        Public Function CanPrintEmployeeProfile() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

        Public Sub PrintSummaryReport()
            ShowReport(EmployeeSummary(Entities), "Summary")
        End Sub

        Public Sub PrintDirectory()
            ShowReport(EmployeeDirectory(Entities), "Directory")
        End Sub

        Public Sub PrintTaskList()
            ShowReport(TaskListReport(CreateUnitOfWork().Tasks.ToList()), "TaskList")
        End Sub

        Private Sub ShowReport(ByVal reportInfo As IReportInfo, ByVal reportId As String)
            ExportService.ShowReport(reportInfo)
            PrintService.ShowReport(reportInfo)
            Log(String.Format("OutlookInspiredApp: Create Report : Employees: {0}", reportId))
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

        Protected Overrides Sub OnSelectedEntityChanged()
            MyBase.OnSelectedEntityChanged()
            QuickLetter.Entity = SelectedEntity
            QuickLetter.RaisePropertyChanged(Function(x) x.Entity)
            If SelectedEntity IsNot Nothing Then EntityPanelViewModel.Entity = SelectedEntity
            SetDefaultReport(EmployeeProfile(SelectedEntity))
            TableViewSelectedEntity = SelectedEntity
            CardViewSelectedEntity = SelectedEntity
        End Sub

        Public Overrides Sub UpdateSelectedEntity()
            MyBase.UpdateSelectedEntity()
            QuickLetter.RaisePropertyChanged(Function(x) x.Entity)
            EntityPanelViewModel.RaisePropertyChanged(Function(x) x.Entity)
        End Sub

        Protected Overrides Sub OnEntitiesAssigned(ByVal getSelectedEntityCallback As Func(Of Employee))
            MyBase.OnEntitiesAssigned(getSelectedEntityCallback)
            If Entities.Any() AndAlso SelectedEntity Is Nothing Then SelectedEntity = Entities.OrderBy(Function(x) x.FullName).FirstOrDefault()
        End Sub

        Public Overridable Sub OnTableViewSelectedEntityChanged()
            If viewSettingsField.ViewKind = CollectionViewKind.ListView Then SelectedEntity = TableViewSelectedEntity
        End Sub

        Public Overridable Sub OnCardViewSelectedEntityChanged()
            If viewSettingsField.ViewKind = CollectionViewKind.CardView Then SelectedEntity = CardViewSelectedEntity
        End Sub

        Public Sub ShowScheduler(ByVal title As String)
            MessageBoxService.Show("This demo does not include integration with our WPF Scheduler Suite but you can easily introduce Outlook-inspired scheduling and task management capabilities to your apps with DevExpress Tools.", title, MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK)
        End Sub

        Public Sub CreateCustomFilter()
            Call Messenger.Default.Send(New CreateCustomFilterMessage(Of Employee)())
        End Sub

        Public Sub AddTask()
#If DXCORE3
            Action<EmployeeTask> initializer = x => {
                x.AssignedEmployeeId = SelectedEntity.Id;
                x.OwnerId = SelectedEntity.Id;
            };
            this.GetRequiredService<IDocumentManagerService>().CreateDocument("EmployeeTaskView", null, initializer, this).Show();
#Else
            Dim initializer As Action(Of EmployeeTask) = Sub(x) x.OwnerId = SelectedEntity.Id
            EmployeeTaskDetailsCollectionViewModel.LastSelectedEmployeeId = SelectedEntity.Id
            Dim document As IDocument = GetRequiredService(Of IDocumentManagerService)().CreateDocument("EmployeeTaskView", Nothing, initializer, Me)
            document.Show()
            EmployeeTaskDetailsCollectionViewModel.LastSelectedEmployeeId = Nothing
            Refresh()
#End If
        End Sub

        Public Function CanAddTask() As Boolean
            Return SelectedEntity IsNot Nothing
        End Function

#Region "ISupportFiltering"
        Private Property ISupportFiltering_FilterExpression As Expression(Of Func(Of Employee, Boolean)) Implements ISupportFiltering(Of Employee).FilterExpression
            Get
                Return FilterExpression
            End Get

            Set(ByVal value As Expression(Of Func(Of Employee, Boolean)))
                FilterExpression = value
            End Set
        End Property
#End Region
    End Class
End Namespace
