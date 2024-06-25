using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using DevExpress.Utils;
using System.Linq;
using System.Windows.Data;
using System.Collections.Generic;
using DevExpress.Mvvm;
using Microsoft.Win32;
using System.IO;
using DevExpress.Xpf.Core.Native;

namespace RibbonDemo {
    public class DemoTextBox : TextBox {
        const string filterString = "TXT Files (*.TXT)|*.txt";

        #region static
        public static readonly DependencyProperty CaretPosProperty =
            DependencyProperty.Register("CaretPos", typeof(Point), typeof(DemoTextBox), new PropertyMetadata(new Point()));
        #endregion

        #region dep props
        public Point CaretPos {
            get { return (Point)GetValue(CaretPosProperty); }
            set { SetValue(CaretPosProperty, value); }
        }
        #endregion

        #region commands
        public ICommand CutCommand { get; set; }
        public ICommand CopyCommand { get; set; }
        public ICommand PasteCommand { get; set; }
        public ICommand SelectAllCommand { get; set; }
        public ICommand ClearCommand { get; set; }
        public ICommand UndoCommand { get; set; }
        public ICommand RedoCommand { get; set; }
        public ICommand OpenFromFileCommand { get; set; }
        public ICommand SaveToFileCommand { get; set; }
        #endregion

        #region commands implementation
        private void OnCutCommandExecute() {
            DXClipboard.SetText(SelectedText); SelectedText = String.Empty;
        }
        private void OnCopyCommandExecute() {
            DXClipboard.SetText(SelectedText);
        }
        private void OnPasteCommandExecute() {
            SelectedText = DXClipboard.GetText(); SelectionStart += SelectionLength; SelectionLength = 0;
        }
        private void OnSelectAllCommandExecute() {
            SelectionStart = 0; SelectionLength = Text.Count();
        }
        private void OnClearCommandExecute() {
            SelectionStart = 0;
            SelectionLength = Text.Count();
            SelectedText = String.Empty;
        }
        private bool UndoCommandCanExecute() {
            return UndoStack.Count != 0;
        }
        
        private void OnUndoCommandExecute() {
            if(UndoStack.Count != 0) {                
                RedoStack.Push(UndoStack.Pop());
                disableUndoRedoLogic = true;
                Text = UndoStack.Count == 0 ? string.Empty : UndoStack.Peek();                
                ((DelegateCommand)UndoCommand).RaiseCanExecuteChanged();
                ((DelegateCommand)RedoCommand).RaiseCanExecuteChanged();
            }            
        }
        private bool RedoCommandCanExecute() {
            return RedoStack.Count != 0;
        }
        private void OnRedoCommandExecute() {
            disableUndoRedoLogic = true;
            Text = RedoStack.Pop();            
            UndoStack.Push(Text);
            ((DelegateCommand)UndoCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)RedoCommand).RaiseCanExecuteChanged();
        }
        private void OnOpenFromFileCommandExecute() {
            OpenFileDialog dialog = new OpenFileDialog() { Filter = filterString, Title = "Open file..." };
            if(dialog.ShowDialog().Value == true) {
                Text = new StreamReader(dialog.FileName).ReadToEnd();
                UndoStack.Clear();
                RedoStack.Clear();
            }
        }
        private void OnSaveToFileCommandExecute() {
            SaveFileDialog dlg = new SaveFileDialog() { Filter = filterString, Title = "Save file..." };
            if(dlg.ShowDialog() == true) {
                using(Stream stream = dlg.OpenFile()) {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.Write(Text);
                    writer.Close();                    
                }
            }
        }
        #endregion
        public DemoTextBox() {
            CutCommand = new DelegateCommand(OnCutCommandExecute);
            CopyCommand = new DelegateCommand(OnCopyCommandExecute);
            PasteCommand = new DelegateCommand(OnPasteCommandExecute);
            SelectAllCommand = new DelegateCommand(OnSelectAllCommandExecute);
            ClearCommand = new DelegateCommand(OnClearCommandExecute);
            UndoCommand = new DelegateCommand(OnUndoCommandExecute, UndoCommandCanExecute);
            RedoCommand = new DelegateCommand(OnRedoCommandExecute, RedoCommandCanExecute);
            OpenFromFileCommand = new DelegateCommand(OnOpenFromFileCommandExecute);
            SaveToFileCommand = new DelegateCommand(OnSaveToFileCommandExecute);

            UndoStack = new Stack<string>();
            RedoStack = new Stack<string>();
            
            AcceptsReturn = true;
            TextWrapping = System.Windows.TextWrapping.Wrap;
            this.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            AcceptsTab = true;
            SelectionChanged += new RoutedEventHandler(OnSelectionChanged);
            TextChanged += new TextChangedEventHandler(OnTextChanged);
        }
        private Stack<string> UndoStack { get; set; }
        private Stack<string> RedoStack { get; set; }
        bool disableUndoRedoLogic;
        public void Close() {
            
            UndoStack.Clear();
            RedoStack.Clear();
        }

        void OnTextChanged(object sender, TextChangedEventArgs e) {
            if(disableUndoRedoLogic) {
                disableUndoRedoLogic = false;
                return;            
            }
            RedoStack.Clear();            
            UndoStack.Push(Text);
            ((DelegateCommand)RedoCommand).RaiseCanExecuteChanged();
            ((DelegateCommand)UndoCommand).RaiseCanExecuteChanged();
        }
        void OnSelectionChanged(object sender, RoutedEventArgs e) {
            UpdateCaretPositionInfo();
        }
        int selPos = 0;
        int index = 0;
        int lastLineBreakPos = 0;
        void UpdateCaretPositionInfo() {
            selPos = this.SelectionStart;
            index = 0;
            lastLineBreakPos = -1;
            int lineBreaksCount = Text.Count(updateCaretPositionPredicate);
            CaretPos = new Point(CaretPos.Y, lineBreaksCount + 1);
            CaretPos = new Point(selPos - lastLineBreakPos, CaretPos.Y);
        }
        bool updateCaretPositionPredicate(char ch) {
            index++; if(ch == '\r' && index <= selPos) { lastLineBreakPos = index; return true; } return false;
        }
    }

}
