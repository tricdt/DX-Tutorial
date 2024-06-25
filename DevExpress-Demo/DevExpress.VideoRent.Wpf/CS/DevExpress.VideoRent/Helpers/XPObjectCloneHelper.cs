using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace DevExpress.VideoRent.Helpers {
    public class XPObjectCloneHelper {
        Session targetSession;
        Dictionary<IXPSimpleObject, IXPSimpleObject> copiedObjects;
        EventHandler objectCopied;

        public XPObjectCloneHelper(Session targetSession, Dictionary<IXPSimpleObject, IXPSimpleObject> copiedObjects, EventHandler objectCopied) {
            this.targetSession = targetSession;
            this.copiedObjects = copiedObjects != null ? copiedObjects : new Dictionary<IXPSimpleObject, IXPSimpleObject>();
            this.objectCopied = objectCopied;
        }
        public IXPSimpleObject Clone(IXPSimpleObject source) {
            if(source == null) return null;
            if(copiedObjects.ContainsKey(source)) return copiedObjects[source];
            XPClassInfo targetClassInfo = targetSession.GetClassInfo(source.GetType());
            IXPSimpleObject clone = (IXPSimpleObject)targetClassInfo.CreateNewObject(targetSession);
            copiedObjects.Add(source, clone);
            if(objectCopied != null) objectCopied(this, EventArgs.Empty);
            foreach(XPMemberInfo m in targetClassInfo.PersistentProperties) {
                if(m is DevExpress.Xpo.Metadata.Helpers.ServiceField || m.IsKey) continue;
                object val;
                if(m.ReferenceType != null)
                    val = Clone((IXPSimpleObject)m.GetValue(source));
                else val = m.GetValue(source);
                m.SetValue(clone, val);
            }
            foreach(XPMemberInfo m in targetClassInfo.CollectionProperties) {
                XPBaseCollection col = (XPBaseCollection)m.GetValue(clone);
                XPBaseCollection colSource = (XPBaseCollection)m.GetValue(source);
                foreach(IXPSimpleObject obj in CollectionHelper.CreateList((colSource)))
                    col.BaseAdd(Clone(obj));
            }
            Dictionary<string, object> indexedProperties = RetrieveIndexedProperties(targetClassInfo, clone);
            if(indexedProperties.Count > 0) {
                string requestString = BuildRequest(indexedProperties);
                object[] values = BuildValuesArray(indexedProperties);
                IXPSimpleObject foundedClone = (IXPSimpleObject)targetSession.FindObject(PersistentCriteriaEvaluationBehavior.InTransaction, targetClassInfo, CriteriaOperator.Parse(requestString, values));
                if(foundedClone != null && foundedClone != clone) ((XPBaseObject)foundedClone).Delete();
            }
            return clone;
        }
        static object[] BuildValuesArray(Dictionary<string, object> indexedProperties) {
            List<object> values = new List<object>();
            foreach(string name in indexedProperties.Keys) {
                values.Add(indexedProperties[name]);
            }
            return values.ToArray();
        }
        static string BuildRequest(Dictionary<string, object> indexedProperties) {
            StringBuilder fieldNames = new StringBuilder();
            bool isFirst = true;
            foreach(string name in indexedProperties.Keys) {
                if(isFirst) {
                    isFirst = false;
                    fieldNames.AppendFormat("{0} = ?", name);
                } else {
                    fieldNames.AppendFormat(" and {0} = ?", name);
                }
            }
            return fieldNames.ToString();
        }
        static Dictionary<string, object> RetrieveIndexedProperties(XPClassInfo classInfo, IXPSimpleObject source) {
            Dictionary<string, object> indexedProperties = new Dictionary<string, object>();
            foreach(XPMemberInfo item in classInfo.PersistentProperties) {
                if(item.HasAttribute(typeof(IndexedAttribute))) {
                    IndexedAttribute attribute = (IndexedAttribute)item.GetAttributeInfo(typeof(IndexedAttribute));
                    if(attribute.Unique) {
                        indexedProperties.Add(item.Name, item.GetValue(source));
                    } else continue;
                    if(attribute.AdditionalFields != null && attribute.AdditionalFields.Count > 0) {
                        foreach(string fieldName in attribute.AdditionalFields) {
                            indexedProperties.Add(fieldName, classInfo.GetMember(fieldName).GetValue(source));
                        }
                    }
                }
            }
            return indexedProperties;
        }
    }
}
