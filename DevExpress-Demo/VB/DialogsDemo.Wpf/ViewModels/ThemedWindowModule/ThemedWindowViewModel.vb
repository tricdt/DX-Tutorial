Imports System.Collections.ObjectModel
Imports System.Windows.Media
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Core

Namespace DialogsDemo

    Public Class ThemedWindowViewModel

        Private _HeaderItems As ObservableCollection(Of DialogsDemo.BaseHeaderItemModel)

        Public Sub New()
            PinWindow = True
            ShowTitle = True
            InitializeHeaderItems()
            OnWindowKindChanged()
        End Sub

        Private Sub InitializeHeaderItems()
            HeaderItems = New ObservableCollection(Of BaseHeaderItemModel)()
            HeaderItems.Add(CreateChildModel(Of WindowTitleEditorHeaderItemModel)())
            HeaderItems.Add(CreateChildModel(Of HelpHeaderItemModel)())
            HeaderItems.Add(CreateChildModel(Of PinHeaderItemModel)())
            ShowPredefinedItems = True
        End Sub

        Protected Overridable ReadOnly Property WindowService As IWindowService
            Get
                Return Nothing
            End Get
        End Property

        Public Overridable Property PinWindow As Boolean

        Public Overridable Property ShowTitle As Boolean

        Public Overridable Property UseGlowColors As Boolean

        Public Overridable Property TitleAlignment As WindowTitleAlignment

        Public Overridable Property WindowKind As WindowKind

        Public Overridable Property Title As String

        Public Overridable Property Icon As ImageSource

        Public Overridable Property ActiveGlowColor As SolidColorBrush

        Public Overridable Property InactiveGlowColor As SolidColorBrush

        Public Overridable Property ShowPredefinedItems As Boolean

        Public Overridable Property Content As BaseThemedWindowContentModel

        Public Property HeaderItems As ObservableCollection(Of BaseHeaderItemModel)
            Get
                Return _HeaderItems
            End Get

            Private Set(ByVal value As ObservableCollection(Of BaseHeaderItemModel))
                _HeaderItems = value
            End Set
        End Property

        Public Sub TogglePredefinedItemsVisibility()
            ShowPredefinedItems = Not ShowPredefinedItems
        End Sub

        Public Sub ShowWindow()
            WindowService.Show(Me)
        End Sub

        Public Function CanAddHeaderItem() As Boolean
            Return True
        End Function

        Public Sub AddHeaderItem()
            Dim model = ViewModelSource(Of CustomHeaderItemModel).Create(HeaderItems.Count - 3)
            model.SetParentViewModel(Me)
            HeaderItems.Insert(0, model)
        End Sub

        Public Function CanRemoveHeaderItem() As Boolean
            Return HeaderItems.Count > 3
        End Function

        <Command(UseCommandManager:=True)>
        Public Sub RemoveHeaderItem()
            HeaderItems.RemoveAt(0)
        End Sub

        Protected Sub OnShowPredefinedItemsChanged()
            For i As Integer = 1 To 3
                HeaderItems(HeaderItems.Count - i).IsVisible = ShowPredefinedItems
            Next
        End Sub

        Protected Function CreateChildModel(Of TChildModel)() As TChildModel
            Dim model = ViewModelSource(Of TChildModel).Create()
            model.SetParentViewModel(Me)
            Return model
        End Function

        Protected Sub OnWindowKindChanged()
            Select Case WindowKind
                Case WindowKind.Normal, WindowKind.Auto
                    Content = CreateChildModel(Of SimpleThemedWindowContentModel)()
                Case WindowKind.Tabbed
                    Content = CreateChildModel(Of TabbedThemedWindowContentModel)()
                Case WindowKind.Ribbon
                    Content = CreateChildModel(Of RibbonThemedWindowContentModel)()
            End Select
        End Sub
    End Class
End Namespace
