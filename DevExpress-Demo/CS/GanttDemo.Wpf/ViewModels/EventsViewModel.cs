using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Gantt;
using DevExpress.Mvvm.POCO;
using DevExpress.Xpf.Gantt;

namespace GanttDemo {
    [POCOViewModel]
    public class EventsViewModel {
        protected EventsViewModel() {
            var startupPlanModel = new StartupBusinessPlanViewModel();
            this.Items = startupPlanModel.Items;
            this.Resources = startupPlanModel.Resources;
        }
        readonly ObservableCollection<LogEntry> log = new ObservableCollection<LogEntry>();
        const int LogEntriesMaxCount = 100;
        public IEnumerable<LogEntry> Log { get { return log; } }

        public List<GanttTask> Items { get; private set; }
        public List<GanttResource> Resources { get; private set; }

        void AddToLog(string eventName, string ownerClassName, string argsFormat, params object[] args) {
            log.Add(LogEntry.Create(eventName, string.Format(argsFormat, args), ownerClassName));
            if(log.Count > LogEntriesMaxCount)
                log.RemoveAt(0);
            var scrollService = this.GetService<IScrollToEndService>();
            if(scrollService != null)
                scrollService.ScrollToEnd();
        }
        public void ClearLog() {
            log.Clear();
        }
        public virtual IEnumerable<GanttEventNode> EventsInfo { get; protected set; }
        public void InitializeEventsInfo(GanttControl eventsOwner) {
            EventsInfo = new GanttEventsInfo(eventsOwner, AddToLog).Initialize();
        }
    }
    [POCOViewModel]
    public class LogEntry {
        public static LogEntry Create(string eventName, string eventArgs, string ownerClassName) {
            return ViewModelSource.Create(() => new LogEntry(eventName, eventArgs, ownerClassName));
        }
        protected LogEntry(string eventName, string eventArgs, string ownerClassName) {
            EventName = eventName;
            EventArgs = eventArgs;
            this.ownerClassName = ownerClassName;
        }
        readonly string ownerClassName;
        public virtual string EventName { get; protected set; }
        public virtual string EventArgs { get; protected set; }
        public void ShowHelp() {
            Process.Start(new ProcessStartInfo { FileName = "https://documentation.devexpress.com/WPF/" + ownerClassName + "." + EventName + ".event", UseShellExecute = true });
        }
    }
}
