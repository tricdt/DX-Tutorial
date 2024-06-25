using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.POCO;

namespace GanttDemo {
    [POCOViewModel]
    public class GanttEventNode {
        public static GanttEventNode CreateGroup(string title, bool? isChecked) { return Create(title, string.Empty, isChecked, GanttEventNodeKind.Group); }
        public static GanttEventNode Create(string title, string ownerClassName, bool? isChecked, GanttEventNodeKind kind) { return ViewModelSource.Create(() => new GanttEventNode(title, ownerClassName, isChecked, kind)); }
        protected GanttEventNode(string title, string ownerClassName, bool? isChecked, GanttEventNodeKind kind) {
            this.title = title;
            this.kind = kind;
            this.ownerClassName = ownerClassName;
            IsChecked = isChecked;
        }
        readonly string title;
        public string Title { get { return title; } }
        readonly GanttEventNodeKind kind;
        public GanttEventNodeKind Kind { get { return kind; } }
        public virtual bool? IsChecked { get; set; }
        public bool ActualIsChecked { get { return IsChecked.HasValue && IsChecked.Value; } }
        public virtual GanttEventNode Parent { get; protected set; }
        readonly ObservableCollection<GanttEventNode> children = new ObservableCollection<GanttEventNode>();
        readonly string ownerClassName;
        public IEnumerable<GanttEventNode> Children { get { return children; } }
        public void AddChild(GanttEventNode child) {
            child.Parent = this;
            children.Add(child);
        }
        public void ShowHelp() {
            Process.Start(new ProcessStartInfo { FileName = "https://documentation.devexpress.com/WPF/" + ownerClassName + "." + Title + ".event", UseShellExecute = true });
        }
    }
    public enum GanttEventNodeKind {
        Group,
        EventNode,
        Parameter
    }
}
