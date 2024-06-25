using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using DevExpress.Mvvm;

namespace BarsDemo {
    [POCOViewModel]
    public class BarButtonInfo {
        Action action;
        public virtual string Caption { get; set; }
        public virtual ImageSource LargeGlyph { get; set; }
        public virtual ImageSource SmallGlyph { get; set; }
        [Command]
        public void ExecuteAction() {
            if(action != null) {
                action();
            }
        }
        public BarButtonInfo(Action action) {
            Caption = "";
            this.action = action;
        }
    }

    public enum MyParentCommandType { CommandCreation, BarCreation };

    public class ParentBarButtonInfo : BarButtonInfo {
        public ParentBarButtonInfo(MVVMBarViewModel viewModel, MyParentCommandType type)
            : base(() => Execute(type, viewModel)) { }

        static void Execute(MyParentCommandType type, MVVMBarViewModel viewModel) {
            switch(type) {
                case MyParentCommandType.CommandCreation:
                    viewModel.Bars[0].Commands.Add(CreateNewBarButtonInfo(viewModel));
                    break;
                case MyParentCommandType.BarCreation:
                    BarViewModel model = ViewModelSource.Create(() => new BarViewModel() { Name = "New Bar" });
                    model.Commands.Add(CreateNewBarButtonInfo(viewModel));
                    viewModel.Bars.Add(model);
                    break;
            }
        }
        static BarButtonInfo CreateNewBarButtonInfo(MVVMBarViewModel viewModel) {
            string caption = "New Command";
            Action action = () => viewModel.MessageBoxService.ShowMessage(String.Format("Command \"{0}\" executed", caption));
            return ViewModelSource.Create(() => new BarButtonInfo(action) {
                Caption = caption,
                SmallGlyph = DXImageHelper.GetDXImage("SvgImages/RichEdit/InsertImage.svg"),
                LargeGlyph = DXImageHelper.GetDXImage("SvgImages/RichEdit/InsertImage.svg")
            });
        }
    }
    public class GroupBarButtonInfo : BarButtonInfo {
        public GroupBarButtonInfo() : base(null) {
            Commands = new ObservableCollection<BarButtonInfo>();
        }
        public virtual ObservableCollection<BarButtonInfo> Commands { get; set; }
    }
}
