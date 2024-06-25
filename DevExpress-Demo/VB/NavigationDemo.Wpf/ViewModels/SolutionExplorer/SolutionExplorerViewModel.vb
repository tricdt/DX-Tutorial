Imports System.Collections.Generic

Namespace NavigationDemo

    Public Class SolutionExplorerViewModel

        Private _Nodes As IEnumerable(Of NavigationDemo.SolutionNode)

        Public Property Nodes As IEnumerable(Of SolutionNode)
            Get
                Return _Nodes
            End Get

            Private Set(ByVal value As IEnumerable(Of SolutionNode))
                _Nodes = value
            End Set
        End Property

        Public Sub New()
#Region "Activity.cs"
            Dim activityFileName As String = "Activity.txt"
            Dim activityCS = New SolutionNode() With {.FileName = activityFileName, .Name = "Activity.cs", .TypeNode = TypeNode.File}
            Dim activity = New SolutionNode() With {.FileName = activityFileName, .Name = "Activity", .TypeNode = TypeNode.Class, .SearchString = "public class Activity :"}
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "Id", .TypeName = "int", .TypeNode = TypeNode.Property, .SearchString = "public int Id"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "ActivityType", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string ActivityType"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "Description", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string Description"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "DocumentPath", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string DocumentPath"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "DocumentName", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string DocumentName"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "Icon", .TypeName = "Bitmap", .TypeNode = TypeNode.Property, .SearchString = "public Bitmap Icon"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "Image", .TypeName = "Bitmap", .TypeNode = TypeNode.Property, .SearchString = "public Bitmap Image"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "CompletedDate", .TypeName = "DateTime", .TypeNode = TypeNode.Property, .SearchString = "public DateTime CompletedDate"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "OwnerId", .TypeName = "int", .TypeNode = TypeNode.Property, .SearchString = "public int OwnerId"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "OwnerName", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string OwnerName"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "PropertyChanged", .TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", .TypeNode = TypeNode.Event, .SearchString = "public event PropertyChangedEventHandler PropertyChanged;"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "InvokePropertyChanged(string)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "private void InvokePropertyChanged(string name)", .SearchName = "InvokePropertyChanged"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "_id", .TypeName = "int", .TypeNode = TypeNode.Field, .SearchString = "private int _id;"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "_completedDate", .TypeName = "DateTime", .TypeNode = TypeNode.Field, .SearchString = "private DateTime _completedDate;"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "_ownerId", .TypeName = "int", .TypeNode = TypeNode.Field, .SearchString = "private int _ownerId;"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "_activityType", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "private string _activityType;"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "_description", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "private string _description;"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "_documentPath", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "private string _documentPath;"})
            activity.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "_image", .TypeName = "Bitmap", .TypeNode = TypeNode.Field, .SearchString = "private Bitmap _image"})
            activityCS.AddChildren(activity)
            activityCS.AddChildren(New SolutionNode() With {.FileName = activityFileName, .Name = "ActivityList", .TypeNode = TypeNode.Class, .SearchString = "public class ActivityList :"})
