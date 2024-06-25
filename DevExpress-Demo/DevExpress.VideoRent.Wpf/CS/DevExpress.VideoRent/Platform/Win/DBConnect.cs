using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using DevExpress.Internal;
using DevExpress.VideoRent.Helpers;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.DB.Exceptions;

namespace DevExpress.VideoRent {
    public class IniFile {
        SortedDictionary<string, string> data = new SortedDictionary<string, string>();
        public IniFile() : base() { }
        public void Load(string path) {
            using(StreamReader sr = new StreamReader(path)) {
                string folder = "[]";
                while(!sr.EndOfStream) {
                    string s = sr.ReadLine().Trim();
                    if(s.Length == 0 || s[0] == ';') continue;
                    if(s[0] == '[') {
                        folder = s;
                        continue;
                    }
                    string key, value;
                    int delim = s.IndexOf('=');
                    if(delim < 0) {
                        key = folder + s.Replace("[", string.Empty).Replace("]", string.Empty);
                        value = string.Empty;
                    } else {
                        key = folder + s.Remove(delim).TrimEnd().Replace("[", string.Empty).Replace("]", string.Empty);
                        value = s.Substring(delim + 1).TrimStart();
                    }
                    if(!data.ContainsKey(key)) data.Add(key, value);
                    else data[key] = value;
                }
            }
        }
        public void Save(string path) {
            using(StreamWriter sw = new StreamWriter(path)) {
                string folder = "[]";
                foreach(string key in data.Keys) {
                    int delim = key.IndexOf(']');
                    string keyFolder = key.Remove(delim + 1);
                    string keyName = key.Substring(delim + 1);
                    if(keyFolder != folder) {
                        folder = keyFolder;
                        sw.WriteLine(folder);
                    }
                    sw.WriteLine(keyName + " = " + data[key]);
                }
            }
        }
        public ICollection<string> Keys { get { return data.Keys; } }
        public bool ContainKey(string key) { return data.ContainsKey(key); }
        public void Remove(string key) { data.Remove(key); }
        public void Add<T>(string key, T value) {
            AddRawValue(key, AddQuotes(value.ToString()));
        }
        public T Get<T>(string key) {
            if(typeof(T) == typeof(string)) return (T)(object)RemoveQuotes(GetRawValue(key));
            if(typeof(T).BaseType == typeof(Enum)) return (T)Enum.Parse(typeof(T), RemoveQuotes(GetRawValue(key)));
            MethodInfo parseMethod = typeof(T).GetMethod("Parse", BindingFlags.Public | BindingFlags.Static, null, new Type[] { typeof(string) }, null);
            if(parseMethod == null) throw new ArgumentException();
            return (T)parseMethod.Invoke(null, new object[] { RemoveQuotes(GetRawValue(key)) });
        }
        public void Set<T>(string key, T value) {
            SetRawValue(key, AddQuotes(value.ToString()));
        }
        private string GetRawValue(string key) { return data[key]; }
        void SetRawValue(string key, string value) { data[key] = value; }
        void AddRawValue(string key, string value) {
            key = key.Trim();
            value = value.Trim();
            int folderBegin = key.IndexOf('[');
            int folderEnd = key.IndexOf(']');
            if(folderBegin != 0 || folderEnd < 0) throw new ArgumentException("key");
            data.Add(key, value);
        }
        static string AddQuotes(string s) { return "\"" + s + "\""; }
        static string RemoveQuotes(string s) {
            if(string.IsNullOrEmpty(s) || s[0] != '\"') return s;
            s = s.Substring(1);
            if(s.Length != 0 && s[s.Length - 1] == '\"') s = s.Remove(s.Length - 1);
            return s;
        }
    }
    public class DBConnectData {
        const string DBFormatKey = "[]DBFormat";
        const string MdbPathKey = "[]MdbPath";
        const string SqlDBNameKey = "[]SqlDbName";
        const string ServerKey = "[]Server";
        const string SqlAuthenticationTypeKey = "[]SqlAuthenticationType";
        const string LoginKey = "[]Login";
        const string PasswordKey = "[]Password";
        const string GenerateRentsHistoryKey = "[]GenerateRentsHistory";
        internal const string LanguageKey = "[]Language";
        string mdbPath;
        bool generateRentsHistory;
        string language;

