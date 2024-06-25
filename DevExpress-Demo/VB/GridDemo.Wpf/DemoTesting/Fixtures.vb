Imports DevExpress.DemoData.Models
Imports DevExpress.Mvvm
Imports DevExpress.Utils
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.DemoBase.DemoTesting
Imports DevExpress.Xpf.Editors
Imports DevExpress.Xpf.Editors.ExpressionEditor
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Controls
Imports DevExpress.Xpf.Editors.ExpressionEditor.Native
Imports System.Windows.Controls.Primitives

Namespace GridDemo.Tests

    Public Class GridCheckAllDemosFixture
        Inherits CheckAllDemosFixture

        Protected Overrides Function SwitchAllThemes(ByVal moduleType As Type) As Boolean
            Return False
        End Function

        Protected Overrides Function CheckMemoryLeaks(ByVal moduleType As Type) As Boolean
            Return moduleType IsNot GetType(ClipboardFormats)
        End Function

        Protected Overrides Function TestingThemesCount() As Integer
            Return 5
        End Function
    End Class

#Region "GridDemoModulesAccessor"
    Public Class GridDemoModulesAccessor
        Inherits DemoModulesAccessor(Of GridDemoModule)

        Public Sub New(ByVal fixture As BaseDemoTestingFixture)
            MyBase.New(fixture)
        End Sub

        Public ReadOnly Property GridControl As GridControl
            Get
                Return DemoModule.GridControl
            End Get
        End Property

        Public ReadOnly Property View As GridViewBase
            Get
                Return CType(GridControl.View, GridViewBase)
            End Get
        End Property

        Public ReadOnly Property TableView As TableView
            Get
                Return CType(View, TableView)
            End Get
        End Property

        Public ReadOnly Property CardView As DevExpress.Xpf.Grid.CardView
            Get
                Return CType(View, DevExpress.Xpf.Grid.CardView)
            End Get
        End Property

        Public ReadOnly Property TableViewModule As StandardTableView
            Get
                Return CType(DemoModule, StandardTableView)
            End Get
        End Property

        Public ReadOnly Property MultiSelectionModule As MultiRowSelection
            Get
                Return CType(DemoModule, MultiRowSelection)
            End Get
        End Property

        Public ReadOnly Property CopyPasteModule As CopyPasteOperations
            Get
                Return CType(DemoModule, CopyPasteOperations)
            End Get
        End Property

        Public ReadOnly Property LargeDataSetModule As LargeDataSource
            Get
                Return CType(DemoModule, LargeDataSource)
            End Get
        End Property

        Public ReadOnly Property DragDropModule As DragDrop
            Get
                Return CType(DemoModule, DragDrop)
            End Get
        End Property

        Public ReadOnly Property HitTestModule As HitTesting
            Get
                Return CType(DemoModule, HitTesting)
            End Get
        End Property

        Public ReadOnly Property UnboundColumnsModule As UnboundColumns
            Get
                Return CType(DemoModule, UnboundColumns)
            End Get
        End Property
    End Class

