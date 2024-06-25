using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Grid;
using System.IO;
using System.Collections;
using DevExpress.Data;
using System.Collections.Concurrent;
using DevExpress.Mvvm;
using DevExpress.Data.TreeList;
using TreeListDemo.Data;

namespace TreeListDemo {
    public partial class NodeChecking : TreeListDemoModule {
        public NodeChecking() {
            InitializeComponent();
            SizeUpdater.Instance.WindowDispatcher = this.Dispatcher;
            Loaded += (sender, e) => { if(view.Nodes.Count > 0) view.Nodes[0].IsExpanded = true; };
            Unloaded += (sender, e) => SizeUpdater.Instance.ClearTasks();
        }
        void view_NodeChanged(object sender, DevExpress.Xpf.Grid.TreeList.TreeListNodeChangedEventArgs e) {
            if(e.ChangeType == NodeChangeType.Add) {
                FileSystemItemModelBase item = e.Node.Content as FileSystemItemModelBase;
                if(item.ItemType == "File")
                    e.Node.IsExpandButtonVisible = DevExpress.Utils.DefaultBoolean.False;
            }
        }
    }

    public class FileSystem : BindableBase, IChildNodesSelector {
        FileSystemItemSize totalSizeCore;
        public FileSystemItemSize TotalSize {
            get { return totalSizeCore; }
            set { SetProperty(ref totalSizeCore, value, () => TotalSize); }
        }
        public FileSystem() {
            Source = new List<FileSystemItemModelBase>();
            InitializeSource(Source);
            TotalSize = new FileSystemItemSize(0);
            SizeUpdater.Instance.TotalSize.SizeChanged += new EventHandler<ItemSizeChangedEventArgs>(TotalSize_SizeChanged);
        }

        void TotalSize_SizeChanged(object sender, ItemSizeChangedEventArgs e) {
            TotalSize = new FileSystemItemSize(e.Size.NumSize);
        }
        public List<FileSystemItemModelBase> Source { get; private set; }
        protected virtual void InitializeSource(IList<FileSystemItemModelBase> source) {
            string[] driveNames = DataHelper.Instance.GetLogicalDrives();
            foreach(string driveName in driveNames)
                source.Add(new LogicalDriveSystemItemModel(driveName));
        }
        #region IChildNodesSelector Members
        IEnumerable IChildNodesSelector.SelectChildren(object item) {
            IChildNodesSelector fileSystemItem = item as IChildNodesSelector;
            if(fileSystemItem != null)
                return fileSystemItem.SelectChildren(item);
            return null;
        }
        #endregion
    }

    public abstract class FileSystemItemModelBase : BindableBase {
        bool? checkedCore;
        FileSystemItemSize sizeCore;
        bool affectsTotalSizeCore;

        public FileSystemItemModelBase(string name, string type, FileSystemItemSize size, string fullName, bool? check = false) {
            this.Name = name;
            this.ItemType = type;
            this.FullName = fullName;
            this.Size = size;
            Size.SizeChanged += new EventHandler<ItemSizeChangedEventArgs>(SizeChanged);
            Checked = check;
            UpdateAffectsTotalSize();
        }
        void SizeChanged(object sender, ItemSizeChangedEventArgs e) {
            RaisePropertyChanged("Size");
            if(Size.DisplaySize != FileSystemItemSize.Calculating)
                UpdateAffectsTotalSize();
        }

        public string Name { get; set; }
        public string ItemType { get; set; }
        public FileSystemItemSize Size {
            get { return sizeCore; }
            private set {
                if(SetProperty(ref sizeCore, value, "Size"))
                    UpdateAffectsTotalSize();
            }
        }
        public string FullName { get; set; }
        public bool? Checked {
            get { return checkedCore; }
            set {
                if(SetProperty(ref checkedCore, value, "Checked")) {
                    SizeUpdater.Instance.AddTask(this);
                    UpdateAffectsTotalSize();
                }
            }
        }
        public bool AffectsTotalSize {
            get { return affectsTotalSizeCore; }
            private set {
                if(SetProperty(ref affectsTotalSizeCore, value, "AffectsTotalSize")) {
                    if(value)
                        SizeUpdater.Instance.IncreaseTotalSize(this.Size.NumSize);
                    else
                        SizeUpdater.Instance.DecreaseTotalSize(this.Size.NumSize);
                }
            }
        }
        internal void UpdateAffectsTotalSize() {
            bool hasChildren = HasChildren();
            UpdateAffectsTotalSize(hasChildren);
        }
        internal void UpdateAffectsTotalSize(bool hasChildren) {
            AffectsTotalSize = Checked.HasValue && Checked.Value && Size.IsCalculated() && !hasChildren;
        }
        public abstract bool HasChildren();
    }

