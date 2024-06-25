using DevExpress.Mvvm.UI;
using DevExpress.Xpf.Bars.Native;
using DevExpress.Xpf.Docking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Linq;

namespace DockingDemo {
    internal interface IDockLayoutManagerEventsService {
        void ClearEventsLog();
    }
    public class DockLayoutManagerEventsService : ServiceBase, IDockLayoutManagerEventsService {
        public static readonly DependencyProperty LogProperty = DependencyProperty.Register("Log", typeof(string), typeof(DockLayoutManagerEventsService));
        public string Log {
            get { return (string)GetValue(LogProperty); }
            set { SetValue(LogProperty, value); }
        }
        DockLayoutManager DockLayoutManager { get { return AssociatedObject as DockLayoutManager; } }
        protected override void OnAttached() {
            base.OnAttached();
            DockLayoutManager.DockOperationStarting += DockLayoutManager_DockOperationStarting;
            DockLayoutManager.DockOperationCompleted += DockLayoutManager_DockOperationCompleted;
            DockLayoutManager.DockItemActivating += DockLayoutManager_DockItemActivating;
            DockLayoutManager.DockItemActivated += DockLayoutManager_DockItemActivated;
            DockLayoutManager.DockItemExpanded += DockLayoutManager_DockItemExpanded;
            DockLayoutManager.DockItemCollapsed += DockLayoutManager_DockItemCollapsed;
            DockLayoutManager.DockItemStartDocking += DockLayoutManager_DockItemStartDocking;
            DockLayoutManager.DockItemDragging += DockLayoutManager_DockItemDragging;
            DockLayoutManager.DockItemEndDocking += DockLayoutManager_DockItemEndDocking;
        }
        protected override void OnDetaching() {
            DockLayoutManager.DockOperationStarting -= DockLayoutManager_DockOperationStarting;
            DockLayoutManager.DockOperationCompleted -= DockLayoutManager_DockOperationCompleted;
            DockLayoutManager.DockItemActivating -= DockLayoutManager_DockItemActivating;
            DockLayoutManager.DockItemActivated -= DockLayoutManager_DockItemActivated;
            DockLayoutManager.DockItemExpanded -= DockLayoutManager_DockItemExpanded;
            DockLayoutManager.DockItemCollapsed -= DockLayoutManager_DockItemCollapsed;
            DockLayoutManager.DockItemStartDocking -= DockLayoutManager_DockItemStartDocking;
            DockLayoutManager.DockItemDragging -= DockLayoutManager_DockItemDragging;
            DockLayoutManager.DockItemEndDocking -= DockLayoutManager_DockItemEndDocking;
            base.OnDetaching();
        }
        private void DockLayoutManager_DockItemEndDocking(object sender, DevExpress.Xpf.Docking.Base.DockItemDockingEventArgs e) {
            AddLogEntry("DockItemEndDocking: " +  e.Item.CustomizationCaption);
        }
        private void DockLayoutManager_DockItemDragging(object sender, DevExpress.Xpf.Docking.Base.DockItemDraggingEventArgs e) {
            AddLogEntry("DockItemDragging: " + e.Item.CustomizationCaption, true);
        }
        private void DockLayoutManager_DockItemStartDocking(object sender, DevExpress.Xpf.Docking.Base.ItemCancelEventArgs e) {
            AddLogEntry("DockItemStartDocking: " + e.Item.CustomizationCaption);
        }
        private void DockLayoutManager_DockItemCollapsed(object sender, DevExpress.Xpf.Docking.Base.DockItemCollapsedEventArgs e) {
            AddLogEntry("DockItemCollapsed: " + e.Item.CustomizationCaption);
        }
        private void DockLayoutManager_DockItemExpanded(object sender, DevExpress.Xpf.Docking.Base.DockItemExpandedEventArgs e) {
            AddLogEntry("DockItemExpanded: " + e.Item.CustomizationCaption);
        }
        private void DockLayoutManager_DockItemActivated(object sender, DevExpress.Xpf.Docking.Base.DockItemActivatedEventArgs e) {
            if(e.Item == null) return;
            AddLogEntry("DockItemActivated: " + e.Item.CustomizationCaption);
        }
        private void DockLayoutManager_DockItemActivating(object sender, DevExpress.Xpf.Docking.Base.ItemCancelEventArgs e) {
            AddLogEntry("DockItemActivating: " + e.Item.CustomizationCaption);
        }
        private void DockLayoutManager_DockOperationCompleted(object sender, DevExpress.Xpf.Docking.Base.DockOperationCompletedEventArgs e) {
            AddLogEntry("DockOperationCompleted: " + e.Item.CustomizationCaption + ", Operation: " + e.DockOperation);
        }
        private void DockLayoutManager_DockOperationStarting(object sender, DevExpress.Xpf.Docking.Base.DockOperationStartingEventArgs e) {
            AddLogEntry("DockOperationStarting: " + e.Item.CustomizationCaption + ", Operation: " + e.DockOperation);
        }
        const int logEntriesCount = 100;
        Queue<LogEntry> logQueue = new Queue<LogEntry>();
        LogEntry lastEntry;
        void AddLogEntry(string str, bool groupEvents = false) {
            LogEntry entry = new LogEntry(str);
            if(entry == lastEntry && groupEvents) {
                lastEntry.AddRecord();
            }
            else {
                logQueue.Enqueue(entry);
                lastEntry = entry;
            }
            if(logQueue.Count > logEntriesCount)
                logQueue.Dequeue();
            StringBuilder builder = new StringBuilder();
            foreach(var e in logQueue) {
                builder.Append(builder.Length != 0 ? Environment.NewLine : string.Empty);
                builder.Append(e);
            }
            Log = builder.ToString();
        }
        public void ClearEventsLog() {
            logQueue.Clear();
            Log = string.Empty;
            lastEntry = null;
        }
        class LogEntry : IEquatable<LogEntry> {
            string text;
            int count = 1;
            public LogEntry(string text) {
                this.text = text;
            }
            public void AddRecord() {
                count++;
            }
            public override bool Equals(object obj) {
                if(obj is LogEntry)
                    return Equals((LogEntry)obj);
                return base.Equals(obj);
            }
            public static bool operator ==(LogEntry first, LogEntry second) {
                if((object)first == null)
                    return (object)second == null;
                return first.Equals(second);
            }
            public static bool operator !=(LogEntry first, LogEntry second) {
                return !(first == second);
            }
            public bool Equals(LogEntry other) {
                if(ReferenceEquals(null, other))
                    return false;
                if(ReferenceEquals(this, other))
                    return true;
                return Equals(this.text, other.text);
            }
            public override int GetHashCode() {
                int hashCode = 47;
                if(text != null)
                    hashCode = (hashCode * 53) ^ text.GetHashCode();
                return hashCode;
            }
            public override string ToString() {
                return text + (count > 1 ? " (x" + count + ")" : string.Empty);
            }
        }
    }
}