        public string MdbPath { get { return mdbPath; } set { mdbPath = value; } }
        public bool GenerateRentsHistory { get { return generateRentsHistory; } set { generateRentsHistory = value; } }
        public string Language { get { return language; } set { language = value; } }
        public void LoadIniFile(IniFile iniFile) {
            MdbPath = iniFile.Get<string>(MdbPathKey);
            GenerateRentsHistory = iniFile.Get<bool>(GenerateRentsHistoryKey);
            Language = iniFile.Get<string>(LanguageKey);
        }
        public void SaveIniFile(IniFile iniFile) {
            iniFile.Set<string>(MdbPathKey, MdbPath);
            iniFile.Set<bool>(GenerateRentsHistoryKey, GenerateRentsHistory);
            iniFile.Set<string>(LanguageKey, Language);
        }
        public static bool CheckDefaults(IniFile iniFile) {
            bool ret = true;
            if(!iniFile.ContainKey(MdbPathKey)) { iniFile.Add<string>(MdbPathKey, "VideoRent.mdb"); ret = false; }
            if(!iniFile.ContainKey(SqlDBNameKey)) { iniFile.Add<string>(SqlDBNameKey, "VideoRent"); ret = false; }
            if(!iniFile.ContainKey(ServerKey)) { iniFile.Add<string>(ServerKey, DbEngineDetector.GetSqlServerInstanceName()); ret = false; }
            if(!iniFile.ContainKey(GenerateRentsHistoryKey)) { iniFile.Add<bool>(GenerateRentsHistoryKey, true); ret = false; }
            if(!iniFile.ContainKey(LoginKey)) { iniFile.Add<string>(LoginKey, "sa"); ret = false; }
            if(!iniFile.ContainKey(PasswordKey)) { iniFile.Add<string>(PasswordKey, string.Empty); ret = false; }
            if(!iniFile.ContainKey(LanguageKey)) { iniFile.Add<string>(LanguageKey, CurrentLanguageString); ret = false; }
            return ret;
        }
        public static string CurrentLanguageString { get { return System.Threading.Thread.CurrentThread.CurrentCulture.Name; } }
        public string GetMdbConnectionString() {
            return string.Format("Provider=Microsoft.Jet.OLEDB.4.0;User ID={0};Data Source={1};Mode=Share Deny None;", "Admin", MdbPath);
        }
    }
    public interface ICreateInitialDbDialog {
        void Show(DBConnectData dbConnectData);
        void ShowDialog();
        void Close();
        IBackgroundWorker CreateDbWorker { get; }
        IBackgroundWorker GenerateRentsHistoryWorker { get; }
        event EventHandler Start;
        void BeginWork();
        void EndWork(bool complete);
        void ShowUnableToOpenMessage(bool createNew);
    }
    public class InitialDbCreator {
        public const int HistoryDays = 550;

        ICreateInitialDbDialog createInitialDbDialog;
        bool operationsInProgress = false;
        bool operationsComplete = false;
        UnitOfWork session;
        IniFile iniFile;
        DBConnectData dbConnectData;
        bool unableToCreateDb = false;
        IExceptionProcesser exceptionProcesser;