#End Region
#Region "CategoryStat.cs"
            Dim categoryStatFileName As String = "CategoryStat.txt"
            Dim categoryStatCS = New SolutionNode() With {.FileName = categoryStatFileName, .Name = "CategoryStat.cs", .TypeNode = TypeNode.File}
            Dim categoryStat = New SolutionNode() With {.FileName = categoryStatFileName, .Name = "CategoryStat", .TypeNode = TypeNode.Class, .SearchString = "public class CategoryStat :"}
            categoryStat.AddChildren(New SolutionNode() With {.FileName = categoryStatFileName, .Name = "Tech", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string Tech"})
            categoryStat.AddChildren(New SolutionNode() With {.FileName = categoryStatFileName, .Name = "Count", .TypeName = "long", .TypeNode = TypeNode.Property, .SearchString = "public long Count"})
            categoryStat.AddChildren(New SolutionNode() With {.FileName = categoryStatFileName, .Name = "Amount", .TypeName = "long", .TypeNode = TypeNode.Property, .SearchString = "public long Amount"})
            categoryStat.AddChildren(New SolutionNode() With {.FileName = categoryStatFileName, .Name = "PropertyChanged", .TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", .TypeNode = TypeNode.Event, .SearchString = "public event PropertyChangedEventHandler PropertyChanged;"})
            categoryStat.AddChildren(New SolutionNode() With {.FileName = categoryStatFileName, .Name = "InvokePropertyChanged(string)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "private void InvokePropertyChanged(string name)", .SearchName = "InvokePropertyChanged"})
            categoryStat.AddChildren(New SolutionNode() With {.FileName = categoryStatFileName, .Name = "_count", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "private long _count;"})
            categoryStat.AddChildren(New SolutionNode() With {.FileName = categoryStatFileName, .Name = "_amount", .TypeName = "long", .TypeNode = TypeNode.Field, .SearchString = "private long _amount;"})
            categoryStat.AddChildren(New SolutionNode() With {.FileName = categoryStatFileName, .Name = "_tech", .TypeName = "long", .TypeNode = TypeNode.Field, .SearchString = "private string _tech;"})
            categoryStatCS.AddChildren(categoryStat)
            categoryStatCS.AddChildren(New SolutionNode() With {.FileName = categoryStatFileName, .Name = "CategoryStatList", .TypeNode = TypeNode.Class, .SearchString = "public class CategoryStatList :"})
#End Region
#Region "CompanyStat.cs"
            Dim companyStatFileName As String = "CompanyStat.txt"
            Dim companyStatCS = New SolutionNode() With {.FileName = companyStatFileName, .Name = "CompanyStat.cs", .TypeNode = TypeNode.File}
            Dim companyStat = New SolutionNode() With {.FileName = companyStatFileName, .Name = "CompanyStat", .TypeNode = TypeNode.Class, .SearchString = "public class CompanyStat :"}
            companyStat.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "Date", .TypeName = "DateTime", .TypeNode = TypeNode.Property, .SearchString = "public DateTime Date"})
            companyStat.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "DateString", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string DateString"})
            companyStat.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "Count", .TypeName = "long", .TypeNode = TypeNode.Property, .SearchString = "public long Count"})
            companyStat.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "Amount", .TypeName = "long", .TypeNode = TypeNode.Property, .SearchString = "public long Amount"})
            companyStat.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "PropertyChanged", .TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", .TypeNode = TypeNode.Event, .SearchString = "public event PropertyChangedEventHandler PropertyChanged;"})
            companyStat.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "InvokePropertyChanged(string)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "private void InvokePropertyChanged(string name)", .SearchName = "InvokePropertyChanged"})
            companyStat.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "_count", .TypeName = "long", .TypeNode = TypeNode.Field, .SearchString = "private long _count;"})
            companyStat.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "_dateString", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "private string _dateString;"})
            companyStat.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "_amount", .TypeName = "long", .TypeNode = TypeNode.Field, .SearchString = "private long _amount;"})
            companyStat.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "_date", .TypeName = "DateTime", .TypeNode = TypeNode.Field, .SearchString = "private DateTime _date"})
            companyStatCS.AddChildren(companyStat)
            companyStatCS.AddChildren(New SolutionNode() With {.FileName = companyStatFileName, .Name = "CompanyStatList", .TypeNode = TypeNode.Class, .SearchString = "public class CompanyStatList :"})
#End Region
#Region "CustomAppointment.cs"
            Dim customAppointmentFileName As String = "CustomAppointment.txt"
            Dim customAppointmentCS = New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "CustomAppointment.cs", .TypeNode = TypeNode.File}
