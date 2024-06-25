using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

using PrivateAttribute = System.ObsoleteAttribute;

namespace DevExpress.VideoRent.Helpers {
    public interface IExceptionProcesser {
        void Process(Exception e);
    }
    public abstract class ExtendedXPBaseObject : XPBaseObject {
        Guid oid = Guid.Empty;

        public ExtendedXPBaseObject(Session session) : base(session) { }
        public override void AfterConstruction() {
            base.AfterConstruction();
            this.oid = Guid.NewGuid();
        }
        [Browsable(false)]
        [PersistentAlias("_Oid")]
        public Guid Oid { get { return oid; } }
        [Key(false)]
        [Persistent("Oid"), Browsable(false), Private("", true)]
        public Guid _Oid {
            get { return oid; }
            set { SetPropertyValue<Guid>("_Oid", ref oid, value); }
        }
        public event EventHandler Saved;
        protected sealed override void OnSaving() {
            base.OnSaving();
            if(IsDeleted || Session is NestedUnitOfWork) return;
            OnSavingOverride();
            if(Saved != null)
                Saved(this, new EventArgs());
        }
        protected virtual void OnSavingOverride() { }
    }
    public class SessionHelper {
        const string xpoPrefix = "DevExpress.Xpo.";
        static string[] xpoServicesClasses = new string[] { "PersistentBase", "XPBaseObject", "XPObjectType" };

        public static void CommitInBackground(UnitOfWork session, IExceptionProcesser exceptionProcesser, IBackgroundWorker worker, int maxProgress) {
            ICollection objectsToSave = session.GetObjectsToSave();
            double saveSessionPercentProgress = worker.ReportedProgress;
            double saveSessionPercentPerObject = 0.95 * (maxProgress - saveSessionPercentProgress) / objectsToSave.Count;
            EventHandler objectSaved = delegate(object sender, EventArgs e) {
                saveSessionPercentProgress += saveSessionPercentPerObject;
                worker.ReportProgress((int)saveSessionPercentProgress);
            };
            foreach(IXPSimpleObject obj in session.GetObjectsToSave()) {
                ExtendedXPBaseObject vrbo = obj as ExtendedXPBaseObject;
                if(vrbo == null) continue;
                vrbo.Saved += objectSaved;
            }
            SessionHelper.CommitSession(session, exceptionProcesser);
            worker.ReportProgress(maxProgress);
        }
        public static Exception CommitSession(UnitOfWork session, IExceptionProcesser exceptionProcesser) {
            try {
                session.CommitChanges();
                return null;
            } catch(Exception e) {
                if(exceptionProcesser != null) {
                    exceptionProcesser.Process(e);
                    return e;
                } else throw;
            }
        }
        public static object GetObject(object obj, Session session) {
            IXPSimpleObject xpObject = obj as IXPSimpleObject;
            if(xpObject == null) return null;
            while(xpObject.Session != session && xpObject.Session is NestedUnitOfWork)
                xpObject = (IXPSimpleObject)((NestedUnitOfWork)xpObject.Session).GetParentObject(xpObject);
            if(xpObject.Session == session) return xpObject;
            XPClassInfo targetClassInfo = session.GetClassInfo(xpObject.GetType());
            return session.GetObjectByKey(targetClassInfo, xpObject.Session.GetKeyValue(xpObject));
        }
        public static T GetObject<T>(T obj, Session session) {
            return (T)GetObject((object)obj, session);
        }
        public static object GetObjectByKey(Type classType, object objectKey, Session session) {
            object theObject = session.GetObjectByKey(classType, objectKey);
            if(theObject == null)
                theObject = GetObjectByKeyCore(classType, objectKey, session);
            return theObject;
        }
        public static T GetObjectByKey<T>(object objectKey, Session session) {
            return (T)GetObjectByKey(typeof(T), objectKey, session);
        }
        public static void CopySession(Session source, Session target, XPClassInfo[] classes, IBackgroundWorker worker, int maxProgress) {
            Dictionary<IXPSimpleObject, IXPSimpleObject> copiedObjects = new Dictionary<IXPSimpleObject, IXPSimpleObject>();
            double percentProgress = worker == null ? 0 : worker.ReportedProgress;
            Dictionary<IXPSimpleObject, bool> objectsForCopy = GetObjectsForCopy(source, classes);
            double percentInterval = maxProgress - percentProgress;
            double percentProgressPerObject = 0.97 * percentInterval / objectsForCopy.Keys.Count;
            percentProgress += 0.03 * percentInterval;
            if(worker != null) worker.ReportProgress((int)percentProgress);
            XPObjectCloneHelper cloneHelper = new XPObjectCloneHelper(target, copiedObjects, delegate(object sender, EventArgs e) {
                percentProgress += percentProgressPerObject;
                if(worker != null) worker.ReportProgress((int)percentProgress);
            });
            foreach(IXPSimpleObject obj in objectsForCopy.Keys) {
                if(!copiedObjects.ContainsKey(obj)) cloneHelper.Clone(obj);
            }
            if(worker != null) worker.ReportProgress(maxProgress);
        }
        static object GetObjectByKeyCore(Type classType, object objectKey, Session session) {
            NestedUnitOfWork nested = session as NestedUnitOfWork;
            Session parent = nested != null ? nested.Parent : session;
            object parentObject = parent.GetObjectByKey(classType, objectKey);
            if(parentObject == null) {
                foreach(PersistentBase item in parent.GetObjectsToSave()) {
                    if(item.IsDeleted) continue;
                    if(parent.GetKeyValue(item).Equals(objectKey)) {
                        parentObject = item;
                        break;
                    }
                }
            }
            if(parentObject == null) return null;
            return nested == null ? parentObject : nested.GetNestedObject(parentObject);
        }
        static Dictionary<IXPSimpleObject, bool> GetObjectsForCopy(Session session, XPClassInfo[] classes) {
            Dictionary<IXPSimpleObject, bool> objectsForCopy = new Dictionary<IXPSimpleObject, bool>();
            foreach(XPClassInfo classInfo in classes) {
                bool isServiceClass = false;
                foreach(string className in xpoServicesClasses) {
                    if(classInfo.FullName == xpoPrefix + className) {
                        isServiceClass = true;
                        break;
                    }
                }
                if(isServiceClass) continue;
                XPCollection objects = new XPCollection(session, classInfo);
                foreach(IXPSimpleObject obj in objects) {
                    if(obj is DevExpress.Xpo.Metadata.Helpers.IntermediateObject) continue;
                    if(objectsForCopy.ContainsKey(obj)) continue;
                    objectsForCopy.Add(obj, true);
                }
            }
            return objectsForCopy;
        }
    }
}
