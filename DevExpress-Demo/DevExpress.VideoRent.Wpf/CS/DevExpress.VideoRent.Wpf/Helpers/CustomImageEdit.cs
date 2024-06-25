using System;
using DevExpress.Xpf.Editors;
using System.Windows.Input;

namespace DevExpress.VideoRent.Wpf.Helpers {
    public class CustomImageEdit : ImageEdit {
        #region Commands
        abstract class ImageEditCommand : ICommand {
            public ImageEditCommand(ImageEdit imageEdit) {
                ImageEdit = imageEdit;
            }
            public event EventHandler CanExecuteChanged { add { } remove { } }
            public bool CanExecute(object parameter) { return true; }
            public abstract void Execute(object parameter);
            protected ImageEdit ImageEdit { get; private set; }
        }
        class ImageEditLoadCommand : ImageEditCommand {
            public ImageEditLoadCommand(ImageEdit imageEdit) : base(imageEdit) { }
            public override void Execute(object parameter) { ImageEdit.Load(); }
        }
        class ImageEditClearCommand : ImageEditCommand {
            public ImageEditClearCommand(ImageEdit imageEdit) : base(imageEdit) { }
            public override void Execute(object parameter) { ImageEdit.Clear(); }
        }
        ImageEditLoadCommand loadCommand;
        ImageEditClearCommand clearCommand;
        public ICommand LoadCommand {
            get {
                if(loadCommand == null)
                    loadCommand = new ImageEditLoadCommand(this);
                return loadCommand;
            }
        }
        public ICommand ClearCommand {
            get {
                if(clearCommand == null)
                    clearCommand = new ImageEditClearCommand(this);
                return clearCommand;
            }
        }
        #endregion
    }
}
