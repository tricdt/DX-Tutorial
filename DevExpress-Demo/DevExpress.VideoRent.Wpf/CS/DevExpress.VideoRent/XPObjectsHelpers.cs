using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using DevExpress.Data.Filtering;
using DevExpress.Utils;
using DevExpress.Xpo;
using DevExpress.Xpo.DB.Exceptions;

namespace DevExpress.VideoRent.Helpers {
    public class VideoRentDateTime {
        static DateTime debugNow;
        static bool realTime = true;
        public static bool RealTime {
            get { return realTime; }
            set {
                if(realTime && !value) debugNow = Now;
                realTime = value;
            }
        }
        public static void AddDays(int days) {
            if(RealTime) throw new Exception("In real-time mode this method not implemented");
            debugNow = debugNow.AddDays(days);
        }
        public static void SetTimeOfDay(DateTime time) {
            if(RealTime) throw new Exception("In real-time mode this method not implemented");
            debugNow = new DateTime(debugNow.Year, debugNow.Month, debugNow.Day, time.Hour, time.Minute, time.Second, time.Millisecond);
        }
        public static void SetTimeOfDay(int timeInSeconds) {
            if(RealTime) throw new Exception("In real-time mode this method not implemented");
            int seconds = timeInSeconds % 60;
            timeInSeconds /= 60;
            int minutes = timeInSeconds % 60;
            timeInSeconds /= 60;
            int hours = timeInSeconds % 24;
            debugNow = new DateTime(debugNow.Year, debugNow.Month, debugNow.Day, hours, minutes, seconds);
        }
        public static DateTime Now {
            get {
                if(RealTime) return DateTime.Now;
                return debugNow;
            }
        }
    }
    [Persistent("IDGeneratorTable"), MemberDesignTimeVisibility(false)]
    public class IDGenerator : VideoRentBaseObject {
        public const int MaxIdGenerationAttempts = 7;
        public const int MinConflictDelay = 50;
        public const int MaxConflictDelay = 500;

        static object SyncRoot = new object();

        static Session fastGeneratorSession = null;
        static Dictionary<string, int> lastGeneratedIds = new Dictionary<string, int>();
        static Session disableGeneratorSession = null;

        public IDGenerator(Session session) : base(session) { }
        public IDGenerator(Session session, string type)
            : base(session) {
            Type = type;
        }
        public override void AfterConstruction() {
            base.AfterConstruction();
            LastGeneratedID = 1;
        }
        public static int GenerateId(Session session, string type, int requiredId) {
            if(disableGeneratorSession == session) return requiredId;
            if(fastGeneratorSession == session) return GenerateIdFast(type, requiredId);
            return GenerateIdSlow(session, type, requiredId);
        }
        static int GenerateIdFast(string type, int requiredId) {
            if(!lastGeneratedIds.ContainsKey(type)) lastGeneratedIds.Add(type, 1);
            if(requiredId > lastGeneratedIds[type]) lastGeneratedIds[type] = requiredId;
            return lastGeneratedIds[type]++;
        }
        static int GenerateIdSlow(Session session, string type, int requiredId) {
            if(session is NestedUnitOfWork) return requiredId;
            lock(SyncRoot) {
                for(int attempt = 1; ; ++attempt) {
                    try {
                        using(UnitOfWork generatorSession = new UnitOfWork(session.DataLayer)) {
                            IDGenerator generator = generatorSession.FindObject<IDGenerator>(new BinaryOperator("Type", type));
                            if(generator == null) generator = new IDGenerator(generatorSession, type);
                            if(requiredId > generator.LastGeneratedID) generator.LastGeneratedID = requiredId;
                            int result = generator.LastGeneratedID++;
                            generatorSession.CommitChanges();
                            return result;
                        }
                    } catch(LockingException) {
                        if(attempt >= MaxIdGenerationAttempts) throw;
                    }
                    if(attempt > MaxIdGenerationAttempts / 2) {
                        Thread.Sleep(new Random().Next(MinConflictDelay, MaxConflictDelay));
                    }
                }
            }
        }
        public int LastGeneratedID {
            get { return GetPropertyValue<int>("LastGeneratedID"); }
            set { SetPropertyValue<int>("LastGeneratedID", value); }
        }
        [Indexed(Unique = true), Size(100)]
        public string Type {
            get { return GetPropertyValue<string>("Type"); }
            set { SetPropertyValue<string>("Type", value); }
        }
        public static void EnableFastGeneration(Session session) {
            if(session == null) throw new ArgumentNullException("session");
            if(fastGeneratorSession != null) return;
            fastGeneratorSession = session;
            XPCollection<IDGenerator> generators = new XPCollection<IDGenerator>(fastGeneratorSession);
            foreach(IDGenerator generator in generators) {
                lastGeneratedIds.Add(generator.Type, generator.LastGeneratedID);
            }
        }
        public static void DisableFastGeneration() {
            if(fastGeneratorSession == null) return;
            foreach(string type in lastGeneratedIds.Keys) {
                IDGenerator generator = fastGeneratorSession.FindObject<IDGenerator>(CriteriaOperator.Parse("Type = ?", type));
                if(generator == null) generator = new IDGenerator(fastGeneratorSession, type);
                generator.LastGeneratedID = lastGeneratedIds[type];
            }
            lastGeneratedIds.Clear();
            fastGeneratorSession = null;
        }
        public static void DisableGeneration(Session session) {
            if(session == null) throw new ArgumentNullException("session");
            if(disableGeneratorSession != null) return;
            disableGeneratorSession = session;
        }
        public static void EnableGeneration() {
            disableGeneratorSession = null;
        }
    }
#if !SL // TODO
    public class ResourcesHelper {
        public static string GetXmlDbFileFromResource() {
            string path = System.IO.Path.GetTempFileName();
            using(System.IO.FileStream file = new System.IO.FileStream(path, System.IO.FileMode.Open)) {
                using(System.IO.BinaryWriter writer = new System.IO.BinaryWriter(file)) {
                    Stream xmlStream = AssemblyHelper.GetEmbeddedResourceStream(typeof(ResourcesHelper).Assembly, "Data.VideoRent.xml", false);
                    byte[] data = new byte[xmlStream.Length];
                    xmlStream.Read(data, 0, data.Length);
                    writer.Write(data, 0, data.Length);
                }
            }
            return path;
        }
    }
#endif
    public class EnumItem {
        object value;
        string title;

        public EnumItem() { }
        public EnumItem(object value, string title) {
            Value = value;
            Title = title;
        }
        public object Value { get { return value; } set { this.value = value; } }
        public string Title { get { return title; } set { title = value; } }
    }
}