#End Region
    Public MustInherit Class BaseGridDemoTestingFixture
        Inherits BaseDemoTestingFixture

        Private ReadOnly modulesAccessor As GridDemoModulesAccessor

        Public Sub New()
            modulesAccessor = New GridDemoModulesAccessor(Me)
        End Sub

        Public ReadOnly Property GridControl As GridControl
            Get
                Return modulesAccessor.GridControl
            End Get
        End Property

        Public ReadOnly Property View As GridViewBase
            Get
                Return modulesAccessor.View
            End Get
        End Property

        Public ReadOnly Property TableView As TableView
            Get
                Return modulesAccessor.TableView
            End Get
        End Property

        Public ReadOnly Property CardView As DevExpress.Xpf.Grid.CardView
            Get
                Return modulesAccessor.CardView
            End Get
        End Property

        Public ReadOnly Property TableViewModule As StandardTableView
            Get
                Return modulesAccessor.TableViewModule
            End Get
        End Property

        Public ReadOnly Property MultiSelectionModule As MultiRowSelection
            Get
                Return modulesAccessor.MultiSelectionModule
            End Get
        End Property

        Public ReadOnly Property CopyPasteModule As CopyPasteOperations
            Get
                Return modulesAccessor.CopyPasteModule
            End Get
        End Property

        Public ReadOnly Property LargeDataSetModule As LargeDataSource
            Get
                Return modulesAccessor.LargeDataSetModule
            End Get
        End Property

        Public ReadOnly Property DragDropModule As DragDrop
            Get
                Return modulesAccessor.DragDropModule
            End Get
        End Property

        Public ReadOnly Property HitTestModule As HitTesting
            Get
                Return modulesAccessor.HitTestModule
            End Get
        End Property

        Public ReadOnly Property UnboundColumnsModule As UnboundColumns
            Get
                Return modulesAccessor.UnboundColumnsModule
            End Get
        End Property
    End Class

    Public Class CheckDemoOptionsFixture
        Inherits BaseGridDemoTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            CreateCheckUnboundColumnsDemoActions()
            CreateCheckMultiSelectionDemoActions()
            CreateCheckCopyPasteDemoActions()
            CreateCheckTableViewDemoActions()
            CreateCheckFixedColumnsDemoActions()
            CreateSetCurrentDemoActions(Nothing, False)
        End Sub

        Private Function IsListBox(ByVal element As FrameworkElement) As Boolean
            Return TypeOf element Is ListBox
        End Function

        Private Function GetListBoxEditCount(ByVal listBoxEdit As ListBoxEdit) As Integer
            Return CType(listBoxEdit.ItemsSource, Collections.IList).Count
        End Function

#Region "UnboundColumns"
        Private Sub CreateCheckUnboundColumnsDemoActions()
            AddLoadModuleActions(GetType(UnboundColumns))
            Dispatch(Sub()
                Assert.AreEqual(0, GridControl.Columns("OrderID").GroupIndex)
                Assert.AreEqual(1, GridControl.GroupCount)
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-1))
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-2))
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-3))
                Assert.IsTrue(UnboundColumnsModule.showExpressionEditorButton.IsEnabled)
                Assert.IsTrue(GridControl.Columns("Total").AllowUnboundExpressionEditor)
                Assert.IsTrue(GridControl.Columns("DiscountAmount").AllowUnboundExpressionEditor)
                Assert.AreEqual(2, UnboundColumnsModule.columnsList.Items.Count)
                Dim listBox As Windows.Controls.ListBox
                listBox = CType(LayoutHelper.FindElement(UnboundColumnsModule.columnsList, New Predicate(Of FrameworkElement)(AddressOf IsListBox)), Windows.Controls.ListBox)
                Assert.AreEqual("Discount Amount", CType(listBox.ItemContainerGenerator.ContainerFromIndex(0), Windows.Controls.ListBoxItem).Content)
                Assert.AreEqual("Total", CType(listBox.ItemContainerGenerator.ContainerFromIndex(1), Windows.Controls.ListBoxItem).Content)
                Assert.AreEqual("DiscountAmount", CType(listBox.ItemContainerGenerator.ContainerFromIndex(0), Windows.Controls.ListBoxItem).Tag)
                Assert.AreEqual("Total", CType(listBox.ItemContainerGenerator.ContainerFromIndex(1), Windows.Controls.ListBoxItem).Tag)
                Assert.AreEqual(CDec(0.0), GridControl.GetCellValue(0, GridControl.Columns("DiscountAmount")))
                Assert.AreEqual(CDec(174.0), GridControl.GetCellValue(0, GridControl.Columns("Total")))
                Assert.IsTrue(Math.Abs(CDbl(38.0) - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns("DiscountAmount")))) < 0.001)
                Assert.IsTrue(Math.Abs(CDbl(214.2) - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns("Total")))) < 0.001)
                UnboundColumnsModule.columnsList.SelectedIndex = 0
                AssertShowExpressionEditor("Round([UnitPrice] * [Quantity] - [Total])")
                UpdateLayoutAndDoEvents()
                UnboundColumnsModule.columnsList.SelectedIndex = 1
                AssertShowExpressionEditor("[UnitPrice] * [Quantity] * (1 - [Discount])")
                UpdateLayoutAndDoEvents()
                GridControl.Columns("DiscountAmount").UnboundExpression = "[UnitPrice] * [Quantity] * (1 - [Discount])+[Discount][[Discount]]"
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(DevExpress.Data.UnboundErrorObject.Value, GridControl.GetCellValue(0, GridControl.Columns("DiscountAmount")))
            End Sub)
        End Sub

        Private _expectedExpression As String

        Private expressionEditorControl As ExpressionEditorControl = Nothing

        Private Sub OnExpressionEditorLoaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim _dw As Window = TryCast(expressionEditorControl.Parent, Window)
            Assert.IsNotNull(_dw, "2")
            RemoveHandler expressionEditorControl.Loaded, AddressOf OnExpressionEditorLoaded
            Assert.IsTrue(expressionEditorControl.IsVisible)
            Assert.AreEqual(_expectedExpression, CType(expressionEditorControl, ISupportExpressionString).GetExpressionString(Nothing))
            _dw.DialogResult = True
            _dw.Close()
        End Sub

        Private Sub OnExpressionEditorCreated(ByVal sender As Object, ByVal e As UnboundExpressionEditorEventArgs)
            expressionEditorControl = e.ExpressionEditorControl
            Assert.IsNotNull(expressionEditorControl, "1")
            AddHandler expressionEditorControl.Loaded, AddressOf OnExpressionEditorLoaded
            RemoveHandler View.UnboundExpressionEditorCreated, AddressOf OnExpressionEditorCreated
        End Sub

        Private Sub AssertShowExpressionEditor(ByVal expectedExpression As String)
            _expectedExpression = expectedExpression
            AddHandler View.UnboundExpressionEditorCreated, AddressOf OnExpressionEditorCreated
            UIAutomationActions.ClickButton(UnboundColumnsModule.showExpressionEditorButton)
            UpdateLayoutAndDoEvents()
        End Sub

