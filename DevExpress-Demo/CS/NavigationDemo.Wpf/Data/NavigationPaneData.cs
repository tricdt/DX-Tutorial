using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using DevExpress.DemoData.Helpers;
using DevExpress.Utils;
using DevExpress.Xpf.DemoBase.DataClasses;

namespace NavigationDemo {
    public static class NavigationPaneData {
        public static Random rnd = new Random();
        static string[] users = new string[] {"Peter Dolan", "Ryan Fischer", "Richard Fisher",
                                                 "Tom Hamlett", "Mark Hamilton", "Steve Lee", "Jimmy Lewis", "Jeffrey W McClain",
                                                 "Andrew Miller", "Dave Murrel", "Bert Parkins", "Mike Roller", "Ray Shipman",
                                                 "Paul Bailey", "Brad Barnes", "Carl Lucas", "Jerry Campbell"};
        static string[] subject = new string[] {
                                                   "Implementing DevExpress MasterView control into Accounting System.",
                                                   "Web Edition: Data Entry Page. The issue with date validation.",
                                                   "Payables Due Calculator. It is ready for testing.",
                                                   "Web Edition: Search Page. It is ready for testing.",
                                                   "Main Menu: Duplicate Items. Somebody has to review all menu items in the system.",
                                                   "Receivables Calculator. Where can I found the complete specs",
                                                   "Ledger: Inconsistency. Please fix it.",
                                                   "Receivables Printing. It is ready for testing.",
                                                   "Screen Redraw. Somebody has to look at it.",
                                                   "Email System. What library we are going to use?",
                                                   "Adding New Vendors Fails. This module doesn't work completely!",
                                                   "History. Will we track the sales history in our system?",
                                                   "Main Menu: Add a File menu. File menu is missed!!!",
                                                   "Currency Mask. The current currency mask in completely inconvinience.",
                                                   "Drag & Drop. In the schedule module drag & drop is not available.",
                                                   "Data Import. What competitors databases will we support?",
                                                   "Reports. The list of incomplete reports.",
                                                   "Data Archiving. This features is still missed in our application",
                                                   "Email Attachments. How to add the multiple attachment? I did not find a way to do it.",
                                                   "Check Register. We are using different paths for different modules.",
                                                   "Data Export. Our customers asked for export into Excel"};

        static bool GetBoolean() {
            int ret = rnd.Next(7);
            return ret > 4;
        }
        static bool GetIsRead() {
            int ret = rnd.Next(7);
            return ret > 1;
        }
        static int GetImportance() {
            int ret = rnd.Next(7);
            if(ret > 2) ret = 1;
            return ret;
        }
        static DateTime GetSent() {
            DateTime ret = DateTime.Now;
            int r = rnd.Next(12);
            if(r > 1) ret = ret.AddDays(-rnd.Next(50));
            ret = ret.AddMinutes(-rnd.Next(ret.Minute + ret.Hour * 60 + 360));
            return ret;
        }
        static DateTime GetReceived(DateTime sent) {
            return sent.AddMinutes(10 + rnd.Next(120));
        }
        static string GetSubject() {
            return subject[rnd.Next(subject.Length - 1)];
        }
        static string GetRandomUser() {
            return users[rnd.Next(users.Length - 2)];
        }
        static string GetFixedUser() {
            return users[users.Length - 1];
        }

        public static IList<JournalItem> JournalData { get { return journalData; } }
        public static IList<NotesItem> NotesData { get { return notesData; } }
        public static IList<TasksItem> TasksData { get { return tasksData; } }
        public static IList<ContactItem> ContactsData { get { return contactsData; } }
        public static IDictionary<NavigationId, IList<MailItem>> MailData { get { return mailData; } }

        static IList<JournalItem> journalData = CreateJournalData();
        static IList<NotesItem> notesData = CreateNotesData();
        static IList<TasksItem> tasksData = CreateTasksData();
        static IList<ContactItem> contactsData = CreateContactsData();
        static Dictionary<NavigationId, IList<MailItem>> mailData = CreateMailData();

