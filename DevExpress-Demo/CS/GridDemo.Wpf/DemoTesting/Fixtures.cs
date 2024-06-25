using CommonChartsDemo;
using DevExpress.Data.Filtering;
using DevExpress.DemoData.Models;
using DevExpress.Mvvm;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.DemoBase.DemoTesting;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.ExpressionEditor;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors.ExpressionEditor.Native;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace GridDemo.Tests {
    public class GridCheckAllDemosFixture : CheckAllDemosFixture {
        protected override bool SwitchAllThemes(Type moduleType) {
            return false;
        }
        protected override bool CheckMemoryLeaks(Type moduleType) {
            return moduleType != typeof(ClipboardFormats);
        }
        protected override int TestingThemesCount() { return 5; }
    }
    #region GridDemoModulesAccessor
    public class GridDemoModulesAccessor : DemoModulesAccessor<GridDemoModule> {
        public GridDemoModulesAccessor(BaseDemoTestingFixture fixture)
            : base(fixture) {
        }
        public DevExpress.Xpf.Grid.GridControl GridControl { get { return DemoModule.GridControl; } }
        public DevExpress.Xpf.Grid.GridViewBase View { get { return (DevExpress.Xpf.Grid.GridViewBase)GridControl.View; } }
        public DevExpress.Xpf.Grid.TableView TableView { get { return (DevExpress.Xpf.Grid.TableView)View; } }
        public DevExpress.Xpf.Grid.CardView CardView { get { return (DevExpress.Xpf.Grid.CardView)View; } }
        public StandardTableView TableViewModule { get { return (StandardTableView)DemoModule; } }
        public MultiRowSelection MultiSelectionModule { get { return (MultiRowSelection)DemoModule; } }
        public CopyPasteOperations CopyPasteModule { get { return (CopyPasteOperations)DemoModule; } }
        public LargeDataSource LargeDataSetModule { get { return (LargeDataSource)DemoModule; } }
        public GridDemo.DragDrop DragDropModule { get { return (GridDemo.DragDrop)DemoModule; } }
        public GridDemo.HitTesting HitTestModule { get { return (GridDemo.HitTesting)DemoModule; } }
        public UnboundColumns UnboundColumnsModule { get { return (UnboundColumns)DemoModule; } }
    }
    #endregion
    public abstract class BaseGridDemoTestingFixture : BaseDemoTestingFixture {
        readonly GridDemoModulesAccessor modulesAccessor;
        public BaseGridDemoTestingFixture() {
            modulesAccessor = new GridDemoModulesAccessor(this);
        }
        public DevExpress.Xpf.Grid.GridControl GridControl { get { return modulesAccessor.GridControl; } }
        public DevExpress.Xpf.Grid.GridViewBase View { get { return modulesAccessor.View; } }
        public DevExpress.Xpf.Grid.TableView TableView { get { return modulesAccessor.TableView; } }
        public DevExpress.Xpf.Grid.CardView CardView { get { return modulesAccessor.CardView; } }
        public StandardTableView TableViewModule { get { return modulesAccessor.TableViewModule; } }
        public MultiRowSelection MultiSelectionModule { get { return modulesAccessor.MultiSelectionModule; } }
        public CopyPasteOperations CopyPasteModule { get { return modulesAccessor.CopyPasteModule; } }
        public LargeDataSource LargeDataSetModule { get { return modulesAccessor.LargeDataSetModule; } }
        public GridDemo.DragDrop DragDropModule { get { return modulesAccessor.DragDropModule; } }
        public GridDemo.HitTesting HitTestModule { get { return modulesAccessor.HitTestModule; } }
        public UnboundColumns UnboundColumnsModule { get { return modulesAccessor.UnboundColumnsModule; } }
    }
    public class CheckDemoOptionsFixture : BaseGridDemoTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            CreateCheckUnboundColumnsDemoActions();
            CreateCheckMultiSelectionDemoActions();
            CreateCheckCopyPasteDemoActions();
            CreateCheckTableViewDemoActions();
            CreateCheckFixedColumnsDemoActions();
            CreateSetCurrentDemoActions(null, false);
        }
        bool IsListBox(FrameworkElement element) {
            return element is ListBox;
        }
        int GetListBoxEditCount(ListBoxEdit listBoxEdit) {
            return ((System.Collections.IList)listBoxEdit.ItemsSource).Count;
        }
        #region UnboundColumns
        void CreateCheckUnboundColumnsDemoActions() {
            AddLoadModuleActions(typeof(GridDemo.UnboundColumns));
            Dispatch(delegate () {
                Assert.AreEqual(0, GridControl.Columns["OrderID"].GroupIndex);
                Assert.AreEqual(1, GridControl.GroupCount);
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-1));
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-2));
                Assert.IsTrue(GridControl.IsGroupRowExpanded(-3));
                Assert.IsTrue(UnboundColumnsModule.showExpressionEditorButton.IsEnabled);
                Assert.IsTrue(GridControl.Columns["Total"].AllowUnboundExpressionEditor);
                Assert.IsTrue(GridControl.Columns["DiscountAmount"].AllowUnboundExpressionEditor);
                Assert.AreEqual(2, UnboundColumnsModule.columnsList.Items.Count);

                System.Windows.Controls.ListBox listBox;
                listBox = (System.Windows.Controls.ListBox)LayoutHelper.FindElement(UnboundColumnsModule.columnsList, IsListBox);

                Assert.AreEqual("Discount Amount", ((System.Windows.Controls.ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(0)).Content);
                Assert.AreEqual("Total", ((System.Windows.Controls.ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(1)).Content);
                Assert.AreEqual("DiscountAmount", ((System.Windows.Controls.ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(0)).Tag);
                Assert.AreEqual("Total", ((System.Windows.Controls.ListBoxItem)listBox.ItemContainerGenerator.ContainerFromIndex(1)).Tag);
                Assert.AreEqual((decimal)0.0, GridControl.GetCellValue(0, GridControl.Columns["DiscountAmount"]));
                Assert.AreEqual((decimal)174.0, GridControl.GetCellValue(0, GridControl.Columns["Total"]));
                Assert.IsTrue(Math.Abs((double)38.0 - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns["DiscountAmount"]))) < 0.001);
                Assert.IsTrue(Math.Abs((double)214.2 - Convert.ToDouble(GridControl.GetCellValue(6, GridControl.Columns["Total"]))) < 0.001);

                UnboundColumnsModule.columnsList.SelectedIndex = 0;
                AssertShowExpressionEditor("Round([UnitPrice] * [Quantity] - [Total])");
                UpdateLayoutAndDoEvents();
                UnboundColumnsModule.columnsList.SelectedIndex = 1;
                AssertShowExpressionEditor("[UnitPrice] * [Quantity] * (1 - [Discount])");
                UpdateLayoutAndDoEvents();

                GridControl.Columns["DiscountAmount"].UnboundExpression = "[UnitPrice] * [Quantity] * (1 - [Discount])+[Discount][[Discount]]";
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(DevExpress.Data.UnboundErrorObject.Value, GridControl.GetCellValue(0, GridControl.Columns["DiscountAmount"]));
            });
        }

        string _expectedExpression;
        ExpressionEditorControl expressionEditorControl = null;

        void OnExpressionEditorLoaded(object sender, RoutedEventArgs e) {
            Window _dw = expressionEditorControl.Parent as Window;
            Assert.IsNotNull(_dw, "2");
            expressionEditorControl.Loaded -= OnExpressionEditorLoaded;
            Assert.IsTrue(expressionEditorControl.IsVisible);
            Assert.AreEqual(_expectedExpression, ((ISupportExpressionString)expressionEditorControl).GetExpressionString(null));
            _dw.DialogResult = true;
            _dw.Close();
        }

        void OnExpressionEditorCreated(object sender, UnboundExpressionEditorEventArgs e) {
            expressionEditorControl = e.ExpressionEditorControl;
            Assert.IsNotNull(expressionEditorControl, "1");
            expressionEditorControl.Loaded += OnExpressionEditorLoaded;
            View.UnboundExpressionEditorCreated -= OnExpressionEditorCreated;
        }

        void AssertShowExpressionEditor(string expectedExpression) {
            _expectedExpression = expectedExpression;
            View.UnboundExpressionEditorCreated += OnExpressionEditorCreated;
            UIAutomationActions.ClickButton(UnboundColumnsModule.showExpressionEditorButton);
            UpdateLayoutAndDoEvents();
        }
        #endregion
        #region table view
        void CreateCheckTableViewDemoActions() {
            AddLoadModuleActions(typeof(StandardTableView));
            Dispatch(delegate () {
                Assert.IsTrue(TableView.AllowSorting);
                Assert.IsTrue(TableView.AllowGrouping);
                Assert.IsTrue(TableView.AllowColumnMoving);
                Assert.IsTrue(TableView.AllowResizing);
                Assert.IsTrue(TableView.AllowBestFit);
                Assert.IsTrue(TableView.ShowHorizontalLines);
                Assert.IsTrue(TableView.ShowVerticalLines);

                bool hasStarColumn = false;

                foreach(var column in GridControl.Columns) {
                    hasStarColumn = hasStarColumn || column.Width.IsStar;
                    switch(column.FieldName) {
                        case "Phone":
                            Assert.IsFalse(column.AllowSorting.ToBoolean(false));
                            Assert.IsFalse(column.ActualAllowGrouping);
                            break;
                        default:
                            Assert.IsTrue(column.ActualAllowSorting);
                            Assert.IsTrue(column.ActualAllowGrouping);
                            break;
                    }
                }
                Assert.IsTrue(hasStarColumn);

                EditorsActions.ToggleCheckEdit(TableViewModule.allowSortingCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AllowSorting);
                foreach(var column in GridControl.Columns) {
                    Assert.IsFalse(column.ActualAllowSorting);
                    Assert.IsFalse(column.ActualAllowGrouping);
                }

                EditorsActions.ToggleCheckEdit(TableViewModule.allowSortingCheckEdit);
                EditorsActions.ToggleCheckEdit(TableViewModule.allowGroupingCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AllowGrouping);
                foreach(var column in GridControl.Columns) {
                    switch(column.FieldName) {
                        case "Phone":
                            Assert.IsFalse(column.AllowSorting.ToBoolean(false));
                            Assert.IsFalse(column.ActualAllowGrouping);
                            break;
                        default:
                            Assert.IsTrue(column.ActualAllowSorting);
                            Assert.IsFalse(column.ActualAllowGrouping);
                            break;
                    }
                }

                EditorsActions.ToggleCheckEdit(TableViewModule.allowMovingCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AllowColumnMoving);
                foreach(var column in GridControl.Columns) Assert.IsFalse(column.ActualAllowMoving);


                EditorsActions.ToggleCheckEdit(TableViewModule.allowResizingCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AllowResizing);
                foreach(var column in GridControl.Columns) Assert.IsFalse(column.ActualAllowResizing);

                EditorsActions.ToggleCheckEdit(TableViewModule.allowBestFitCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.AllowBestFit);

                EditorsActions.ToggleCheckEdit(TableViewModule.showHorizontalLinesCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.ShowHorizontalLines);

                EditorsActions.ToggleCheckEdit(TableViewModule.showVerticalLinesCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsFalse(TableView.ShowVerticalLines);

                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Row, ((EnumMemberInfo)TableViewModule.NavigationStyleComboBox.SelectedItem).Id);
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Row, View.NavigationStyle);
                TableViewModule.NavigationStyleComboBox.SelectedIndex = 1;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Cell, ((EnumMemberInfo)TableViewModule.NavigationStyleComboBox.SelectedItem).Id);
                TableViewModule.NavigationStyleComboBox.SelectedIndex = 2;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.None, ((EnumMemberInfo)TableViewModule.NavigationStyleComboBox.SelectedItem).Id);
            });
        }
        #endregion
        #region Multi-Selection
        void CreateCheckMultiSelectionDemoActions() {
            AddLoadModuleActions(typeof(MultiRowSelection));
            Dispatch(delegate () {
                Assert.AreEqual(GridControl.View, TableView);
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Cell, TableView.NavigationStyle);
                AssertMuliSelection(TableView);
                MultiSelectionModule.viewsListBox.SelectedIndex = 1;
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(GridControl.View, CardView);
                Assert.AreEqual(DevExpress.Xpf.Grid.GridViewNavigationStyle.Row, CardView.NavigationStyle);
                GridControl.SelectItem(0);
                AssertMuliSelection(CardView);
            });
        }
        void AssertMuliSelection(DevExpress.Xpf.Grid.GridViewBase gridView) {
            AssertMultiSelectMode(gridView, true);

            MultiSelectionModule.selectionModeListBox.EditValue = DevExpress.Xpf.Grid.MultiSelectMode.None;
            UpdateLayoutAndDoEvents();
            AssertMultiSelectMode(gridView, false);
            Assert.IsFalse(MultiSelectionModule.ProductsMultiSelectionOptionsControl.IsEnabled);
            Assert.IsFalse(MultiSelectionModule.PriceMultiSelectionOptionsControl.IsEnabled);
            MultiSelectionModule.selectionModeListBox.EditValue = DevExpress.Xpf.Grid.MultiSelectMode.Row;
            UpdateLayoutAndDoEvents();
            AssertMultiSelectMode(gridView, true);
            Assert.IsTrue(MultiSelectionModule.ProductsMultiSelectionOptionsControl.IsEnabled);
            Assert.IsTrue(MultiSelectionModule.PriceMultiSelectionOptionsControl.IsEnabled);

            Assert.AreEqual(77, ((IEnumerable<Product>)((ComboBoxEdit)MultiSelectionModule.ProductsMultiSelectionOptionsControl.comboBoxControl).ItemsSource).Count());
            Assert.AreEqual(1, gridView.Grid.SelectedItems.Count);

            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.SelectButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(39, gridView.Grid.SelectedItems.Count);
            Assert.AreEqual(39, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox));

            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.UnselectButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(1, gridView.Grid.SelectedItems.Count);
            Assert.AreEqual(1, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox));

            UIAutomationActions.ClickButton(MultiSelectionModule.ProductsMultiSelectionOptionsControl.ReselectButton);
            UpdateLayoutAndDoEvents();
            Assert.AreEqual(38, gridView.Grid.SelectedItems.Count);
            Assert.AreEqual(38, GetListBoxEditCount(MultiSelectionModule.SelectionRowsListBox));

            gridView.Grid.UnselectAll();
            gridView.Grid.SelectRange(0, 3);

            Assert.AreEqual("Selection Total=${0:N}", gridView.VisibleColumns[4].TotalSummaries[0].Item.DisplayFormat);
            Assert.AreEqual(Convert.ToString(607.4, System.Globalization.CultureInfo.CurrentCulture), gridView.VisibleColumns[4].TotalSummaries[0].Value.ToString());
            GridControl.UnselectAll();

            int count = ((MultiSelectionModule.PriceMultiSelectionOptionsControl.comboBoxControl.ItemsSource) as System.Collections.Generic.List<Range>).Count;
            Assert.AreEqual(9, count);
            for(int i = 0; i < count; i++) {
                MultiSelectionModule.PriceMultiSelectionOptionsControl.comboBoxControl.SelectedIndex = i;
                UIAutomationActions.ClickButton(MultiSelectionModule.PriceMultiSelectionOptionsControl.SelectButton);
                UpdateLayoutAndDoEvents();
            }
            Assert.AreEqual(GridControl.VisibleRowCount, gridView.Grid.SelectedItems.Count);
            GridControl.UnselectAll();

            GridControl.GroupBy("OrderID");
            UpdateLayoutAndDoEvents();
            GridControl.SelectItem(0);
            GridControl.SelectItem(10);
            Assert.AreEqual("Grand Total=${0:N}", ((DevExpress.Xpf.Grid.GroupRowData)gridView.RootRowsContainer.Items[0]).GroupSummaryData[0].SummaryItem.DisplayFormat);
            Assert.AreEqual(168, Convert.ToInt32(((DevExpress.Xpf.Grid.GroupRowData)gridView.RootRowsContainer.Items[0]).GroupSummaryData[0].SummaryValue));
            Assert.AreEqual(336, Convert.ToInt32(((DevExpress.Xpf.Grid.GroupRowData)gridView.RootRowsContainer.Items[3]).GroupSummaryData[0].SummaryValue));
            Assert.AreEqual(504, Convert.ToInt32(gridView.VisibleColumns[4].TotalSummaries[0].Value));
            GridControl.ClearGrouping();
            GridControl.UnselectAll();
        }
        void AssertMultiSelectMode(DevExpress.Xpf.Grid.GridViewBase view, bool isMultiSelect) {
            Assert.AreEqual(isMultiSelect ? DevExpress.Xpf.Grid.MultiSelectMode.Row : DevExpress.Xpf.Grid.MultiSelectMode.None, view.DataControl.SelectionMode);
        }
        #endregion
        #region Copy Paste
        void CreateCheckCopyPasteDemoActions() {
            AddLoadModuleActions(typeof(CopyPasteOperations));
            Dispatch(delegate () {
                Assert.IsTrue(CopyPasteModule.firstGrid.ClipboardCopyMode != DevExpress.Xpf.Grid.ClipboardCopyMode.None);
                Assert.IsTrue(CopyPasteModule.secondGrid.ClipboardCopyMode != DevExpress.Xpf.Grid.ClipboardCopyMode.None);
                EditorsActions.ToggleCheckEdit(CopyPasteModule.allowCopyingtoClipboardCheckEdit);
                UpdateLayoutAndDoEvents();
                Assert.IsTrue(CopyPasteModule.firstGrid.ClipboardCopyMode == DevExpress.Xpf.Grid.ClipboardCopyMode.None);
                Assert.IsTrue(CopyPasteModule.secondGrid.ClipboardCopyMode == DevExpress.Xpf.Grid.ClipboardCopyMode.None);
                EditorsActions.ToggleCheckEdit(CopyPasteModule.allowCopyingtoClipboardCheckEdit);
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.None, false, false, false, false);

                Assert.AreEqual(10, CopyPasteModule.firstGrid.VisibleRowCount);
                Assert.AreEqual(0, CopyPasteModule.secondGrid.VisibleRowCount);

                Assert.AreEqual(0, CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex);

                ClickOnGrid(CopyPasteModule.firstGrid);
                CopyPasteModule.firstGrid.SelectRange(1, 4);
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.First, true, true, false, true);

                Assert.AreEqual(0, CopyPasteModule.PasteRule.SelectedIndex);
                UIAutomationActions.ClickButton(CopyPasteModule.CopyButton);
                UpdateLayoutAndDoEvents();
                ClickOnGrid(CopyPasteModule.secondGrid);
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.Second, false, false, true, false);

                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
            });
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, () => DemoBaseTesting.CurrentDemoModule, null, null);
            Dispatch(delegate () {
                Assert.AreEqual(5, CopyPasteModule.secondGrid.VisibleRowCount);
                ClickOnGrid(CopyPasteModule.firstGrid);
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.First, true, true, true, true);
                UIAutomationActions.ClickButton(CopyPasteModule.DeleteButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(5, CopyPasteModule.firstGrid.VisibleRowCount);

                CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex = 1;
                UpdateLayoutAndDoEvents();
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.First, false, false, true, false);

                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40));
                CopyPasteModule.textEdit.Select(0, 8);
                UpdateLayoutAndDoEvents();
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                AssertGridsAndButtonState(FocusedGrid.First, true, true, true, true);
                UIAutomationActions.ClickButton(CopyPasteModule.CutButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual("Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 32));
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                CopyPasteModule.textEdit.CaretIndex = 0;
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40));
                CopyPasteModule.textEdit.SelectAll();
                UpdateLayoutAndDoEvents();
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(CopyPasteModule.CopyButton);
                UpdateLayoutAndDoEvents();
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(CopyPasteModule.DeleteButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual("", CopyPasteModule.textEdit.Text);
                CopyPasteModule.textEdit.Focus();
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual("Id From Sent Hours Active Has Attachment", CopyPasteModule.textEdit.Text.Substring(0, 40));

                CopyPasteModule.CopyPasteDemoTabControl.SelectedIndex = 0;
                UpdateLayoutAndDoEvents();
                CopyPasteModule.secondGrid.UnselectAll();
                CopyPasteModule.secondGrid.SelectItem(2);
                UpdateLayoutAndDoEvents();
                ((GridDemo.CopyPasteOutlookData)((DevExpress.Xpf.Grid.GridViewBase)CopyPasteModule.secondGrid.View).Grid.SelectedItems[0]).From = "QWERTY";
                ClickOnGrid(CopyPasteModule.secondGrid);
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(CopyPasteModule.CutButton);
                UpdateLayoutAndDoEvents();
                Assert.AreEqual(4, CopyPasteModule.secondGrid.VisibleRowCount);

                CopyPasteModule.firstGrid.View.FocusedRowHandle = 2;
                ClickOnGrid(CopyPasteModule.firstGrid);
                UpdateLayoutAndDoEvents();
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
            });
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, () => DemoBaseTesting.CurrentDemoModule, null, null);
            Dispatch(delegate () {
                Assert.AreEqual(6, CopyPasteModule.firstGrid.VisibleRowCount);
                Assert.AreEqual("QWERTY", ((GridDemo.CopyPasteOutlookData)CopyPasteModule.firstGrid.GetRow(3)).From);

                CopyPasteModule.PasteRule.SelectedIndex = 1;
                UIAutomationActions.ClickButton(CopyPasteModule.PasteButton);
            });
            AddEventAction(CopyPasteOperations.PasteCompetedEvent, () => DemoBaseTesting.CurrentDemoModule, null, null);
            Dispatch(delegate () {
                Assert.AreEqual(7, CopyPasteModule.firstGrid.VisibleRowCount);
                Assert.AreEqual("QWERTY", ((GridDemo.CopyPasteOutlookData)CopyPasteModule.firstGrid.GetRow(6)).From);
            });
        }
        void ClickOnGrid(DevExpress.Xpf.Grid.GridControl grid) {
            MouseActions.LeftMouseDown(grid.View, 2, 2);
            MouseActions.LeftMouseUp(grid.View, 2, 2);
        }
        void AssertGridsAndButtonState(FocusedGrid focusedGrid, bool copyBtn, bool cutBtn, bool pasteBtn, bool delBtn) {
            Assert.AreEqual(focusedGrid, CopyPasteModule.FocusedGrid);
            Assert.AreEqual(copyBtn, CopyPasteModule.CopyButton.IsEnabled);
            Assert.AreEqual(cutBtn, CopyPasteModule.CutButton.IsEnabled);
            Assert.AreEqual(pasteBtn, CopyPasteModule.PasteButton.IsEnabled);
            Assert.AreEqual(delBtn, CopyPasteModule.DeleteButton.IsEnabled);
        }
        #endregion

        void CreateCheckFixedColumnsDemoActions() {
            AddLoadModuleActions(typeof(FixedDataColumns));
        }
    }
    public class BugFixesFixture : BaseGridDemoTestingFixture {
        protected override void CreateActions() {
            base.CreateActions();
            Create_B144178_Actions();
            CreateSetCurrentDemoActions(null, false);
        }
        #region B144178
        void Create_B144178_Actions() {
            AddLoadModuleActions(typeof(LargeDataSource));
            Dispatch(delegate () {
                AssertPrioritySorting("Priority6");
            });
            AddLoadModuleActions(typeof(CellEditors));
            Dispatch(delegate () {
                AssertPrioritySorting("Priority");
            });
        }

        private void AssertPrioritySorting(string fieldName) {
            GridControl.SortBy(fieldName);
            for(int i = 0; i < 10; i++) {
                Assert.AreEqual(Priority.Low, (Priority)GridControl.GetCellValue(i, fieldName), fieldName);
                Assert.AreEqual(Priority.High, (Priority)GridControl.GetCellValue(GridControl.VisibleRowCount - 1 - i, fieldName));
            }
        }
        #endregion
    }
}