#End Region
#Region "table view"
        Private Sub CreateCheckTableViewDemoActions()
            AddLoadModuleActions(GetType(StandardTableView))
            Dispatch(Sub()
                Assert.IsTrue(TableView.AllowSorting)
                Assert.IsTrue(TableView.AllowGrouping)
                Assert.IsTrue(TableView.AllowColumnMoving)
                Assert.IsTrue(TableView.AllowResizing)
                Assert.IsTrue(TableView.AllowBestFit)
                Assert.IsTrue(TableView.ShowHorizontalLines)
                Assert.IsTrue(TableView.ShowVerticalLines)
                Dim hasStarColumn As Boolean = False
                For Each column In GridControl.Columns
                    hasStarColumn = hasStarColumn OrElse column.Width.IsStar
                    Select Case column.FieldName
                        Case "Phone"
                            Assert.IsFalse(column.AllowSorting.ToBoolean(False))
                            Assert.IsFalse(column.ActualAllowGrouping)
                        Case Else
                            Assert.IsTrue(column.ActualAllowSorting)
                            Assert.IsTrue(column.ActualAllowGrouping)
                    End Select
                Next

                Assert.IsTrue(hasStarColumn)
                EditorsActions.ToggleCheckEdit(TableViewModule.allowSortingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowSorting)
                For Each column In GridControl.Columns
                    Assert.IsFalse(column.ActualAllowSorting)
                    Assert.IsFalse(column.ActualAllowGrouping)
                Next

                EditorsActions.ToggleCheckEdit(TableViewModule.allowSortingCheckEdit)
                EditorsActions.ToggleCheckEdit(TableViewModule.allowGroupingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowGrouping)
                For Each column In GridControl.Columns
                    Select Case column.FieldName
                        Case "Phone"
                            Assert.IsFalse(column.AllowSorting.ToBoolean(False))
                            Assert.IsFalse(column.ActualAllowGrouping)
                        Case Else
                            Assert.IsTrue(column.ActualAllowSorting)
                            Assert.IsFalse(column.ActualAllowGrouping)
                    End Select
                Next

                EditorsActions.ToggleCheckEdit(TableViewModule.allowMovingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowColumnMoving)
                For Each column In GridControl.Columns
                    Assert.IsFalse(column.ActualAllowMoving)
                Next

                EditorsActions.ToggleCheckEdit(TableViewModule.allowResizingCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowResizing)
                For Each column In GridControl.Columns
                    Assert.IsFalse(column.ActualAllowResizing)
                Next

                EditorsActions.ToggleCheckEdit(TableViewModule.allowBestFitCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.AllowBestFit)
                EditorsActions.ToggleCheckEdit(TableViewModule.showHorizontalLinesCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.ShowHorizontalLines)
                EditorsActions.ToggleCheckEdit(TableViewModule.showVerticalLinesCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsFalse(TableView.ShowVerticalLines)
                Assert.AreEqual(GridViewNavigationStyle.Row, CType(TableViewModule.NavigationStyleComboBox.SelectedItem, EnumMemberInfo).Id)
                Assert.AreEqual(GridViewNavigationStyle.Row, View.NavigationStyle)
                TableViewModule.NavigationStyleComboBox.SelectedIndex = 1
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(GridViewNavigationStyle.Cell, CType(TableViewModule.NavigationStyleComboBox.SelectedItem, EnumMemberInfo).Id)
                TableViewModule.NavigationStyleComboBox.SelectedIndex = 2
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(GridViewNavigationStyle.None, CType(TableViewModule.NavigationStyleComboBox.SelectedItem, EnumMemberInfo).Id)
            End Sub)
        End Sub

#End Region
#Region "Multi-Selection"
        Private Sub CreateCheckMultiSelectionDemoActions()
            AddLoadModuleActions(GetType(MultiRowSelection))
            Dispatch(Sub()
                Assert.AreEqual(GridControl.View, TableView)
                Assert.AreEqual(GridViewNavigationStyle.Cell, TableView.NavigationStyle)
                AssertMuliSelection(TableView)
                MultiSelectionModule.viewsListBox.SelectedIndex = 1
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(GridControl.View, CardView)
                Assert.AreEqual(GridViewNavigationStyle.Row, CardView.NavigationStyle)
                GridControl.SelectItem(0)
                AssertMuliSelection(CardView)
            End Sub)
        End Sub

        Private Sub AssertMuliSelection(ByVal gridView As GridViewBase)
            AssertMultiSelectMode(gridView, True)
            MultiSelectionModule.selectionModeListBox.EditValue = MultiSelectMode.None
            UpdateLayoutAndDoEvents()
            AssertMultiSelectMode(gridView, False)
            Assert.IsFalse(MultiSelectionModule.ProductsMultiSelectionOptionsControl.IsEnabled)
            Assert.IsFalse(MultiSelectionModule.PriceMultiSelectionOptionsControl.IsEnabled)
            MultiSelectionModule.selectionModeListBox.EditValue = MultiSelectMode.Row
            UpdateLayoutAndDoEvents()
            AssertMultiSelectMode(gridView, True)
            Assert.IsTrue(MultiSelectionModule.ProductsMultiSelectionOptionsControl.IsEnabled)
            Assert.IsTrue(MultiSelectionModule.PriceMultiSelectionOptionsControl.IsEnabled)
            Assert.AreEqual(77, CType(CType(MultiSelectionModule.ProductsMultiSelectionOptionsControl.comboBoxControl, ComboBoxEdit).ItemsSource, IEnumerable(Of Product)).Count())
            Assert.AreEqual(1, gridView.Grid.SelectedItems.Count)
            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.SelectButton)
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(39, gridView.Grid.SelectedItems.Count)
            Assert.AreEqual(39, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox))
            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.UnselectButton)
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(1, gridView.Grid.SelectedItems.Count)
            Assert.AreEqual(1, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox))
            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.ReselectButton)
            UpdateLayoutAndDoEvents()
            Assert.AreEqual(38, gridView.Grid.SelectedItems.Count)
            Assert.AreEqual(38, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox))
            gridView.Grid.UnselectAll()
            gridView.Grid.SelectRange(0, 3)
            Assert.AreEqual("Selection Total=${0:N}", gridView.VisibleColumns(4).TotalSummaries(0).Item.DisplayFormat)
            Assert.AreEqual(Convert.ToString(607.4, Globalization.CultureInfo.CurrentCulture), gridView.VisibleColumns(4).TotalSummaries(0).Value.ToString())
            GridControl.UnselectAll()
            Dim count As Integer = TryCast(MultiSelectionModule.PriceMultiSelectionOptionsControl.comboBoxControl.ItemsSource, List(Of Range)).Count
            Assert.AreEqual(9, count)
            For i As Integer = 0 To count - 1
                MultiSelectionModule.PriceMultiSelectionOptionsControl.comboBoxControl.SelectedIndex = i
                UIAutomationActions.ClickButton(MultiSelectionModule.PriceMultiSelectionOptionsControl.SelectButton)
                UpdateLayoutAndDoEvents()
            Next

            Assert.AreEqual(GridControl.VisibleRowCount, gridView.Grid.SelectedItems.Count)
            GridControl.UnselectAll()
            GridControl.GroupBy("OrderID")
            UpdateLayoutAndDoEvents()
            GridControl.SelectItem(0)
            GridControl.SelectItem(10)
            Assert.AreEqual("Grand Total=${0:N}", CType(gridView.RootRowsContainer.Items(0), GroupRowData).GroupSummaryData(0).SummaryItem.DisplayFormat)
            Assert.AreEqual(168, Convert.ToInt32(CType(gridView.RootRowsContainer.Items(0), GroupRowData).GroupSummaryData(0).SummaryValue))
            Assert.AreEqual(336, Convert.ToInt32(CType(gridView.RootRowsContainer.Items(3), GroupRowData).GroupSummaryData(0).SummaryValue))
            Assert.AreEqual(504, Convert.ToInt32(gridView.VisibleColumns(4).TotalSummaries(0).Value))
            GridControl.ClearGrouping()
            GridControl.UnselectAll()
        End Sub

        Private Sub AssertMultiSelectMode(ByVal view As GridViewBase, ByVal isMultiSelect As Boolean)
            Assert.AreEqual(If(isMultiSelect, MultiSelectMode.Row, MultiSelectMode.None), view.DataControl.SelectionMode)
        End Sub

