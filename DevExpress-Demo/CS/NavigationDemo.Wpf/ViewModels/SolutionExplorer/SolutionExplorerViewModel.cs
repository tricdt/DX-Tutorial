using System.Collections.Generic;

namespace NavigationDemo {
    public class SolutionExplorerViewModel {

        public IEnumerable<SolutionNode> Nodes { get; private set; }

        public SolutionExplorerViewModel() {

            #region Activity.cs

            string activityFileName = "Activity.txt";

            var activityCS = new SolutionNode() { FileName = activityFileName, Name = "Activity.cs", TypeNode = TypeNode.File };
            var activity = new SolutionNode() { FileName = activityFileName, Name = "Activity", TypeNode = TypeNode.Class, SearchString = "public class Activity :" };

            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "Id", TypeName = "int", TypeNode = TypeNode.Property, SearchString = "public int Id" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "ActivityType", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string ActivityType" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "Description", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string Description" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "DocumentPath", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string DocumentPath" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "DocumentName", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string DocumentName" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "Icon", TypeName = "Bitmap", TypeNode = TypeNode.Property, SearchString = "public Bitmap Icon" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "Image", TypeName = "Bitmap", TypeNode = TypeNode.Property, SearchString = "public Bitmap Image" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "CompletedDate", TypeName = "DateTime", TypeNode = TypeNode.Property, SearchString = "public DateTime CompletedDate" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "OwnerId", TypeName = "int", TypeNode = TypeNode.Property, SearchString = "public int OwnerId" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "OwnerName", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string OwnerName" });

            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "PropertyChanged", TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", TypeNode = TypeNode.Event, SearchString = "public event PropertyChangedEventHandler PropertyChanged;" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "InvokePropertyChanged(string)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "private void InvokePropertyChanged(string name)", SearchName = "InvokePropertyChanged" });

            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "_id", TypeName = "int", TypeNode = TypeNode.Field, SearchString = "private int _id;" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "_completedDate", TypeName = "DateTime", TypeNode = TypeNode.Field, SearchString = "private DateTime _completedDate;" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "_ownerId", TypeName = "int", TypeNode = TypeNode.Field, SearchString = "private int _ownerId;" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "_activityType", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "private string _activityType;" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "_description", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "private string _description;" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "_documentPath", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "private string _documentPath;" });
            activity.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "_image", TypeName = "Bitmap", TypeNode = TypeNode.Field, SearchString = "private Bitmap _image" });

            activityCS.AddChildren(activity);
            activityCS.AddChildren(new SolutionNode() { FileName = activityFileName, Name = "ActivityList", TypeNode = TypeNode.Class, SearchString = "public class ActivityList :" });

            #endregion

            #region CategoryStat.cs

            string categoryStatFileName = "CategoryStat.txt";

            var categoryStatCS = new SolutionNode() { FileName = categoryStatFileName, Name = "CategoryStat.cs", TypeNode = TypeNode.File };
            var categoryStat = new SolutionNode() { FileName = categoryStatFileName, Name = "CategoryStat", TypeNode = TypeNode.Class, SearchString = "public class CategoryStat :" };

            categoryStat.AddChildren(new SolutionNode() { FileName = categoryStatFileName, Name = "Tech", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string Tech" });
            categoryStat.AddChildren(new SolutionNode() { FileName = categoryStatFileName, Name = "Count", TypeName = "long", TypeNode = TypeNode.Property, SearchString = "public long Count" });
            categoryStat.AddChildren(new SolutionNode() { FileName = categoryStatFileName, Name = "Amount", TypeName = "long", TypeNode = TypeNode.Property, SearchString = "public long Amount" });

            categoryStat.AddChildren(new SolutionNode() { FileName = categoryStatFileName, Name = "PropertyChanged", TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", TypeNode = TypeNode.Event, SearchString = "public event PropertyChangedEventHandler PropertyChanged;" });
            categoryStat.AddChildren(new SolutionNode() { FileName = categoryStatFileName, Name = "InvokePropertyChanged(string)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "private void InvokePropertyChanged(string name)", SearchName = "InvokePropertyChanged" });

            categoryStat.AddChildren(new SolutionNode() { FileName = categoryStatFileName, Name = "_count", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "private long _count;" });
            categoryStat.AddChildren(new SolutionNode() { FileName = categoryStatFileName, Name = "_amount", TypeName = "long", TypeNode = TypeNode.Field, SearchString = "private long _amount;" });
            categoryStat.AddChildren(new SolutionNode() { FileName = categoryStatFileName, Name = "_tech", TypeName = "long", TypeNode = TypeNode.Field, SearchString = "private string _tech;" });

            categoryStatCS.AddChildren(categoryStat);
            categoryStatCS.AddChildren(new SolutionNode() { FileName = categoryStatFileName, Name = "CategoryStatList", TypeNode = TypeNode.Class, SearchString = "public class CategoryStatList :" });

            #endregion

            #region CompanyStat.cs

            string companyStatFileName = "CompanyStat.txt";

            var companyStatCS = new SolutionNode() { FileName = companyStatFileName, Name = "CompanyStat.cs", TypeNode = TypeNode.File };
            var companyStat = new SolutionNode() { FileName = companyStatFileName, Name = "CompanyStat", TypeNode = TypeNode.Class, SearchString = "public class CompanyStat :" };

            companyStat.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "Date", TypeName = "DateTime", TypeNode = TypeNode.Property, SearchString = "public DateTime Date" });
            companyStat.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "DateString", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string DateString" });
            companyStat.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "Count", TypeName = "long", TypeNode = TypeNode.Property, SearchString = "public long Count" });
            companyStat.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "Amount", TypeName = "long", TypeNode = TypeNode.Property, SearchString = "public long Amount" });

            companyStat.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "PropertyChanged", TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", TypeNode = TypeNode.Event, SearchString = "public event PropertyChangedEventHandler PropertyChanged;" });
            companyStat.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "InvokePropertyChanged(string)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "private void InvokePropertyChanged(string name)", SearchName = "InvokePropertyChanged" });

            companyStat.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "_count", TypeName = "long", TypeNode = TypeNode.Field, SearchString = "private long _count;" });
            companyStat.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "_dateString", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "private string _dateString;" });
            companyStat.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "_amount", TypeName = "long", TypeNode = TypeNode.Field, SearchString = "private long _amount;" });
            companyStat.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "_date", TypeName = "DateTime", TypeNode = TypeNode.Field, SearchString = "private DateTime _date" });

            companyStatCS.AddChildren(companyStat);
            companyStatCS.AddChildren(new SolutionNode() { FileName = companyStatFileName, Name = "CompanyStatList", TypeNode = TypeNode.Class, SearchString = "public class CompanyStatList :" });

            #endregion

            #region CustomAppointment.cs

            string customAppointmentFileName = "CustomAppointment.txt";

            var customAppointmentCS = new SolutionNode() { FileName = customAppointmentFileName, Name = "CustomAppointment.cs", TypeNode = TypeNode.File };

            #region customAppointment
            var customAppointment = new SolutionNode() { FileName = customAppointmentFileName, Name = "CustomAppointment", TypeNode = TypeNode.Class, SearchString = "public class CustomAppointment :" };

            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fStart", TypeName = "DateTime", TypeNode = TypeNode.Field, SearchString = "e fStart;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fEnd", TypeName = "DateTime", TypeNode = TypeNode.Field, SearchString = "e fEnd;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fSubject", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "g fSubject;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fStatus", TypeName = "int", TypeNode = TypeNode.Field, SearchString = "t fStatus;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fDescription", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "g fDescription;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fLabel", TypeName = "long", TypeNode = TypeNode.Field, SearchString = "g fLabel;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fLocation", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "g fLocation;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fAllday", TypeName = "bool", TypeNode = TypeNode.Field, SearchString = "l fAllday;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fEventType", TypeName = "int", TypeNode = TypeNode.Field, SearchString = "t fEventType;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fRecurrenceInfo", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "g fRecurrenceInfo;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fReminderInfo", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "g fReminderInfo;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "fOwnerId", TypeName = "object", TypeNode = TypeNode.Field, SearchString = "t fOwnerId;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "events", TypeName = "CustomEventList", TypeNode = TypeNode.Field, SearchString = "ist events;" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "committed", TypeName = "bool", TypeNode = TypeNode.Field, SearchString = "l committed" });

            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "CustomAppointment(CustomEventList)", TypeNode = TypeNode.Method, SearchString = "public CustomAppointment(CustomEventList events)", SearchName = "CustomAppointment" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "OnListChanged()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "private void OnListChanged()", SearchName = "OnListChanged" });

            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "StartTime", TypeName = "DateTime", TypeNode = TypeNode.Property, SearchString = "public DateTime StartTime" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "EndTime", TypeName = "DateTime", TypeNode = TypeNode.Property, SearchString = "public DateTime EndTime" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "Subject", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string Subject" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "Status", TypeName = "int", TypeNode = TypeNode.Property, SearchString = "public int Status" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "Description", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string Description" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "Label", TypeName = "long", TypeNode = TypeNode.Property, SearchString = "public long Label " });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "Location", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string Location" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "AllDay", TypeName = "bool", TypeNode = TypeNode.Property, SearchString = "public bool AllDay" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "EventType", TypeName = "int", TypeNode = TypeNode.Property, SearchString = "public int EventType" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "RecurrenceInfo", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string RecurrenceInfo" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "ReminderInfo", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string ReminderInfo" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "OwnerId", TypeName = "object", TypeNode = TypeNode.Property, SearchString = "public object OwnerId" });

            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "BeginEdit()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public void BeginEdit()", SearchName = "BeginEdit" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "CancelEdit()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public void CancelEdit()", SearchName = "CancelEdit" });
            customAppointment.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "EndEdit()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public void EndEdit()", SearchName = "EndEdit" });

            #endregion

            #region customAppointment
            var customEventList = new SolutionNode() { FileName = customAppointmentFileName, Name = "CustomEventList", TypeNode = TypeNode.Class, SearchString = "public class CustomEventList :" };
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "this[int]", TypeName = "CustomAppointment", TypeNode = TypeNode.Property, SearchString = "public CustomAppointment this[int idx]", SearchName = "this" });

            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "Clear()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public new void Clear()", SearchName = "Clear" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "Add(CustomAppointment)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public void Add(CustomAppointment appointment)", SearchName = "Add" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "IndexOf(CustomAppointment)", TypeName = "int", TypeNode = TypeNode.Method, SearchString = "public int IndexOf(CustomAppointment appointment)", SearchName = "IndexOf" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "AddNew()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public object AddNew()", SearchName = "AddNew" });

            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "AllowEdit", TypeName = "bool", TypeNode = TypeNode.Property, SearchString = "public bool AllowEdit" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "AllowNew", TypeName = "bool", TypeNode = TypeNode.Property, SearchString = "public bool AllowNew" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "AllowRemove", TypeName = "bool", TypeNode = TypeNode.Property, SearchString = "public bool AllowRemove" });

            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "listChangedHandler", TypeName = "ListChangedEventHandler(object, ListChangedEventArgs)", TypeNode = TypeNode.Field, SearchString = "private ListChangedEventHandler listChangedHandler" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "ListChanged", TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", TypeNode = TypeNode.Event, SearchString = "public event ListChangedEventHandler ListChanged" });

            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "OnListChanged(ListChangedEventArgs)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "internal void OnListChanged(ListChangedEventArgs args)", SearchName = "OnListChanged" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "OnRemoveComplete(int, object)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "protected override void OnRemoveComplete(int index, object value)", SearchName = "OnRemoveComplete" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "OnInsertComplete(int, object)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "protected override void OnInsertComplete(int index, object value)", SearchName = "OnInsertComplete" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "AddIndex(PropertyDescriptor)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public void AddIndex(PropertyDescriptor pd)", SearchName = "AddIndex" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "ApplySort(PropertyDescriptor, ListSortDirection)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public void ApplySort(PropertyDescriptor pd, ListSortDirection dir)", SearchName = "ApplySort" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "Find(PropertyDescriptor, object)", TypeName = "int", TypeNode = TypeNode.Method, SearchString = "public int Find(PropertyDescriptor property, object key)", SearchName = "Find" });

            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "IsSorted", TypeName = "bool", TypeNode = TypeNode.Property, SearchString = "public bool IsSorted" });

            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "RemoveIndex(PropertyDescriptor)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public void RemoveIndex(PropertyDescriptor pd)", SearchName = "RemoveIndex" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "RemoveSort()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public void RemoveSort()", SearchName = "RemoveSort" });

            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "SortDirection", TypeName = "ListSortDirection", TypeNode = TypeNode.Property, SearchString = "public ListSortDirection SortDirection", SearchName= " SortDirection" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "SortProperty", TypeName = "PropertyDescriptor", TypeNode = TypeNode.Property, SearchString = "public PropertyDescriptor SortProperty" });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "SupportsChangeNotification", TypeName = "bool", TypeNode = TypeNode.Property, SearchString = "public bool SupportsChangeNotification " });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "SupportsSearching", TypeName = "bool", TypeNode = TypeNode.Property, SearchString = "public bool SupportsSearching " });
            customEventList.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "SupportsSorting", TypeName = "bool", TypeNode = TypeNode.Property, SearchString = "public bool SupportsSorting" });
            #endregion

            #region CustomResource
            var customResource = new SolutionNode() { FileName = customAppointmentFileName, Name = "CustomResource", TypeNode = TypeNode.Class, SearchString = "public class CustomResource {" };
            customResource.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "name", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "string name;" });
            customResource.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "res_id", TypeName = "int", TypeNode = TypeNode.Field, SearchString = "int res_id;" });
            customResource.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "res_color", TypeName = "Color", TypeNode = TypeNode.Field, SearchString = "Color res_color;" });

            customResource.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "Name", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string Name" });
            customResource.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "ResID", TypeName = "int", TypeNode = TypeNode.Property, SearchString = "public int ResID" });
            customResource.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "ResColor", TypeName = "Color", TypeNode = TypeNode.Property, SearchString = "public Color ResColor" });

            customResource.AddChildren(new SolutionNode() { FileName = customAppointmentFileName, Name = "CustomResource()", TypeNode = TypeNode.Method, SearchString = "public CustomResource() {", SearchName = "CustomResource" });
            #endregion

            customAppointmentCS.AddChildren(customAppointment);
            customAppointmentCS.AddChildren(customEventList);
            customAppointmentCS.AddChildren(customResource);

            #endregion

            #region DataGenerator.cs

            string dataGeneratorFileName = "DataGenerator.txt";

            var dataGeneratorCS = new SolutionNode() { FileName = dataGeneratorFileName, Name = "DataGenerator.cs", TypeNode = TypeNode.File };
            var dataGenerator = new SolutionNode() { FileName = dataGeneratorFileName, Name = "DataGenerator", TypeNode = TypeNode.Class, SearchString = "s DataGenerator" };

            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "GenerateOpportunities()", TypeName = "BindingList<Opportunity>", TypeNode = TypeNode.Method, SearchString = "> GenerateOpportunities() {", SearchName = "GenerateOpportunities" });
            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "Projects", TypeName = "List<ProjectTuple>", TypeNode = TypeNode.Field, SearchString = "> Projects" });

            var projectTuple = new SolutionNode() { FileName = dataGeneratorFileName, Name = "ProjectTuple", TypeNode = TypeNode.Class, SearchString = "ss ProjectTuple" };
            projectTuple.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "public ProjectTuple(string, string)", TypeNode = TypeNode.Method, SearchString = "ProjectTuple(string name", SearchName = "ProjectTuple" });
            projectTuple.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "Name", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "ng Name;" });
            projectTuple.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "Technology", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "ng Technology;" });
            dataGenerator.AddChildren(projectTuple);

            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "Descriptions", TypeName = "Dictionary<string, string>", TypeNode = TypeNode.Field, SearchString = "g> Descriptions" });
            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "Amounts", TypeName = "List<int>", TypeNode = TypeNode.Field, SearchString = "t> Amounts" });
            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "Companies", TypeName = "List<string>", TypeNode = TypeNode.Field, SearchString = "g> Companies" });
            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "DocNames", TypeName = "List<string>", TypeNode = TypeNode.Field, SearchString = "g> DocNames" });
            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "DocPath", TypeName = "string", TypeNode = TypeNode.Field, SearchString = @"DocPath = ""S" });

            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "CreateSampleDocuments()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "d CreateSampleDocuments", SearchName = "CreateSampleDocuments" });
            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "GenerateActivities(Opportunity, int)", TypeName = "List<Activity>", TypeNode = TypeNode.Method, SearchString = "y> GenerateActivities", SearchName = "GenerateActivities" });
            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "GenerateKeywordStats()", TypeName = "BindingList<KeywordStat>", TypeNode = TypeNode.Method, SearchString = "t> GenerateKeywordStats", SearchName = "GenerateKeywordStats" });
            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "GenerateTeamMembers()", TypeName = "BindingList<TeamMember>", TypeNode = TypeNode.Method, SearchString = "r> GenerateTeamMembers", SearchName = "GenerateTeamMembers" });

            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "AccountOwnerInfo", TypeName = "List<List<string>>", TypeNode = TypeNode.Field, SearchString = @"g>> AccountOwnerInfo" });

            dataGenerator.AddChildren(new SolutionNode() { FileName = dataGeneratorFileName, Name = "GenerateAppointments(IList<TeamMember>)", TypeName = "CustomEventList", TypeNode = TypeNode.Method, SearchString = "st GenerateAppointments", SearchName = "GenerateAppointments" });

            dataGeneratorCS.AddChildren(dataGenerator);
            #endregion

            #region DataProvider.cs

            string dataProviderFileName = "DataProvider.txt";

            var dataProviderCS = new SolutionNode() { FileName = dataProviderFileName, Name = "DataProvider.cs", TypeNode = TypeNode.File };
            var dataProvider = new SolutionNode() { FileName = dataProviderFileName, Name = "DataProvider", TypeNode = TypeNode.Class, SearchString = "s DataProvider" };

            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "instance", TypeName = "DataProvider", TypeNode = TypeNode.Field, SearchString = "private static DataProvider instance"});

            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "DataProvider()", TypeNode = TypeNode.Method, SearchString = "private DataProvider() { }", SearchName= "DataProvider" });

            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "Instance", TypeName= "DataProvider", TypeNode = TypeNode.Property, SearchString = "public static DataProvider Instance {" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "Opportunities", TypeName = "BindingList<Opportunity>", TypeNode = TypeNode.Property, SearchString = "public BindingList<Opportunity> Opportunities { get; set; }" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "NewestOpportunities", TypeName = "BindingList<Opportunity>", TypeNode = TypeNode.Property, SearchString = "public BindingList<Opportunity> NewestOpportunities { get; set; }" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "Activities", TypeName = "BindingList<Activity>", TypeNode = TypeNode.Property, SearchString = "public BindingList<Activity> Activities { get; set; }" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "KeywordStats", TypeName = "BindingList<KeywordStat>", TypeNode = TypeNode.Property, SearchString = " public BindingList<KeywordStat> KeywordStats { get; set; }" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "TeamMembers", TypeName = "BindingList<TeamMember>", TypeNode = TypeNode.Property, SearchString = "public BindingList<TeamMember> TeamMembers { get; set; }" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "GeneralStats", TypeName = "GeneralStats", TypeNode = TypeNode.Property, SearchString = "public GeneralStats GeneralStats { get; set; }" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "Appointments", TypeName = "CustomEventList", TypeNode = TypeNode.Property, SearchString = "public CustomEventList Appointments { get; set; }" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "NewestAppointments", TypeName = "BindingList<CustomAppointment>", TypeNode = TypeNode.Property, SearchString = "public BindingList<CustomAppointment> NewestAppointments { get; set; }" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "Technologies", TypeName = "IList<string>", TypeNode = TypeNode.Property, SearchString = "public IList<string> Technologies {" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "LeadSources", TypeName = "IList<string>", TypeNode = TypeNode.Property, SearchString = "public IList<string> LeadSources {" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "OpportunityTypes", TypeName = "IList<string>", TypeNode = TypeNode.Property, SearchString = "public IList<string> OpportunityTypes {" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "Stages", TypeName = "IList<StageTuple>", TypeNode = TypeNode.Property, SearchString = "public IList<StageTuple> Stages {" });

            var stageTuple = new SolutionNode() { FileName = dataProviderFileName, Name = "StageTuple", TypeNode = TypeNode.Class, SearchString = "ss StageTuple {" };
            stageTuple.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "stage", TypeName = "Stage", TypeNode = TypeNode.Field, SearchString = "Stage stage;" });
            stageTuple.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "name", TypeName = "string", TypeNode = TypeNode.Field, SearchString = " string name;" });
            stageTuple.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "StageTuple(Stage, string)", TypeNode = TypeNode.Method, SearchString = "public StageTuple(Stage stage, string name) {", SearchName = "StageTuple" });
            stageTuple.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "Stage", TypeName = "Stage", TypeNode = TypeNode.Property, SearchString = "public Stage Stage {" });
            stageTuple.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "Name", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string Name {" });
            dataProvider.AddChildren(stageTuple);

            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "InitializeData()", TypeName="void", TypeNode = TypeNode.Method, SearchString = "private void InitializeData() {", SearchName = "InitializeData" });

            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "CreateNewOpportunity()", TypeName = "Opportunity", TypeNode = TypeNode.Method, SearchString = "public Opportunity CreateNewOpportunity() {", SearchName = "CreateNewOpportunity" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "CreateNewActivity(Opportunity)", TypeName = "Activity", TypeNode = TypeNode.Method, SearchString = "public Activity CreateNewActivity(Opportunity opp) {", SearchName = "CreateNewActivity" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "Opportunities_ListChanged(object, ListChangedEventArgs)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "void Opportunities_ListChanged(object sender, ListChangedEventArgs e) {", SearchName = "Opportunities_ListChanged" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "RefreshNewestOppsList()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "void RefreshNewestOppsList() {", SearchName = "RefreshNewestOppsList" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "RefreshGeneralStats()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "void RefreshGeneralStats() {", SearchName = "RefreshGeneralStats" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "GetOpenOppsCount(int)", TypeName = "int", TypeNode = TypeNode.Method, SearchString = "int GetOpenOppsCount(int teamMemberId) {", SearchName = "GetOpenOppsCount" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = " Appointments_ListChanged(object, ListChangedEventArgs)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "void Appointments_ListChanged(object sender, ListChangedEventArgs e) {", SearchName = "Appointments_ListChanged" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "RefreshNewAppointmentsList()", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public void RefreshNewAppointmentsList() {", SearchName = "RefreshNewAppointmentsList" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "RefreshCompanyStats(DateTime, DateTime, CompanyStatList)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public static void RefreshCompanyStats(DateTime startDate, DateTime endDate, CompanyStatList stats) {", SearchName = "RefreshCompanyStats" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "RefreshCategoryStats(DateTime, DateTime, CategoryStatList)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public static void RefreshCategoryStats(DateTime startDate, DateTime endDate, CategoryStatList stats) {", SearchName = "RefreshCategoryStats" });
            dataProvider.AddChildren(new SolutionNode() { FileName = dataProviderFileName, Name = "RefreshTeamStats(DateTime, DateTime, TeamMemberStatList, TeamMemberStatDetailList)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "public static void RefreshTeamStats(DateTime startDate, DateTime endDate, TeamMemberStatList stats, TeamMemberStatDetailList statDetails) {", SearchName = "RefreshTeamStats" });

            dataProviderCS.AddChildren(dataProvider);
            #endregion

            #region KeywordStat.cs

            string keywordStatFileName = "KeywordStat.txt";

            var keywordStatCS = new SolutionNode() { FileName = keywordStatFileName, Name = "KeywordStat.cs", TypeNode = TypeNode.File };
            var keywordStat = new SolutionNode() { FileName = keywordStatFileName, Name = "KeywordStat", TypeNode = TypeNode.Class, SearchString = "public class KeywordStat :" };

            keywordStat.AddChildren(new SolutionNode() { FileName = keywordStatFileName, Name = "Keyword", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "string Keyword {" });
            keywordStat.AddChildren(new SolutionNode() { FileName = keywordStatFileName, Name = "Total", TypeName = "int", TypeNode = TypeNode.Property, SearchString = " int Total {" });
            keywordStat.AddChildren(new SolutionNode() { FileName = keywordStatFileName, Name = "Image", TypeName = "Bitmap", TypeNode = TypeNode.Property, SearchString = " Bitmap Image " });

            keywordStat.AddChildren(new SolutionNode() { FileName = keywordStatFileName, Name = "PropertyChanged", TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", TypeNode = TypeNode.Event, SearchString = "public event PropertyChangedEventHandler PropertyChanged;" });
            keywordStat.AddChildren(new SolutionNode() { FileName = keywordStatFileName, Name = "InvokePropertyChanged(string)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "private void InvokePropertyChanged(string name)", SearchName = "InvokePropertyChanged" });

            keywordStat.AddChildren(new SolutionNode() { FileName = keywordStatFileName, Name = "_keyword", TypeName = "string", TypeNode = TypeNode.Field, SearchString = " string _keyword;" });
            keywordStat.AddChildren(new SolutionNode() { FileName = keywordStatFileName, Name = "_total", TypeName = "int", TypeNode = TypeNode.Field, SearchString = "int _total;" });
            keywordStat.AddChildren(new SolutionNode() { FileName = keywordStatFileName, Name = "_image", TypeName = "Bitmap", TypeNode = TypeNode.Field, SearchString = " Bitmap _image" });

            keywordStatCS.AddChildren(keywordStat);
            keywordStatCS.AddChildren(new SolutionNode() { FileName = keywordStatFileName, Name = "KeywordStatList", TypeNode = TypeNode.Class, SearchString = " public class KeywordStatList :" });

            #endregion

            #region Opportunity.cs

            string opportunityFileName = "Opportunity.txt";

            var opportunityCS = new SolutionNode() { FileName = opportunityFileName, Name = "Opportunity.cs", TypeNode = TypeNode.File };

            var state = new SolutionNode() { FileName = opportunityFileName, Name = "Stage", TypeNode = TypeNode.Enum, SearchString = "public enum Stage {" };
            state.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "Lead", TypeNode = TypeNode.EnumElement, SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled" });
            state.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "Qualification", TypeNode = TypeNode.EnumElement, SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled" });
            state.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "NeedsAnalysis", TypeNode = TypeNode.EnumElement, SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled" });
            state.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "Proposed", TypeNode = TypeNode.EnumElement, SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled" });
            state.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "Negotiation", TypeNode = TypeNode.EnumElement, SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled" });
            state.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "Closed", TypeNode = TypeNode.EnumElement, SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled" });
            state.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "Cancelled", TypeNode = TypeNode.EnumElement, SearchString = "Lead, Qualification, NeedsAnalysis, Proposed, Negotiation, Closed, Cancelled" });
            opportunityCS.AddChildren(state);

            var opportunity = new SolutionNode() { FileName = opportunityFileName, Name = "Opportunity", TypeNode = TypeNode.Class, SearchString = "public class Opportunity {" };

            opportunity.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "ID", TypeName = "int", TypeNode = TypeNode.Property, SearchString = "public int ID {" });
            opportunity.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "Name", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string Name {" });
            opportunity.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "AccountName", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string AccountName {" });

            opportunity.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "_id", TypeName = "int", TypeNode = TypeNode.Field, SearchString = "int _id;" });
            opportunity.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "_name", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "string _name;" });
            opportunity.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "_accountName", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "string _accountName;" });

            opportunityCS.AddChildren(opportunity);
            opportunityCS.AddChildren(new SolutionNode() { FileName = opportunityFileName, Name = "OpportunityList", TypeNode = TypeNode.Class, SearchString = "public class OpportunityList :" });

            #endregion

            #region TeamMember.cs

            string teamMemberFileName = "TeamMember.txt";

            var teamMemberCS = new SolutionNode() { FileName = teamMemberFileName, Name = "TeamMember.cs", TypeNode = TypeNode.File };
            var teamMember = new SolutionNode() { FileName = teamMemberFileName, Name = "TeamMember", TypeNode = TypeNode.Class, SearchString = "public class TeamMember :" };

            teamMember.AddChildren(new SolutionNode() { FileName = teamMemberFileName, Name = "Id", TypeName = "int", TypeNode = TypeNode.Property, SearchString = "public int Id" });
            teamMember.AddChildren(new SolutionNode() { FileName = teamMemberFileName, Name = "FullName", TypeName = "string", TypeNode = TypeNode.Property, SearchString = "public string FullName" });
            teamMember.AddChildren(new SolutionNode() { FileName = teamMemberFileName, Name = "Photo", TypeName = "Bitmap", TypeNode = TypeNode.Property, SearchString = "public Bitmap Photo" });
            teamMember.AddChildren(new SolutionNode() { FileName = teamMemberFileName, Name = "PhotoSmall", TypeName = "Bitmap", TypeNode = TypeNode.Property, SearchString = "public Bitmap PhotoSmall" });

            teamMember.AddChildren(new SolutionNode() { FileName = teamMemberFileName, Name = "PropertyChanged", TypeName = "PropertyChangedEventHandler(object, PropertyChangedEventArgs)", TypeNode = TypeNode.Event, SearchString = "public event PropertyChangedEventHandler PropertyChanged;" });
            teamMember.AddChildren(new SolutionNode() { FileName = teamMemberFileName, Name = "InvokePropertyChanged(string)", TypeName = "void", TypeNode = TypeNode.Method, SearchString = "private void InvokePropertyChanged(string name)", SearchName = "InvokePropertyChanged" });

            teamMember.AddChildren(new SolutionNode() { FileName = teamMemberFileName, Name = "_id", TypeName = "int", TypeNode = TypeNode.Field, SearchString = "private int _id;" });
            teamMember.AddChildren(new SolutionNode() { FileName = teamMemberFileName, Name = "_fullName", TypeName = "string", TypeNode = TypeNode.Field, SearchString = "private string _fullName;" });
            teamMember.AddChildren(new SolutionNode() { FileName = teamMemberFileName, Name = "_photo", TypeName = "Bitmap", TypeNode = TypeNode.Field, SearchString = "private Bitmap _photo;" });
            teamMember.AddChildren(new SolutionNode() { FileName = teamMemberFileName, Name = "_photoSmall", TypeName = "Bitmap", TypeNode = TypeNode.Field, SearchString = "private Bitmap _photoSmall;" });

            teamMemberCS.AddChildren(teamMember);


            #endregion

            Nodes = new List<SolutionNode> {
                activityCS,
                categoryStatCS,
                companyStatCS,
                customAppointmentCS,
                dataGeneratorCS,
                dataProviderCS,
                keywordStatCS,
                opportunityCS,
                teamMemberCS
            };
        }

    }
}