#Region "customAppointment"
            Dim customAppointment = New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "CustomAppointment", .TypeNode = TypeNode.Class, .SearchString = "public class CustomAppointment :"}
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fStart", .TypeName = "DateTime", .TypeNode = TypeNode.Field, .SearchString = "e fStart;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fEnd", .TypeName = "DateTime", .TypeNode = TypeNode.Field, .SearchString = "e fEnd;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fSubject", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "g fSubject;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fStatus", .TypeName = "int", .TypeNode = TypeNode.Field, .SearchString = "t fStatus;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fDescription", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "g fDescription;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fLabel", .TypeName = "long", .TypeNode = TypeNode.Field, .SearchString = "g fLabel;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fLocation", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "g fLocation;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fAllday", .TypeName = "bool", .TypeNode = TypeNode.Field, .SearchString = "l fAllday;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fEventType", .TypeName = "int", .TypeNode = TypeNode.Field, .SearchString = "t fEventType;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fRecurrenceInfo", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "g fRecurrenceInfo;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fReminderInfo", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "g fReminderInfo;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "fOwnerId", .TypeName = "object", .TypeNode = TypeNode.Field, .SearchString = "t fOwnerId;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "events", .TypeName = "CustomEventList", .TypeNode = TypeNode.Field, .SearchString = "ist events;"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "committed", .TypeName = "bool", .TypeNode = TypeNode.Field, .SearchString = "l committed"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "CustomAppointment(CustomEventList)", .TypeNode = TypeNode.Method, .SearchString = "public CustomAppointment(CustomEventList events)", .SearchName = "CustomAppointment"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "OnListChanged()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "private void OnListChanged()", .SearchName = "OnListChanged"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "StartTime", .TypeName = "DateTime", .TypeNode = TypeNode.Property, .SearchString = "public DateTime StartTime"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "EndTime", .TypeName = "DateTime", .TypeNode = TypeNode.Property, .SearchString = "public DateTime EndTime"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "Subject", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string Subject"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "Status", .TypeName = "int", .TypeNode = TypeNode.Property, .SearchString = "public int Status"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "Description", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string Description"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "Label", .TypeName = "long", .TypeNode = TypeNode.Property, .SearchString = "public long Label "})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "Location", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string Location"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "AllDay", .TypeName = "bool", .TypeNode = TypeNode.Property, .SearchString = "public bool AllDay"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "EventType", .TypeName = "int", .TypeNode = TypeNode.Property, .SearchString = "public int EventType"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "RecurrenceInfo", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string RecurrenceInfo"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "ReminderInfo", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string ReminderInfo"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "OwnerId", .TypeName = "object", .TypeNode = TypeNode.Property, .SearchString = "public object OwnerId"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "BeginEdit()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public void BeginEdit()", .SearchName = "BeginEdit"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "CancelEdit()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public void CancelEdit()", .SearchName = "CancelEdit"})
            customAppointment.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "EndEdit()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public void EndEdit()", .SearchName = "EndEdit"})