#End Region
#Region "Copy Paste"
        Private Sub CreateCheckCopyPasteDemoActions()
            AddLoadModuleActions(GetType(CopyPasteOperations))
            Dispatch(Sub()
                Assert.IsTrue(CopyPasteModule.firstGrid.ClipboardCopyMode <> ClipboardCopyMode.None)
                Assert.IsTrue(CopyPasteModule.secondGrid.ClipboardCopyMode <> ClipboardCopyMode.None)
                EditorsActions.ToggleCheckEdit(CopyPasteModule.allowCopyingtoClipboardCheckEdit)
                UpdateLayoutAndDoEvents()
                Assert.IsTrue(CopyPasteModule.firstGrid.ClipboardCopyMode = ClipboardCopyMode.None)
                Assert.IsTrue(CopyPasteModule.secondGrid.ClipboardCopyMode = ClipboardCopyMode.None)
                EditorsActions.ToggleCheckEdit(CopyPasteModule.allowCopyingtoClipboardCheckEdit)
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.None, False, False, False, False)
                Assert.AreEqual(10, CopyPasteModule.firstGrid.VisibleRowCount)
                Assert.AreEqual(0, CopyPasteModule.secondGrid.VisibleRowCount)
                Assert.AreEqual(0, CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex)
                ClickOnGrid(CopyPasteModule.firstGrid)
                CopyPasteModule.firstGrid.SelectRange(1, 4)
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.First, True, True, False, True)
                Assert.AreEqual(0, CopyPasteModule.PasteRule.SelectedIndex)
                UIAutomationActions.ClickButton(CopyPasteModule.CopyButton)
                UpdateLayoutAndDoEvents()
                ClickOnGrid(CopyPasteModule.secondGrid)
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.Second, False, False, True, False)
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
            End Sub)
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, Function() DemoBaseTesting.CurrentDemoModule, Nothing, Nothing)
            Dispatch(Sub()
                Assert.AreEqual(5, CopyPasteModule.secondGrid.VisibleRowCount)
                ClickOnGrid(CopyPasteModule.firstGrid)
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.First, True, True, True, True)
                UIAutomationActions.ClickButton(CopyPasteModule.DeleteButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(5, CopyPasteModule.firstGrid.VisibleRowCount)
                CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex = 1
                UpdateLayoutAndDoEvents()
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.First, False, False, True, False)
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40))
                CopyPasteModule.textEdit.Select(0, 8)
                UpdateLayoutAndDoEvents()
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                AssertGridsAndButtonState(FocusedGrid.First, True, True, True, True)
                UIAutomationActions.ClickButton(CopyPasteModule.CutButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual("Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 32))
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                CopyPasteModule.textEdit.CaretIndex = 0
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40))
                CopyPasteModule.textEdit.SelectAll()
                UpdateLayoutAndDoEvents()
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(CopyPasteModule.CopyButton)
                UpdateLayoutAndDoEvents()
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(CopyPasteModule.DeleteButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual("", CopyPasteModule.textEdit.Text)
                CopyPasteModule.textEdit.Focus()
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40))
                CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex = 0
                UpdateLayoutAndDoEvents()
                CopyPasteModule.secondGrid.UnselectAll()
                CopyPasteModule.secondGrid.SelectItem(2)
                UpdateLayoutAndDoEvents()
                CType(CType(CopyPasteModule.secondGrid.View, GridViewBase).Grid.SelectedItems(0), CopyPasteOutlookData).From = "QWERTY"
                ClickOnGrid(CopyPasteModule.secondGrid)
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(CopyPasteModule.CutButton)
                UpdateLayoutAndDoEvents()
                Assert.AreEqual(4, CopyPasteModule.secondGrid.VisibleRowCount)
                CopyPasteModule.firstGrid.View.FocusedRowHandle = 2
                ClickOnGrid(CopyPasteModule.firstGrid)
                UpdateLayoutAndDoEvents()
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
            End Sub)
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, Function() DemoBaseTesting.CurrentDemoModule, Nothing, Nothing)
            Dispatch(Sub()
                Assert.AreEqual(6, CopyPasteModule.firstGrid.VisibleRowCount)
                Assert.AreEqual("QWERTY", CType(CopyPasteModule.firstGrid.GetRow(3), CopyPasteOutlookData).From)
                CopyPasteModule.PasteRule.SelectedIndex = 1
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton)
            End Sub)
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, Function() DemoBaseTesting.CurrentDemoModule, Nothing, Nothing)
            Dispatch(Sub()
                Assert.AreEqual(7, CopyPasteModule.firstGrid.VisibleRowCount)
                Assert.AreEqual("QWERTY", CType(CopyPasteModule.firstGrid.GetRow(6), CopyPasteOutlookData).From)
            End Sub)
        End Sub

        Private Sub ClickOnGrid(ByVal grid As GridControl)
            MouseActions.LeftMouseDown(grid.View, 2, 2)
            MouseActions.LeftMouseUp(grid.View, 2, 2)
        End Sub

        Private Sub AssertGridsAndButtonState(ByVal focusedGrid As FocusedGrid, ByVal copyBtn As Boolean, ByVal cutBtn As Boolean, ByVal pasteBtn As Boolean, ByVal delBtn As Boolean)
            Assert.AreEqual(focusedGrid, CopyPasteModule.FocusedGrid)
            Assert.AreEqual(copyBtn, CopyPasteModule.CopyButton.IsEnabled)
            Assert.AreEqual(cutBtn, CopyPasteModule.CutButton.IsEnabled)
            Assert.AreEqual(pasteBtn, CopyPasteModule.PasteButton.IsEnabled)
            Assert.AreEqual(delBtn, CopyPasteModule.DeleteButton.IsEnabled)
        End Sub

