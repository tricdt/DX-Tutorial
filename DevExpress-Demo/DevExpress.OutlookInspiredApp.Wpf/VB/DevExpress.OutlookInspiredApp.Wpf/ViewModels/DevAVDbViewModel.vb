Imports System
Imports System.Collections.Generic
Imports System.Diagnostics
Imports System.Linq
Imports System.Threading.Tasks
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports System.Windows.Threading
Imports DevExpress.DevAV.Common.ViewModel
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.ViewModel
Imports DevExpress.Xpf.Core

Namespace DevExpress.DevAV.ViewModels

    ''' <summary>
    ''' Represents the root POCO view model for the DevAVDb data model.
    ''' </summary>
    Public Partial Class DevAVDbViewModel
        Inherits DocumentsViewModel(Of DevAVDbModuleDescription, IDevAVDbUnitOfWork)

        Private Shared IsDebug As Boolean

        Const TablesGroup As String = "Tables"

        ''' <summary>
        ''' Creates a new instance of DevAVDbViewModel as a POCO view model.
        ''' </summary>
        Public Shared Function Create() As DevAVDbViewModel
            Return ViewModelSource.Create(Function() New DevAVDbViewModel())
        End Function

        ''' <summary>
        ''' Initializes a new instance of the DevAVDbViewModel class.
        ''' This constructor is declared protected to avoid undesired instantiation of the DevAVDbViewModel type without the POCO proxy factory.
        ''' </summary>
        Protected Sub New()
            MyBase.New(GetUnitOfWorkFactory())
            SetIsDebug()
            NavigationPaneVisibility = NavigationPaneVisibility.Normal
            CreateNotificationsInfo()
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata(Of EmployeeTaskStatusWithExternalMetadata)().AddMetadata(Of EmployeeTaskPriorityWithExternalMetadata)().AddMetadata(Of PersonPrefixWithExternalMetadata)()
        End Sub

        Protected Overrides Function CreateModules() As DevAVDbModuleDescription()
#If Not DXCORE3
            Return New DevAVDbModuleDescription() {New DevAVDbModuleDescription("Employees", "EmployeeCollectionView", TablesGroup, GetEmployeesFilterTree(Me), GetPeekCollectionViewModelFactory(Function(x) x.Employees)), New DevAVDbModuleDescription("Tasks", "TaskCollectionView", TablesGroup, GetTasksFilterTree(Me)), New DevAVDbModuleDescription("Customers", "CustomerCollectionView", TablesGroup, GetCustomersFilterTree(Me), GetPeekCollectionViewModelFactory(Function(x) x.Customers)), New DevAVDbModuleDescription("Products", "ProductCollectionView", TablesGroup, GetProductsFilterTree(Me), GetPeekCollectionViewModelFactory(Function(x) x.Products)), New DevAVDbModuleDescription("Sales", "OrderCollectionView", TablesGroup, GetSalesFilterTree(Me)), New DevAVDbModuleDescription("Opportunities", "QuoteCollectionView", TablesGroup, GetOpportunitiesFilterTree(Me))}
#Else
            return new DevAVDbModuleDescription[] {
                new DevAVDbModuleDescription("Employees", "EmployeeCollectionView", TablesGroup, FiltersSettings.GetEmployeesFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Employees)),
                new DevAVDbModuleDescription("Customers", "CustomerCollectionView", TablesGroup, FiltersSettings.GetCustomersFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Customers)),
                new DevAVDbModuleDescription("Products", "ProductCollectionView", TablesGroup, FiltersSettings.GetProductsFilterTree(this), GetPeekCollectionViewModelFactory(x => x.Products)),
                new DevAVDbModuleDescription("Sales", "OrderCollectionView", TablesGroup, FiltersSettings.GetSalesFilterTree(this)),
                new DevAVDbModuleDescription("Opportunities", "QuoteCollectionView", TablesGroup, FiltersSettings.GetOpportunitiesFilterTree(this)),
            };
