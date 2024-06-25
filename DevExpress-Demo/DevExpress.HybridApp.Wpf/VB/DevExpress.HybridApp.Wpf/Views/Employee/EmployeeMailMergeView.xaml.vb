Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows.Controls
Imports DevExpress.Xpf.Bars
Imports DevExpress.XtraRichEdit
Imports System
Imports DevExpress.Xpf.RichEdit

Namespace DevExpress.DevAV.Views

    Public Partial Class EmployeeMailMergeView
        Inherits UserControl

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub Button_Click(ByVal sender As Object, ByVal e As Windows.RoutedEventArgs)
            CType(Me.richEdit, IRichEditControl).Print()
        End Sub

        Private Sub UserControl_Loaded(ByVal sender As Object, ByVal e As Windows.RoutedEventArgs)
            Me.richEdit.Views.PrintLayoutView.AllowDisplayLineNumbers = False
        End Sub

        Private Sub richEdit_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs)
            Me.radialMenu.Items.Clear()
            Dim items = Validate(e.Menu.Items)
            For Each item In items
                Me.radialMenu.Items.Add(item)
            Next

            Me.radialMenu.IsOpen = True
            e.Menu.Items.Clear()
        End Sub

        Const maxItemsInRadialmenu As Integer = 8

        Private Function Validate(ByVal items As CommonBarItemCollection) As List(Of BarItem)
            Dim filteredItems = items.Where(Function(x) TypeOf x Is BarItem).[Select](Function(x) TryCast(x, BarItem)).Where(Function(x) x.IsEnabled AndAlso x.IsVisible AndAlso Not(TypeOf x Is BarItemSeparator)).ToList()
            If filteredItems.Count <= maxItemsInRadialmenu Then Return filteredItems
            Dim firstLevelItems = filteredItems.Where(Function(i) TypeOf i Is BarSubItem).ToList()
            Dim anotherItems = filteredItems.Where(Function(i) Not(TypeOf i Is BarSubItem)).ToList()
            Dim additionCount As Integer = maxItemsInRadialmenu - 1 - firstLevelItems.Count
            Dim firstLevelAnotherItems = anotherItems.Take(additionCount).ToList()
            anotherItems.RemoveRange(0, additionCount)
            Dim secondLevelItems = anotherItems
            firstLevelItems.AddRange(firstLevelAnotherItems)
            Dim popupMenu = New PopupMenu()
            For Each item In secondLevelItems
                popupMenu.Items.Add(item)
            Next

            firstLevelItems.Add(New BarSplitButtonItem() With {.PopupControl = popupMenu, .Content = "Actions"})
            Return firstLevelItems.ToList()
        End Function
    End Class
End Namespace
