using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace GridDemo {
    [POCOViewModel]
    public class CardViewExplorerViewModel {
#region Properties

        const string Root = "Root";

        Stack<string> ForwardStack = new Stack<string>();
        Stack<string> BackStack = new Stack<string>();

        public virtual SizeIcon SizeType { get; set; }
        protected void OnSizeTypeChanged() {
            Resize();
            this.RaisePropertyChanged(x => x.Size);
        }

        public int Size {
            get {
                switch (SizeType) {
                    case SizeIcon.ExtraLarge:
                        return 256;
                    case SizeIcon.Large:
                        return 128;
                    case SizeIcon.Medium:
                        return 65;
                    default:
                        return 128;
                }
            }
        }

        string path;
        public string Path {
            get { return path; }
            set {
                if (value != Root && !System.IO.Directory.Exists(value)) {
                    RaisePathChanged();
                    return;
                }

                if (path != null)
                    ForwardStack.Push(path);
                path = value;
                OpenFolder(value);
                RaisePathChanged();
            }
        }

        public virtual bool IsLoading { get; set; }
        public virtual string SearchText { get; set; }
        public ObservableCollectionCore<CardViewExplorerFileSource> Source { get; private set; }
        public CardViewExplorerFileSource CurrentItem { get; set; }

#endregion

#region POCO commands

        public void Back() {
            BackStack.Push(Path);
            string tmp = ForwardStack.Pop();
            path = tmp;
            RaisePathChanged();
            OpenFolder(tmp, false);
        }
        public bool CanBack() {
            return ForwardStack.Count > 0;
        }

        public void Forward() {
            string tmp = BackStack.Pop();
            ForwardStack.Push(path);
            path = tmp;
            RaisePathChanged();
            OpenFolder(tmp, false);
        }
        public bool CanForward() {
            return BackStack.Count > 0;
        }

        public void Open() {
            CardViewExplorerFileSource element = CurrentItem;
            Path = element.FullPath;
        }
        public bool CanOpen() {
            return CurrentItem != null && CurrentItem.Type != CardViewExplorerFileSource.TypeElement.File;
        }

        public void Up() {
            if (Path.Length != 3)
                Path = System.IO.Directory.GetParent(Path).FullName;
            else
                Path = Root;
        }
        public bool CanUp() {
            return Path != Root;
        }

#endregion

#region Members

        public CardViewExplorerViewModel() {
            Source = new ObservableCollectionCore<CardViewExplorerFileSource>();
            SizeType = SizeIcon.Medium;
            OpenRoot();
        }

        void OpenFolder(string path, bool clearNextStack = true) {
            Source.Clear();
            Source.BeginUpdate();
            try {
                IsLoading = true;
                if (path == Root)
                    OpenRoot();
                else {
                    SizeIcon sizeType = SizeType;
                    int size = Size;
                    foreach (var item in System.IO.Directory.EnumerateDirectories(path))
                        Source.Add(CardViewExplorerFileSource.Create(System.IO.Path.Combine(path, item), CardViewExplorerFileSource.TypeElement.Folder, sizeType, size));
                    foreach (var item in System.IO.Directory.EnumerateFiles(path))
                        Source.Add(CardViewExplorerFileSource.Create(item, CardViewExplorerFileSource.TypeElement.File, sizeType, size));
                }

                if (clearNextStack)
                    BackStack.Clear();
            }
            catch (UnauthorizedAccessException ex) {
                System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                Back();
            }
            finally {
                IsLoading = false;
                Source.EndUpdate();
            }
        }

        void Resize() {
            try {
                IsLoading = true;
                CardViewExplorerFileSource.ClearCache();
                foreach (CardViewExplorerFileSource item in Source)
                    item.Resize(SizeType, Size);
            }
            finally {
                IsLoading = false;
            }
        }

        void OpenRoot() {
            Source.Clear();
            foreach (var drive in System.IO.DriveInfo.GetDrives().Where(x => x.DriveType == System.IO.DriveType.Fixed))
                Source.Add(CardViewExplorerFileSource.Create(drive.RootDirectory.Name, CardViewExplorerFileSource.TypeElement.Drive, SizeType, Size));
            path = Root;
            RaisePathChanged();
        }

        void RaisePathChanged() {
            this.RaisePropertyChanged(x => x.Path);
        }

#endregion
    }

    public enum SizeIcon {
        [Display(Name = "Extra large icons")] ExtraLarge,
        [Display(Name = "Large icons")] Large,
        [Display(Name = "Medium icons")] Medium,
        [Display(Name = "Small icons")] Small
    }
}
