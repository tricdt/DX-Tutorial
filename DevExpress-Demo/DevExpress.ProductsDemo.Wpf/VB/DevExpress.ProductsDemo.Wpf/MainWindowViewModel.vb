Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Windows
Imports DevExpress.Images
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core
Imports ProductsDemo.Modules
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.DevAV
Imports System.Windows.Media

Namespace ProductsDemo

    Public Class MainWindowViewModel

        Public Sub New()
            CanShowSplashScreen = False
            Modules = New List(Of ModuleInfo)() From {ViewModelSource.Create(Function() New ModuleInfo("GridTasksModule", Me, "Grid: Tasks")).SetIcon("GridTasks"), ViewModelSource.Create(Function() New ModuleInfo("GridContactsModule", Me, "Grid: Contacts")).SetIcon("GridContacts"), ViewModelSource.Create(Function() New ModuleInfo("SpreadsheetModule", Me, "Spreadsheet")).SetIcon("Spreadsheet"), ViewModelSource.Create(Function() New ModuleInfo("RichEditModule", Me, "Word Processing")).SetIcon("WordProcessing"), ViewModelSource.Create(Function() New ModuleInfo("ReportsModule", Me, "Banded Reports")).SetIcon("BandedReports"), ViewModelSource.Create(Function() New ModuleInfo("PivotGridModule", Me, "Pivot Table")).SetIcon("Pivot"), ViewModelSource.Create(Function() New ModuleInfo("SalesDashboard", Me, "Analytics")).SetIcon("Analytics"), ViewModelSource.Create(Function() New ModuleInfo("PhotoGallery", Me, "Photo Gallery Map")).SetIcon("WeatherMap"), ViewModelSource.Create(Function() New ModuleInfo("SchedulerModule", Me, "Scheduler")).SetIcon("Scheduler"), ViewModelSource.Create(Function() New ModuleInfo("PdfViewerDemo", Me, "Pdf Viewer")).SetIcon("PDFViewer")}
            MetadataLocator.Default = MetadataLocator.Create().AddMetadata(Of PrefixEnumWithExternalMetadata)()
        End Sub

        Public Overridable Property Modules As IEnumerable(Of ModuleInfo)

        Public Overridable Property SelectedModuleInfo As ModuleInfo

        Public Overridable Property CanShowSplashScreen As Boolean

        Public Overridable Property DefaultBackstatgeIndex As Integer

        Public Overridable Property HasPrinting As Boolean

        Public Overridable Property IsBackstageOpen As Boolean

        Public Sub [Exit]()
            CurrentWindowService.Close()
        End Sub

        Public Sub OnModulesLoaded()
            If SelectedModuleInfo Is Nothing Then
                SelectedModuleInfo = Modules.First()
                SelectedModuleInfo.IsSelected = True
                SelectedModuleInfo.Show()
            End If

            ApplicationJumpListService.Items.AddOrReplace("New Task", GetIcon("SvgImages/Outlook Inspired/Task.svg"), New Action(AddressOf ShowGridTasksModuleNewItemWindow))
            ApplicationJumpListService.Items.AddOrReplace("New Contact", GetIcon("SvgImages/Outlook Inspired/Mr.svg"), New Action(AddressOf ShowGridContactsModuleNewItemWindow))
            ApplicationJumpListService.Apply()
            CanShowSplashScreen = True
        End Sub

        <Required>
        Protected Overridable ReadOnly Property CurrentWindowService As ICurrentWindowService
            Get
                Return Nothing
            End Get
        End Property

        <Required>
        Protected Overridable ReadOnly Property ApplicationJumpListService As IApplicationJumpListService
            Get
                Return Nothing
            End Get
        End Property

        <Required>
        Protected Overridable ReadOnly Property NavigationService As INavigationService
            Get
                Return Nothing
            End Get
        End Property

        Protected Overridable Sub OnSelectedModuleInfoChanged()
            If Not allowSelectedModuleInfoChanged Then Return
            PrintableControlLink = Nothing
            SelectedModuleInfo.IsSelected = True
            SelectedModuleInfo.Show()
        End Sub

        Protected Overridable Sub OnIsBackstageOpenChanged()
            HasPrinting = PrintingService.HasPrinting
            If Not HasPrinting AndAlso DefaultBackstatgeIndex = 1 Then DefaultBackstatgeIndex = 0
        End Sub

        Private Function GetIcon(ByVal path As String) As ImageSource
            Dim extension = New SvgImageSourceExtension() With {.Uri = AssemblyHelper.GetResourceUri(GetType(DXImages).Assembly, path), .Size = New Size(16, 16)}
            Dim icon = CType(extension.ProvideValue(Nothing), ImageSource)
            Return icon
        End Function

        Private allowSelectedModuleInfoChanged As Boolean = True

        Private Sub ShowGridModuleNewItemWindow(Of T As Class)(ByVal moduleType As String)
            If Application.Current.Windows.Count <> 1 Then Return
            Dim viewModel As GridViewModelBase(Of T) = TryCast(ViewHelper.GetViewModelFromView(NavigationService.Current), GridViewModelBase(Of T))
            If viewModel IsNot Nothing Then
                viewModel.ShowNewItemWindow()
            Else
                Dim moduleInfo = Modules.[Single](Function(m) Equals(m.Type, moduleType))
                allowSelectedModuleInfoChanged = False
                SelectedModuleInfo = moduleInfo
                allowSelectedModuleInfoChanged = True
                moduleInfo.Show(GridModuleNavigationParameter.NewItem)
            End If
        End Sub

        Private Sub ShowGridTasksModuleNewItemWindow()
            ShowGridModuleNewItemWindow(Of Task)("GridTasksModule")
        End Sub

        Private Sub ShowGridContactsModuleNewItemWindow()
            ShowGridModuleNewItemWindow(Of Contact)("GridContactsModule")
        End Sub
    End Class

    Public Class ModuleInfo

        Private _Type As String, _Title As String

        Private parent As ISupportServices

        Public Sub New(ByVal _type As String, ByVal parent As Object, ByVal _title As String)
            Type = _type
            Me.parent = CType(parent, ISupportServices)
            Title = _title
        End Sub

        Public Property Type As String
            Get
                Return _Type
            End Get

            Private Set(ByVal value As String)
                _Type = value
            End Set
        End Property

        Public Overridable Property IsSelected As Boolean

        Public Property Title As String
            Get
                Return _Title
            End Get

            Private Set(ByVal value As String)
                _Title = value
            End Set
        End Property

        Public Overridable Property Icon As ImageSource

        Public Function SetIcon(ByVal icon As String) As ModuleInfo
            Dim extension = New SvgImageSourceExtension() With {.Uri = New Uri(String.Format("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/{0}.svg", icon), UriKind.RelativeOrAbsolute)}
            Me.Icon = CType(extension.ProvideValue(Nothing), ImageSource)
            Return Me
        End Function

        Public Sub Show(ByVal Optional parameter As Object = Nothing)
            Dim navigationService As INavigationService = parent.ServiceContainer.GetRequiredService(Of INavigationService)()
            navigationService.Navigate(Type, parameter, parent)
        End Sub
    End Class

    Public Class PrefixEnumWithExternalMetadata

        Public Shared Sub BuildMetadata(ByVal builder As EnumMetadataBuilder(Of PersonPrefix))
            builder.Member(PersonPrefix.Dr).ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Doctor.svg").EndMember().Member(PersonPrefix.Mr).ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Mr.svg").EndMember().Member(PersonPrefix.Ms).ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Ms.svg").EndMember().Member(PersonPrefix.Miss).ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Miss.svg").EndMember().Member(PersonPrefix.Mrs).ImageUri("pack://application:,,,/DevExpress.ProductsDemo.Wpf;component/Images/Tasks/Mrs.svg").EndMember()
        End Sub
    End Class
End Namespace