#End Region
#Region "customAppointment"
            Dim customEventList = New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "CustomEventList", .TypeNode = TypeNode.Class, .SearchString = "public class CustomEventList :"}
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "this[int]", .TypeName = "CustomAppointment", .TypeNode = TypeNode.Property, .SearchString = "public CustomAppointment this[int idx]", .SearchName = "this"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "Clear()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public new void Clear()", .SearchName = "Clear"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "Add(CustomAppointment)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public void Add(CustomAppointment appointment)", .SearchName = "Add"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "IndexOf(CustomAppointment)", .TypeName = "int", .TypeNode = TypeNode.Method, .SearchString = "public int IndexOf(CustomAppointment appointment)", .SearchName = "IndexOf"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "AddNew()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public object AddNew()", .SearchName = "AddNew"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "AllowEdit", .TypeName = "bool", .TypeNode = TypeNode.Property, .SearchString = "public bool AllowEdit"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "AllowNew", .TypeName = "bool", .TypeNode = TypeNode.Property, .SearchString = "public bool AllowNew"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "AllowRemove", .TypeName = "bool", .TypeNode = TypeNode.Property, .SearchString = "public bool AllowRemove"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "listChangedHandler", .TypeName = "ListChangedEventHandler(object, ListChangedEventArgs)", .TypeNode = TypeNode.Field, .SearchString = "private ListChangedEventHandler listChangedHandler"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "ListChanged", .TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", .TypeNode = TypeNode.Event, .SearchString = "public event ListChangedEventHandler ListChanged"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "OnListChanged(ListChangedEventArgs)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "internal void OnListChanged(ListChangedEventArgs args)", .SearchName = "OnListChanged"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "OnRemoveComplete(int, object)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "protected override void OnRemoveComplete(int index, object value)", .SearchName = "OnRemoveComplete"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "OnInsertComplete(int, object)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "protected override void OnInsertComplete(int index, object value)", .SearchName = "OnInsertComplete"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "AddIndex(PropertyDescriptor)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public void AddIndex(PropertyDescriptor pd)", .SearchName = "AddIndex"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "ApplySort(PropertyDescriptor, ListSortDirection)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public void ApplySort(PropertyDescriptor pd, ListSortDirection dir)", .SearchName = "ApplySort"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "Find(PropertyDescriptor, object)", .TypeName = "int", .TypeNode = TypeNode.Method, .SearchString = "public int Find(PropertyDescriptor property, object key)", .SearchName = "Find"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "IsSorted", .TypeName = "bool", .TypeNode = TypeNode.Property, .SearchString = "public bool IsSorted"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "RemoveIndex(PropertyDescriptor)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public void RemoveIndex(PropertyDescriptor pd)", .SearchName = "RemoveIndex"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "RemoveSort()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public void RemoveSort()", .SearchName = "RemoveSort"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "SortDirection", .TypeName = "ListSortDirection", .TypeNode = TypeNode.Property, .SearchString = "public ListSortDirection SortDirection", .SearchName = " SortDirection"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "SortProperty", .TypeName = "PropertyDescriptor", .TypeNode = TypeNode.Property, .SearchString = "public PropertyDescriptor SortProperty"})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "SupportsChangeNotification", .TypeName = "bool", .TypeNode = TypeNode.Property, .SearchString = "public bool SupportsChangeNotification "})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "SupportsSearching", .TypeName = "bool", .TypeNode = TypeNode.Property, .SearchString = "public bool SupportsSearching "})
            customEventList.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "SupportsSorting", .TypeName = "bool", .TypeNode = TypeNode.Property, .SearchString = "public bool SupportsSorting"})
#End Region
#Region "CustomResource"
            Dim customResource = New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "CustomResource", .TypeNode = TypeNode.Class, .SearchString = "public class CustomResource {"}
            customResource.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "name", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "string name;"})
            customResource.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "res_id", .TypeName = "int", .TypeNode = TypeNode.Field, .SearchString = "int res_id;"})
            customResource.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "res_color", .TypeName = "Color", .TypeNode = TypeNode.Field, .SearchString = "Color res_color;"})
            customResource.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "Name", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string Name"})
            customResource.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "ResID", .TypeName = "int", .TypeNode = TypeNode.Property, .SearchString = "public int ResID"})
            customResource.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "ResColor", .TypeName = "Color", .TypeNode = TypeNode.Property, .SearchString = "public Color ResColor"})
            customResource.AddChildren(New SolutionNode() With {.FileName = customAppointmentFileName, .Name = "CustomResource()", .TypeNode = TypeNode.Method, .SearchString = "public CustomResource() {", .SearchName = "CustomResource"})
#End Region
            customAppointmentCS.AddChildren(customAppointment)
            customAppointmentCS.AddChildren(customEventList)
            customAppointmentCS.AddChildren(customResource)