#End Region
        Private Sub CreateCheckFixedColumnsDemoActions()
            AddLoadModuleActions(GetType(FixedDataColumns))
        End Sub
    End Class

    Public Class BugFixesFixture
        Inherits BaseGridDemoTestingFixture

        Protected Overrides Sub CreateActions()
            MyBase.CreateActions()
            Create_B144178_Actions()
            CreateSetCurrentDemoActions(Nothing, False)
        End Sub

#Region "B144178"
        Private Sub Create_B144178_Actions()
            AddLoadModuleActions(GetType(LargeDataSource))
            Dispatch(Sub() AssertPrioritySorting("Priority6"))
            AddLoadModuleActions(GetType(CellEditors))
            Dispatch(Sub() AssertPrioritySorting("Priority"))
        End Sub

        Private Sub AssertPrioritySorting(ByVal fieldName As String)
            GridControl.SortBy(fieldName)
            For i As Integer = 0 To 10 - 1
                Assert.AreEqual(Priority.Low, CType(GridControl.GetCellValue(i, fieldName), Priority), fieldName)
                Assert.AreEqual(Priority.High, CType(GridControl.GetCellValue(GridControl.VisibleRowCount - 1 - i, fieldName), Priority))
            Next
        End Sub
#End Region
    End Class
End Namespace
