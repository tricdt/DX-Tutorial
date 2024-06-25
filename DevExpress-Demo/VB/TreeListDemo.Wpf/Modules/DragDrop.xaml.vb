Imports System.Collections.ObjectModel
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace TreeListDemo

    Public Partial Class DragDrop
        Inherits TreeListDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub OnDragRecordOver(ByVal sender As Object, ByVal e As DragRecordOverEventArgs)
            If e.IsFromOutside AndAlso GetType(Employee).IsAssignableFrom(e.GetRecordType()) Then e.Effects = System.Windows.DragDropEffects.Move
            If e.DropPosition = DropPosition.Inside AndAlso allowDropInside.IsChecked = False Then e.DropPosition = If(e.DropPositionRelativeCoefficient > 0.5, DropPosition.After, DropPosition.Before)
            e.Handled = True
        End Sub
    End Class

    Public Class DragDropViewModel

        Public Sub New()
            ActiveEmployees = New ObservableCollection(Of Employee)(GetActiveEmployees())
            NewEmployees = New ObservableCollection(Of Employee)(GetNewEmployees())
        End Sub

        Public Property ActiveEmployees As ObservableCollection(Of Employee)

        Public Property NewEmployees As ObservableCollection(Of Employee)
    End Class
End Namespace
