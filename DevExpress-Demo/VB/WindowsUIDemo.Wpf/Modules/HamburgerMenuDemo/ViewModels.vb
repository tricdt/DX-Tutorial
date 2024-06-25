Imports DevExpress.Mvvm
Imports DevExpress.Xpf.WindowsUI
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Windows.Input
Imports System.Linq
Imports DevExpress.Xpf.Grid
Imports DevExpress.Mvvm.DataAnnotations

Namespace WindowsUIDemo

#Region "HamburgerMenuItemsModels "
    Public MustInherit Class BottomBarItemViewModelBase
        Inherits DevExpress.Mvvm.BindableBase

        Private _placement As DevExpress.Xpf.WindowsUI.HamburgerMenuBottomBarItemPlacement = DevExpress.Xpf.WindowsUI.HamburgerMenuBottomBarItemPlacement.Left

        Private _icon As System.Uri = Nothing

        Public Property Placement As HamburgerMenuBottomBarItemPlacement
            Get
                Return Me._placement
            End Get

            Set(ByVal value As HamburgerMenuBottomBarItemPlacement)
                SetProperty(Me._placement, value, "Placement")
            End Set
        End Property

        Public Property Icon As Uri
            Get
                Return Me._icon
            End Get

            Set(ByVal value As Uri)
                SetProperty(Me._icon, value, "Icon")
            End Set
        End Property

        Public Sub New(ByVal icon As String)
            Me.Icon = WindowsUIDemo.HamburgerItemViewModelBase.GetIconUriFromName(icon)
        End Sub
    End Class

    Public Class BottomBarNavigationItemModel
        Inherits WindowsUIDemo.BottomBarItemViewModelBase

        Private _navigationTargetType As System.Type = Nothing

        Private _command As System.Windows.Input.ICommand = Nothing

        Public Property NavigationTargetType As Type
            Get
                Return Me._navigationTargetType
            End Get

            Set(ByVal value As Type)
                SetProperty(Me._navigationTargetType, value, "NavigationTargetType")
            End Set
        End Property

        Public Property Command As ICommand
            Get
                Return Me._command
            End Get

            Set(ByVal value As ICommand)
                SetProperty(Me._command, value, "Command")
            End Set
        End Property

        Public Sub New(ByVal icon As String, ByVal navigationTargetType As System.Type, ByVal Optional command As System.Windows.Input.ICommand = Nothing)
            MyBase.New(icon)
            Me.Command = command
            Me.NavigationTargetType = navigationTargetType
        End Sub
    End Class

    Public Class BottomBarCheckItemViewModel
        Inherits WindowsUIDemo.BottomBarItemViewModelBase

        Private _isChecked As Boolean? = Nothing

        Public Property IsChecked As Boolean?
            Get
                Return Me._isChecked
            End Get

            Set(ByVal value As Boolean?)
                SetProperty(Me._isChecked, value, "IsChecked", New Global.System.Action(AddressOf Me.OnIsCheckedChanged))
            End Set
        End Property

        Private checkedChangedAction As System.Action(Of Boolean?)

        Public Sub New(ByVal icon As String, ByVal checkAction As System.Action(Of Boolean?))
            MyBase.New(icon)
            Me.checkedChangedAction = checkAction
            Me.IsChecked = False
        End Sub

        Protected Overridable Sub OnIsCheckedChanged()
            Me.checkedChangedAction(Me.IsChecked)
        End Sub
    End Class

    Public MustInherit Class HamburgerItemViewModelBase
        Inherits DevExpress.Mvvm.BindableBase

        Private _navigationTargetType As System.Type = Nothing

        Private _content As Object = Nothing

        Private _icon As System.Uri = Nothing

        Private _placement As DevExpress.Xpf.WindowsUI.HamburgerMenuItemPlacement = DevExpress.Xpf.WindowsUI.HamburgerMenuItemPlacement.Top

        Public Property NavigationTargetType As Type
            Get
                Return Me._navigationTargetType
            End Get

            Set(ByVal value As Type)
                SetProperty(Me._navigationTargetType, value, "NavigationTargetType")
            End Set
        End Property

        Public Property Content As Object
            Get
                Return Me._content
            End Get

            Set(ByVal value As Object)
                SetProperty(Me._content, value, "Content")
            End Set
        End Property

        Public Property Icon As Uri
            Get
                Return Me._icon
            End Get

            Set(ByVal value As Uri)
                SetProperty(Me._icon, value, "Icon")
            End Set
        End Property

        Public Property Placement As HamburgerMenuItemPlacement
            Get
                Return Me._placement
            End Get

            Set(ByVal value As HamburgerMenuItemPlacement)
                SetProperty(Me._placement, value, "Placement")
            End Set
        End Property

        Public Sub New()
            Me.Placement = DevExpress.Xpf.WindowsUI.HamburgerMenuItemPlacement.Top
        End Sub

        Friend Shared Function GetIconUriFromName(ByVal name As String) As Uri
            If String.IsNullOrEmpty(name) Then Return Nothing
            Return New System.Uri(String.Format("pack://application:,,,/WindowsUIDemo;component/Images/Hamburger/{0}.png", name), System.UriKind.Absolute)
        End Function
    End Class

    Public MustInherit Class HamburgerSubMenuItemViewModelBase
        Inherits WindowsUIDemo.HamburgerItemViewModelBase

        Private _moreButtonVisibilityMode As DevExpress.Xpf.WindowsUI.HamburgerSubMenuMoreButtonVisibility = DevExpress.Xpf.WindowsUI.HamburgerSubMenuMoreButtonVisibility.Auto

        Private _isStandaloneSelectionItemMode As Boolean = True

        Public Property MoreButtonVisibilityMode As HamburgerSubMenuMoreButtonVisibility
            Get
                Return Me._moreButtonVisibilityMode
            End Get

            Set(ByVal value As HamburgerSubMenuMoreButtonVisibility)
                SetProperty(Me._moreButtonVisibilityMode, value, "MoreButtonVisibilityMode")
            End Set
        End Property

        Public Property IsStandaloneSelectionItemMode As Boolean
            Get
                Return Me._isStandaloneSelectionItemMode
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Me._isStandaloneSelectionItemMode, value, "IsStandaloneSelectionItemMode")
            End Set
        End Property

        Public Sub New()
            MyBase.New()
            Me.MoreButtonVisibilityMode = DevExpress.Xpf.WindowsUI.HamburgerSubMenuMoreButtonVisibility.Auto
        End Sub
    End Class

    Public Class HamburgerSubMenuItemViewModel
        Inherits WindowsUIDemo.HamburgerSubMenuItemViewModelBase

        Private _items As System.Collections.ObjectModel.ReadOnlyCollection(Of WindowsUIDemo.HamburgerSubItemViewModel)

        Public ReadOnly Property Items As ReadOnlyCollection(Of WindowsUIDemo.HamburgerSubItemViewModel)
            Get
                Return Me._items
            End Get
        End Property

        Public Sub New(ByVal content As Object, ByVal icon As String, ByVal items As System.Collections.Generic.IList(Of WindowsUIDemo.HamburgerSubItemViewModel))
            Me.Content = content
            Me.Icon = WindowsUIDemo.HamburgerItemViewModelBase.GetIconUriFromName(icon)
            Me._items = New System.Collections.ObjectModel.ReadOnlyCollection(Of WindowsUIDemo.HamburgerSubItemViewModel)(items)
        End Sub
    End Class

    Public Class HamburgerThemeSwitcherItemViewModel
        Inherits WindowsUIDemo.HamburgerSubMenuItemViewModelBase

        Public Sub New(ByVal content As Object, ByVal icon As String)
            Me.Content = content
            Me.Icon = WindowsUIDemo.HamburgerItemViewModelBase.GetIconUriFromName(icon)
        End Sub
    End Class

    Public MustInherit Class HamburgerItemWithCommand
        Inherits WindowsUIDemo.HamburgerItemViewModelBase

        Private _command As System.Windows.Input.ICommand = Nothing

        Private _commandParameter As Object = Nothing

        Public Property Command As ICommand
            Get
                Return Me._command
            End Get

            Set(ByVal value As ICommand)
                SetProperty(Me._command, value, "Command")
            End Set
        End Property

        Public Property CommandParameter As Object
            Get
                Return Me._commandParameter
            End Get

            Set(ByVal value As Object)
                SetProperty(Me._commandParameter, value, "CommandParameter")
            End Set
        End Property
    End Class

    Public Class HamburgerSubItemViewModel
        Inherits WindowsUIDemo.HamburgerItemWithCommand

        Private _rightContent As Object = Nothing

        Private _showInPreview As Boolean = True

        Private _previewContent As Object = Nothing

        Private _isSelected As Boolean = False

        Public Property RightContent As Object
            Get
                Return Me._rightContent
            End Get

            Set(ByVal value As Object)
                SetProperty(Me._rightContent, value, "RightContent")
            End Set
        End Property

        Public Property ShowInPreview As Boolean
            Get
                Return Me._showInPreview
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Me._showInPreview, value, "ShowInPreview")
            End Set
        End Property

        Public Property PreviewContent As Object
            Get
                Return Me._previewContent
            End Get

            Set(ByVal value As Object)
                SetProperty(Me._previewContent, value, "PreviewContent")
            End Set
        End Property

        Public Property IsSelected As Boolean
            Get
                Return Me._isSelected
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Me._isSelected, value, "IsSelected")
            End Set
        End Property

        Public Sub New(ByVal content As Object, ByVal rightContent As Object, ByVal navigationTargetType As System.Type, ByVal showInPreview As Boolean, ByVal parentViewModel As Object)
            Me.Content = content
            Me.RightContent = rightContent
            Me.NavigationTargetType = navigationTargetType
            Me.ShowInPreview = showInPreview
        End Sub
    End Class

    Public Class HamburgerLinkItemViewModel
        Inherits WindowsUIDemo.HamburgerItemViewModelBase

        Private _navigationUri As System.Uri = Nothing

        Public Property NavigationUri As Uri
            Get
                Return Me._navigationUri
            End Get

            Set(ByVal value As Uri)
                SetProperty(Me._navigationUri, value, "NavigationUri")
            End Set
        End Property

        Public Sub New(ByVal content As Object, ByVal navigationUri As System.Uri)
            Me.Content = content
            Me.NavigationUri = navigationUri
        End Sub
    End Class

    Public Class HamburgerNavigationItemViewModel
        Inherits WindowsUIDemo.HamburgerItemWithCommand

        Private _hideMenuWhenSelected As Boolean = False

        Private _selectOnClick As Boolean = True

        Public Property HideMenuWhenSelected As Boolean
            Get
                Return Me._hideMenuWhenSelected
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Me._hideMenuWhenSelected, value, "HideMenuWhenSelected")
            End Set
        End Property

        Public Property SelectOnClick As Boolean
            Get
                Return Me._selectOnClick
            End Get

            Set(ByVal value As Boolean)
                SetProperty(Me._selectOnClick, value, "SelectOnClick")
            End Set
        End Property

        Public Sub New(ByVal content As Object, ByVal icon As String, ByVal navigationTargetType As System.Type, ByVal Optional command As System.Windows.Input.ICommand = Nothing)
            Me.Content = content
            Me.Icon = WindowsUIDemo.HamburgerItemViewModelBase.GetIconUriFromName(icon)
            Me.NavigationTargetType = navigationTargetType
            Me.Command = command
        End Sub
    End Class

