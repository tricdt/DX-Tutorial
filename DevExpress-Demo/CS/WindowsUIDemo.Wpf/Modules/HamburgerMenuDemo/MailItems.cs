using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using DevExpress.Xpf.DemoBase.Helpers;
using DevExpress.XtraRichEdit;
using System.IO;

namespace WindowsUIDemo {
    public static class MailItems {
        internal static DataSet DataSet { get; private set; }

        static ObservableCollection<Message> messages;
        static DataTable MailTable {
            get { return CreateDataTable("Messages"); }
        }
        static DataTable CreateDataTable(string table) {
            if(DataSet == null) {
                DataSet = new DataSet();
                Assembly assembly = typeof(Message).Assembly;
                using(Stream stream = assembly.GetManifestResourceStream(DemoHelper.GetPath("WindowsUIDemo.Data.", assembly) + "MailDevAv.xml")) {
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
                        int index = 0;
                        if(tbl != null) {
                            foreach(DataRow row in tbl.Rows) {
                                var newMessage = new Message(row);
                                newMessage.UpdateMailType(GetMailType(index++));
                                messages.Add(newMessage);
                            }
                        }
                    }
                } catch(Exception) {
                    messages = new ObservableCollection<Message>();
                }
                return messages;
            }
        }

        static MailType GetMailType(int rowIndex) {
            if(rowIndex % 15 == 0)
                return MailType.Draft;
            if(rowIndex % 7 == 0)
                return MailType.Deleted;
            if(rowIndex % 5 == 0)
                return MailType.Sent;
            return MailType.Inbox;
        }
    }

    public class Message : INotifyPropertyChanged {
        Random random { get { return new Random(DateTime.Now.Millisecond); } }
        DataRow _row;
        DateTime _received;
        bool _read, _deleted, _hasAttachment;
        int _priority = 1;
        MailType _mailType;
        MailFolder _mailFolder;
        string _from = String.Empty, _subject = String.Empty, _text = String.Empty, _plainText;

        public Message() {
            _received = DateTime.Now;
        }
        public Message(DataRow row) {
            Employee = new MailEmployee(row.GetParentRow(MailItems.DataSet.Relations[0]));
            _row = row;
            _received = DateTime.Now.AddDays((int)row["Day"]);
            _from = string.Format("{0}", row["From"]);
            _subject = string.Format("{0}", row["Subject"]);
            _hasAttachment = (bool)row["HasAttachment"];
            IsUnread = false;
            if(random.Next() % 10 == 0)
                IsUnread = true;
            _text = string.Format("{0}", row["Text"]);
            _deleted = false;
            _priority = CalcPriority();
            if(!(row["CategoryID"] is DBNull))
                _mailFolder = (MailFolder)row["CategoryID"];
        }
        int CalcPriority() {
            if(Subject.IndexOf("Review") >= 0 || Subject.IndexOf("Important") >= 0) return 2;
            if(Subject.IndexOf("FW:") >= 0 && Delay > TimeSpan.FromHours(48)) return 0;
            return 1;
        }

        MailEmployee _employee;
        public MailEmployee Employee { get { return _employee; } set { _employee = value; } }
        public DateTime Received { get { return _received; } set { _received = value; } }
        public string From { get { return _from; } set { _from = value; } }
        public string Subject { get { return _subject; } set { _subject = value; } }
        public string SubjectDisplayText { get { return string.Format("{0}", Subject); } }
        public bool Attachment { get { return _hasAttachment; } }
        public int Read { get { return _read ? 1 : 0; } }
        public int Priority { get { return _priority; } set { _priority = value; } }
        public bool IsUnread { get { return !_read; } set { _read = !value; } }
        internal string Folder { get { return string.Format("{0}", _mailFolder); } }
        public string Text { get { return _text; } set { _text = value; OnPropertyChanged("PlainText"); } }

        public string PlainText {
            get {
                if(_plainText == null)
                    _plainText = ObjectHelper.GetPlainText(Text);
                return _plainText;
            }
        }

        public MailType MailType { get { return _mailType; } set { _mailType = value; } }
        public MailFolder MailFolder { get { return _mailFolder; } set { _mailFolder = value; } }
        public bool Deleted { get { return _deleted; } set { _deleted = value; } }
        internal TimeSpan Delay { get { return DateTime.Now - _received; } }

        public void ToggleRead() {
            _read = !_read;
            OnPropertyChanged("Read");
            OnPropertyChanged("IsUnread");
        }
        public void UpdateMailType(MailType type) {
            _mailType = type;
            OnPropertyChanged("MailType");
        }

        #region INotifyPropertyChanged
        void OnPropertyChanged(string propertyName = null) {
            if(PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    public enum MailFolder { All = 0, General, Management, IT, Sales, Support, Engineering, HR, Design };

    public enum MailType { Inbox, Deleted, Sent, Draft };

    static class ObjectHelper {
        static RichEditDocumentServer rich = new RichEditDocumentServer();

        public static string GetPlainText(string mhtText) {
            rich.MhtText = mhtText;
            return rich.Text.TrimStart().Substring(0, Math.Min(100, mhtText.Length)).Replace('\r', ' ').Replace('\n', ' ');
        }
    }
    public class MailEmployee {
        public MailEmployee(DataRow row) {
            FirstName = string.Format("{0}", row["FirstName"]);
            LastName = string.Format("{0}", row["LastName"]);
            FullName = string.Format("{0} {1}", FirstName, LastName);
            Gender = string.Format("{0}", row["Gender"]);
            var employee = DevAVHelper.GetEmployee(string.Format("{0}", row["Email"]));
            Image = null;
            if(employee != null) {
                FullName = employee.FullName;
                var picture = employee.Picture;
                if(picture != null)
                    Image = picture.Data;
                return;
            }
        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string FullName { get; private set; }
        public string Initials { get { return String.Format("{0}{1}", FirstName.First(), LastName.First()); } }
        public string Gender { get; private set; }
        public byte[] Image { get; private set; }
    }
}
