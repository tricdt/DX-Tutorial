using System;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using DevExpress.DemoData.Helpers;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Core;

namespace NavigationDemo {
    public class ReportLibraryViewModel {
        protected virtual IFolderBrowserDialogService FolderBrowserDialogService { get { return this.GetService<IFolderBrowserDialogService>(); } }

        public IEnumerable<ReportLibraryNode> Nodes { get; private set; }
        public virtual ReportLibraryNode SelectedNode { get; set; }

        public ObservableCollection<ReportLibraryNode> CheckedReportNodes { get; set; }

        public ReportLibraryViewModel() {
            var reportsFolder = DataFilesHelper.FindFile("Reports", DataFilesHelper.DataPath);
            Nodes = new ObservableCollection<ReportLibraryNode>(GetFolderChildren(reportsFolder));
            CheckedReportNodes = new ObservableCollection<ReportLibraryNode>();
            SelectedNode = Nodes.First().Children.FirstOrDefault(x => x.Document != null);
        }

        public void OnNodePositionChanging(DragRecordOverEventArgs e) {
            var isTargetFolder = ((ReportLibraryNode)e.TargetRecord).IsFolder;
            var isInside = e.DropPosition == DropPosition.Inside;
            if((!isTargetFolder && isInside)
                || (isTargetFolder && !isInside && !SelectedNode.IsFolder)) 
                e.Effects = DragDropEffects.None;
             else 
                e.Effects = DragDropEffects.All;
        }
        public void OnCheckStateChanged(ReportLibraryNode node, object isChecked) {
            if((bool?)isChecked == true && !node.IsFolder && !CheckedReportNodes.Contains(node))
                CheckedReportNodes.Add(node);
            else
                CheckedReportNodes.Remove(node);
        }
        public virtual void SaveReports() {
            if(FolderBrowserDialogService.ShowDialog())
                SaveReportsCore(FolderBrowserDialogService.ResultPath);            
        }
        public virtual bool CanSaveReports() {
            return CheckedReportNodes.Any();
        }
        void SaveReportsCore(string targetFolder) {
            foreach (var node in CheckedReportNodes) {
                var fileName = Path.GetFileName(node.Document);
                var targetPath = Path.Combine(targetFolder, fileName);
                try {
                    File.Copy(node.Document, targetPath, overwrite: true);
                } catch { }               
            }
        }
        static ReportLibraryNode CreateFolderNode(string path) {
            var node = ReportLibraryNode.Create(Path.GetFileName(path));
            foreach(var child in GetFolderChildren(path))
                node.Children.Add(child);
            return node;
        }
        static IEnumerable<ReportLibraryNode> GetFolderChildren(string path) {
            return Directory.GetDirectories(path).Select(x => CreateFolderNode(Path.Combine(path, x)))
                .Concat(Directory.GetFiles(path).Select(x => ReportLibraryNode.Create(Path.GetFileNameWithoutExtension(x), x)));
        }
    }
    public class ReportLibraryNode {
        public static ReportLibraryNode Create(string name, string document = null) {
            var node = ViewModelSource.Create(() => new ReportLibraryNode());
            node.Name = name;
            node.Document = document;
            return node;
        }
        public virtual string Name { get; set; }
        public virtual string Document { get; set; }
        readonly ObservableCollection<ReportLibraryNode> children;
        public ObservableCollection<ReportLibraryNode> Children { get { return children; } }
        public virtual bool IsFolder { get { return string.IsNullOrEmpty(Document); } }
        protected ReportLibraryNode() {
            children = new ObservableCollection<ReportLibraryNode>();
        }
        public override string ToString() {
            return Name;
        }
    }
}
