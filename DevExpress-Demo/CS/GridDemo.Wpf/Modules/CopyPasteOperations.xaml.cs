using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace GridDemo {
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/CopyPasteTemplates.xaml")]
    [DevExpress.Xpf.DemoBase.CodeFile("ModuleResources/CopyPasteClasses.(cs)")]
    public partial class CopyPasteOperations : GridDemoModule {
        public static readonly RoutedEvent PasteCompetedEvent;

        internal int Counter { get; set; }
        public static readonly DependencyProperty PastUnderFocusedRowProperty;
        public static readonly DependencyProperty FocusedGridProperty;
        static CopyPasteOperations() {
            PastUnderFocusedRowProperty = DependencyProperty.Register("PastUnderFocusedRow", typeof(bool), typeof(CopyPasteOperations), new PropertyMetadata(true));
            FocusedGridProperty = DependencyProperty.Register("FocusedGrid", typeof(FocusedGrid), typeof(CopyPasteOperations), new PropertyMetadata(FocusedGrid.None, (d, e) => ((CopyPasteOperations)d).UpdateAnimationTarget()));
            PasteCompetedEvent = EventManager.RegisterRoutedEvent("PasteCompeted", RoutingStrategy.Direct, typeof(RoutedEventHandler), typeof(CopyPasteOperations));
        }
        public event RoutedEventHandler PasteCompeted {
            add { AddHandler(PasteCompetedEvent, value); }
            remove { RemoveHandler(PasteCompetedEvent, value); }
        }
        public bool PastUnderFocusedRow {
            get { return (bool)GetValue(PastUnderFocusedRowProperty); }
            set { SetValue(PastUnderFocusedRowProperty, value); }
        }
        public FocusedGrid FocusedGrid {
            get { return (FocusedGrid)GetValue(FocusedGridProperty); }
            set { SetValue(FocusedGridProperty, value); }
        }
        Dictionary<int, RowsAnimationElement> animationElements = new Dictionary<int, RowsAnimationElement>();
        BindingList<CopyPasteOutlookData> firstList = new BindingList<CopyPasteOutlookData>();
        BindingList<CopyPasteOutlookData> secondList = new BindingList<CopyPasteOutlookData>();
        Dictionary<FocusedGrid, GridControl> GridDictionary = new Dictionary<FocusedGrid, GridControl>();
        public CopyPasteOperations() {
            Counter = 0;
            InitializeComponent();
            OptionsDataContext = this;
            DemoContentGrid.DataContext = this;
            var list = OutlookDataGenerator.CreateOutlookDataList(10);
            object[] objectForCopying = list.ToArray();
            for(int i = 0; i < objectForCopying.Length; i++) {
                firstList.Add(CopyPasteOutlookData.ConvertOutlookDataToCopyPasteOutlookData((OutlookData)objectForCopying[i], this));
            }
            firstGrid.ItemsSource = firstList;
            secondGrid.ItemsSource = secondList;
            allowCopyingtoClipboardCheckEdit.IsChecked = true;
            copyHeaderCheckEdit.IsChecked = true;

            firstGrid.PreviewMouseDown += new MouseButtonEventHandler(firstGrid_PreviewMouseDown);
            secondGrid.PreviewMouseDown += new MouseButtonEventHandler(secondGrid_PreviewMouseDown);
            GridDictionary.Add(FocusedGrid.First, firstGrid);
            GridDictionary.Add(FocusedGrid.Second, secondGrid);
        }
        void firstGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            FocusedGrid = FocusedGrid.First;
        }
        void secondGrid_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
            FocusedGrid = FocusedGrid.Second;
        }
        delegate void Action(TableView view, BindingList<CopyPasteOutlookData> list);
        int CompareForDeleteList(int x, int y) {
            if(x > y) {
                return -1;
            } else if(x < y) {
                return 1;
            }
            return 0;
        }
        protected override void HidePopupContent() {
            if(secondGrid != null)
                secondGrid.View.HideColumnChooser();
            base.HidePopupContent();
        }
        void CopyingRows(TableView view, BindingList<CopyPasteOutlookData> list) {
            if(view != null) {
                if(view.Grid.SelectedItems.Count != 0) {
                    view.Grid.CopySelectedItemsToClipboard();
                } else if(view.FocusedRowHandle != DataControlBase.InvalidRowHandle) {
                    view.Grid.CopyRowsToClipboard(new int[] { view.FocusedRowHandle });
                }
            } else {
                textEdit.Copy();
            }
        }
        void RemoveRow(TableView view, int rowHandle) {
            RemoveAnimationElement(view.Grid.GetListIndexByRowHandle(rowHandle));
            view.DeleteRow(rowHandle);
        }
        void DeleteRows(TableView view, BindingList<CopyPasteOutlookData> list) {
            if(view != null) {
                if(view.Grid.SelectedItems.Count != 0) {
                    view.Grid.BeginDataUpdate();
                    List<int> selectedList = new List<int>(view.Grid.GetSelectedRowHandles());
                    selectedList.Sort(CompareForDeleteList);
                    foreach(int row in selectedList) {
                        RemoveRow(view, row);
                    }
                    view.Grid.EndDataUpdate();
                } else if(view.FocusedRowHandle != DataControlBase.InvalidRowHandle) {
                    RemoveRow(view, view.FocusedRowHandle);
                }
            } else {
                textEdit.Delete();
            }
        }
        void CutRows(TableView view, BindingList<CopyPasteOutlookData> list) {
            CopyingRows(view, null);
            DeleteRows(view, null);
        }
        readonly int maxAnimationRows = 30;
        internal int MaxAnimationRows { get { return this.maxAnimationRows; } }
        internal void PasteRowsWithoutAnimation(ref int positionNewRow, GridViewBase view, BindingList<CopyPasteOutlookData> list, object[] objectsForCopy, int start, int end) {
            view.Grid.BeginDataUpdate();
            PasteRowsWithAnimation(ref positionNewRow, view, list, objectsForCopy, start, end);
            view.Grid.EndDataUpdate();
        }
        void PasteRowsWithAnimation(ref int positionNewRow, GridViewBase view, BindingList<CopyPasteOutlookData> list, object[] objectsForCopy, int start, int end) {
            bool insertToEndOfRows = view.FocusedRowHandle == GridControl.InvalidRowHandle;
            for(int i = start; i < end; i++) {
                CopyPasteOutlookData obj = ((CopyPasteOutlookData)objectsForCopy[i]);
                obj.UniqueID = ++Counter;
                if(i == maxAnimationRows - 1) {
                    pasteWithoutAnimation = new PasteHelper() { List = list, ObjectsForCopy = objectsForCopy, Owner = this, PositionNewRow = positionNewRow, View = view };
                }
                if(PastUnderFocusedRow && (list.Count != 0) && !insertToEndOfRows) {
                    list.Insert(positionNewRow, obj);
                    positionsNewRowsList.Add(positionNewRow);
                    positionNewRow++;
                } else {
                    list.Add(obj);
                    positionsNewRowsList.Add(list.Count - 1);
                }
            }
        }
        List<int> positionsNewRowsList = new List<int>();
        bool isPasting = false;
        PasteHelper pasteWithoutAnimation = null;
        FocusedGrid animationTarget = FocusedGrid.None;
        void UpdateAnimationTarget() {
            if(!animation)
                animationTarget = FocusedGrid;
        }
        void PasteRows(GridViewBase view, BindingList<CopyPasteOutlookData> list) {
            if(view != null) {
                ArrayList arrayList = null;
                IDataObject dataObj = GetClipboardDataObject();
                string format = typeof(ArrayList).FullName;
                if(dataObj != null && dataObj.GetDataPresent(format)) {
                    positionsNewRowsList.Clear();
                    pasteWithoutAnimation = null;
                    animation = true;
                    StartPasteForCanCommands();
                    arrayList = dataObj.GetData(format) as ArrayList;
                    object[] objectsForCopy = arrayList.ToArray();
                    int positionNewRow = view.FocusedRowHandle == DataControlBase.InvalidRowHandle ? 0 : view.Grid.GetListIndexByRowHandle(view.FocusedRowHandle) + 1;
                    if(maxAnimationRows > objectsForCopy.Length) {
                        PasteRowsWithAnimation(ref positionNewRow, view, list, objectsForCopy, 0, objectsForCopy.Length);
                    } else {
                        PasteRowsWithAnimation(ref positionNewRow, view, list, objectsForCopy, 0, maxAnimationRows);
                    }
                    Dispatcher.BeginInvoke(new System.Action(AnimationRowsOfPasted), DispatcherPriority.Background);
                }
            } else {
                textEdit.Paste();
            }
        }
        void StartPasteForCanCommands() {
            Cursor = Cursors.Wait;
            isPasting = true;
            CommandManager.InvalidateRequerySuggested();
        }
        internal void EndPasteForCanCommands() {
            Cursor = Cursors.Arrow;
            isPasting = false;
            CommandManager.InvalidateRequerySuggested();
        }
        void AnimationRowsOfPasted() {
            for(int i = 0; i < positionsNewRowsList.Count; i++) {
                if(i == (positionsNewRowsList.Count - 1)) {
                    StartAnimation(positionsNewRowsList[i], pasteWithoutAnimation);
                    if(pasteWithoutAnimation == null)
                        EndPasteForCanCommands();
                } else {
                    StartAnimation(positionsNewRowsList[i], null);
                }
            }
            animation = false;
            UpdateAnimationTarget();
        }
        void StartAnimation(int positionNewRow, PasteHelper pasteWithoutAnimation) {
            Storyboard addingStoryboard = GetStoryboard("newRowStoryboard");
            Storyboard colorStoryboard = GetStoryboard("newRowColorStoryboard");
            if(pasteWithoutAnimation != null) {
                pasteWithoutAnimation.ColorStoryboard = colorStoryboard;
                colorStoryboard.Completed += new EventHandler(pasteWithoutAnimation.ColorStoryboardCompleted);
            }
            if(positionsNewRowsList[positionsNewRowsList.Count - 1] == positionNewRow) {
                PasteCompetedHelper pasteHelper = new PasteCompetedHelper() { Owner = this, ColorStoryboard = colorStoryboard };
                colorStoryboard.Completed += new EventHandler(pasteHelper.ColorStoryboardCompleted);
            }
            StartStoryboard(addingStoryboard, positionNewRow, RowsAnimationElement.NewRowsProgressProperty);
            StartStoryboard(colorStoryboard, positionNewRow, RowsAnimationElement.NewRowsColorProperty);
        }
        internal void RaisePasteCompetedEvent(RoutedEventArgs e) {
            RaiseEvent(e);
        }
        void StartStoryboard(Storyboard storyboard, int indexElement, DependencyProperty property) {
            Storyboard.SetTargetProperty(storyboard, new PropertyPath(property.Name));
            storyboard.Begin(GetAnimationElement(indexElement), HandoffBehavior.SnapshotAndReplace);
        }
        Storyboard GetStoryboard(string resourceKey) {
            return ((Storyboard)FindResource(resourceKey)).Clone();
        }
        void CommandExecute(Action actionForCommand) {
            if(IsFocusedTextEdit()) {
                actionForCommand(null, null);
            } else if(FocusedGrid == FocusedGrid.First) {
                actionForCommand((TableView)firstGrid.View, firstList);
            } else if(FocusedGrid == FocusedGrid.Second) {
                actionForCommand((TableView)secondGrid.View, secondList);
            }
        }
        bool IsFocusedTextEdit() {
            return ((textEdit != null) && (textEdit.IsKeyboardFocusWithin)) ? true : false;
        }
        bool IsSelectRows(TableView view) {
            return ((view.Grid.SelectedItems.Count != 0) || (view.FocusedRowHandle != DataControlBase.InvalidRowHandle)) ? true : false;
        }
        bool CanExecuteOutputCommands() {
            if(IsFocusedTextEdit()) {
                return (textEdit.SelectionLength != 0);
            } else if(FocusedGrid != FocusedGrid.None) {
                return IsSelectRows((TableView)GridDictionary[FocusedGrid].View);
            }
            return false;
        }
        bool CanExecuteInputCommands() {
            if(isPasting)
                return false;
            if(IsFocusedTextEdit()) {
                IDataObject dataObject = Clipboard.GetDataObject();
                string text = dataObject != null ? ((DataObject)dataObject).GetText() : null;
                if(!string.IsNullOrEmpty(text))
                    return true;
            }
            if(FocusedGrid == FocusedGrid.None)
                return false;
            IDataObject dataObj = GetClipboardDataObject();
            string format = typeof(ArrayList).FullName;
            if(dataObj != null && dataObj.GetDataPresent(format))
                return true;
            return false;
        }
        bool CanCopyCommands() {
            if(IsFocusedTextEdit()) {
                return (textEdit.SelectionLength != 0);
            } else if(FocusedGrid != FocusedGrid.None) {
                return (GridDictionary[FocusedGrid].ClipboardCopyMode != ClipboardCopyMode.None ? true : false);
            }
            return false;
        }
        IDataObject GetClipboardDataObject() {
            try {
                return Clipboard.GetDataObject();
            } catch {
                return null;
            }
        }
        void CopyCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
            CommandExecute(CopyingRows);
        }
        void CutCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
            CommandExecute(CutRows);
        }
        void PasteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
            CommandExecute(PasteRows);
        }
        void DeleteCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e) {
            CommandExecute(DeleteRows);
        }
        void CopyCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = CanExecuteOutputCommands();
            if(e.CanExecute)
                e.CanExecute = CanCopyCommands();
            e.Handled = true;
        }
        void CutCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = CanExecuteOutputCommands();
            if(e.CanExecute)
                e.CanExecute = CanCopyCommands();
            e.Handled = true;
        }
        void PasteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = CanExecuteInputCommands();
            e.Handled = true;
        }
        void DeleteCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e) {
            e.CanExecute = CanExecuteOutputCommands();
            e.Handled = true;
        }

        void Grid_CustomUnboundColumnData(object sender, GridColumnDataEventArgs e) {
            if(e.Column != null && e.Column.FieldName == "AnimationElement") {
                int index = GetIndexForAnimationElements(e.ListSourceRowIndex, e.Source.Equals(secondGrid));
                e.Value = GetAnimationElementCore(index);
            }
        }
        int GetIndexForAnimationElements(int index) {
            return GetIndexForAnimationElements(index, animationTarget == FocusedGrid.Second);
        }
        int GetIndexForAnimationElements(int index, bool isSecondGrid) {
            return isSecondGrid ? secondList[index].UniqueID : firstList[index].UniqueID;
        }
        bool animation = false;
        RowsAnimationElement GetAnimationElement(int index) {
            return GetAnimationElementCore(GetIndexForAnimationElements(index));
        }
        RowsAnimationElement GetAnimationElementCore(int index) {
            RowsAnimationElement result = null;
            if(!animationElements.TryGetValue(index, out result)) {
                result = new RowsAnimationElement();
                animationElements.Add(index, result);
                if(animation)
                    result.NewRowsProgress = 0;
            }
            return result;
        }
        void RemoveAnimationElement(int index) {
            index = GetIndexForAnimationElements(index);
            if(animationElements.ContainsKey(index))
                animationElements.Remove(index);
        }
        void allowCopyingtoClipboardCheckEdit_Checked(object sender, RoutedEventArgs e) {
            SetPropertyToGrids(ClipboardCopyMode.Default);
        }
        void allowCopyingtoClipboardCheckEdit_Unchecked(object sender, RoutedEventArgs e) {
            SetPropertyToGrids(ClipboardCopyMode.None);
        }
        void copyHeaderCheckEdit_Checked(object sender, RoutedEventArgs e) {
            SetPropertyToGrids(ClipboardCopyMode.IncludeHeader);
        }
        void copyHeaderCheckEdit_Unchecked(object sender, RoutedEventArgs e) {
            SetPropertyToGrids(ClipboardCopyMode.ExcludeHeader);
        }
        void SetPropertyToGrids(ClipboardCopyMode copyMode) {
            firstGrid.ClipboardCopyMode = copyMode;
            secondGrid.ClipboardCopyMode = copyMode;
        }
    }
}
