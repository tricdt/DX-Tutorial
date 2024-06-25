using System;
using System.ComponentModel;
using System.Drawing;
using DevExpress.Data.Filtering;
using DevExpress.VideoRent.Helpers;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

#if SL
using PrivateAttribute = System.ObsoleteAttribute;
#endif

namespace DevExpress.VideoRent {
    public class InitialRecords {
        public static void CheckForInitialRecords(Session session) {
            using(UnitOfWork saver = new UnitOfWork(session.DataLayer)) {
                CheckForDefaultAdministrator(saver);
                CheckForDefaultCategories(saver);
                CheckForCompanyTypes(saver);
                CheckForDirectorLine(saver);
                CheckForMovieLanguages(saver);
                saver.CommitChanges();
            }
        }
        static void CheckForDefaultAdministrator(Session session) {
            if(session.FindObject<Employee>(CriteriaOperator.Parse("Role = ?", EmployeeRole.Administrator)) != null) return;
            new Employee(session, ReferenceData.AdministratorString, string.Empty).Role = EmployeeRole.Administrator;
        }
        static void CheckForDefaultCategories(Session session) {
            if(session.FindObject<MovieCategory>(null) != null) return;
            MovieCategory category = new MovieCategory(session, Properties.Resources.DefaultMovieCategoryName);
            category.IsDefault = true;
        }
        static void CheckForDirectorLine(Session session) {
            if(MovieArtistLine.GetDirector(session) != null) return;
            new MovieArtistLine(session, ReferenceData.DirectorString);
        }
        static void CheckForCompanyTypes(Session session) {
            if(session.FindObject<CompanyType>(null) != null) return;
            foreach(string name in ReferenceData.CompanyTypes)
                new CompanyType(session, name);
        }
        static void CheckForMovieLanguages(Session session) {
            if(session.FindObject<Language>(null) != null) return;
            foreach(string name in ReferenceData.MovieLanguages)
                new Language(session, name);
        }
    }
    public class VideoRentCurrentUser {
        //[ThreadStatic] TODO uncommment for web
        static Guid currentUserOid = Guid.Empty;
        public static bool Login(Session session, string login) {
            return Login(session, login, string.Empty);
        }
        public static bool Login(Session session, string login, string password) {
            InitialRecords.CheckForInitialRecords(session);
            Employee administrator = session.FindObject<Employee>(CriteriaOperator.Parse("Login = ? And Password = ?", login, StringEncoder.CalcHash(password)));
            if(administrator == null) return false;
            currentUserOid = administrator.Oid;
            return true;
        }
        public static Employee GetCurrentUser(Session session) {
            return currentUserOid == Guid.Empty ? null : session.GetObjectByKey<Employee>(currentUserOid);
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true)]
    public class DeleteIfNullAttribute : Attribute { }
    public class VideoRentBaseObject : ExtendedXPBaseObject {
        Employee createdBy;
        DateTime createdAt;
        string tag;

        public VideoRentBaseObject(Session session) : base(session) { }
        public override void AfterConstruction() {
            base.AfterConstruction();
            CreatedBy = VideoRentCurrentUser.GetCurrentUser(Session);
            CreatedAt = DevExpress.VideoRent.Helpers.VideoRentDateTime.Now;
        }
        protected override void OnSavingOverride() {
            base.OnSavingOverride();
            if(Session.IsNewObject(this)) {
                if(SelfId >= 0) SelfId = DevExpress.VideoRent.Helpers.IDGenerator.GenerateId(Session, GeneratedIdType, SelfId);
                CreatedBy = VideoRentCurrentUser.GetCurrentUser(Session);
                CreatedAt = DevExpress.VideoRent.Helpers.VideoRentDateTime.Now;
            }
        }
        protected override void TriggerObjectChanged(ObjectChangeEventArgs args) {
            if(!string.IsNullOrEmpty(args.PropertyName)) {
                XPMemberInfo mi = ClassInfo.FindMember(args.PropertyName);
                if(mi != null && mi.ReferenceType != null) {
                    base.TriggerObjectChanged(new ObjectChangeEventArgs(args.Session, args.Object, args.Reason, args.PropertyName + "!", args.OldValue, args.NewValue));
                }
            }
            base.TriggerObjectChanged(args);
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue) {
            base.OnChanged(propertyName, oldValue, newValue);
            XPMemberInfo member = ClassInfo.FindMember(propertyName);
            Attribute attribute = member.FindAttributeInfo(typeof(DeleteIfNullAttribute));
            if(attribute != null && newValue == null)
                Delete();
        }
        public Employee CreatedBy {
            get { return createdBy; }
            set { SetPropertyValue<Employee>("CreatedBy", ref createdBy, value); }
        }
        public DateTime CreatedAt {
            get { return createdAt; }
            set { SetPropertyValue<DateTime>("CreatedAt", ref createdAt, value); }
        }
        [Size(SizeAttribute.Unlimited)]
        public string Tag {
            get { return tag; }
            set { SetPropertyValue<string>("Tag", ref tag, value); }
        }
        [NonPersistent]
        protected int SelfId {
            get {
                if(string.IsNullOrEmpty(GeneratedIdType)) return -1;
#if SL
                return GetPropertyValue<int>(string.Format("_{0}Id", GeneratedIdType));
#else
                return GetPropertyValue<int>(string.Format("{0}Id", GeneratedIdType));
#endif
            }
            set {
                if(string.IsNullOrEmpty(GeneratedIdType)) return;
#if SL
                SetPropertyValue<int>(string.Format("_{0}Id", GeneratedIdType), value);
#else
                SetPropertyValue<int>(string.Format("{0}Id", GeneratedIdType), value);
#endif
            }
        }
        protected virtual string GeneratedIdType { get { return null; } }
        public virtual bool AllowDelete { get { return true; } }
    }
    public class Person : VideoRentBaseObject {
        string firstName, lastName;
        PersonGender gender;
        DateTime? birthDate;

