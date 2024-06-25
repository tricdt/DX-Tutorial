Imports DevExpress.Xpf.Grid
Imports System.ComponentModel
Imports System.Windows

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/GroupIntervalData.(cs)")>
    Public Partial Class SortBySummary
        Inherits GridDemoModule

#Region "static"
        Public Shared ReadOnly IsSelectedProperty As DependencyProperty

        Shared Sub New()
            IsSelectedProperty = DependencyProperty.RegisterAttached("IsSelected", GetType(Boolean), GetType(SortBySummary), New PropertyMetadata(False))
        End Sub

        Public Shared Sub SetIsSelected(ByVal element As DependencyObject, ByVal value As Boolean)
            element.SetValue(IsSelectedProperty, value)
        End Sub

        Public Shared Function GetIsSelected(ByVal element As DependencyObject) As Integer
            Return CInt(element.GetValue(IsSelectedProperty))
        End Function

#End Region
        Public Sub New()
            InitializeComponent()
            grid.GroupBy("OrderDate")
            sortModeList.SelectedIndex = 0
        End Sub

        Private ReadOnly Property CurrentSortOrder As ListSortDirection
            Get
                Return If(sortModeList.SelectedIndex Mod 2 = 0, ListSortDirection.Ascending, ListSortDirection.Descending)
            End Get
        End Property

        Private ReadOnly Property CurrentSummaryItemIndex As Integer
            Get
                Return CInt(sortModeList.SelectedIndex \ 2)
            End Get
        End Property

        Private Sub sortModeList_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            grid.GroupSummarySortInfo.Clear()
            For i As Integer = 0 To grid.GroupSummary.Count - 1
                Dim item As GridSummaryItem = grid.GroupSummary(i)
                If i = CurrentSummaryItemIndex Then
                    SetIsSelected(item, True)
                    grid.GroupSummarySortInfo.Add(New GridGroupSummarySortInfo(item, "OrderDate", CurrentSortOrder))
                Else
                    SetIsSelected(item, False)
                End If
            Next
        End Sub
    End Class
End Namespace
