using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Windows;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Dialogs;
using DevExpress.Xpf.Gantt;

namespace GanttDemo {
    [POCOViewModel]
    public class ProjectViewModel {
        const string xmlFilePath = "Data\\SoftwareDevelopmentPlan.xml";

        public virtual TaskDataItem[] Tasks { get; set; }
        public virtual IEnumerable<ResourceDataItem> Resources { get; set; }
        public virtual IEnumerable<ResourceLinkDataItem> ResourceLinks { get; set; }
        public virtual IEnumerable<WorkingTimeRule> WorkingTimeRules { get; set; }
        public virtual IEnumerable<WorkdayRule> WorkdaysRules { get; set; }

        public ProjectViewModel() {
            using(var stream = ResourceUtils.GetResourceStream(xmlFilePath))
                ProjectXMLLoader.LoadModel(stream, this);
        }
        public void LoadFile() {
            using(var dialog = new DXOpenFileDialog() { Filter = "MS Project XML files (*.xml)|*.xml", Multiselect = false }) {
                var dialogResult = dialog.ShowDialog();
                if(dialogResult.HasValue && dialogResult.Value) {
                    using(var stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read)) {
                        try {
                            ProjectXMLLoader.LoadModel(stream, this);
                        }
                        catch {
                            var fileName = Path.GetFileName(dialog.FileName);
                            ThemedMessageBox.Show("Invalid File", string.Format("The \"{0}\" file is not a valid MS Project file.", fileName), MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
        }
    }

    public enum ContentDisplayMode {
        Resources,
        Name
    }

    public class ResourceDataItem : ImmutableObject {
        public string UID { get; private set; }
        public string Name { get; private set; }

        public ResourceDataItem(string uid, string name) {
            UID = uid;
            Name = name;
        }
    }

    public class ResourceLinkDataItem : ImmutableObject {
        public string TaskUID { get; private set; }
        public string ResourceUID { get; private set; }
        public double Units { get; private set; }

        public ResourceLinkDataItem(string taskUID, string resourceUID, double units) {
            TaskUID = taskUID;
            ResourceUID = resourceUID;
            Units = units;
        }
    }

    public class TaskDataItem : ImmutableObject {
        public string ParentUID { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime FinishDate { get; private set; }
        public DateTime? BaselineStartDate { get; private set; }
        public DateTime? BaselineFinishDate { get; private set; }
        public string Name { get; private set; }
        public string UID { get; private set; }
        public IEnumerable<string> Predecessors { get; private set; }
        public double Progress { get; private set; }

        public TaskDataItem(string uid, string parentUID, string name, string startDate, string finishDate, string baselineStartDate, string baselineFinishDate, double progress, IEnumerable<string> predecessors) {
            UID = uid;
            ParentUID = parentUID;
            Name = name;
            StartDate = ParseDateTime(startDate).Value;
            FinishDate = ParseDateTime(finishDate).Value;
            BaselineStartDate = ParseDateTime(baselineStartDate);
            BaselineFinishDate = ParseDateTime(baselineFinishDate);
            Progress = progress;
            Predecessors = predecessors;
        }
        DateTime? ParseDateTime(string inputString) {
            if (string.IsNullOrEmpty(inputString))
                return null;
            return DateTime.Parse(inputString, CultureInfo.InvariantCulture);
        }
    }
}