    public class FileSystemItemModel : FileSystemItemModelBase, IChildNodesSelector {
        public FileSystemItemModel(string name, string type, FileSystemItemSize size, string fullName, bool? check)
            : base(name, type, size, fullName, check) {
        }
        public override bool HasChildren() {
            return false;
        }
        IEnumerable IChildNodesSelector.SelectChildren(object item) {
            return null;
        }
    }

    public class FolderSystemItemModel : FileSystemItemModelBase, IChildNodesSelector {
        public FolderSystemItemModel(string name, string type, FileSystemItemSize size, string fullName, bool? check)
            : base(name, type, size, fullName, check) {
            Source = new List<FileSystemItemModelBase>();
        }
        public List<FileSystemItemModelBase> Source { get; private set; }
        public override bool HasChildren() {
            return Source == null ? false : Source.Count != 0;
        }
        IEnumerable IChildNodesSelector.SelectChildren(object item) {
            InitFolders(Source);
            InitFiles(Source);
            UpdateAffectsTotalSize(Source.Count != 0);
            return Source;
        }
        private void InitFolders(IList<FileSystemItemModelBase> items) {
            try {
                string[] directoryNames = DataHelper.Instance.GetDirectories(FullName);
                foreach(string directoryName in directoryNames) {
                    try {
                        items.Add(new FolderSystemItemModel(GetDirectoryName(directoryName), "Folder", new FileSystemItemSize(FileSystemItemSize.Folder), directoryName, Checked));
                    } catch { }
                }
            } catch { }
        }
        private void InitFiles(IList<FileSystemItemModelBase> items) {
            try {
                string[] fileNames = DataHelper.Instance.GetFiles(FullName);
                foreach(string fileName in fileNames) {
                    items.Add(new FileSystemItemModel(GetFileName(fileName), "File", GetFileSize(fileName), fileName, Checked));
                }
            } catch { }
        }
        protected string GetDirectoryName(string path) {
            return DataHelper.Instance.GetDirectoryName(path);
        }
        protected string GetFileName(string path) {
            return DataHelper.Instance.GetFileName(path);
        }
        public FileSystemItemSize GetFileSize(string path) {
            long size = DataHelper.Instance.GetFileNumSize(path);
            return new FileSystemItemSize(size);
        }
    }

    public class LogicalDriveSystemItemModel : FolderSystemItemModel {
        public LogicalDriveSystemItemModel(string driveName) : base(driveName, "Drive", new FileSystemItemSize(FileSystemItemSize.Drive), driveName, false) { }
    }

    public sealed class SizeUpdater {
        static readonly SizeUpdater instanceCore = new SizeUpdater();
        SizeUpdater() { TotalSize = new FileSystemItemSize(0); }
        public static SizeUpdater Instance {
            get { return instanceCore; }
        }

        public FileSystemItemSize TotalSize { get; private set; }
        public void IncreaseTotalSize(long itemSize) {
            TotalSize.Change(TotalSize.NumSize + itemSize);
        }
        public void DecreaseTotalSize(long itemSize) {
            TotalSize.Change(TotalSize.NumSize - itemSize);
        }