#End Region
#Region "DataGenerator.cs"
            Dim dataGeneratorFileName As String = "DataGenerator.txt"
            Dim dataGeneratorCS = New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "DataGenerator.cs", .TypeNode = TypeNode.File}
            Dim dataGenerator = New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "DataGenerator", .TypeNode = TypeNode.Class, .SearchString = "s DataGenerator"}
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "GenerateOpportunities()", .TypeName = "BindingList<Opportunity>", .TypeNode = TypeNode.Method, .SearchString = "> GenerateOpportunities() {", .SearchName = "GenerateOpportunities"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "Projects", .TypeName = "List<ProjectTuple>", .TypeNode = TypeNode.Field, .SearchString = "> Projects"})
            Dim projectTuple = New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "ProjectTuple", .TypeNode = TypeNode.Class, .SearchString = "ss ProjectTuple"}
            projectTuple.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "public ProjectTuple(string, string)", .TypeNode = TypeNode.Method, .SearchString = "ProjectTuple(string name", .SearchName = "ProjectTuple"})
            projectTuple.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "Name", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "ng Name;"})
            projectTuple.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "Technology", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "ng Technology;"})
            dataGenerator.AddChildren(projectTuple)
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "Descriptions", .TypeName = "Dictionary<string, string>", .TypeNode = TypeNode.Field, .SearchString = "g> Descriptions"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "Amounts", .TypeName = "List<int>", .TypeNode = TypeNode.Field, .SearchString = "t> Amounts"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "Companies", .TypeName = "List<string>", .TypeNode = TypeNode.Field, .SearchString = "g> Companies"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "DocNames", .TypeName = "List<string>", .TypeNode = TypeNode.Field, .SearchString = "g> DocNames"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "DocPath", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "DocPath = ""S"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "CreateSampleDocuments()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "d CreateSampleDocuments", .SearchName = "CreateSampleDocuments"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "GenerateActivities(Opportunity, int)", .TypeName = "List<Activity>", .TypeNode = TypeNode.Method, .SearchString = "y> GenerateActivities", .SearchName = "GenerateActivities"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "GenerateKeywordStats()", .TypeName = "BindingList<KeywordStat>", .TypeNode = TypeNode.Method, .SearchString = "t> GenerateKeywordStats", .SearchName = "GenerateKeywordStats"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "GenerateTeamMembers()", .TypeName = "BindingList<TeamMember>", .TypeNode = TypeNode.Method, .SearchString = "r> GenerateTeamMembers", .SearchName = "GenerateTeamMembers"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "AccountOwnerInfo", .TypeName = "List<List<string>>", .TypeNode = TypeNode.Field, .SearchString = "g>> AccountOwnerInfo"})
            dataGenerator.AddChildren(New SolutionNode() With {.FileName = dataGeneratorFileName, .Name = "GenerateAppointments(IList<TeamMember>)", .TypeName = "CustomEventList", .TypeNode = TypeNode.Method, .SearchString = "st GenerateAppointments", .SearchName = "GenerateAppointments"})
            dataGeneratorCS.AddChildren(dataGenerator)
