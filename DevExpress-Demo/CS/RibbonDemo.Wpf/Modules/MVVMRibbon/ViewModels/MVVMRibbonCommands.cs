using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace RibbonDemo {
    [POCOViewModel]
    public class PageGroupModel : ICommand {
        public PageGroupModel() {
            Commands = new ObservableCollection<CommandModel>();
        }
        public virtual string Name { get; set; }
        public ObservableCollection<CommandModel> Commands { get; set; }
        public virtual ImageSource Glyph { get; set; }
        protected internal void OnGlyphChanged() {
            Glyph.Freeze();
        }
        public void Clear() {
            Commands.Clear();
        }
        #region ICommand
        bool b = false;
        event EventHandler _canExecuteChanged;
        bool ICommand.CanExecute(object parameter) {
            if(b == true) _canExecuteChanged.Invoke(this, new EventArgs());
            return true;
        }
        event EventHandler ICommand.CanExecuteChanged { add { _canExecuteChanged += value; } remove { _canExecuteChanged -= value; } }
        void ICommand.Execute(object parameter) {
            MessageBox.Show(Name + "'s command executed");
        }
        #endregion
    }
    [POCOViewModel]
    public class CommandModel : ICommand {
        Action action;

        public CommandModel() : this(null) { }
        public CommandModel(Action action) {
            this.action = action != null ? action : new Action(ShowMessageBox);
            Caption = "";
        }
        public virtual string Caption { get; set; }
        public virtual ImageSource LargeGlyph { get; set; }
        public virtual ImageSource SmallGlyph { get; set; }

        public void ShowMessageBox() {
            MessageBox.Show(String.Format("Command \"{0}\" executed", this.Caption));
        }
        protected internal void OnSmallGlyphChanged() {
            OnGlyphChanged(SmallGlyph);
        }
        protected internal void OnLargeGlyphChanged() {
            OnGlyphChanged(LargeGlyph);
        }
        protected internal void OnGlyphChanged(ImageSource e) {
            e.Freeze();
        }
        #region ICommand
        event EventHandler ICommand.CanExecuteChanged {
            add { _canExecuteChanged += value; }
            remove { _canExecuteChanged -= value; }
        }
        #endregion
        bool b = false;
        public virtual bool CanExecute(object parameter) {
            if(b == true) _canExecuteChanged.Invoke(this, new EventArgs());
            return true;
        }
        event EventHandler _canExecuteChanged;
        public virtual void Execute(object parameter) {
            action();
        }
    }

    public enum MyParentCommandType { CommandCreation, GroupCreation, PageCreation };
    [POCOViewModel]
    public class MyParentCommand : CommandModel {
        MVVMRibbonViewModel viewModel;
        MyParentCommandType type;
        public MyParentCommand(MVVMRibbonViewModel viewModel, MyParentCommandType type) {
            this.viewModel = viewModel;
            this.type = type;
        }
        public override void Execute(object parameter) {
            switch(type) {
                case MyParentCommandType.CommandCreation:
                    CommandCreation();
                    break;
                case MyParentCommandType.GroupCreation:
                    PageGroupCreation();
                    break;
                case MyParentCommandType.PageCreation:
                    PageCreation();
                    break;
            }
            viewModel.RibbonMergeingService.Remerge();
        }
        private void PageCreation() {
            viewModel.Categories[0].Pages.Add(ViewModelSource.Create(() => new PageModel() { Name = "New Page" + IndexCreator.GetIndex() }));            
        }
        private void PageGroupCreation() {
            viewModel.Categories[0].Pages[0].Groups.Add(ViewModelSource.Create(() => new PageGroupModel() { Name = "New Group", Glyph = GlyphHelper.GetGlyph("SvgImages/RichEdit/InsertImage.svg") }));
        }
        private void CommandCreation() {
            CommandModel newCommand = ViewModelSource.Create(() => new CommandModel() { Caption = "New Command", LargeGlyph = GlyphHelper.GetGlyph("SvgImages/RichEdit/InsertImage.svg"), SmallGlyph = GlyphHelper.GetGlyph("SvgImages/RichEdit/InsertImage.svg") });
            viewModel.Categories[0].Pages[0].Groups[0].Commands.Add(newCommand);
        }
    }

    public class MyGroupCommand : CommandModel {
        public ObservableCollection<CommandModel> Commands { get; set; }
        public MyGroupCommand()
            : base(emptyFunc) {
            Commands = new ObservableCollection<CommandModel>();
        }
        public static void emptyFunc() { }
    }

    public static class IndexCreator {
        static int Value = 0;
        public static string GetIndex() {
            Value++;
            return Value.ToString();
        }
        public static void Refresh() {
            Value = 0;
        }
    }
}