        static IList<JournalItem> CreateJournalData() {
            var result = new List<JournalItem>();
            for(int i = 0; i < 10; i++)
                result.Add(new JournalItem(GetSent(), GetSubject()));
            return result;
        }
        static IList<NotesItem> CreateNotesData() {
            var result = new List<NotesItem>();
            for(int i = 0; i < 10; i++)
                result.Add(new NotesItem(GetRandomUser(), GetSubject()));
            return result;
        }
        static IList<TasksItem> CreateTasksData() {
            var result = new List<TasksItem>();
            for(int i = 0; i < 10; i++)
                result.Add(new TasksItem(GetIcon("Tasks"), GetBoolean(), GetSubject(), GetSent()));
            return result;
        }
        static Dictionary<NavigationId, IList<MailItem>> CreateMailData() {
            var result = new Dictionary<NavigationId, IList<MailItem>>();
            result.Add(NavigationId.Inbox, CreateMailCollection());
            result.Add(NavigationId.Outbox, CreateEmptyMailCollection());
            result.Add(NavigationId.SentItems, CreateMailCollection(allItemsRead: true, fromFixedUser: true));
            result.Add(NavigationId.DeletedItems, CreateMailCollection(allItemsRead: true));
            result.Add(NavigationId.Drafts, CreateEmptyMailCollection());
            return result;
        }
        static IList<MailItem> CreateMailCollection(bool? allItemsRead = null, bool fromFixedUser = false) {
            var result = new List<MailItem>();
            for(int i = 0; i < 80; i++) {
                DateTime sent = GetSent();
                result.Add(new MailItem(GetImportance(), GetBoolean(), allItemsRead ?? GetIsRead(), GetSubject(), fromFixedUser ? GetFixedUser() : GetRandomUser(), fromFixedUser ? GetRandomUser() : GetFixedUser(), sent, GetReceived(sent)));
            }
            return result;
        }
        static IList<ContactItem> CreateContactsData() {
            return new List<ContactItem>(EmployeesWithPhotoData.DataSource.Take(15)
                .Select(x => new ContactItem() { FirstName = x.FirstName, LastName = x.LastName, Email = x.EmailAddress }));
        }
        static IList<MailItem> CreateEmptyMailCollection() {
            return new List<MailItem>();
        }
        public static Uri GetIcon(string id) {
            return new Uri("/NavigationDemo;component/Images/Navigation/" + id + ".svg", UriKind.RelativeOrAbsolute);
        }
    }
    public class JournalItem {
        public JournalItem() {
            Date = DateTime.Now;
            Time = DateTime.Now;
        }
        public JournalItem(DateTime dateTime, string description) {
            Date = dateTime;
            Time = dateTime;
            Description = description;
        }
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }

        public string Description { get; set; }
    }
    public class NotesItem {
        public NotesItem() { }
        public NotesItem(string name, string description) {
            Name = name;
            Description = description;
        }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class TasksItem {
        public TasksItem() {
            Image = NavigationPaneData.GetIcon("Tasks");
            Date = DateTime.Now;
        }
        public TasksItem(Uri image, bool check, string subject, DateTime date) {
            Image = image;
            Check = check;
            Subject = subject;
            Date = date;
        }
        public Uri Image { get; set; }
        public bool Check { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
    }

    public class MailItem {
        public MailItem(int importance, bool hasAttachment, bool isRead, string subject, string from, string to, DateTime sent, DateTime receive) {
            Importance = importance;
            HasAttachment = hasAttachment;
            IsRead = isRead;
            Image = NavigationPaneData.GetIcon(IsRead ? "TextBox" : "Mail");
            Subject = subject;
            From = from;
            To = to;
            Sent = sent;
            Receive = receive;
        }
        public int Importance { get; set; }
        public bool HasAttachment { get; set; }
        public bool IsRead { get; set; }
        public Uri Image { get; set; }
        public string Subject { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Sent { get; set; }
        public DateTime Receive { get; set; }
    }
    public class ContactItem {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