#End Region
#Region "DataProvider.cs"
            Dim dataProviderFileName As String = "DataProvider.txt"
            Dim dataProviderCS = New SolutionNode() With {.FileName = dataProviderFileName, .Name = "DataProvider.cs", .TypeNode = TypeNode.File}
            Dim dataProvider = New SolutionNode() With {.FileName = dataProviderFileName, .Name = "DataProvider", .TypeNode = TypeNode.Class, .SearchString = "s DataProvider"}
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "instance", .TypeName = "DataProvider", .TypeNode = TypeNode.Field, .SearchString = "private static DataProvider instance"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "DataProvider()", .TypeNode = TypeNode.Method, .SearchString = "private DataProvider() { }", .SearchName = "DataProvider"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "Instance", .TypeName = "DataProvider", .TypeNode = TypeNode.Property, .SearchString = "public static DataProvider Instance {"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "Opportunities", .TypeName = "BindingList<Opportunity>", .TypeNode = TypeNode.Property, .SearchString = "public BindingList<Opportunity> Opportunities { get; set; }"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "NewestOpportunities", .TypeName = "BindingList<Opportunity>", .TypeNode = TypeNode.Property, .SearchString = "public BindingList<Opportunity> NewestOpportunities { get; set; }"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "Activities", .TypeName = "BindingList<Activity>", .TypeNode = TypeNode.Property, .SearchString = "public BindingList<Activity> Activities { get; set; }"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "KeywordStats", .TypeName = "BindingList<KeywordStat>", .TypeNode = TypeNode.Property, .SearchString = " public BindingList<KeywordStat> KeywordStats { get; set; }"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "TeamMembers", .TypeName = "BindingList<TeamMember>", .TypeNode = TypeNode.Property, .SearchString = "public BindingList<TeamMember> TeamMembers { get; set; }"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "GeneralStats", .TypeName = "GeneralStats", .TypeNode = TypeNode.Property, .SearchString = "public GeneralStats GeneralStats { get; set; }"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "Appointments", .TypeName = "CustomEventList", .TypeNode = TypeNode.Property, .SearchString = "public CustomEventList Appointments { get; set; }"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "NewestAppointments", .TypeName = "BindingList<CustomAppointment>", .TypeNode = TypeNode.Property, .SearchString = "public BindingList<CustomAppointment> NewestAppointments { get; set; }"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "Technologies", .TypeName = "IList<string>", .TypeNode = TypeNode.Property, .SearchString = "public IList<string> Technologies {"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "LeadSources", .TypeName = "IList<string>", .TypeNode = TypeNode.Property, .SearchString = "public IList<string> LeadSources {"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "OpportunityTypes", .TypeName = "IList<string>", .TypeNode = TypeNode.Property, .SearchString = "public IList<string> OpportunityTypes {"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "Stages", .TypeName = "IList<StageTuple>", .TypeNode = TypeNode.Property, .SearchString = "public IList<StageTuple> Stages {"})
            Dim stageTuple = New SolutionNode() With {.FileName = dataProviderFileName, .Name = "StageTuple", .TypeNode = TypeNode.Class, .SearchString = "ss StageTuple {"}
            stageTuple.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "stage", .TypeName = "Stage", .TypeNode = TypeNode.Field, .SearchString = "Stage stage;"})
            stageTuple.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "name", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = " string name;"})
            stageTuple.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "StageTuple(Stage, string)", .TypeNode = TypeNode.Method, .SearchString = "public StageTuple(Stage stage, string name) {", .SearchName = "StageTuple"})
            stageTuple.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "Stage", .TypeName = "Stage", .TypeNode = TypeNode.Property, .SearchString = "public Stage Stage {"})
            stageTuple.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "Name", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string Name {"})
            dataProvider.AddChildren(stageTuple)
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "InitializeData()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "private void InitializeData() {", .SearchName = "InitializeData"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "CreateNewOpportunity()", .TypeName = "Opportunity", .TypeNode = TypeNode.Method, .SearchString = "public Opportunity CreateNewOpportunity() {", .SearchName = "CreateNewOpportunity"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "CreateNewActivity(Opportunity)", .TypeName = "Activity", .TypeNode = TypeNode.Method, .SearchString = "public Activity CreateNewActivity(Opportunity opp) {", .SearchName = "CreateNewActivity"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "Opportunities_ListChanged(object, ListChangedEventArgs)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "void Opportunities_ListChanged(object sender, ListChangedEventArgs e) {", .SearchName = "Opportunities_ListChanged"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "RefreshNewestOppsList()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "void RefreshNewestOppsList() {", .SearchName = "RefreshNewestOppsList"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "RefreshGeneralStats()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "void RefreshGeneralStats() {", .SearchName = "RefreshGeneralStats"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "GetOpenOppsCount(int)", .TypeName = "int", .TypeNode = TypeNode.Method, .SearchString = "int GetOpenOppsCount(int teamMemberId) {", .SearchName = "GetOpenOppsCount"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = " Appointments_ListChanged(object, ListChangedEventArgs)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "void Appointments_ListChanged(object sender, ListChangedEventArgs e) {", .SearchName = "Appointments_ListChanged"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "RefreshNewAppointmentsList()", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public void RefreshNewAppointmentsList() {", .SearchName = "RefreshNewAppointmentsList"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "RefreshCompanyStats(DateTime, DateTime, CompanyStatList)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public static void RefreshCompanyStats(DateTime startDate, DateTime endDate, CompanyStatList stats) {", .SearchName = "RefreshCompanyStats"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "RefreshCategoryStats(DateTime, DateTime, CategoryStatList)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public static void RefreshCategoryStats(DateTime startDate, DateTime endDate, CategoryStatList stats) {", .SearchName = "RefreshCategoryStats"})
            dataProvider.AddChildren(New SolutionNode() With {.FileName = dataProviderFileName, .Name = "RefreshTeamStats(DateTime, DateTime, TeamMemberStatList, TeamMemberStatDetailList)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "public static void RefreshTeamStats(DateTime startDate, DateTime endDate, TeamMemberStatList stats, TeamMemberStatDetailList statDetails) {", .SearchName = "RefreshTeamStats"})
            dataProviderCS.AddChildren(dataProvider)
#End Region
#Region "KeywordStat.cs"
            Dim keywordStatFileName As String = "KeywordStat.txt"
            Dim keywordStatCS = New SolutionNode() With {.FileName = keywordStatFileName, .Name = "KeywordStat.cs", .TypeNode = TypeNode.File}
            Dim keywordStat = New SolutionNode() With {.FileName = keywordStatFileName, .Name = "KeywordStat", .TypeNode = TypeNode.Class, .SearchString = "public class KeywordStat :"}
            keywordStat.AddChildren(New SolutionNode() With {.FileName = keywordStatFileName, .Name = "Keyword", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "string Keyword {"})
            keywordStat.AddChildren(New SolutionNode() With {.FileName = keywordStatFileName, .Name = "Total", .TypeName = "int", .TypeNode = TypeNode.Property, .SearchString = " int Total {"})
            keywordStat.AddChildren(New SolutionNode() With {.FileName = keywordStatFileName, .Name = "Image", .TypeName = "Bitmap", .TypeNode = TypeNode.Property, .SearchString = " Bitmap Image "})
            keywordStat.AddChildren(New SolutionNode() With {.FileName = keywordStatFileName, .Name = "PropertyChanged", .TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", .TypeNode = TypeNode.Event, .SearchString = "public event PropertyChangedEventHandler PropertyChanged;"})
            keywordStat.AddChildren(New SolutionNode() With {.FileName = keywordStatFileName, .Name = "InvokePropertyChanged(string)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "private void InvokePropertyChanged(string name)", .SearchName = "InvokePropertyChanged"})
            keywordStat.AddChildren(New SolutionNode() With {.FileName = keywordStatFileName, .Name = "_keyword", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = " string _keyword;"})
            keywordStat.AddChildren(New SolutionNode() With {.FileName = keywordStatFileName, .Name = "_total", .TypeName = "int", .TypeNode = TypeNode.Field, .SearchString = "int _total;"})
            keywordStat.AddChildren(New SolutionNode() With {.FileName = keywordStatFileName, .Name = "_image", .TypeName = "Bitmap", .TypeNode = TypeNode.Field, .SearchString = " Bitmap _image"})
            keywordStatCS.AddChildren(keywordStat)
            keywordStatCS.AddChildren(New SolutionNode() With {.FileName = keywordStatFileName, .Name = "KeywordStatList", .TypeNode = TypeNode.Class, .SearchString = " public class KeywordStatList :"})
#End Region
#Region "Opportunity.cs"
            Dim opportunityFileName As String = "Opportunity.txt"
            Dim opportunityCS = New SolutionNode() With {.FileName = opportunityFileName, .Name = "Opportunity.cs", .TypeNode = TypeNode.File}
            Dim state = New SolutionNode() With {.FileName = opportunityFileName, .Name = "Stage", .TypeNode = TypeNode.Enum, .SearchString = "public enum Stage {"}
            state.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "Lead", .TypeNode = TypeNode.EnumElement, .SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled"})
            state.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "Qualification", .TypeNode = TypeNode.EnumElement, .SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled"})
            state.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "NeedsAnalysis", .TypeNode = TypeNode.EnumElement, .SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled"})
            state.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "Proposed", .TypeNode = TypeNode.EnumElement, .SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled"})
            state.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "Negotiation", .TypeNode = TypeNode.EnumElement, .SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled"})
            state.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "Closed", .TypeNode = TypeNode.EnumElement, .SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled"})
            state.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "Cancelled", .TypeNode = TypeNode.EnumElement, .SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled"})
            opportunityCS.AddChildren(state)
            Dim opportunity = New SolutionNode() With {.FileName = opportunityFileName, .Name = "Opportunity", .TypeNode = TypeNode.Class, .SearchString = "public class Opportunity {"}
            opportunity.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "ID", .TypeName = "int", .TypeNode = TypeNode.Property, .SearchString = "public int ID {"})
            opportunity.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "Name", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string Name {"})
            opportunity.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "AccountName", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string AccountName {"})
            opportunity.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "_id", .TypeName = "int", .TypeNode = TypeNode.Field, .SearchString = "int _id;"})
            opportunity.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "_name", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "string _name;"})
            opportunity.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "_accountName", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "string _accountName;"})
            opportunityCS.AddChildren(opportunity)
            opportunityCS.AddChildren(New SolutionNode() With {.FileName = opportunityFileName, .Name = "OpportunityList", .TypeNode = TypeNode.Class, .SearchString = "public class OpportunityList :"})
#End Region
#Region "TeamMember.cs"
            Dim teamMemberFileName As String = "TeamMember.txt"
            Dim teamMemberCS = New SolutionNode() With {.FileName = teamMemberFileName, .Name = "TeamMember.cs", .TypeNode = TypeNode.File}
            Dim teamMember = New SolutionNode() With {.FileName = teamMemberFileName, .Name = "TeamMember", .TypeNode = TypeNode.Class, .SearchString = "public class TeamMember :"}
            teamMember.AddChildren(New SolutionNode() With {.FileName = teamMemberFileName, .Name = "Id", .TypeName = "int", .TypeNode = TypeNode.Property, .SearchString = "public int Id"})
            teamMember.AddChildren(New SolutionNode() With {.FileName = teamMemberFileName, .Name = "FullName", .TypeName = "string", .TypeNode = TypeNode.Property, .SearchString = "public string FullName"})
            teamMember.AddChildren(New SolutionNode() With {.FileName = teamMemberFileName, .Name = "Photo", .TypeName = "Bitmap", .TypeNode = TypeNode.Property, .SearchString = "public Bitmap Photo"})
            teamMember.AddChildren(New SolutionNode() With {.FileName = teamMemberFileName, .Name = "PhotoSmall", .TypeName = "Bitmap", .TypeNode = TypeNode.Property, .SearchString = "public Bitmap PhotoSmall"})
            teamMember.AddChildren(New SolutionNode() With {.FileName = teamMemberFileName, .Name = "PropertyChanged", .TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", .TypeNode = TypeNode.Event, .SearchString = "public event PropertyChangedEventHandler PropertyChanged;"})
            teamMember.AddChildren(New SolutionNode() With {.FileName = teamMemberFileName, .Name = "InvokePropertyChanged(string)", .TypeName = "void", .TypeNode = TypeNode.Method, .SearchString = "private void InvokePropertyChanged(string name)", .SearchName = "InvokePropertyChanged"})
            teamMember.AddChildren(New SolutionNode() With {.FileName = teamMemberFileName, .Name = "_id", .TypeName = "int", .TypeNode = TypeNode.Field, .SearchString = "private int _id;"})
            teamMember.AddChildren(New SolutionNode() With {.FileName = teamMemberFileName, .Name = "_fullName", .TypeName = "string", .TypeNode = TypeNode.Field, .SearchString = "private string _fullName;"})
            teamMember.AddChildren(New SolutionNode() With {.FileName = teamMemberFileName, .Name = "_photo", .TypeName = "Bitmap", .TypeNode = TypeNode.Field, .SearchString = "private Bitmap _photo;"})
            teamMember.AddChildren(New SolutionNode() With {.FileName = teamMemberFileName, .Name = "_photoSmall", .TypeName = "Bitmap", .TypeNode = TypeNode.Field, .SearchString = "private Bitmap _photoSmall;"})
            teamMemberCS.AddChildren(teamMember)
#End Region
            Nodes = New List(Of SolutionNode) From {activityCS, categoryStatCS, companyStatCS, customAppointmentCS, dataGeneratorCS, dataProviderCS, keywordStatCS, opportunityCS, teamMemberCS}
        End Sub
    End Class
End Namespace
