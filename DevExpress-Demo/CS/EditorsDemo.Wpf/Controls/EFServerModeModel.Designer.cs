








using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;

[assembly: EdmSchemaAttribute()]

namespace GridDemo.Controls {
    #region Contexts

    
    
    
    public partial class DXGridServerModeDBEntities : ObjectContext {
        #region Constructors

        
        
        
        public DXGridServerModeDBEntities()
            : base("name=DXGridServerModeDBEntities", "DXGridServerModeDBEntities") {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        
        
        
        public DXGridServerModeDBEntities(string connectionString)
            : base(connectionString, "DXGridServerModeDBEntities") {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        
        
        
        public DXGridServerModeDBEntities(EntityConnection connection)
            : base(connection, "DXGridServerModeDBEntities") {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }

        #endregion

        #region Partial Methods

        partial void OnContextCreated();

        #endregion

        #region ObjectSet Properties

        
        
        
        public ObjectSet<EFServerModeTestClass> EFServerModeTestClasses {
            get {
                if ((_EFServerModeTestClasses == null)) {
                    _EFServerModeTestClasses = base.CreateObjectSet<EFServerModeTestClass>("EFServerModeTestClasses");
                }
                return _EFServerModeTestClasses;
            }
        }
        private ObjectSet<EFServerModeTestClass> _EFServerModeTestClasses;

        #endregion
        #region AddTo Methods

        
        
        
        public void AddToEFServerModeTestClasses(EFServerModeTestClass eFServerModeTestClass) {
            base.AddObject("EFServerModeTestClasses", eFServerModeTestClass);
        }

        #endregion
    }


    #endregion

    #region Entities

    
    
    
    [EdmEntityTypeAttribute(NamespaceName = "DXGridServerModeDBModel", Name = "EFServerModeTestClass")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class EFServerModeTestClass : EntityObject {
        #region Factory Method

        
        
        
        
        public static EFServerModeTestClass CreateEFServerModeTestClass(global::System.Int32 oID) {
            EFServerModeTestClass eFServerModeTestClass = new EFServerModeTestClass();
            eFServerModeTestClass.OID = oID;
            return eFServerModeTestClass;
        }

        #endregion
        #region Primitive Properties

        
        
        
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Int32 OID {
            get {
                return _OID;
            }
            set {
                if (_OID != value) {
                    OnOIDChanging(value);
                    ReportPropertyChanging("OID");
                    _OID = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("OID");
                    OnOIDChanged();
                }
            }
        }
        private global::System.Int32 _OID;
        partial void OnOIDChanging(global::System.Int32 value);
        partial void OnOIDChanged();

        
        
        
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String Subject {
            get {
                return _Subject;
            }
            set {
                OnSubjectChanging(value);
                ReportPropertyChanging("Subject");
                _Subject = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("Subject");
                OnSubjectChanged();
            }
        }
        private global::System.String _Subject;
        partial void OnSubjectChanging(global::System.String value);
        partial void OnSubjectChanged();

        
        
        
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String From {
            get {
                return _From;
            }
            set {
                OnFromChanging(value);
                ReportPropertyChanging("From");
                _From = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("From");
                OnFromChanged();
            }
        }
        private global::System.String _From;
        partial void OnFromChanging(global::System.String value);
        partial void OnFromChanged();

        
        
        
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> Sent {
            get {
                return _Sent;
            }
            set {
                OnSentChanging(value);
                ReportPropertyChanging("Sent");
                _Sent = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Sent");
                OnSentChanged();
            }
        }
        private Nullable<global::System.DateTime> _Sent;
        partial void OnSentChanging(Nullable<global::System.DateTime> value);
        partial void OnSentChanged();

        
        
        
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int64> Size {
            get {
                return _Size;
            }
            set {
                OnSizeChanging(value);
                ReportPropertyChanging("Size");
                _Size = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Size");
                OnSizeChanged();
            }
        }
        private Nullable<global::System.Int64> _Size;
        partial void OnSizeChanging(Nullable<global::System.Int64> value);
        partial void OnSizeChanged();

        
        
        
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Boolean> HasAttachment {
            get {
                return _HasAttachment;
            }
            set {
                OnHasAttachmentChanging(value);
                ReportPropertyChanging("HasAttachment");
                _HasAttachment = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("HasAttachment");
                OnHasAttachmentChanged();
            }
        }
        private Nullable<global::System.Boolean> _HasAttachment;
        partial void OnHasAttachmentChanging(Nullable<global::System.Boolean> value);
        partial void OnHasAttachmentChanged();

        #endregion

    }

    #endregion

}
