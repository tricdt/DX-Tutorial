Imports System
Imports System.Windows.Media
Imports System.Windows.Media.Imaging
Imports DevExpress.DevAV.Common.Utils
Imports DevExpress.DevAV.DevAVDbDataModel
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.ViewModel

Namespace DevExpress.DevAV.ViewModels

    Public Partial Class DevAVDbViewModel
        Inherits DocumentsViewModel(Of DevAVDbModuleDescription, IDevAVDbUnitOfWork)

        Const MyWorldGroup As String = "My World"

        Const OperationsGroup As String = "Operations"

        Public Sub New()
            MyBase.New(GetUnitOfWorkFactory())
            IsTablet = True
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata(Of EmployeeTaskStatusWithExternalMetadata)().AddMetadata(Of EmployeeTaskPriorityWithExternalMetadata)()
        End Sub

        Protected Overrides Function CreateModules() As DevAVDbModuleDescription()
            Dim modules = New DevAVDbModuleDescription() {New DevAVDbModuleDescription("Dashboard", "DashboardView", MyWorldGroup, GetDashboardFilterTree(Me)), New DevAVDbModuleDescription("Tasks", "TaskCollectionView", MyWorldGroup, GetTasksFilterTree(Me)), New DevAVDbModuleDescription("Employees", "EmployeeCollectionView", MyWorldGroup, GetEmployeesFilterTree(Me)), New DevAVDbModuleDescription("Products", "ProductCollectionView", OperationsGroup, GetProductsFilterTree(Me)), New DevAVDbModuleDescription("Customers", "CustomerCollectionView", OperationsGroup, GetCustomersFilterTree(Me)), New DevAVDbModuleDescription("Sales", "OrderCollectionView", OperationsGroup, GetSalesFilterTree(Me)), New DevAVDbModuleDescription("Opportunities", "QuoteCollectionView", OperationsGroup, GetOpportunitiesFilterTree(Me))}
            For Each [module] In modules
                Dim moduleRef As DevAVDbModuleDescription = [module]
                [module].FilterTreeViewModel.NavigateAction = Sub() Show(moduleRef)
            Next

            Return modules
        End Function

        Protected Overrides Sub OnActiveModuleChanged(ByVal oldModule As DevAVDbModuleDescription)
            MyBase.OnActiveModuleChanged(oldModule)
            If ActiveModule Is Nothing Then Return
            If ActiveModule.FilterTreeViewModel IsNot Nothing Then ActiveModule.FilterTreeViewModel.SetViewModel(DocumentManagerService.ActiveDocument.Content)
            Call Log(String.Format("HybridApp: Select module: {0}", ActiveModule.ModuleTitle.ToUpper()))
        End Sub

        Protected Overrides Function GetModuleTitle(ByVal [module] As DevAVDbModuleDescription) As String
            Return MyBase.GetModuleTitle([module]) & " - DevAV"
        End Function

        Public Overrides Sub OnLoaded(ByVal [module] As DevAVDbModuleDescription)
            MyBase.OnLoaded([module])
#If Not DXCORE3
            IsTablet = DeviceDetector.IsTablet
#End If
            RegisterJumpList()
        End Sub

        Public Overrides Sub SaveLogicalLayout()
        End Sub

        Public Overrides Function RestoreLogicalLayout() As Boolean
            Return False
        End Function

        Private linksViewModelField As LinksViewModel

        Public ReadOnly Property LinksViewModel As LinksViewModel
            Get
                If linksViewModelField Is Nothing Then linksViewModelField = LinksViewModel.Create()
                Return linksViewModelField
            End Get
        End Property

        Public Overrides ReadOnly Property DefaultModule As DevAVDbModuleDescription
            Get
                Return Modules(2)
            End Get
        End Property

        Public Overridable Property IsTablet As Boolean

        Private Sub RegisterJumpList()
#If Not CLICKONCE And Not DXCORE3
            Dim jumpListService As IApplicationJumpListService = GetRequiredService(Of IApplicationJumpListService)()
            jumpListService.Items.AddOrReplace("Become a UI Superhero", "Explore DevExpress Universal", UniversalIcon, Sub() LinksViewModel.UniversalSubscription())
            jumpListService.Items.AddOrReplace("Online Tutorials", "Explore DevExpress Universal", TutorialsIcon, Sub() LinksViewModel.GettingStarted())
            jumpListService.Items.AddOrReplace("Buy Now", "Explore DevExpress Universal", BuyIcon, Sub() LinksViewModel.BuyNow())
            jumpListService.Apply()
#End If
        End Sub

        Private ReadOnly Property UniversalIcon As ImageSource
            Get
                Return New BitmapImage(New Uri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Universal.ico"))
            End Get
        End Property

        Private ReadOnly Property TutorialsIcon As ImageSource
            Get
                Return New BitmapImage(New Uri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/WPF.ico"))
            End Get
        End Property

        Private ReadOnly Property BuyIcon As ImageSource
            Get
                Return New BitmapImage(New Uri("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/DevExpress.ico"))
            End Get
        End Property
    End Class

    Public Partial Class DevAVDbModuleDescription
        Inherits ModuleDescription(Of DevAVDbModuleDescription)

        Private _ImageSource As Uri, _FilterTreeViewModel As IFilterTreeViewModel

        Public Sub New(ByVal title As String, ByVal documentType As String, ByVal group As String, ByVal filterTreeViewModel As IFilterTreeViewModel)
            MyBase.New(title, documentType, group, Nothing)
            ImageSource = New Uri(String.Format("pack://application:,,,/DevExpress.HybridApp.Wpf;component/Resources/Menu/{0}.svg", title))
            Me.FilterTreeViewModel = filterTreeViewModel
        End Sub

        Public Property ImageSource As Uri
            Get
                Return _ImageSource
            End Get

            Private Set(ByVal value As Uri)
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