        public InitialDbCreator(ICreateInitialDbDialog createInitialDbDialog, IExceptionProcesser exceptionProcesser) {
            this.createInitialDbDialog = createInitialDbDialog;
            this.exceptionProcesser = exceptionProcesser;
        }
        public bool OpenDb(IniFile iniFile) {
            this.iniFile = iniFile;
            dbConnectData = new DBConnectData();
            bool createNew = !DBConnectData.CheckDefaults(iniFile);
            dbConnectData.LoadIniFile(iniFile);
            if(!createNew && dbConnectData.Language != DBConnectData.CurrentLanguageString) {
                createNew = true;
                dbConnectData.Language = DBConnectData.CurrentLanguageString;
            }
            if(!createNew) {
                UnitOfWork session = OpenDb(false, false);
                if(session != null) {
                    XpoDefault.Session = session;
                    return true;
                }
            }
            return CreateInitialDb();
        }
        private bool CreateInitialDb() {
            createInitialDbDialog.Show(dbConnectData);
            createInitialDbDialog.Start += new EventHandler(CreateInitialDbDialog_Start);
            createInitialDbDialog.CreateDbWorker.DoWork += new DoWorkEventHandler(CreateDbWorker_DoWork);
            createInitialDbDialog.CreateDbWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CreateDbWorker_RunWorkerCompleted);
            createInitialDbDialog.GenerateRentsHistoryWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(GenerateRentsHistoryWorker_RunWorkerCompleted);
            createInitialDbDialog.ShowDialog();
            if(operationsComplete) dbConnectData.SaveIniFile(iniFile);
            return operationsComplete;
        }
        void CreateInitialDbDialog_Start(object sender, EventArgs e) {
            if(operationsInProgress) return;
            BeginOperations();
            createInitialDbDialog.CreateDbWorker.RunWorkerAsync(System.Threading.Thread.CurrentThread.CurrentUICulture);
        }
        void BeginOperations() {
            createInitialDbDialog.BeginWork();
            unableToCreateDb = false;
            operationsComplete = false;
            operationsInProgress = true;
        }
        void EndOperations(bool complete) {
            operationsInProgress = false;
            operationsComplete = complete;
            createInitialDbDialog.EndWork(complete);
        }
        string TryOpenMdb(bool deleteExist, bool tryCreateNew, out bool createNew) {
            string connectionString = null;
            createNew = false;
            if(File.Exists(dbConnectData.MdbPath)) {
                if(deleteExist) File.Delete(dbConnectData.MdbPath);
                connectionString = dbConnectData.GetMdbConnectionString();
            } else {
                if(tryCreateNew) {
                    connectionString = dbConnectData.GetMdbConnectionString();
                    createNew = true;
                }
            }
            return connectionString;
        }
        UnitOfWork OpenDb(bool deleteExist, bool tryCreateNew) {
            UnitOfWork session = null;
            bool createNew = false;
            try {
                string connectionString = TryOpenMdb(deleteExist, tryCreateNew, out createNew);
                if(connectionString == null) {
                    session = null;
                } else {
                    XpoDefault.DataLayer = XpoDefault.GetDataLayer(connectionString, AutoCreateOption.DatabaseAndSchema);
                    session = new UnitOfWork(XpoDefault.DataLayer);
                }
            } catch { }
            if(session == null) {
                createInitialDbDialog.ShowUnableToOpenMessage(createNew);
            }
            return session;
        }
        void CreateDbWorker_DoWork(object sender, DoWorkEventArgs e) {
            string xmlPath = ResourcesHelper.GetXmlDbFileFromResource();
            string xmlConnectionString = string.Format("XpoProvider=XmlDataSet;data source={0};read only = true", xmlPath);
            using(UnitOfWork xmlSession = new UnitOfWork()) {
                xmlSession.ConnectionString = xmlConnectionString;
                xmlSession.Connect();
                UnitOfWork dbSession = OpenDb(true, true);
                if(dbSession == null) {
                    unableToCreateDb = true;
                } else {
                    IDGenerator.DisableGeneration(dbSession);
                    SessionHelper.CopySession(xmlSession, dbSession,
                        xmlSession.Dictionary.CollectClassInfos(Assembly.GetAssembly(typeof(VideoRentBaseObject))), createInitialDbDialog.CreateDbWorker, 90);
                    SessionHelper.CommitInBackground(dbSession, exceptionProcesser, createInitialDbDialog.CreateDbWorker, 100);
                    IDGenerator.EnableGeneration();
                    CorrectCustomersBirthDate(dbSession);
                    XpoDefault.Session = dbSession;
                }
            }
            File.Delete(xmlPath);
        }
        void CreateDbWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if(unableToCreateDb) {
                EndOperations(false);
                return;
            }
            if(dbConnectData.GenerateRentsHistory) {
                session = new UnitOfWork(XpoDefault.DataLayer);
                new RentsHistory(HistoryDays, session, createInitialDbDialog.GenerateRentsHistoryWorker, exceptionProcesser);
            } else {
                EndOperations(true);
                createInitialDbDialog.Close();
            }
        }
        void GenerateRentsHistoryWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            session.Dispose();
            EndOperations(true);
            createInitialDbDialog.Close();
        }
        void CorrectCustomersBirthDate(UnitOfWork session) {
            foreach(Customer customer in new XPCollection<Customer>(session)) {
                if(customer.BirthDate == null) continue;
                int age = 2000 - customer.BirthDate.Value.Year;
                int month = customer.BirthDate.Value.Month;
                int day = customer.BirthDate.Value.Day;
                customer.BirthDate = new DateTime(DevExpress.VideoRent.Helpers.VideoRentDateTime.Now.Year - age, month, day);
            }
            SessionHelper.CommitSession(session, exceptionProcesser);
        }
    }
}