#End Region
#Region "MainViewModel"
    <DevExpress.Mvvm.DataAnnotations.POCOViewModelAttribute>
    Public Class HamburgerMenuDemoViewModel

        Private _Items As ReadOnlyCollection(Of WindowsUIDemo.HamburgerItemViewModelBase), _BottomBarItems As ReadOnlyCollection(Of WindowsUIDemo.BottomBarItemViewModelBase), _CompactFilterItems As ObservableCollection(Of DevExpress.Xpf.Grid.CompactModeFilterItem)

        Public Overridable Property FocusedRow As Message

        Public Overridable Property MailFilter As String

        Public Overridable Property Header As String

        Public Overridable Property DateColumnHeader As String

        Public Overridable Property ShowMenuOnEmptySpaceBarClick As Boolean

        Public Overridable Property AvailableViewStates As HamburgerMenuAvailableViewStates

        Public Overridable Property IsFlyoutViewStateEnabled As Boolean

        Public Overridable Property IsOverlayViewStateEnabled As Boolean

        Public Overridable Property IsInlineViewStateEnabled As Boolean

        Public Overridable Property ShowRevealHighlightEffect As Boolean

        Public ReadOnly Property Data As IEnumerable(Of WindowsUIDemo.Message)
            Get
                Return WindowsUIDemo.MailItems.Messages
            End Get
        End Property

        Public Property Items As ReadOnlyCollection(Of WindowsUIDemo.HamburgerItemViewModelBase)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As ReadOnlyCollection(Of WindowsUIDemo.HamburgerItemViewModelBase))
                _Items = value
            End Set
        End Property

        Public Property BottomBarItems As ReadOnlyCollection(Of WindowsUIDemo.BottomBarItemViewModelBase)
            Get
                Return _BottomBarItems
            End Get

            Private Set(ByVal value As ReadOnlyCollection(Of WindowsUIDemo.BottomBarItemViewModelBase))
                _BottomBarItems = value
            End Set
        End Property

        Public Property CompactFilterItems As ObservableCollection(Of DevExpress.Xpf.Grid.CompactModeFilterItem)
            Get
                Return _CompactFilterItems
            End Get

            Private Set(ByVal value As ObservableCollection(Of DevExpress.Xpf.Grid.CompactModeFilterItem))
                _CompactFilterItems = value
            End Set
        End Property

        Public Sub New()
            Me.ShowMenuOnEmptySpaceBarClick = True
            Me.IsInlineViewStateEnabled = True
            Me.IsOverlayViewStateEnabled = True
            Me.IsFlyoutViewStateEnabled = True
            Me.ShowRevealHighlightEffect = True
            Me.CompactFilterItems = New System.Collections.ObjectModel.ObservableCollection(Of DevExpress.Xpf.Grid.CompactModeFilterItem)() From {New DevExpress.Xpf.Grid.CompactModeFilterItem() With {.DisplayValue = "All"}, New DevExpress.Xpf.Grid.CompactModeFilterItem() With {.DisplayValue = "Unread", .EditValue = "[IsUnread] = True"}}
            Me.Items = New System.Collections.ObjectModel.ReadOnlyCollection(Of WindowsUIDemo.HamburgerItemViewModelBase)(Me.CreateMenuItems())
            Me.BottomBarItems = New System.Collections.ObjectModel.ReadOnlyCollection(Of WindowsUIDemo.BottomBarItemViewModelBase)(Me.CreateBottomBarItems())
            If Me.Data IsNot Nothing AndAlso Me.Data.Any() Then Me.FocusedRow = Me.Data.First()
            Me.UpdateMailFilter(WindowsUIDemo.MailType.Inbox)
        End Sub

        Private Function CreateBottomBarItems() As IList(Of WindowsUIDemo.BottomBarItemViewModelBase)
            Return New System.Collections.Generic.List(Of WindowsUIDemo.BottomBarItemViewModelBase)()
        End Function

        Private Sub UpdateMailFilter(ByVal mailType As WindowsUIDemo.MailType)
            Me.MailFilter = "[MailType] = '" & mailType & "'"
            Select Case mailType
                Case WindowsUIDemo.MailType.Deleted
                    Me.DateColumnHeader = "Deleted"
                    Me.Header = "Trash"
                Case WindowsUIDemo.MailType.Sent
                    Me.DateColumnHeader = "Sent"
                    Me.Header = "Sent"
                Case WindowsUIDemo.MailType.Draft
                    Me.DateColumnHeader = "Created"
                    Me.Header = "Drafts"
                Case Else
                    Me.DateColumnHeader = "Received"
                    Me.Header = "Inbox"
            End Select

            If Me.CompactFilterItems IsNot Nothing Then
                System.Linq.Enumerable.First(Of DevExpress.Xpf.Grid.CompactModeFilterItem)(Me.CompactFilterItems).EditValue = Me.MailFilter
                Me.CompactFilterItems(CInt((1))).EditValue = Me.MailFilter & " And [IsUnread] = True"
            End If

            If Me.Data IsNot Nothing AndAlso Me.Data.Any() Then Me.FocusedRow = Me.Data.First(Function(p) p.MailType = mailType)
        End Sub

        Private Function GetMailCountByType(ByVal type As WindowsUIDemo.MailType) As Integer
            Return Me.Data.Count(Function(p) p.MailType = type)
        End Function

        Private Function CreateMenuItems() As IList(Of WindowsUIDemo.HamburgerItemViewModelBase)
            Dim command = New DevExpress.Mvvm.DelegateCommand(Of WindowsUIDemo.MailType)(Sub(t) Me.UpdateMailFilter(t))
            Dim subItems = New System.Collections.Generic.List(Of WindowsUIDemo.HamburgerSubItemViewModel)() From {New WindowsUIDemo.HamburgerSubItemViewModel("Inbox", Me.GetMailCountByType(WindowsUIDemo.MailType.Inbox), Nothing, True, Me) With {.Command = command, .CommandParameter = WindowsUIDemo.MailType.Inbox, .IsSelected = True}, New WindowsUIDemo.HamburgerSubItemViewModel("Drafts", Me.GetMailCountByType(WindowsUIDemo.MailType.Draft), Nothing, True, Me) With {.Command = command, .CommandParameter = WindowsUIDemo.MailType.Draft}, New WindowsUIDemo.HamburgerSubItemViewModel("Sent", Me.GetMailCountByType(WindowsUIDemo.MailType.Sent), Nothing, True, Me) With {.Command = command, .CommandParameter = WindowsUIDemo.MailType.Sent}, New WindowsUIDemo.HamburgerSubItemViewModel("Trash", Me.GetMailCountByType(WindowsUIDemo.MailType.Deleted), Nothing, False, Me) With {.Command = command, .CommandParameter = WindowsUIDemo.MailType.Deleted}}
            Dim items = New System.Collections.Generic.List(Of WindowsUIDemo.HamburgerItemViewModelBase)() From {New WindowsUIDemo.HamburgerSubMenuItemViewModel("Folders", "Folders", subItems), New WindowsUIDemo.HamburgerLinkItemViewModel("Additional Information", New System.Uri("https://documentation.devexpress.com/")) With {.Placement = DevExpress.Xpf.WindowsUI.HamburgerMenuItemPlacement.Bottom}, New WindowsUIDemo.HamburgerThemeSwitcherItemViewModel("Color Scheme", "ThemeSelector") With {.IsStandaloneSelectionItemMode = True, .MoreButtonVisibilityMode = DevExpress.Xpf.WindowsUI.HamburgerSubMenuMoreButtonVisibility.Hidden}}
            Return items
        End Function

        Protected Sub OnIsFlyoutViewStateEnabledChanged(ByVal oldValue As Boolean)
            If Not Me.IsFlyoutViewStateEnabled AndAlso Me.IsAvailableViewStateEmpty() Then
                Me.IsFlyoutViewStateEnabled = True
                Return
            End If

            Me.UpdateAvailableViewState()
        End Sub

        Protected Sub OnIsOverlayViewStateEnabledChanged(ByVal oldValue As Boolean)
            If Not Me.IsOverlayViewStateEnabled AndAlso Me.IsAvailableViewStateEmpty() Then
                Me.IsOverlayViewStateEnabled = True
                Return
            End If

            Me.UpdateAvailableViewState()
        End Sub

        Protected Sub OnIsInlineViewStateEnabledChanged(ByVal oldValue As Boolean)
            If Not Me.IsInlineViewStateEnabled AndAlso Me.IsAvailableViewStateEmpty() Then
                Me.IsInlineViewStateEnabled = True
                Return
            End If

            Me.UpdateAvailableViewState()
        End Sub

        Private Function IsAvailableViewStateEmpty() As Boolean
            Return Not Me.IsInlineViewStateEnabled AndAlso Not Me.IsOverlayViewStateEnabled AndAlso Not Me.IsFlyoutViewStateEnabled
        End Function

        Private Sub UpdateAvailableViewState()
            Dim state As DevExpress.Xpf.WindowsUI.HamburgerMenuAvailableViewStates = CType(0, DevExpress.Xpf.WindowsUI.HamburgerMenuAvailableViewStates)
            If Me.IsInlineViewStateEnabled Then state = state Or DevExpress.Xpf.WindowsUI.HamburgerMenuAvailableViewStates.Inline
            If Me.IsOverlayViewStateEnabled Then state = state Or DevExpress.Xpf.WindowsUI.HamburgerMenuAvailableViewStates.Overlay
            If Me.IsFlyoutViewStateEnabled Then state = state Or DevExpress.Xpf.WindowsUI.HamburgerMenuAvailableViewStates.Flyout
            Me.AvailableViewStates = state
        End Sub
    End Class
#End Region
End Namespace
