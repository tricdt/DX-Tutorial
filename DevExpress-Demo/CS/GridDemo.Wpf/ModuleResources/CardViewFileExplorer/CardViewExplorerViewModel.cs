using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Controls;
using DevExpress.Xpf.Core;
using GridDemo.ModuleResources;

namespace GridDemo {
    [POCOViewModel]
    public class CardViewExplorerViewModel : IChildSelector {
        #region Properties
        public string Root { get; private set; }

        Stack<string> ForwardStack = new Stack<string>();
        Stack<string> BackStack = new Stack<string>();

        public virtual SizeIcon SizeType { get; set; }
        protected void OnSizeTypeChanged() {
            Resize();
            this.RaisePropertyChanged(x => x.Size);
        }

        public int Size
        {
            get
            {
                switch(SizeType) {
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

        string _path;
        public string Path
        {
            get { return _path; }
            set
            {
                if(value == null) return;
                if(value != Root && !value.EndsWith("\\"))
                    value += "\\";
                if(value != Root && !Directory.Exists(value)) {
                    RaisePathChanged();
                    return;
                }
                if(_path != null)
                    ForwardStack.Push(_path);
                _path = value;
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
            _path = tmp;
            RaisePathChanged();
            OpenFolder(tmp, false);
        }
        public bool CanBack() {
            return ForwardStack.Count > 0;
        }

        public void Forward() {
            string tmp = BackStack.Pop();
            ForwardStack.Push(_path);
            _path = tmp;
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
            string path = Path.TrimEnd('\\');
            if(path.Length != 2)
                Path = Directory.GetParent(path).FullName;
            else
                Path = Root;
        }
        public bool CanUp() {
            return Path != Root;
        }
        #endregion

        #region Members
        protected CardViewExplorerViewModel() {
            Root = "Root";
            Source = new ObservableCollectionCore<CardViewExplorerFileSource>();
            SizeType = SizeIcon.Medium;
            OpenRoot();
        }

        void OpenFolder(string path, bool clearNextStack = true) {
            Source.Clear();
            Source.BeginUpdate();
            try {
                IsLoading = true;
                if(path == Root)
                    OpenRoot();
                else {
                    SizeIcon sizeType = SizeType;
                    int size = Size;
                    var info = new DirectoryInfo(path);
                    if(info.Exists) {
                        foreach(var item in info.EnumerateDirectories().Where(x => (x.Attributes & (FileAttributes.Hidden | FileAttributes.System)) == 0))
                            Source.Add(CardViewExplorerFileSource.Create(item.FullName, CardViewExplorerFileSource.TypeElement.Folder, sizeType, size));
                        foreach(var item in info.EnumerateFiles())
                            Source.Add(CardViewExplorerFileSource.Create(item.FullName, CardViewExplorerFileSource.TypeElement.File, sizeType, size));
                    }
                }
                if(clearNextStack)
                    BackStack.Clear();
            } catch(UnauthorizedAccessException ex) {
                System.Windows.MessageBox.Show(ex.Message, "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                Back();
            } finally {
                IsLoading = false;
                Source.EndUpdate();
            }
        }

        void Resize() {
            try {
                IsLoading = true;
                CardViewExplorerFileSource.ClearCache();
                foreach(CardViewExplorerFileSource item in Source)
                    item.Resize(SizeType, Size);
            } finally {
                IsLoading = false;
            }
        }

        void OpenRoot() {
            Source.Clear();
            foreach(var drive in DriveInfo.GetDrives().Where(x => x.DriveType == DriveType.Fixed))
                Source.Add(CardViewExplorerFileSource.Create(drive.RootDirectory.Name, CardViewExplorerFileSource.TypeElement.Drive, SizeType, Size));
            _path = Root;
            RaisePathChanged();
        }

        void RaisePathChanged() {
            this.RaisePropertyChanged(x => x.Path);
        }
        #endregion

        #region IChildSelector
        IEnumerable IChildSelector.SelectChildren(object item) {
            var info = item as DirectoryInfo;
            if(info == null || !info.Exists) return null;
            try {
                var dirs = info.EnumerateDirectories().Where(x => (x.Attributes & (FileAttributes.Hidden | FileAttributes.System)) == 0);
                return dirs;
            } catch {
                return Enumerable.Empty<FileSystemInfo>();
            }
        }
        public void CustomDisplayText(BreadcrumbCustomDisplayTextEventArgs arg) {
            var info = arg.Item as DirectoryInfo;
            if(info == null) return;
            arg.DisplayText = info.Name.TrimEnd('\\');
        }
        public void CustomImage(BreadcrumbCustomImageEventArgs arg) {
            var info = arg.Item as DirectoryInfo;
            if(info == null) return;
            arg.Image = CardViewExplorerIconManager.GetSmallIcon(info.FullName);
        }
        public void QueryPath(BreadcrumbQueryPathEventArgs arg) {
            if(arg.Path.Count() == 0) return;
            string argPath = arg.Path.Count() == 1 ? arg.Path.FirstOrDefault() + arg.PathSeparator : string.Join(arg.PathSeparator, arg.Path);
            var infoList = new List<DirectoryInfo>();
            var info = new DirectoryInfo(argPath);
            if(!info.Exists) {
                arg.Breadcrumbs = infoList;
                return;
            }
            do {
                infoList.Insert(0, info);
                info = info.Parent;
            } while(info != null && info.Exists);
            arg.Breadcrumbs = infoList;
        }
        #endregion
    }

    public enum SizeIcon {
        [Display(Name = "Extra large icons")]
        ExtraLarge,
        [Display(Name = "Large icons")]
        Large,
        [Display(Name = "Medium icons")]
        Medium
    }
}