        public Dispatcher WindowDispatcher { get; set; }
        SizeCalculatingQueue calcQueue = new SizeCalculatingQueue();
        internal void AddTask(FileSystemItemModelBase item) {
            if(item.Checked != true) return;
            calcQueue.ProcessTask(item);
        }
        public void ClearTasks() {
            calcQueue.ClearTasks();
            TotalSize = new FileSystemItemSize(0);
        }
        internal void RecursiveCalculator(FileSystemItemModelBase item, CancellationToken cancellationToken) {
            RecursiveCalculatorHelper(item, cancellationToken);
        }
        long RecursiveCalculatorHelper(FileSystemItemModelBase item, CancellationToken cancellationToken) {
            long resSize = 0;
            DispatcherOperation op;
            Action<FileSystemItemModelBase> sizeCalculatingAction = delegate(FileSystemItemModelBase i) {
                i.Size.Change(FileSystemItemSize.Calculating);
            };
            Action<FileSystemItemModelBase> sizeCalculatedAction = delegate(FileSystemItemModelBase i) {
                i.Size.Change(resSize);
            };
            if(item.ItemType == "File") return item.Size.NumSize;
            FolderSystemItemModel folderItem = item as FolderSystemItemModel;
            if(folderItem == null) return 0;
            if(item.Size.IsCalculated())
                return item.Size.NumSize;
            op = WindowDispatcher.BeginInvoke(DispatcherPriority.Normal, sizeCalculatingAction, item);
            if(item.HasChildren()) {
                foreach(FileSystemItemModelBase child in folderItem.Source) {
                    if(cancellationToken.IsCancellationRequested) break;
                    resSize += RecursiveCalculatorHelper(child, cancellationToken);
                }
            } else {
                resSize = DataHelper.Instance.GetFolderSize(item.FullName, cancellationToken);
            }
            op = WindowDispatcher.BeginInvoke(DispatcherPriority.Normal, sizeCalculatedAction, item);
            return resSize;
        }
    }

    public class SizeCalculatingQueue {
        EventWaitHandle waitHandle;
        Thread calculator;
        object locker;
        CancellationTokenSource cancellationTokenSource;
        Queue<FileSystemItemModelBase> items;

        public SizeCalculatingQueue() {
            waitHandle = new AutoResetEvent(false);
            locker = new object();
            items = new Queue<FileSystemItemModelBase>();
        }
        public void ProcessTask(FileSystemItemModelBase item) {
            lock(locker) items.Enqueue(item);
            if(calculator == null || !calculator.IsAlive)
                CreateCalcThread();
            waitHandle.Set();
        }
        public void ClearTasks() {
            lock(locker) items.Clear();
            if(cancellationTokenSource != null && calculator != null && calculator.IsAlive)
                cancellationTokenSource.Cancel();
            waitHandle.Set();
        }
        void CreateCalcThread() {
            cancellationTokenSource = new CancellationTokenSource();
            calculator = new Thread(() => {
                while(true) {
                    if(cancellationTokenSource.Token.IsCancellationRequested) break;
                    FileSystemItemModelBase item = GetItemFromQueue();
                    if(item != null)
                        SizeUpdater.Instance.RecursiveCalculator(item, cancellationTokenSource.Token);
                    else
                        waitHandle.WaitOne();
                }
                cancellationTokenSource.Dispose();
            });
            calculator.IsBackground = true;
            calculator.Start();
        }
        FileSystemItemModelBase GetItemFromQueue() {
            FileSystemItemModelBase item = null;
            lock(locker) {
                if(items.Count != 0) item = items.Dequeue();
            }
            return item;
        }
    }



    public class WaitIndicatorVisibilityConverter : IValueConverter {
        object IValueConverter.Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            if(value.ToString() != FileSystemItemSize.Calculating)
                return Visibility.Visible;
            return Visibility.Collapsed;
        }
        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

    public class FileSystemImageSelector : TreeListNodeImageSelector {
        public override ImageSource Select(DevExpress.Xpf.Grid.TreeList.TreeListRowData rowData) {
            FileSystemItemModelBase item = rowData.Row as FileSystemItemModelBase;
            if(item == null) return null;
            return GetImageByFileItemType(item.ItemType, rowData.Node.IsExpanded, item.HasChildren());
        }
        ImageSource GetImageByFileItemType(string type, bool isExpanded, bool hasChildren) {
            ImageSource image = null;
            switch(type) {
                case "File":
                    image = FileSystemImages.FileImage;
                    break;
                case "Folder":
                    image = isExpanded && hasChildren ? FileSystemImages.OpenedFolderImage : FileSystemImages.ClosedFolderImage;
                    break;
                case "Drive":
                    image = FileSystemImages.DiskImage;
                    break;
            }
            return image;
        }
    }

}
