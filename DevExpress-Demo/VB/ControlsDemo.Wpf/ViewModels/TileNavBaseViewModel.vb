Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows
Imports System.Windows.Input
Imports System.Windows.Media
Imports CommonDemo.Helpers
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.Native
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm.UI
Imports DevExpress.Xpf.Navigation.Internal

Namespace ControlsDemo

    Public Class TileNavViewLocator
        Inherits ViewLocator

        Public Sub New()
            MyBase.New(GetType(TileNavViewLocator).Assembly)
        End Sub
    End Class

    Public Class TileNavBaseViewModel
        Inherits ViewModelBase

        Private _BackCommand As ICommand, _ViewUnloadedCommand As ICommand

        Private _Actions As ObservableCollection(Of TileNavBaseItemViewModel)

        Private _Categories As ObservableCollection(Of TileNavBaseItemViewModel)

        Private Selected As TileNavBaseItemViewModel

        Public Sub New()
            Call Messenger.Default.Register(Of NavigationMessage)(Me, New Action(Of NavigationMessage)(AddressOf OnNavigationMessage))
            _Categories = New ObservableCollection(Of TileNavBaseItemViewModel)(TileNavBaseViewModelDataProvider.CreateCategories())
            _Actions = New ObservableCollection(Of TileNavBaseItemViewModel)(TileNavBaseViewModelDataProvider.CreateActions())
            BackCommand = DelegateCommandFactory.Create(New Action(AddressOf OnBackCommand), New Func(Of Boolean)(AddressOf CanBackCommand))
            ViewUnloadedCommand = New DelegateCommand(AddressOf OnViewUnloaded)
        End Sub

        Public ReadOnly Property Actions As ObservableCollection(Of TileNavBaseItemViewModel)
            Get
                Return _Actions
            End Get
        End Property

        Public Property BackCommand As ICommand
            Get
                Return _BackCommand
            End Get

            Private Set(ByVal value As ICommand)
                _BackCommand = value
            End Set
        End Property

        Public ReadOnly Property Categories As ObservableCollection(Of TileNavBaseItemViewModel)
            Get
                Return _Categories
            End Get
        End Property

        Public Property ViewUnloadedCommand As ICommand
            Get
                Return _ViewUnloadedCommand
            End Get

            Private Set(ByVal value As ICommand)
                _ViewUnloadedCommand = value
            End Set
        End Property

        Private ReadOnly Property NavigationService As INavigationService
            Get
                Return ServiceContainer.GetService(Of INavigationService)()
            End Get
        End Property

        Protected Overridable Sub OnNavigationMessage(ByVal message As NavigationMessage)
            Select Case message.MessageType
                Case NavigationMessageType.New
                    Dim vm As TileNavBaseItemViewModel = TryCast(message.Source, TileNavBaseItemViewModel)
                    If vm IsNot Nothing AndAlso vm.IsSelected Then
                        NavigationService.Navigate("TileNavDetailsView", vm)
                        If Selected IsNot Nothing Then Selected.IsSelected = False
                        Selected = vm
                    End If

                Case NavigationMessageType.Back
                    OnBackCommand()
            End Select
        End Sub

        Protected Overridable Sub OnViewUnloaded()
            Call Messenger.Default.Unregister(Of NavigationMessage)(Me, New Action(Of NavigationMessage)(AddressOf OnNavigationMessage))
        End Sub

        Private Function CanBackCommand() As Boolean
            Return NavigationService IsNot Nothing AndAlso NavigationService.CanGoBack
        End Function

        Private Sub OnBackCommand()
            If NavigationService.CanGoBack Then
                NavigationService.GoBack()
                If Selected IsNot Nothing Then Selected.IsSelected = False
                Selected = Nothing
            End If
        End Sub
    End Class

    Public Class TileNavBaseItemViewModel

        Private _BackCommand As ICommand, _Items As ObservableCollection(Of ControlsDemo.TileNavBaseItemViewModel)

        Protected Sub New()
            Items = New ObservableCollection(Of TileNavBaseItemViewModel)()
            BackCommand = DelegateCommandFactory.Create(New Action(AddressOf OnBackCommand))
            ShowNotificationCommand = DelegateCommandFactory.Create(New Action(Of String)(AddressOf OnShowNotificationCommand))
        End Sub

        Public Property BackCommand As ICommand
            Get
                Return _BackCommand
            End Get

            Private Set(ByVal value As ICommand)
                _BackCommand = value
            End Set
        End Property

        Public Overridable Property Color As Color

        Public Overridable Property ContentImage As ImageSource

        Public Overridable Property DisplayName As String

        Public Overridable Property Image As ImageSource

        Public Overridable Property IsSelected As Boolean

        Public Property Items As ObservableCollection(Of TileNavBaseItemViewModel)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As ObservableCollection(Of TileNavBaseItemViewModel))
                _Items = value
            End Set
        End Property

        Public Overridable Property ShowNotificationCommand As ICommand

        Public Property Size As TileSize

        Public Shared Function Create() As TileNavBaseItemViewModel
            Return ViewModelSource.Create(Function() New TileNavBaseItemViewModel())
        End Function

        Private Sub OnBackCommand()
            Call Messenger.Default.Send(New NavigationMessage(NavigationMessageType.Back))
        End Sub

        Protected Sub OnIsSelectedChanged()
            If IsSelected Then Call Messenger.Default.Send(New NavigationMessage(Me))
        End Sub

        Private Sub OnShowNotificationCommand(ByVal message As String)
            Call Messenger.Default.Send(New NotificationMessage(DisplayName & " clicked"))
        End Sub
    End Class

    Friend Module TileNavBaseViewModelDataProvider

        Private ContentImages As String() = New String() {"pack://application:,,,/ControlsDemo;component/Images/Calc.svg", "pack://application:,,,/ControlsDemo;component/Images/Rates.svg", "pack://application:,,,/ControlsDemo;component/Images/Research.svg", "pack://application:,,,/ControlsDemo;component/Images/Statistics.svg", "pack://application:,,,/ControlsDemo;component/Images/System.svg", "pack://application:,,,/ControlsDemo;component/Images/UserManagment.svg", "pack://application:,,,/ControlsDemo;component/Images/Home.svg"}

        Private ItemImages As String() = New String() {"pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement01.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement02.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement03.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement04.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement05.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement06.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement07.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement08.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement09.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement10.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement11.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement12.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement13.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement14.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement15.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement16.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement17.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement18.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement19.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement20.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement21.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement22.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/itemElement23.Glyph.svg"}

        Private SubItemImages As String() = New String() {"pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement01.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement02.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement03.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement04.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement05.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement06.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement07.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement08.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement09.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement10.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement11.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement12.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement13.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement14.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement15.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement16.Glyph.svg", "pack://application:,,,/ControlsDemo;component/Images/TileNavPane/subItemElement17.Glyph.svg"}

        Private Colors As Color() = New Color() {CType(ColorConverter.ConvertFromString("#FFC3213F"), Color), CType(ColorConverter.ConvertFromString("#FFE65E20"), Color), CType(ColorConverter.ConvertFromString("#FFD4AF00"), Color), CType(ColorConverter.ConvertFromString("#FF6652A2"), Color), CType(ColorConverter.ConvertFromString("#FF54AF0E"), Color), CType(ColorConverter.ConvertFromString("#FF00ABDC"), Color), CType(ColorConverter.ConvertFromString("#FFDA8515"), Color)}

        Private random As Random = New Random(Date.Now.Millisecond)

        Private Function CreateItem(ByVal displayName As String, ByVal Images As String(), ByVal imageSize As Size) As TileNavBaseItemViewModel
            Dim vm As TileNavBaseItemViewModel = TileNavBaseItemViewModel.Create()
            vm.[Do](Sub(x)
                x.DisplayName = displayName
                x.Color = Colors(random.Next(Colors.Length))
                x.Image = ImagesHelper.GetSvgImage(Images(random.Next(Images.Length)), imageSize)
                x.ContentImage = ImagesHelper.GetSvgImage(ContentImages(random.Next(ContentImages.Length)), New Size(64, 64))
            End Sub)
            Return vm
        End Function

        Public Function CreateCategories() As IEnumerable(Of TileNavBaseItemViewModel)
            Dim categories = New List(Of TileNavBaseItemViewModel)()
            For i As Integer = 1 To 6 - 1
                Dim category As TileNavBaseItemViewModel = CreateItem(String.Format("Category {0}", i), ItemImages, New Size(32, 32))
                For j As Integer = 1 To 5 - 1
                    Dim item As TileNavBaseItemViewModel = CreateItem(String.Format("Item {0}.{1}", i, j), ItemImages, New Size(32, 32))
                    For k As Integer = 1 To 4 - 1
                        Dim subItem As TileNavBaseItemViewModel = CreateItem(String.Format("SubItem {0}.{1}.{2}", i, j, k), SubItemImages, New Size(16, 16))
                        item.Items.Add(subItem)
                    Next

                    category.Items.Add(item)
                Next

                categories.Add(category)
            Next

            Return categories
        End Function

        Public Function CreateActions() As IEnumerable(Of TileNavBaseItemViewModel)
            Dim actions = New List(Of TileNavBaseItemViewModel)()
            For i As Integer = 1 To 5 - 1
                Dim action As TileNavBaseItemViewModel = CreateItem(String.Format("Action {0}", i), ItemImages, New Size(32, 32))
                If i < 3 Then action.Size = TileSize.Small
                actions.Add(action)
            Next

            Return actions
        End Function
    End Module

    Public Enum NavigationMessageType
        [New]
        Back
    End Enum

    Public NotInheritable Class NavigationMessage

        Private _Source As Object, _MessageType As NavigationMessageType

        Public Sub New(ByVal source As Object)
            Me.Source = source
        End Sub

        Public Sub New(ByVal messageType As NavigationMessageType, ByVal Optional source As Object = Nothing)
            Me.New(source)
            Me.MessageType = messageType
        End Sub

        Public Property Source As Object
            Get
                Return _Source
            End Get

            Private Set(ByVal value As Object)
                _Source = value
            End Set
        End Property

        Public Property MessageType As NavigationMessageType
            Get
                Return _MessageType
            End Get

            Private Set(ByVal value As NavigationMessageType)
                _MessageType = value
            End Set
        End Property
    End Class

    Public NotInheritable Class NotificationMessage

        Private _Source As String

        Public Sub New(ByVal source As String)
            Me.Source = source
        End Sub

        Public Property Source As String
            Get
                Return _Source
            End Get

            Private Set(ByVal value As String)
                _Source = value
            End Set
        End Property
    End Class
End Namespace
