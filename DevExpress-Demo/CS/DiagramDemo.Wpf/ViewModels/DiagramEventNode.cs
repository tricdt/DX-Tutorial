using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;

namespace DiagramDemo {
    [POCOViewModel]
    public class DiagramEventNode {
        public static DiagramEventNode Create(string title, bool? isChecked, DiagramEventNodeKind kind) { return ViewModelSource.Create(() => new DiagramEventNode(title, isChecked, kind)); }
        protected DiagramEventNode(string title, bool? isChecked, DiagramEventNodeKind kind) {
            this.title = title;
            this.kind = kind;
            IsChecked = isChecked;
        }
        readonly string title;
        public string Title { get { return title; } }
        readonly DiagramEventNodeKind kind;
        public DiagramEventNodeKind Kind { get { return kind; } }
        public virtual bool? IsChecked { get; set; }
        public bool ActualIsChecked { get { return IsChecked.HasValue && IsChecked.Value; } }
        public virtual DiagramEventNode Parent { get; protected set; }
        readonly ObservableCollection<DiagramEventNode> children = new ObservableCollection<DiagramEventNode>();
        public IEnumerable<DiagramEventNode> Children { get { return children; } }
        public void AddChild(DiagramEventNode child) {
            child.Parent = this;
            children.Add(child);
        }
        public void ShowHelp() {
            Process.Start(new ProcessStartInfo { FileName = "https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramControl." + Title + ".event", UseShellExecute = true });
        }
    }
    public enum DiagramEventNodeKind {
        Group,
        EventNode,
        Parameter
    }
}
