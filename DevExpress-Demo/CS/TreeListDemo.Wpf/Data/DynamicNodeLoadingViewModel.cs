using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TreeListDemo.Data {
    public class DynamicNodeLoadingViewModel {

        public IEnumerable<FileSystemItem> Items { get; set; }

        public FileSystemHelper DataProvider { get; private set; }

        public DynamicNodeLoadingViewModel() {

            DataProvider = new FileSystemHelper();
            Items = DataProvider.GetLogicalDrives().Select(x => new FileSystemItem(x, "Drive", "<Drive>", x)).ToArray();
        }
    }

    public interface IDataProvider {
        bool HasChildren(object item);
        IEnumerable GetChildren(object item);
    }   



}
