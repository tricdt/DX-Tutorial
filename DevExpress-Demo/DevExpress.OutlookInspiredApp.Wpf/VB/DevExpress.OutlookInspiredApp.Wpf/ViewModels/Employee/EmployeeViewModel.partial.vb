Imports System.Linq
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.DataAnnotations

Namespace DevExpress.DevAV.ViewModels

    Partial Class EmployeeViewModel

        Private contactsField As EmployeeContactsViewModel

        Private quickLetterField As EmployeeQuickLetterViewModel

        Private linksViewModelField As LinksViewModel

        Public ReadOnly Property Contacts As EmployeeContactsViewModel
            Get
                If contactsField Is Nothing Then
                    contactsField = EmployeeContactsViewModel.Create().SetParentViewModel(Me)
                End If

                Return contactsField
            End Get
        End Property

#If DXCORE3
        public CollectionViewModelBase<EmployeeTask, EmployeeTask, long, IDevAVDbUnitOfWork> EmployeeAssignedTasksDetails {
            get {
                return GetDetailsCollectionViewModel<EmployeeViewModel, EmployeeTask, long, long?>(
                    propertyExpression: (EmployeeViewModel x) => x.EmployeeAssignedTasksDetails,
                    getRepositoryFunc: x => x.Tasks,
                    foreignKeyExpression: x => x.AssignedEmployeeId,
                    navigationExpression: x => x.AssignedEmployee);
            }
        }
#Else
        Private employeeAssignedTasksDetailsField As EmployeeTaskDetailsCollectionViewModel

        Public ReadOnly Property EmployeeAssignedTasksDetails As EmployeeTaskDetailsCollectionViewModel
            Get
                If employeeAssignedTasksDetailsField Is Nothing Then
                    employeeAssignedTasksDetailsField = EmployeeTaskDetailsCollectionViewModel.Create().SetParentViewModel(Me)
                End If

                Return employeeAssignedTasksDetailsField
            End Get
        End Property

#End If
        Protected Overrides Sub OnEntityChanged()
            MyBase.OnEntityChanged()
            Contacts.Entity = Entity
            QuickLetter.Entity = Entity
            SetDefaultReport(EmployeeProfile(Entity))
#If Not DXCORE3
            EmployeeAssignedTasksDetails.UpdateFilter()
#End If
            If Entity IsNot Nothing Then Log(String.Format("OutlookInspiredApp: Edit Employee: {0}", If(String.IsNullOrEmpty(Entity.FullName), "<New>", Entity.FullName)))
        End Sub

        Protected Overrides Function TryClose() As Boolean
            Dim closed = MyBase.TryClose()
            If closed Then Enumerable.First(DocumentManagerService.Documents, Function(x) x.Content Is Me).DestroyOnClose = True
            Return closed
        End Function

        Protected Overrides Function GetTitle() As String
            Return Entity.FullName
        End Function

        Public ReadOnly Property QuickLetter As EmployeeQuickLetterViewModel
            Get
                If quickLetterField Is Nothing Then quickLetterField = EmployeeQuickLetterViewModel.Create().SetParentViewModel(Me)
                Return quickLetterField
            End Get
        End Property

        Public Sub ShowMap()
            Dim mapViewModel = EmployeeCollectionViewModel.CreateEmployeeMapViewModel(Entity, Sub(destination)
                Entity.Address = destination
                RaisePropertyChanged(Function(x) x.Entity)
            End Sub)
            DocumentManagerService.CreateDocument("NavigatorMapView", mapViewModel, Nothing, Me).Show()
        End Sub

        Protected ReadOnly Property DocumentManagerService As IDocumentManagerService
            Get
                Return GetRequiredService(Of IDocumentManagerService)()
            End Get
        End Property

        Public ReadOnly Property LinksViewModel As LinksViewModel
            Get
                If linksViewModelField Is Nothing Then linksViewModelField = LinksViewModel.Create()
                Return linksViewModelField
            End Get
        End Property

        Public Sub ShowScheduler(ByVal title As String)
            MessageBoxService.Show("This demo does not include integration with our WPF Scheduler Suite but you can easily introduce Outlook-inspired scheduling and task management capabilities to your apps with DevExpress Tools.", title, MessageButton.OK, MessageIcon.Asterisk, MessageResult.OK)
        End Sub

        Protected Overrides Function SaveCore() As Boolean
            If IsNew() AndAlso String.IsNullOrEmpty(Entity.FullName) Then Entity.FullName = String.Format("{0} {1}", Entity.FirstName, Entity.LastName)
            Return MyBase.SaveCore()
        End Function

        Public Sub PrintEmployeeProfile()
            ShowReport(EmployeeProfile(Entity), "Profile")
        End Sub

        Public Function CanPrintEmployeeProfile() As Boolean
            Return Entity IsNot Nothing
        End Function

        Public Sub PrintSummaryReport()
            ShowReport(EmployeeSummary(Repository.ToList()), "Summary")
        End Sub

        Public Sub PrintDirectory()
            ShowReport(EmployeeDirectory(Repository.ToList()), "Directory")
        End Sub

        Public Sub PrintTaskList()
            ShowReport(EmployeeTaskList(UnitOfWork.Tasks.ToList()), "TaskList")
        End Sub

        Private Sub ShowReport(ByVal reportInfo As IReportInfo, ByVal reportId As String)
            ExportService.ShowReport(reportInfo)
            PrintService.ShowReport(reportInfo)
            Log(String.Format("OutlookInspiredApp: Create Report : Employee: {0}", reportId))
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
    End Class

    Public Class PersonPrefixWithExternalMetadata

        Public Shared Sub BuildMetadata(ByVal builder As EnumMetadataBuilder(Of PersonPrefix))
            builder.Member(PersonPrefix.Dr).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Doctor.svg").EndMember().Member(PersonPrefix.Miss).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Miss.svg").EndMember().Member(PersonPrefix.Mr).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Mr.svg").EndMember().Member(PersonPrefix.Mrs).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Mrs.svg").EndMember().Member(PersonPrefix.Ms).ImageUri("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Ms.svg").EndMember()
        End Sub
    End Class
End Namespace
