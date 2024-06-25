using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace TreeListDemo.Data {
    public class FileSystemItem {
        public FileSystemItem(string name, string type, string size, string fullName) {
            this.Name = name;
            this.ItemType = type;
            this.Size = size;
            this.FullName = fullName;
        }
        public string Name { get; set; }
        public string ItemType { get; set; }
        public string Size { get; set; }
        public string FullName { get; set; }
    }

    public abstract class FileSystemDataProviderBase {
        public abstract string[] GetLogicalDrives();
        public abstract string[] GetDirectories(string path);
        public abstract string[] GetFiles(string path);
        public abstract string GetDirectoryName(string path);
        public abstract string GetFileName(string path);
        public abstract string GetFileSize(string path);
        internal string GetFileSize(long size) {
            if(size >= 1024)
                return string.Format("{0:### ### ###} KB", size / 1024);
            return string.Format("{0} Bytes", size);
        }
    }

    public class FileSystemHelper : FileSystemDataProviderBase, IDataProvider {

        public override string[] GetLogicalDrives() {
            return Directory.GetLogicalDrives();
        }

        public override string[] GetDirectories(string path) {
            try {
                return Directory.EnumerateDirectories(path).ToArray();
            } catch(UnauthorizedAccessException) {
                return new string[] { };
            }
        }

        public override string[] GetFiles(string path) {
            try {
                return Directory.EnumerateFiles(path).ToArray();
            } catch(UnauthorizedAccessException) {
                return new string[] { };
            }
        }

        public override string GetDirectoryName(string path) {
            return new DirectoryInfo(path).Name;
        }

        public override string GetFileName(string path) {
            return new FileInfo(path).Name;
        }

        public override string GetFileSize(string path) {
            long size = new FileInfo(path).Length;
            return GetFileSize(size);
        }

        #region IDataProvider Members
        public bool HasChildren(object item) {
            var fsItem = (FileSystemItem)item;

            if(!Directory.Exists(fsItem.FullName))
                return false;

            try {
                return GetFiles(fsItem.FullName).Length > 0 || GetDirectories(fsItem.FullName).Length > 0;
            } catch(UnauthorizedAccessException) {
                return false;
            }
        }

        public IEnumerable GetChildren(object item) {
            var fsItem = (FileSystemItem)item;

            var items = new List<FileSystemItem>();
            if(!Directory.Exists(fsItem.FullName))
                return items;

            foreach(var s in GetDirectories(fsItem.FullName)) {
                try {
                    items.Add(new FileSystemItem(GetDirectoryName(s), "Folder", "<Folder>", s));
                } catch { }
            }

            foreach(var s in GetFiles(fsItem.FullName)) {
                try {
                    items.Add(new FileSystemItem(GetFileName(s), "File", GetFileSize(s).ToString(), s));
                } catch { }
            }
            return items;
        }
        #endregion
    }
}
