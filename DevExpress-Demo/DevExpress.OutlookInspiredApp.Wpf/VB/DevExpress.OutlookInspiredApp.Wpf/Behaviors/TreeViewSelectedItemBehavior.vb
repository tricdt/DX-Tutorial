Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Threading
Imports DevExpress.DevAV.ViewModels
Imports DevExpress.Mvvm.UI.Interactivity

Namespace DevExpress.DevAV

    Public Class TreeViewSelectedItemBehavior
        Inherits Behavior(Of TreeView)

#Region "SelectedItem Property"
        Public Property SelectedItem As Object
            Get
                Return GetValue(SelectedItemProperty)
            End Get

            Set(ByVal value As Object)
                SetValue(SelectedItemProperty, value)
            End Set
        End Property

        Public Shared ReadOnly SelectedItemProperty As DependencyProperty = DependencyProperty.Register("SelectedItem", GetType(Object), GetType(TreeViewSelectedItemBehavior), New PropertyMetadata(Nothing, Sub(d, e) CType(d, TreeViewSelectedItemBehavior).OnSelectedItemChanged()))

#End Region
        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler AssociatedObject.SelectedItemChanged, AddressOf OnTreeViewSelectedItemChanged
            Dispatcher.BeginInvoke(New Action(AddressOf OnSelectedItemChanged), DispatcherPriority.ApplicationIdle)
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
            RemoveHandler AssociatedObject.SelectedItemChanged, AddressOf OnTreeViewSelectedItemChanged
        End Sub

        Private Sub OnTreeViewSelectedItemChanged(ByVal sender As Object, ByVal e As RoutedPropertyChangedEventArgs(Of Object))
            If TypeOf e.NewValue Is FilterItem Then
                SelectedItem = e.NewValue
                Dim name = If(CType(e.NewValue, FilterItem).Name, "filter")
                Log(String.Format("OutlookInspiredApp: Change Filter: {0}", name))
            End If
        End Sub

        Private Sub OnSelectedItemChanged()
            Dim selectedItem = GetAllItems().FirstOrDefault(Function(x) x.DataContext Is Me.SelectedItem)
            If selectedItem IsNot Nothing Then selectedItem.IsSelected = True
        End Sub

        Private Function GetAllItems() As IEnumerable(Of TreeViewItem)
            If AssociatedObject Is Nothing Then Return Enumerable.Empty(Of TreeViewItem)()
            Return AssociatedObject.Items.Cast(Of TreeViewItem)().SelectMany(Function(x) x.Items.Cast(Of Object)().[Select](Function(y, i) CType(x.ItemContainerGenerator.ContainerFromIndex(i), TreeViewItem)).Where(Function(y) y IsNot Nothing))
        End Function
    End Class
End Namespace
