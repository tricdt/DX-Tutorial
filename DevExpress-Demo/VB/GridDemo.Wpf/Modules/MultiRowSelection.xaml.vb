Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Bars
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Windows

Namespace GridDemo

    <DevExpress.Xpf.DemoBase.CodeFile("Controls/MiltiSelectionOptionsControl.xaml.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("Controls/MiltiSelectionOptionsControl.xaml")>
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/MultiSelectionClasses.(cs)")>
    <DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/MultiSelectionTemplates.xaml")>
    Public Partial Class MultiRowSelection
        Inherits GridDemoModule

        Private ReadOnly Property View As GridViewBase
            Get
                Return CType(grid.View, GridViewBase)
            End Get
        End Property

        Public Sub New()
            InitializeComponent()
            AddHandler viewsListBox.EditValueChanged, New DevExpress.Xpf.Editors.EditValueChangedEventHandler(AddressOf viewsListBox_SelectionChanged)
            AddHandler View.ShowGridMenu, New GridMenuEventHandler(AddressOf View_ShowGridMenu)
            FillComboBoxes()
            AddHandler ProductsMultiSelectionOptionsControl.SelectButtonClick, New EventHandler(AddressOf SelectProductsButtonClick)
            AddHandler ProductsMultiSelectionOptionsControl.UnselectButtonClick, New EventHandler(AddressOf UnselectProductsButtonClick)
            AddHandler ProductsMultiSelectionOptionsControl.ReselectButtonClick, New EventHandler(AddressOf ReselectProductsButtonClick)
            AddHandler PriceMultiSelectionOptionsControl.SelectButtonClick, New EventHandler(AddressOf SelectPriceButtonClick)
            AddHandler PriceMultiSelectionOptionsControl.UnselectButtonClick, New EventHandler(AddressOf UnselectPriceButtonClick)
            AddHandler PriceMultiSelectionOptionsControl.ReselectButtonClick, New EventHandler(AddressOf ReselectPriceButtonClick)
        End Sub

        Private Sub View_ShowGridMenu(ByVal sender As Object, ByVal e As GridMenuEventArgs)
            If e.MenuType = GridMenuType.Column Then
                e.Customizations.Add(New RemoveBarItemAndLinkAction() With {.ItemName = DefaultColumnMenuItemNames.SortBySummary})
            End If
        End Sub

        Private Sub viewsListBox_SelectionChanged(ByVal sender As Object, ByVal e As DevExpress.Xpf.Editors.EditValueChangedEventArgs)
            grid.View = CType(FindResource(If(viewsListBox.SelectedIndex = 0, "tableView", "cardView")), GridViewBase)
        End Sub

        Private dataTableFromGridField As List(Of Invoice) = Nothing

        Private ReadOnly Property DataTableFromGrid As List(Of Invoice)
            Get
                If dataTableFromGridField Is Nothing Then
                    dataTableFromGridField = CType(grid.ItemsSource, List(Of Invoice))
                End If

                Return dataTableFromGridField
            End Get
        End Property

        Private Sub FillComboBoxes()
            Dim listRanges As List(Of Range) = New List(Of Range)()
            Const lastRangeMinLimit As Integer = 240
            Const rangeInList As Integer = 30
            Dim i As Integer = 0
            While i <= lastRangeMinLimit
                listRanges.Add(New Range() With {.Text = "$" & Convert.ToString(i) & " - $" & Convert.ToString(i + rangeInList), .Min = i, .Max = i + rangeInList})
                i += rangeInList
            End While

            PriceMultiSelectionOptionsControl.ComboBox.ItemsSource = listRanges
            PriceMultiSelectionOptionsControl.ComboBox.SelectedIndex = 0
            ProductsMultiSelectionOptionsControl.ComboBox.SelectedIndex = 0
        End Sub

        Private Sub RunAction(ByVal FilterDelegate As RowFilter, ByVal actionForRows As Action)
            Try
                grid.BeginSelection()
                Dim rows As IEnumerable(Of Invoice) = DataTableFromGrid
                For Each row In rows
                    If FilterDelegate(row) Then actionForRows(grid.GetRowHandleByListIndex(DataTableFromGrid.IndexOf(row)))
                Next
            Finally
                grid.EndSelection()
            End Try
        End Sub

        Friend Delegate Function RowFilter(ByVal row As Invoice) As Boolean

        Private Function SelectProductsFilter(ByVal row As Invoice) As Boolean
            Return Equals(row.ProductID, CLng(ProductsMultiSelectionOptionsControl.ComboBox.EditValue))
        End Function

        Private Function SelectRangeFilter(ByVal row As Invoice) As Boolean
            Dim range As Range = CType(PriceMultiSelectionOptionsControl.ComboBox.SelectedItem, Range)
            Try
                Dim unitPrice As Integer = Convert.ToInt32(row.UnitPrice)
                Return unitPrice >= range.Min AndAlso unitPrice <= range.Max
            Catch __unusedOverflowException1__ As OverflowException
                Return False
            End Try
        End Function

        Friend Delegate Sub Action(ByVal rowHandle As Integer)

        Private Sub SelectAction(ByVal rowHandle As Integer)
            grid.SelectItem(rowHandle)
        End Sub

        Private Sub UnselectAction(ByVal rowHandle As Integer)
            grid.UnselectItem(rowHandle)
        End Sub

        Private Sub SelectProductsButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            RunAction(New RowFilter(AddressOf SelectProductsFilter), New Action(AddressOf SelectAction))
        End Sub

        Private Sub UnselectProductsButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            RunAction(New RowFilter(AddressOf SelectProductsFilter), New Action(AddressOf UnselectAction))
        End Sub

        Private Sub ReselectProductsButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            grid.UnselectAll()
            RunAction(New RowFilter(AddressOf SelectProductsFilter), New Action(AddressOf SelectAction))
        End Sub

        Private Sub SelectPriceButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            RunAction(New RowFilter(AddressOf SelectRangeFilter), New Action(AddressOf SelectAction))
        End Sub

        Private Sub UnselectPriceButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            RunAction(New RowFilter(AddressOf SelectRangeFilter), New Action(AddressOf UnselectAction))
        End Sub

        Private Sub ReselectPriceButtonClick(ByVal sender As Object, ByVal e As EventArgs)
            grid.UnselectAll()
            RunAction(New RowFilter(AddressOf SelectRangeFilter), New Action(AddressOf SelectAction))
        End Sub

        Private Sub SetMultiSelectMode(ByVal enabled As Boolean)
            ProductsMultiSelectionOptionsControl.IsEnabled = enabled
            PriceMultiSelectionOptionsControl.IsEnabled = enabled
        End Sub

        Private Sub selectionModeListBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As RoutedEventArgs)
            SetMultiSelectMode(grid.SelectionMode <> MultiSelectMode.None)
        End Sub
    End Class
End Namespace