#End If
        End Function

        Private defaultWorkspace As Workspace = New Workspace()

        Private linksViewModelField As LinksViewModel

        Public Overrides Sub OnLoaded(ByVal [module] As DevAVDbModuleDescription)
            MyBase.OnLoaded([module])
            RegisterJumpList()
            Dim timer = New DispatcherTimer()
            timer.Interval = New TimeSpan(0, 0, 0, 20)
            If Not IsDebug Then AddHandler timer.Tick, AddressOf OnTimerTick
            timer.Start()
        End Sub

        Public Overrides Sub SaveLogicalLayout()
        End Sub

        Public Overrides Function RestoreLogicalLayout() As Boolean
            Return False
        End Function

        Protected Overrides Sub OnActiveModuleChanged(ByVal oldModule As DevAVDbModuleDescription)
            MyBase.OnActiveModuleChanged(oldModule)
            If ActiveModule IsNot Nothing AndAlso ActiveModule.FilterTreeViewModel IsNot Nothing Then ActiveModule.FilterTreeViewModel.SetViewModel(DocumentManagerService.ActiveDocument.Content)
            Dim title = Convert.ToString(DocumentManagerService.ActiveDocument.Title)
            MainWindowService.Title = title & " - DevAV"
            Call Log(String.Format("OutlookInspiredApp: Select module: {0}", title.ToUpper()))
            FinishModuleChangingDispatcherService.BeginInvoke(Sub() UpdateWorkspace(oldModule, ActiveModule))
        End Sub

        <Conditional("DEBUG")>
        Private Sub SetIsDebug()
            IsDebug = True
        End Sub

        Const ResourcePathHeader As String = "pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/"

        Private Notifications As List(Of Tuple(Of String, String, Action(Of Task(Of NotificationResult))))

        Private notificationIndex As Integer = 0

        Private Sub OnTimerTick(ByVal sender As Object, ByVal e As EventArgs)
            Dim info = Notifications(notificationIndex)
            Dim timer = CType(sender, DispatcherTimer)
            NotificationService.CreatePredefinedNotification("DevAV Tips & Tricks", info.Item1, "", New BitmapImage(New Uri(ResourcePathHeader & info.Item2))).ShowAsync().ContinueWith(info.Item3, TaskScheduler.FromCurrentSynchronizationContext())
            If Threading.Interlocked.Increment(notificationIndex) < Notifications.Count Then
                timer.Interval = New TimeSpan(0, 0, 60)
            Else
                timer.Stop()
            End If
        End Sub

        Private ReadOnly Property SignleObjectDocumentManagerService As IDocumentManagerService
            Get
                Return GetService(Of IDocumentManagerService)("SignleObjectDocumentManagerService")
            End Get
        End Property

        Private ReadOnly Property WorkspaceService As IWorkspaceService
            Get
                Return GetRequiredService(Of IWorkspaceService)()
            End Get
        End Property

        Private ReadOnly Property MainWindowService As IMainWindowService
            Get
                Return Me.GetRequiredService(Of IMainWindowService)()
            End Get
        End Property

        Private ReadOnly Property FinishModuleChangingDispatcherService As IDispatcherService
            Get
                Return GetRequiredService(Of IDispatcherService)("FinishModuleChangingDispatcherService")
            End Get
        End Property

        Private ReadOnly Property NotificationService As INotificationService
            Get
                Return GetRequiredService(Of INotificationService)()
            End Get
        End Property

        Private Sub UpdateWorkspace(ByVal oldModule As DevAVDbModuleDescription, ByVal newModule As DevAVDbModuleDescription)
            Dim oldWorkspace As Workspace = WorkspaceService.SaveWorkspace()
            Dim newWorkspace As Workspace = New Workspace()
            Try
                If oldModule IsNot Nothing Then oldModule.Workspace = oldWorkspace
                If newModule IsNot Nothing Then newWorkspace = If(newModule.Workspace, defaultWorkspace)
            Finally
                WorkspaceService.RestoreWorkspace(newWorkspace)
            End Try
        End Sub

        Public ReadOnly Property LinksViewModel As LinksViewModel
            Get
                If linksViewModelField Is Nothing Then linksViewModelField = LinksViewModel.Create()
                Return linksViewModelField
            End Get
        End Property

        Private Sub RegisterJumpList()
