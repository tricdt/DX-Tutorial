using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using DevExpress.DemoData.Models;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.XtraRichEdit;
using DevExpress.Mvvm;
using System.Windows.Data;
using System.Globalization;
using System.IO;

namespace GridDemo {
    public static class MailItems {
        internal static DataSet DataSet { get; private set; }
        static ObservableCollection<Message> messages = null;
        static DataTable MailTable {
            get { return CreateDataTable("Messages"); }
        }
        static DataTable CreateDataTable(string table) {
            if(DataSet == null) {
                DataSet = new DataSet();
                Assembly assembly = typeof(StockItems).Assembly;
                using(Stream stream = assembly.GetManifestResourceStream(DemoHelper.GetPath("GridDemo.Data.", assembly) + "MailDevAv.xml")) {
                    DataSet.ReadXml(stream);
                }
                DataSet.Relations.Add(DataSet.Tables["Employees"].Columns["Email"], DataSet.Tables["Messages"].Columns["From"]);
            }
            return DataSet.Tables[table];
        }

        public static ObservableCollection<Message> Messages {
            get {
                try {
                    if(messages == null) {
                        messages = new ObservableCollection<Message>();
                        DataTable tbl = MailTable;
                        if(tbl != null) {
                            foreach(DataRow row in tbl.Rows)
                                messages.Add(new Message(row));
                        }
                    }
                } catch(Exception) {
                    messages = new ObservableCollection<Message>();
                }
                return messages;
            }
        }
    }

    public class MailEmployee {
        public MailEmployee(DataRow row) {
            FirstName = string.Format("{0}", row["FirstName"]);
            LastName = string.Format("{0}", row["LastName"]);
            FullName = string.Format("{0} {1}", FirstName, LastName);
            Gender = string.Format("{0}", row["Gender"]);
            var employee = DevAVHelper.GetEmployee(string.Format("{0}", row["Email"]));
            if(employee != null) {
                FullName = employee.FullName;
                var picture = employee.Picture;
                if(picture != null)
                    Image = picture.Data;
            }
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Initials { get { return String.Format("{0}{1}", FirstName.First(), LastName.First()); } }
        public string Gender { get; private set; }
        public byte[] Image { get; private set; }
    }

    public class Message {
        public Message(DataRow row) {
            Employee = new MailEmployee(row.GetParentRow(MailItems.DataSet.Relations[0]));
            Received = DateTime.Now.AddDays((int)row["Day"]);
            Subject = string.Format("{0}", row["Subject"]);
            HasAttachment = (bool)row["HasAttachment"];
            Read = Delay > TimeSpan.FromHours(6);
            if(Delay > TimeSpan.FromHours(50) && Delay < TimeSpan.FromHours(100))
                Read = false;
            Text = string.Format("{0}", row["Text"]);
            Priority = CalcPriority();
            if(!(row["CategoryID"] is DBNull))
                MailFolder = (MailFolder)row["CategoryID"];
        }

        MailEmployee _employee;
        public MailEmployee Employee { get { return _employee; } set { _employee = value; } }

        DateTime _received;
        public DateTime Received { get { return _received; } set { _received = value; } }

        string _subject;
        public string Subject { get { return _subject; } set { _subject = value; } }

        bool _hasAttachment;
        public bool HasAttachment { get { return _hasAttachment; } set { _hasAttachment = value; } }

        bool _read;
        public bool Read { get { return _read; } set { _read = value; } }

        string _text;
        public string Text { get { return _text; } set { _text = value; } }

        int _priority;
        public int Priority { get { return _priority; } set { _priority = value; } }

        MailFolder _mailFolder;
        public MailFolder MailFolder { get { return _mailFolder; } set { _mailFolder = value; } }

        string _plainText;
        public string PlainText {
            get {
                if(_plainText == null)
                    _plainText = PlainTextProvider.GetPlainText(Text);
                return _plainText;
            }
        }
        internal TimeSpan Delay { get { return DateTime.Now - Received; } }

        int CalcPriority() {
            if(Subject.IndexOf("Review") >= 0 || Subject.IndexOf("Important") >= 0) return 2;
            if(Subject.IndexOf("FW:") >= 0 && Delay > TimeSpan.FromHours(48)) return 0;
            return 1;
        }
    }

    public enum MailFolder { All = 0, General, Management, IT, Sales, Support, Engineering, HR, Design };

    public static class PlainTextProvider {
        static RichEditDocumentServer rich = new RichEditDocumentServer();

        public static string GetPlainText(string mhtText) {
            rich.MhtText = mhtText;
            return string.IsNullOrEmpty(rich.Text) ? "" : rich.Text.TrimStart().Substring(0, Math.Min(100, mhtText.Length)).Replace('\r', ' ').Replace('\n', ' ');
        }
    }
}
