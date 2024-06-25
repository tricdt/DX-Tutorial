Imports System
Imports System.Windows
Imports DevExpress.Xpf.PivotGrid
Imports DevExpress.Utils
Imports DevExpress.Xpf.Bars
Imports DevExpress.Mvvm
Imports System.Linq
Imports DevExpress.Mvvm.Native

Namespace PivotGridDemo

    Public Partial Class ContextMenu
        Inherits PivotGridDemo.PivotGridDemoModule

        Public Sub New()
            Me.InitializeComponent()
        End Sub

        Private Sub PivotGridDemoModule_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs)
            Me.pivotGrid.BestFit(DevExpress.Xpf.PivotGrid.FieldArea.ColumnArea, True, False)
        End Sub

        Private Sub OnPivotGridShowMenu(ByVal sender As Object, ByVal e As DevExpress.Xpf.PivotGrid.PopupMenuShowingEventArgs)
            If e.MenuType.Equals(DevExpress.Xpf.PivotGrid.PivotGridMenuType.Header) AndAlso e.GetFieldInfo().Field.Area = DevExpress.Xpf.PivotGrid.FieldArea.DataArea Then
                e.Customizations.Add(New DevExpress.Xpf.Bars.BarItemLinkSeparator())
                Dim item As DevExpress.Xpf.Bars.BarSubItem = New DevExpress.Xpf.Bars.BarSubItem() With {.Content = "Summary Type"}
                e.Customizations.Add(item)
                For Each type As DevExpress.Xpf.PivotGrid.FieldSummaryType In DevExpress.Utils.EnumExtensions.GetValues(GetType(DevExpress.Xpf.PivotGrid.FieldSummaryType)).Cast(Of DevExpress.Xpf.PivotGrid.FieldSummaryType)().Except(DevExpress.Xpf.PivotGrid.FieldSummaryType.Custom.Yield())
                    item.ItemLinks.Add(New DevExpress.Xpf.Bars.BarButtonItem() With {.Name = "item" & type, .Content = type.ToString(), .CommandParameter = New PivotGridDemo.FieldSummaryItem() With {.Type = type, .Field = e.GetFieldInfo().Field}, .Command = New DevExpress.Mvvm.DelegateCommand(Of PivotGridDemo.FieldSummaryItem)(AddressOf Me.SetFieldSummaryType, AddressOf Me.CanSetFieldSummaryType)})
                Next
            End If
        End Sub

        Private Sub SetFieldSummaryType(ByVal item As PivotGridDemo.FieldSummaryItem)
            item.Field.SummaryType = item.Type
            item.Field.CellFormat = Me.GetFormat(item.Type, item.Field)
            Me.pivotGrid.BestFit(DevExpress.Xpf.PivotGrid.FieldArea.ColumnArea, True, False)
        End Sub

        Private Function GetFormat(ByVal fieldSummaryType As DevExpress.Xpf.PivotGrid.FieldSummaryType, ByVal field As DevExpress.Xpf.PivotGrid.PivotGridField) As String
            If field Is Me.fieldQuantity Then Return ""
            Select Case fieldSummaryType
                Case DevExpress.Xpf.PivotGrid.FieldSummaryType.Sum, DevExpress.Xpf.PivotGrid.FieldSummaryType.Median, DevExpress.Xpf.PivotGrid.FieldSummaryType.Mode
                    Return "N"
                Case DevExpress.Xpf.PivotGrid.FieldSummaryType.Custom, DevExpress.Xpf.PivotGrid.FieldSummaryType.Max, DevExpress.Xpf.PivotGrid.FieldSummaryType.Min
                    Return "c"
                Case DevExpress.Xpf.PivotGrid.FieldSummaryType.Count
                    Return ""
                Case DevExpress.Xpf.PivotGrid.FieldSummaryType.Average
                    Return If(field Is Me.fieldDiscount, "p", "c")
                Case DevExpress.Xpf.PivotGrid.FieldSummaryType.StdDev, DevExpress.Xpf.PivotGrid.FieldSummaryType.StdDevp, DevExpress.Xpf.PivotGrid.FieldSummaryType.Var, DevExpress.Xpf.PivotGrid.FieldSummaryType.Varp
                    Return "p"
            End Select

            Return String.Empty
        End Function

        Private Function CanSetFieldSummaryType(ByVal item As PivotGridDemo.FieldSummaryItem) As Boolean
            Return Not Object.Equals(item.Field.SummaryType, item.Type)
        End Function
    End Class

    Public Class FieldSummaryItem

        Public Property Field As PivotGridField

        Public Property Type As FieldSummaryType
    End Class
End Namespace
