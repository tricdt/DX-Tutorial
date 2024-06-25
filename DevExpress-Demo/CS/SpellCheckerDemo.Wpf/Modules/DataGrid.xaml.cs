using System.Windows;
using System;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.SpellChecker;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Core;

namespace SpellCheckerDemo {
    public partial class DataGrid : SpellCheckerDemoModule {
        public DataGrid() {
            InitializeComponent();
        }
    }

    public class GridControlSpellChecker : DXSpellCheckerBase<GridControl> {
        GridControl Grid { get { return AssociatedObject; } }

        bool stopChecking;
        public void Check() {
            stopChecking = false;
            SubscribeToEvents();
            CheckCell(0, 0);
        }
        void SubscribeToEvents() {
            SpellChecker.CheckCompleteFormShowing += new DevExpress.XtraSpellChecker.FormShowingEventHandler(Checker_CheckCompleteFormShowing);
        }
        void UnsubscribeFromEvents() {
            SpellChecker.CheckCompleteFormShowing -= new DevExpress.XtraSpellChecker.FormShowingEventHandler(Checker_CheckCompleteFormShowing);
        }
        void Checker_CheckCompleteFormShowing(object sender, DevExpress.XtraSpellChecker.FormShowingEventArgs e) {
            e.Handled = true;
        }
        void CheckCell(int rowIndex, int columnIndex) {
            GridColumn column = Grid.Columns[columnIndex];
            if (column.IsGrouped)
                return;
            Grid.CurrentColumn = column;
            Grid.View.FocusedRowHandle = rowIndex;
            Grid.View.ShowEditor();
            Grid.UpdateLayout();

            BaseEdit activeEditor = Grid.View.ActiveEditor;
            if (activeEditor == null || !SpellChecker.CanCheck(activeEditor))
                CheckNextCell();
            else {
                UnsubscribeFromEvents();
                SpellChecker.CheckCompleteFormShowing += new DevExpress.XtraSpellChecker.FormShowingEventHandler(Checker_CheckCompleteFormShowing);
                SpellChecker.AfterCheck += Checker_AfterCheck;
                CheckActiveEditor(activeEditor);
            }
        }
        void CheckActiveEditor(BaseEdit activeEditor) {
            if (activeEditor.IsLoaded)
                SpellChecker.Check(activeEditor);
            else
                activeEditor.Loaded += OnActiveEditorLoaded;
        }
        void OnActiveEditorLoaded(object sender, RoutedEventArgs e) {
            BaseEdit activeEditor = (BaseEdit)sender;
            activeEditor.Loaded -= OnActiveEditorLoaded;
            SpellChecker.Check(activeEditor);
        }
        void Checker_AfterCheck(object sender, EventArgs e) {
            SpellChecker.AfterCheck -= Checker_AfterCheck;
            SpellChecker.CheckCompleteFormShowing -= new DevExpress.XtraSpellChecker.FormShowingEventHandler(Checker_CheckCompleteFormShowing);
            SubscribeToEvents();
            stopChecking = (e as DevExpress.XtraSpellChecker.AfterCheckEventArgs).Reason == DevExpress.XtraSpellChecker.StopCheckingReason.User;
            CheckNextCell();
        }
        void CheckNextCell() {
            CheckNextCell(Grid.View.FocusedRowHandle, (GridColumn)Grid.CurrentColumn);
        }
        void CheckNextCell(int rowIndex, GridColumn column) {
            if (stopChecking) {
                UnsubscribeFromEvents();
                return;
            }
            int columnIndex = Grid.Columns.IndexOf(column);
            int nextColumnIndex = columnIndex == Grid.Columns.Count - 1 ? 0 : columnIndex + 1;
            int nextRowIndex = nextColumnIndex == 0 ? rowIndex + 1 : rowIndex;
            if (!Grid.IsValidRowHandle(nextRowIndex)) {
                UnsubscribeFromEvents();
                ThemedMessageBox.Show(Window.GetWindow(Grid), "Spelling", "Spelling check is complete", MessageBoxButton.OK, null, MessageBoxImage.None);
            }
            else
                CheckCell(nextRowIndex, nextColumnIndex);
        }
    }

}
