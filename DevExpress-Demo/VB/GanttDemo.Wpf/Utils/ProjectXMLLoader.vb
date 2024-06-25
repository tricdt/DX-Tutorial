Imports DevExpress.Mvvm
Imports DevExpress.Xpf.Gantt
Imports System
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Linq
Imports System.Xml.Linq
Imports System.Runtime.CompilerServices

Namespace GanttDemo

    Public Module ProjectXMLLoader

        <Extension()>
        Public Function GetElement(ByVal element As XElement, ByVal name As String) As XElement
            Return element.Element(XName.Get(name, element.GetDefaultNamespace().NamespaceName))
        End Function

        <Extension()>
        Public Function GetElements(ByVal element As XElement, ByVal name As String) As IEnumerable(Of XElement)
            Return element.Elements(XName.Get(name, element.GetDefaultNamespace().NamespaceName))
        End Function

        Private Function GetCalendar(ByVal projectElement As XElement) As XElement
            Dim calendarUID = projectElement.GetElement("CalendarUID").Value
            Return projectElement.GetElement("Calendars").Elements().First(Function(x) calendarUID.Equals(x.GetElement("UID").Value))
        End Function

        Private Function GetDayOfWeek(ByVal workdayElement As XElement) As DaysOfWeek
            Dim dayType = Integer.Parse(workdayElement.GetElement("DayType").Value, CultureInfo.InvariantCulture)
            Return CType(Math.Pow(2, dayType - 1), DaysOfWeek)
        End Function

        Private Function ReadWorkdaysRules(ByVal calendarElement As XElement) As IEnumerable(Of WorkdayRule)
            Return calendarElement.GetElement("WeekDays").Elements().Where(Function(x) Equals(x.GetElement("DayWorking").Value, "0")).Aggregate(New List(Of WorkdayRule)(), Function(collection, workday)
                collection.Add(New WorkdayRule() With {.IsWorkday = False, .Recurrence = New Weekly() With {.DayOfWeek = GetDayOfWeek(workday)}})
                Return collection
            End Function)
        End Function

        Private Function ReadWorkingTimeRules(ByVal calendarElement As XElement) As IEnumerable(Of WorkingTimeRule)
            Return calendarElement.GetElement("WeekDays").Elements().Where(Function(x) Equals(x.GetElement("DayWorking").Value, "1")).Aggregate(New List(Of WorkingTimeRule)(), Function(collection, workday)
                collection.Add(New WorkingTimeRule() With {.Recurrence = New Weekly() With {.DayOfWeek = GetDayOfWeek(workday)}, .WorkingTime = New TimeSpanRangeCollection(ReadWorkingTime(workday))})
                Return collection
            End Function)
        End Function

        Private Function ReadWorkingTime(ByVal workdayElement As XElement) As IEnumerable(Of TimeSpanRange)
            Return workdayElement.GetElement("WorkingTimes").Elements().Aggregate(New List(Of TimeSpanRange)(), Function(ranges, worktime)
                ranges.Add(New TimeSpanRange(TimeSpan.Parse(worktime.GetElement("FromTime").Value, CultureInfo.InvariantCulture), TimeSpan.Parse(worktime.GetElement("ToTime").Value, CultureInfo.InvariantCulture)))
                Return ranges
            End Function)
        End Function

        Private Function GetParentWBS(ByVal wbs As String) As String
            If String.IsNullOrEmpty(wbs) Then Return String.Empty
            Dim wbsPaths = wbs.Split("."c)
            Return If(wbsPaths.Length = 1, "0", String.Join(".", wbsPaths.Take(wbsPaths.Length - 1)))
        End Function

        Private Function GetParentUID(ByVal wbs As String, ByVal itemsByWBS As Dictionary(Of String, TaskDataItem)) As String
            Dim parentWBS = GetParentWBS(wbs)
            Dim parentItem As TaskDataItem = Nothing
            If itemsByWBS.TryGetValue(parentWBS, parentItem) Then Return parentItem.UID
            Return Nothing
        End Function

        Private Function ReadPredecessorsUID(ByVal taskElement As XElement) As IEnumerable(Of String)
            Dim predecessors = taskElement.GetElements("PredecessorLink")
            If predecessors Is Nothing Then Return Nothing
            Return Enumerable.ToList(Of String)(Enumerable.Select(Of XElement, Global.System.[String])(predecessors, CType(Function(x) CStr(x.GetElement(CStr("PredecessorUID")).Value), Func(Of XElement, String)))).AsReadOnly()
        End Function

        Private Function CreateTask(ByVal projectElement As XElement, ByVal taskNode As XElement, ByVal itemsByWBS As Dictionary(Of String, TaskDataItem)) As TaskDataItem
            Dim wbs = taskNode.GetElement("WBS").Value
            Dim uid = taskNode.GetElement("UID").Value
            Dim baseline = taskNode.GetElement("Baseline")
            Dim task = New TaskDataItem(uid, GetParentUID(wbs, itemsByWBS), taskNode.GetElement("Name").Value, taskNode.GetElement("Start").Value, taskNode.GetElement("Finish").Value, If(baseline IsNot Nothing, baseline.GetElement("Start").Value, Nothing), If(baseline IsNot Nothing, baseline.GetElement("Finish").Value, Nothing), Convert.ToDouble(taskNode.GetElement("PercentComplete").Value, CultureInfo.InvariantCulture) / 100R, ReadPredecessorsUID(taskNode))
            itemsByWBS.Add(wbs, task)
            Return task
        End Function

        Private Function ReadTasks(ByVal projectElement As XElement) As TaskDataItem()
            Dim itemsByWBS = New Dictionary(Of String, TaskDataItem)()
            Return projectElement.GetElement("Tasks").Elements().[Select](Function(item) CreateTask(projectElement, item, itemsByWBS)).ToArray()
        End Function

        Private Function ReadResources(ByVal projectElement As XElement) As IEnumerable(Of ResourceDataItem)
            Dim resources = New Dictionary(Of String, ResourceDataItem)()
            Return projectElement.GetElement("Resources").Elements().Where(Function(x) x.GetElement("Name") IsNot Nothing).[Select](Function(x) New ResourceDataItem(x.GetElement("UID").Value, x.GetElement("Name").Value)).ToArray()
        End Function

        Private Function ReadResouceLinks(ByVal projectElement As XElement) As IEnumerable(Of ResourceLinkDataItem)
            Return projectElement.GetElement("Assignments").Elements().[Select](Function(x) New ResourceLinkDataItem(x.GetElement("TaskUID").Value, x.GetElement("ResourceUID").Value, Convert.ToDouble(x.GetElement("Units").Value, CultureInfo.InvariantCulture))).ToArray()
        End Function

        Public Sub LoadModel(ByVal xmlStream As Stream, ByVal model As ProjectViewModel)
            Dim root = XDocument.Load(xmlStream).Root
            Dim calendar = GetCalendar(root)
            model.Tasks = ReadTasks(root)
            model.Resources = ReadResources(root)
            model.ResourceLinks = ReadResouceLinks(root)
            model.WorkdaysRules = ReadWorkdaysRules(calendar)
            model.WorkingTimeRules = ReadWorkingTimeRules(calendar)
        End Sub
    End Module
End Namespace