        public Person(Session session) : base(session) { }
        public Person(Session session, int selfId)
            : this(session) {
            SelfId = selfId;
        }
        public string FirstName {
            get { return firstName; }
            set { SetPropertyValue<string>("FirstName", ref firstName, value); }
        }
        public string LastName {
            get { return lastName; }
            set { SetPropertyValue<string>("LastName", ref lastName, value); }
        }
        public PersonGender Gender {
            get { return gender; }
            set { SetPropertyValue<PersonGender>("Gender", ref gender, value); }
        }
        public DateTime? BirthDate {
            get { return birthDate; }
            set { SetPropertyValue<DateTime?>("BirthDate", ref birthDate, value); }
        }
        public virtual string FullName {
            get {
                string ret = FirstName == null ? string.Empty : FirstName.Trim();
                string lastNameTrim = LastName == null ? string.Empty : LastName.Trim();
                if(lastNameTrim.Length != 0) ret += (ret.Length == 0 ? string.Empty : " ") + lastNameTrim;
                return ret;
            }
        }
    }
    public class Employee : Person {
        EmployeeRole role;
        string login;
        string passwordHash;

        public Employee(Session session) : base(session) { }
        public Employee(Session session, int selfId)
            : this(session) {
            SelfId = selfId;
        }
        public Employee(Session session, string login, string password)
            : this(session) {
            Login = login;
            Password = password;
        }
        public Employee(Session session, string login, string password, string firstName, string lastName)
            : this(session, login, password) {
            FirstName = firstName;
            LastName = lastName;
        }
#if SL
        [Indexed(Unique = true)]
        [Persistent("EmployeeId"), Browsable(false), Private("", true)]
        public int _EmployeeId {
            get { return GetPropertyValue<int>("_EmployeeId"); }
            set { SetPropertyValue<int>("_EmployeeId", value); }
        }
        [PersistentAlias("_EmployeeId")]
        public int EmployeeId {
            get { return GetPropertyValue<int>("_EmployeeId"); }
            protected set { SetPropertyValue<int>("_EmployeeId", value); }
        }
#else
        [Persistent, Indexed(Unique = true)]
        public int EmployeeId {
            get { return GetPropertyValue<int>("EmployeeId"); }
            protected set { SetPropertyValue<int>("EmployeeId", value); }
        }
#endif
        protected override string GeneratedIdType { get { return "Employee"; } }
        public EmployeeRole Role {
            get { return role; }
            set { SetPropertyValue<EmployeeRole>("Role", ref role, value); }
        }
        public string Login {
            get { return login; }
            set { SetPropertyValue<string>("Login", ref login, value); }
        }
#if SL
        [Persistent("PasswordHash"), Browsable(false), Private("", true)]
        public string _PasswordHash {
            get { return passwordHash; }
            set { SetPropertyValue<string>("_PasswordHash", ref passwordHash, value); }
        }
        [PersistentAlias("_PasswordHash")]
        protected string PasswordHash {
            get { return passwordHash; }
            set { SetPropertyValue<string>("_PasswordHash", ref passwordHash, value); }
        }
#else
        [Persistent]
        protected string PasswordHash {
            get { return passwordHash; }
            set { SetPropertyValue<string>("PasswordHash", ref passwordHash, value); }
        }
#endif
        [NonPersistent]
        public string Password {
            get { return PasswordHash; }
            set { PasswordHash = StringEncoder.CalcHash(value); }
        }
    }
}