#If Not CLICKONCE And Not DXCORE3
            Dim jumpListService As IApplicationJumpListService = GetRequiredService(Of IApplicationJumpListService)()
            jumpListService.Items.AddOrReplace("Modules", "New Employee", EmployeeIcon, Sub() SignleObjectDocumentManagerService.ShowNewEntityDocument(Of Employee)(Me))
            jumpListService.Items.AddOrReplace("Modules", "Customers", CustomerIcon, Sub()
                Show(Modules.Where(Function(m) Equals(m.DocumentType, "CustomerCollectionView")).First())
            End Sub)
            jumpListService.Items.AddOrReplace("Modules", "Opportunities", OpportunitiesIcon, Sub()
                Show(Modules.Where(Function(m) Equals(m.DocumentType, "QuoteCollectionView")).First())
            End Sub)
            jumpListService.Apply()
#End If
        End Sub

        Private Sub CreateNotificationsInfo()
            Notifications = New List(Of Tuple(Of String, String, Action(Of Task(Of NotificationResult))))()
            Notifications.Add(New Tuple(Of String, String, Action(Of Task(Of NotificationResult)))("Become a UI Superhero, check out our WYSIWYG Reporting in the Sales Module", "reports_96x96.png", Sub(x)
                OnNotificationShown(x, Modules.Where(Function(m) Equals(m.DocumentType, "OrderCollectionView")).First())
            End Sub))
            Notifications.Add(New Tuple(Of String, String, Action(Of Task(Of NotificationResult)))("Become a UI Superhero, explore PDF-documents with DevExpress PDF Viewer", "pdf_96x96.png", Sub(x)
                OnNotificationShown(x, Modules.Where(Function(m) Equals(m.DocumentType, "ProductCollectionView")).First())
            End Sub))
        End Sub

        Private Sub OnNotificationShown(ByVal task As Task(Of NotificationResult), ByVal [module] As DevAVDbModuleDescription)
            Try
                If task.Result = NotificationResult.Activated AndAlso [module] IsNot Nothing Then Show([module])
            Catch __unusedAggregateException1__ As AggregateException
            End Try
        End Sub

        Private ReadOnly Property EmployeeIcon As ImageSource
            Get
                Return CType(New SvgImageSourceExtension() With {.Uri = New Uri(ResourcePathHeader & "NewEmploye.svg")}.ProvideValue(Nothing), ImageSource)
            End Get
        End Property

        Private ReadOnly Property CustomerIcon As ImageSource
            Get
                Return CType(New SvgImageSourceExtension() With {.Uri = New Uri(ResourcePathHeader & "NewCustomer.svg")}.ProvideValue(Nothing), ImageSource)
            End Get
        End Property

        Private ReadOnly Property OpportunitiesIcon As ImageSource
            Get
                Return CType(New SvgImageSourceExtension() With {.Uri = New Uri(ResourcePathHeader & "NewOpportunities.svg")}.ProvideValue(Nothing), ImageSource)
            End Get
        End Property
    End Class

    Public Partial Class DevAVDbModuleDescription
        Inherits ModuleDescription(Of DevAVDbModuleDescription)

        Private _ImageSource As ImageSource, _FilterTreeViewModel As IFilterTreeViewModel

        Public Sub New(ByVal title As String, ByVal documentType As String, ByVal group As String, ByVal filterTreeViewModel As IFilterTreeViewModel, ByVal Optional peekCollectionViewModelFactory As Func(Of DevAVDbModuleDescription, Object) = Nothing)
            MyBase.New(title, documentType, group, peekCollectionViewModelFactory)
            ImageSource = CType(New SvgImageSourceExtension() With {.Uri = New Uri(String.Format("pack://application:,,,/DevExpress.OutlookInspiredApp.Wpf;component/Resources/Modules/{0}.svg", title)), .Size = New Windows.Size(24, 24)}.ProvideValue(Nothing), ImageSource)
            Me.FilterTreeViewModel = filterTreeViewModel
        End Sub

        Public Property Workspace As Workspace

        Public Property ImageSource As ImageSource
            Get
                Return _ImageSource
            End Get

            Private Set(ByVal value As ImageSource)
                _ImageSource = value
            End Set
        End Property

        Public Property FilterTreeViewModel As IFilterTreeViewModel
            Get
                Return _FilterTreeViewModel
            End Get

            Private Set(ByVal value As IFilterTreeViewModel)
                _FilterTreeViewModel = value
            End Set
        End Property
    End Class
End Namespace
