using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TreeListDemo.Data {

    public class FileSystemChildBehavior : Behavior<TreeListView> {

        public IDataProvider DataProvider {
            get { return (IDataProvider)GetValue(DataProviderProperty); }
            set { SetValue(DataProviderProperty, value); }
        }

        public static readonly DependencyProperty DataProviderProperty =
            DependencyProperty.Register("DataProvider", typeof(IDataProvider), typeof(FileSystemChildBehavior), new PropertyMetadata(null,
                (s, e) => ((FileSystemChildBehavior)s).OnDataProviderChanged(e)));

        void OnDataProviderChanged(DependencyPropertyChangedEventArgs e) {
            if(e.NewValue == null)
                return;
            AssociatedObject.ChildNodesSelector = new CustomAsyncChildNodeSelector(DataProvider);
        }
    }

    public class CustomAsyncChildNodeSelector : IAsyncChildNodesSelector {
        private readonly IDataProvider dataProvider;

        public CustomAsyncChildNodeSelector(IDataProvider dataProvider) {
            this.dataProvider = dataProvider;
        }

        public Task<bool> HasChildNode(object item, CancellationToken token) {
            return System.Threading.Tasks.Task.Run(async () => {
                for(int i = 0; i < 10; i++) {
                    token.ThrowIfCancellationRequested();
                    await System.Threading.Tasks.Task.Delay(25);
                }
                return dataProvider.HasChildren(item);
            });
        }

        public IEnumerable SelectChildren(object item) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable> SelectChildrenAsync(object item, CancellationToken token) {
            return System.Threading.Tasks.Task.Run(async () => {
                for(int i = 0; i < 20; i++) {
                    token.ThrowIfCancellationRequested();
                    await System.Threading.Tasks.Task.Delay(100);
                }
                return dataProvider.GetChildren(item);
            });
        }
    }

    public class FileSystemNodeImageSelector : TreeListNodeImageSelector {
        public override ImageSource Select(TreeListRowData rowData) {
            var fsItem = rowData.Row as FileSystemItem;
            if(fsItem == null) return null;

            if(fsItem.ItemType == "Drive")
                return FileSystemImages.DiskImage;
            else if(fsItem.ItemType == "Folder") {
                return rowData.IsExpanded ? FileSystemImages.OpenedFolderImage : FileSystemImages.ClosedFolderImage;
            }
            return FileSystemImages.FileImage;
        }
    }
}
