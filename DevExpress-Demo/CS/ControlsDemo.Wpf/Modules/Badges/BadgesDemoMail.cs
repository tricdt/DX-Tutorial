using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
#if DXCORE3
using Microsoft.EntityFrameworkCore;
using DevExpress.Internal;
#else
using System.Data.Entity;
#endif
using System.IO;
using System.Linq;
using System.Reflection;
using DevExpress.DevAV;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.XtraRichEdit;

namespace ControlsDemo {
#if !DXCORE3
    public class DevAVDbContext : DevAVDbBase {
        static DevAVDbContext() {
            Database.SetInitializer<DevAVDbContext>(null);
        }
    }
#endif
    public static class DevAVHelper {
        static Dictionary<string, Employee> employees = null;
        static Dictionary<string, Employee> Employees {
            get {
                if(employees == null) {
#if DXCORE3
                    SetFilePath();
                    var devAvDb = new DevExpress.DevAV.DevAVDb(string.Format("Data Source={0}", filePath));
                    devAvDb.Pictures.Load();
#else
                    var devAvDb = new DevAVDbContext(); 
#endif
                    devAvDb.Employees.Load();
                    employees = devAvDb.Employees.Local.ToDictionary(x => x.Email);
                }
                return employees;
            }
        }
#if DXCORE3
        static string filePath;
        static void SetFilePath() {
            if(filePath == null)
                filePath = DataDirectoryHelper.GetFile("devav.sqlite3", DataDirectoryHelper.DataFolderName);
            try {
                var attributes = File.GetAttributes(filePath);
                if(attributes.HasFlag(FileAttributes.ReadOnly)) {
                    File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);
                }
            } catch { }
        }
#endif
        internal static DevExpress.DevAV.Employee GetEmployee(string email) {
            Employee result;
            if (Employees.TryGetValue(email, out result))
                return result;
            return null;
        }
    }
    public static class MailItems {
        internal static DataSet DataSet { get; private set; }
        static ObservableCollection<Message> messages = null;
        static DataTable MailTable {
            get { return CreateDataTable("Messages"); }
        }
        static DataTable CreateDataTable(string table) {
            if (DataSet == null)
            {
                DataSet = new DataSet();
                Assembly assembly = typeof(Badges).Assembly;
                using (Stream stream = assembly.GetManifestResourceStream(DemoHelper.GetPath("ControlsDemo.Modules.Badges.", assembly) + "MailDevAv.xml"))
                {
                    DataSet.ReadXml(stream);
                }

                DataSet.Relations.Add(DataSet.Tables["Employees"].Columns["Email"], DataSet.Tables["Messages"].Columns["From"]);
            }

            return DataSet.Tables[table];
        }
        public static ObservableCollection<Message> Messages {
            get {
                try {
                    if (messages == null) {
                        messages = new ObservableCollection<Message>();
                        DataTable tbl = MailTable;
                        if (tbl != null) {
                            foreach (DataRow row in tbl.Rows)
                                messages.Add(new Message(row));
                        }
                    }
                }
                catch (Exception) {
                    messages = new ObservableCollection<Message>();
                }

                return messages;
            }
        }
    }

    public class EmployeeMessages {
        public EmployeeMessages(MailEmployee mailEmployee, IEnumerable<Message> messages) {
            Employee = mailEmployee;
            Messages = messages.ToList();
            UrgentMessagesCount = Messages.Where(x => x.Priority > 1).Count();
        }
        public MailEmployee Employee { get; set; }
        public List<Message> Messages { get; set; }
        public int UrgentMessagesCount { get; set; }
    }
    public class MailEmployee {
        protected bool Equals(MailEmployee other) {
            return Email == other.Email;
        }
        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MailEmployee)obj);
        }
        public override int GetHashCode() {
            return (FullName != null ? FullName.GetHashCode() : 0);
        }
        public static bool operator ==(MailEmployee left, MailEmployee right) {
            return Equals(left, right);
        }
        public static bool operator !=(MailEmployee left, MailEmployee right) {
            return !Equals(left, right);
        }
        static Random statusGenerator = new Random();
        public MailEmployee(DataRow row) {
            FirstName = string.Format("{0}", row["FirstName"]);
            LastName = string.Format("{0}", row["LastName"]);
            FullName = string.Format("{0} {1}", FirstName, LastName);
            Gender = string.Format("{0}", row["Gender"]);
            Email = string.Format("{0}", row["Email"]);
            Status = (MailEmployeeStatus)statusGenerator.Next(0,2);
            var employee = DevAVHelper.GetEmployee(string.Format("{0}", row["Email"]));
            if(employee != null) {
                var convertedDept = Enum.GetName(typeof(EmployeeDepartment), employee.Department);
                var fieldDept = typeof(EmployeeDepartment).GetField(convertedDept, BindingFlags.Static | BindingFlags.Public);

                Department = DataAnnotationsAttributeHelper.GetFieldDisplayName(fieldDept) ?? convertedDept;
            }
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string Department { get; private set; }
        public MailEmployeeStatus Status { get; private set; }
        public string Initials {
            get { return String.Format("{0}{1}", FirstName.First(), LastName.First()); }
        }
        public string Gender { get; private set; }
    }

    public class Message {
        public Message(DataRow row) {
            Employee = new MailEmployee(row.GetParentRow(MailItems.DataSet.Relations[0]));
            Received = DateTime.Now.AddDays((int)row["Day"]);
            Subject = string.Format("{0}", row["Subject"]);
            HasAttachment = (bool)row["HasAttachment"];
            Read = Delay > TimeSpan.FromHours(6);
            if (Delay > TimeSpan.FromHours(50) && Delay < TimeSpan.FromHours(100))
                Read = false;
            Text = string.Format("{0}", row["Text"]);
            Priority = CalcPriority();
            if (!(row["CategoryID"] is DBNull))
                MailFolder = (MailFolder)row["CategoryID"];
        }

        public MailEmployee Employee { get; set; }

        public DateTime Received { get; set; }

        public string Subject { get; set; }

        public bool HasAttachment { get; set; }

        public bool Read { get; set; }

        public string Text { get; set; }

        public int Priority { get; set; }

        public MailFolder MailFolder { get; set; }

        string plainText;
        public string PlainText {
            get {
                if (this.plainText == null)
                    this.plainText = PlainTextProvider.GetPlainText(Text);
                return this.plainText;
            }
        }
        internal TimeSpan Delay {
            get { return DateTime.Now - Received; }
        }

        int CalcPriority() {
            if (Subject.IndexOf("Review") >= 0 || Subject.IndexOf("Important") >= 0) return 2;
            if (Subject.IndexOf("FW:") >= 0 && Delay > TimeSpan.FromHours(48)) return 0;
            return 1;
        }
    }

    public enum MailFolder {
        All = 0
      , General
      , Management
      , IT
      , Sales
      , Support
      , Engineering
      , HR
      , Design
    };

    public enum MailEmployeeStatus {
        Available,
        Away,
        Busy
    }

    public static class PlainTextProvider {
        static RichEditDocumentServer rich = new RichEditDocumentServer();

        public static string GetPlainText(string mhtText) {
            rich.MhtText = mhtText;
            return string.IsNullOrEmpty(rich.Text) ? "" : rich.Text.TrimStart().Substring(0, Math.Min(100, mhtText.Length)).Replace('\r', ' ').Replace('\n', ' ');
        }
    }
}
