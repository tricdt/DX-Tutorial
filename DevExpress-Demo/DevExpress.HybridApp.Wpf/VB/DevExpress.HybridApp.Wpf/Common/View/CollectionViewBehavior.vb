Imports System.Windows.Input
Imports DevExpress.Mvvm
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid

Namespace DevExpress.DevAV

    Public Class CollectionViewBehavior
        Inherits Behavior(Of GridControl)

        Private _SortAscendingCommand As ICommand, _SortDescendingCommand As ICommand

        Public Property SortAscendingCommand As ICommand
            Get
                Return _SortAscendingCommand
            End Get

            Private Set(ByVal value As ICommand)
                _SortAscendingCommand = value
            End Set
        End Property

        Public Property SortDescendingCommand As ICommand
            Get
                Return _SortDescendingCommand
            End Get

            Private Set(ByVal value As ICommand)
                _SortDescendingCommand = value
            End Set
        End Property

        Public Sub New()
            SortAscendingCommand = New DelegateCommand(Of String)(Sub(x)
                If AssociatedObject.Columns(x) IsNot Nothing AndAlso AssociatedObject IsNot Nothing Then AssociatedObject.SortBy(AssociatedObject.Columns(x), Data.ColumnSortOrder.Ascending)
            End Sub)
            SortDescendingCommand = New DelegateCommand(Of String)(Sub(x)
                If AssociatedObject.Columns(x) IsNot Nothing AndAlso AssociatedObject IsNot Nothing Then AssociatedObject.SortBy(AssociatedObject.Columns(x), Data.ColumnSortOrder.Descending)
            End Sub)
        End Sub

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
        End Sub

        Protected Overrides Sub OnDetaching()
            MyBase.OnDetaching()
        End Sub
    End Class
End Namespace
