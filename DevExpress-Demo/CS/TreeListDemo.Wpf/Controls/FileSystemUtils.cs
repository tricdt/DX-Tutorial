using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using TreeListDemo.Data;

namespace TreeListDemo {
    public sealed class DataHelper : FileSystemHelper
 {
        static readonly DataHelper instanceCore = new DataHelper();
        DataHelper() { }
        public static DataHelper Instance {
            get { return instanceCore; }
        }
        public long GetFileNumSize(string path) {
            return new FileInfo(path).Length;
        }
        public long GetFolderSize(string fullName, CancellationToken cancellationToken) {
            DirectoryInfo info = new DirectoryInfo(fullName);
            return GetFolderSize(info, cancellationToken);
        }
        long GetFolderSize(DirectoryInfo d, CancellationToken cancellationToken) {
            long Size = 0;
            FileInfo[] fis = { };
            try {
                fis = d.GetFiles();
            }
            catch {
            }
            if(fis.Length != 0) {
                foreach(FileInfo fi in fis) {
                    if(cancellationToken.IsCancellationRequested) break;
                    Size += fi.Length;
                }
            }
            DirectoryInfo[] dis = { };
            try {
                dis = d.GetDirectories();
            }
            catch {
            }
            if(dis.Length != 0) {
                foreach(DirectoryInfo di in dis) {
                    if(cancellationToken.IsCancellationRequested) break;
                    Size += GetFolderSize(di, cancellationToken);
                }
            }
            return Size;
        }
    }

    public class FileSystemItemSize {
        const int kb = 1024;
        const int mb = 1048576;

        public const string Folder = "<Folder>";
        public const string Drive = "<Drive>";
        public const string Calculating = "Calculating";

        public string DisplaySize { get; private set; }
        public long NumSize { get; private set; }

        public event EventHandler<ItemSizeChangedEventArgs> SizeChanged;
        void OnSizeChanged() {
            if(SizeChanged != null) SizeChanged(this, new ItemSizeChangedEventArgs(this));
        }

        public void Change(long size) {
            NumSize = size;
            DisplaySize = FileSizeToString(size);
            OnSizeChanged();
        }
        public void Change(string displaySize) {
            DisplaySize = displaySize;
            OnSizeChanged();
        }

        public FileSystemItemSize(string displaySize) {
            Change(displaySize);
        }
        public FileSystemItemSize(long size) {
            Change(size);
        }

        public bool IsCalculated() {
            return (DisplaySize != FileSystemItemSize.Calculating &&
                DisplaySize != FileSystemItemSize.Drive &&
                DisplaySize != FileSystemItemSize.Folder);
        }

        string FileSizeToString(long size) {
            if(size > mb)
                return string.Format("{0:### ### ###} MB", size / mb);
            else if(size > kb)
                return string.Format("{0:### ### ###} KB", size / kb);
            else
                return string.Format("{0} Bytes", size);
        }
        public override string ToString() {
            return DisplaySize;
        }
    }

    public class ItemSizeChangedEventArgs : EventArgs {
        public FileSystemItemSize Size { get; set; }
        public ItemSizeChangedEventArgs(FileSystemItemSize itemSize) {
            Size = itemSize;
        }
    }
}
