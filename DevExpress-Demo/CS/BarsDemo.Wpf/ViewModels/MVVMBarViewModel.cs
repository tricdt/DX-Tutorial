using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core.Native;

namespace BarsDemo {
    public class MVVMBarViewModel {
        public IMessageBoxService MessageBoxService { get { return this.GetService<IMessageBoxService>(); } }
        public MVVMBarViewModel() {
            Bars = new ObservableCollection<BarViewModel>();
            SelectedText = string.Empty;
            Text = string.Empty;
            InitializeClipboardBar();
            InitializeAdditionBar();
        }
        void InitializeAdditionBar() {
            BarViewModel addingBar = ViewModelSource.Create(() => new BarViewModel() { Name = "Addition" });
            Bars.Add(addingBar);
            var addGroupCommand = ViewModelSource.Create(() => new GroupBarButtonInfo() { Caption = "Add", SmallGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/AddFile.svg"), LargeGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/AddFile.svg") });
            var parentCommand = ViewModelSource.Create(() => new ParentBarButtonInfo(this, MyParentCommandType.CommandCreation) { Caption = "Add Command", SmallGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/AddFile.svg"), LargeGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/AddFile.svg") });
            var parentBar = ViewModelSource.Create(() => new ParentBarButtonInfo(this, MyParentCommandType.BarCreation) { Caption = "Add Bar", SmallGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/AddFile.svg"), LargeGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/AddFile.svg") });
            addGroupCommand.Commands.Add(parentCommand);
            addGroupCommand.Commands.Add(parentBar);
            addingBar.Commands.Add(addGroupCommand);
            addingBar.Commands.Add(parentCommand);
            addingBar.Commands.Add(parentBar);
        }
        void InitializeClipboardBar() {
            BarViewModel clipboardBar = ViewModelSource.Create(() => new BarViewModel() { Name = "Clipboard" });
            Bars.Add(clipboardBar);
            var cutCommand = ViewModelSource.Create(() => new BarButtonInfo(cutCommandExecuteFunc) { Caption = "Cut", SmallGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/Cut.svg"), LargeGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/Cut.svg") });
            var copyCommand = ViewModelSource.Create(() => new BarButtonInfo(copyCommandExecuteFunc) { Caption = "Copy", SmallGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/Copy.svg"), LargeGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/Copy.svg") });
            var pasteCommand = ViewModelSource.Create(() => new BarButtonInfo(pasteCommandExecuteFunc) { Caption = "Paste", SmallGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/Paste.svg"), LargeGlyph = DXImageHelper.GetDXImage("SvgImages/Outlook Inspired/Paste.svg") });
            var selectCommand = ViewModelSource.Create(() => new BarButtonInfo(selectAllCommandExecuteFunc) { Caption = "Select All", SmallGlyph = DXImageHelper.GetDXImage("SvgImages/PDF Viewer/SelectAll.svg"), LargeGlyph = DXImageHelper.GetDXImage("SvgImages/PDF Viewer/SelectAll.svg") });
            var blankCommand = ViewModelSource.Create(() => new BarButtonInfo(blankCommandExecuteFunc) { Caption = "Clear Page", SmallGlyph = DXImageHelper.GetDXImage("SvgImages/Icon Builder/Actions_Clear.svg"), LargeGlyph = DXImageHelper.GetDXImage("SvgImages/Icon Builder/Actions_Clear.svg") });
            clipboardBar.Commands.Add(cutCommand);
            clipboardBar.Commands.Add(copyCommand);
            clipboardBar.Commands.Add(pasteCommand);
            clipboardBar.Commands.Add(selectCommand);
            clipboardBar.Commands.Add(blankCommand);
        }
        public virtual ObservableCollection<BarViewModel> Bars { get; set; }
        public virtual string Text { get; set; }
        public virtual string SelectedText { get; set; }
        public virtual int SelectionStart { get; set; }
        public virtual int SelectionLength { get; set; }
        #region CommandFuncs
        public void cutCommandExecuteFunc() {
            OnCopyExecute();
            SelectedText = String.Empty;
        }
        public void copyCommandExecuteFunc() {
            OnCopyExecute();
        }

        public void pasteCommandExecuteFunc() {
            SelectedText = DXClipboard.GetText();
            SelectionStart += SelectionLength;
            SelectionLength = 0;
        }
        public void selectAllCommandExecuteFunc() {
            SelectionStart = 0;
            SelectionLength = string.IsNullOrEmpty(Text) ?  0 : Text.Count();
        }
        public void blankCommandExecuteFunc() {
            Text = string.Empty;
        }
        #endregion

        void OnCopyExecute() {
            DXClipboard.SetText(SelectedText);
        }
    }
    [POCOViewModel]
    public class BarViewModel {
        public BarViewModel() {
            Name = "";
            Commands = new ObservableCollection<BarButtonInfo>();
        }
        public virtual string Name { get; set; }
        public virtual ObservableCollection<BarButtonInfo> Commands { get; set; }
    }
}
