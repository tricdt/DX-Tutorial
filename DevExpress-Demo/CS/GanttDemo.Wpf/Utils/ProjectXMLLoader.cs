using DevExpress.Mvvm;
using DevExpress.Xpf.Gantt;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace GanttDemo {
    public static class ProjectXMLLoader {
        public static XElement GetElement(this XElement element, string name) {
            return element.Element(XName.Get(name, element.GetDefaultNamespace().NamespaceName));
        }
        public static IEnumerable<XElement> GetElements(this XElement element, string name) {
            return element.Elements(XName.Get(name, element.GetDefaultNamespace().NamespaceName));
        }

        static XElement GetCalendar(XElement projectElement) {
            var calendarUID = projectElement.GetElement("CalendarUID").Value;
            return projectElement
                    .GetElement("Calendars")
                    .Elements()
                    .First(x => calendarUID.Equals(x.GetElement("UID").Value));
        }
        static DaysOfWeek GetDayOfWeek(XElement workdayElement) {
            var dayType = Int32.Parse(workdayElement.GetElement("DayType").Value, CultureInfo.InvariantCulture);
            return (DaysOfWeek)(Math.Pow(2, dayType - 1));
        }
        static IEnumerable<WorkdayRule> ReadWorkdaysRules(XElement calendarElement) {
            return calendarElement
                        .GetElement("WeekDays")
                        .Elements()
                        .Where(x => x.GetElement("DayWorking").Value == "0")
                        .Aggregate(new List<WorkdayRule>(), (collection, workday) => {
                            collection.Add(new WorkdayRule() { IsWorkday = false, Recurrence = new Weekly() { DayOfWeek = GetDayOfWeek(workday) } });
                            return collection;
                        });
        }
        static IEnumerable<WorkingTimeRule> ReadWorkingTimeRules(XElement calendarElement) {
            return calendarElement
                        .GetElement("WeekDays")
                        .Elements()
                        .Where(x => x.GetElement("DayWorking").Value == "1")
                        .Aggregate(new List<WorkingTimeRule>(), (collection, workday) => {
                            collection.Add(new WorkingTimeRule() { Recurrence = new Weekly() { DayOfWeek = GetDayOfWeek(workday) }, WorkingTime = new TimeSpanRangeCollection(ReadWorkingTime(workday)) });
                            return collection;
                        });
        }
        static IEnumerable<TimeSpanRange> ReadWorkingTime(XElement workdayElement) {
            return workdayElement
                        .GetElement("WorkingTimes")
                        .Elements()
                        .Aggregate(new List<TimeSpanRange>(), (ranges, worktime) => {
                            ranges.Add(new TimeSpanRange(TimeSpan.Parse(worktime.GetElement("FromTime").Value, CultureInfo.InvariantCulture), TimeSpan.Parse(worktime.GetElement("ToTime").Value, CultureInfo.InvariantCulture)));
                            return ranges;
                        });
        }
        static string GetParentWBS(string wbs) {
            if (string.IsNullOrEmpty(wbs))
                return string.Empty;
            var wbsPaths = wbs.Split('.');
            return wbsPaths.Length == 1 ? "0" : string.Join(".", wbsPaths.Take(wbsPaths.Length - 1));
        }
        static string GetParentUID(string wbs, Dictionary<string, TaskDataItem> itemsByWBS) {
            var parentWBS = GetParentWBS(wbs);
            TaskDataItem parentItem = null;
            if (itemsByWBS.TryGetValue(parentWBS, out parentItem))
                return parentItem.UID;
            return null;
        }
        static IEnumerable<string> ReadPredecessorsUID(XElement taskElement) {
            var predecessors = taskElement.GetElements("PredecessorLink");
            if (predecessors == null)
                return null;
           return predecessors
                        .Select(x => x.GetElement("PredecessorUID").Value)
                        .ToList()
                        .AsReadOnly();
        }
        static TaskDataItem CreateTask(XElement projectElement, XElement taskNode, Dictionary<string, TaskDataItem> itemsByWBS) {
            var wbs = taskNode.GetElement("WBS").Value;
            var uid = taskNode.GetElement("UID").Value;
            var baseline = taskNode.GetElement("Baseline");
            var task = new TaskDataItem(
                uid,
                GetParentUID(wbs, itemsByWBS),
                taskNode.GetElement("Name").Value,
                taskNode.GetElement("Start").Value,
                taskNode.GetElement("Finish").Value,
                baseline != null ? baseline.GetElement("Start").Value : null,
                baseline != null ? baseline.GetElement("Finish").Value : null,
                Convert.ToDouble(taskNode.GetElement("PercentComplete").Value, CultureInfo.InvariantCulture) / 100d,
                ReadPredecessorsUID(taskNode));
            itemsByWBS.Add(wbs, task);
            return task;
        }
        static TaskDataItem[] ReadTasks(XElement projectElement) {
            var itemsByWBS = new Dictionary<string, TaskDataItem>();
            return projectElement
                    .GetElement("Tasks")
                    .Elements()
                    .Select(item => CreateTask(projectElement, item, itemsByWBS))
                    .ToArray();
        }

        static IEnumerable<ResourceDataItem> ReadResources(XElement projectElement) {
            var resources = new Dictionary<string, ResourceDataItem>();
            return projectElement
                        .GetElement("Resources")
                        .Elements()
                        .Where(x => x.GetElement("Name") != null)
                        .Select(x => new ResourceDataItem(x.GetElement("UID").Value, x.GetElement("Name").Value))
                        .ToArray();
        }
        static IEnumerable<ResourceLinkDataItem> ReadResouceLinks(XElement projectElement) {
            return projectElement
                        .GetElement("Assignments")
                        .Elements()
                        .Select(x => new ResourceLinkDataItem(
                            x.GetElement("TaskUID").Value,
                            x.GetElement("ResourceUID").Value,
                            Convert.ToDouble(x.GetElement("Units").Value, CultureInfo.InvariantCulture)
                        ))
                        .ToArray();
        }


        public static void LoadModel(Stream xmlStream, ProjectViewModel model) {
            var root = XDocument.Load(xmlStream).Root;
            var calendar = GetCalendar(root);
            model.Tasks = ReadTasks(root);
            model.Resources = ReadResources(root);
            model.ResourceLinks = ReadResouceLinks(root);
            model.WorkdaysRules = ReadWorkdaysRules(calendar);
            model.WorkingTimeRules = ReadWorkingTimeRules(calendar);
        }

    }
}
