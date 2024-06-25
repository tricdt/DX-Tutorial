











namespace EditorsDemo.SCService
{
    
    
    
    
    public partial class SCEntities : global::System.Data.Services.Client.DataServiceContext
    {
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public SCEntities(global::System.Uri serviceRoot) : 
                base(serviceRoot)
        {
            this.ResolveName = new global::System.Func<global::System.Type, string>(this.ResolveNameFromType);
            this.ResolveType = new global::System.Func<string, global::System.Type>(this.ResolveTypeFromName);
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        
        
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected global::System.Type ResolveTypeFromName(string typeName)
        {
            if (typeName.StartsWith("SCModel", global::System.StringComparison.Ordinal))
            {
                return this.GetType().Assembly.GetType(string.Concat("EditorsDemo.SCService", typeName.Substring(7)), false);
            }
            return null;
        }
        
        
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected string ResolveNameFromType(global::System.Type clientType)
        {
            if (clientType.Namespace.Equals("EditorsDemo.SCService", global::System.StringComparison.Ordinal))
            {
                return string.Concat("SCModel.", clientType.Name);
            }
            return null;
        }
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<SCIssuesDemo> SCIssuesDemo
        {
            get
            {
                if ((this._SCIssuesDemo == null))
                {
                    this._SCIssuesDemo = base.CreateQuery<SCIssuesDemo>("SCIssuesDemo");
                }
                return this._SCIssuesDemo;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<SCIssuesDemo> _SCIssuesDemo;
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToSCIssuesDemo(SCIssuesDemo sCIssuesDemo)
        {
            base.AddObject("SCIssuesDemo", sCIssuesDemo);
        }
    }
    
    
    
    
    
    
    [global::System.Data.Services.Common.EntitySetAttribute("SCIssuesDemo")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("Oid")]
    public partial class SCIssuesDemo : global::System.ComponentModel.INotifyPropertyChanged
    {
        
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static SCIssuesDemo CreateSCIssuesDemo(global::System.Guid oid)
        {
            SCIssuesDemo sCIssuesDemo = new SCIssuesDemo();
            sCIssuesDemo.Oid = oid;
            return sCIssuesDemo;
        }
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Guid Oid
        {
            get
            {
                return this._Oid;
            }
            set
            {
                this.OnOidChanging(value);
                this._Oid = value;
                this.OnOidChanged();
                this.OnPropertyChanged("Oid");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Guid _Oid;
        partial void OnOidChanging(global::System.Guid value);
        partial void OnOidChanged();
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                this.OnIDChanging(value);
                this._ID = value;
                this.OnIDChanged();
                this.OnPropertyChanged("ID");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _ID;
        partial void OnIDChanging(string value);
        partial void OnIDChanged();
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Subject
        {
            get
            {
                return this._Subject;
            }
            set
            {
                this.OnSubjectChanging(value);
                this._Subject = value;
                this.OnSubjectChanged();
                this.OnPropertyChanged("Subject");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Subject;
        partial void OnSubjectChanging(string value);
        partial void OnSubjectChanged();
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.DateTime> ModifiedOn
        {
            get
            {
                return this._ModifiedOn;
            }
            set
            {
                this.OnModifiedOnChanging(value);
                this._ModifiedOn = value;
                this.OnModifiedOnChanged();
                this.OnPropertyChanged("ModifiedOn");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<global::System.DateTime> _ModifiedOn;
        partial void OnModifiedOnChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnModifiedOnChanged();
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<global::System.DateTime> CreatedOn
        {
            get
            {
                return this._CreatedOn;
            }
            set
            {
                this.OnCreatedOnChanging(value);
                this._CreatedOn = value;
                this.OnCreatedOnChanged();
                this.OnPropertyChanged("CreatedOn");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<global::System.DateTime> _CreatedOn;
        partial void OnCreatedOnChanging(global::System.Nullable<global::System.DateTime> value);
        partial void OnCreatedOnChanged();
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string ProductName
        {
            get
            {
                return this._ProductName;
            }
            set
            {
                this.OnProductNameChanging(value);
                this._ProductName = value;
                this.OnProductNameChanged();
                this.OnPropertyChanged("ProductName");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _ProductName;
        partial void OnProductNameChanging(string value);
        partial void OnProductNameChanged();
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string TechnologyName
        {
            get
            {
                return this._TechnologyName;
            }
            set
            {
                this.OnTechnologyNameChanging(value);
                this._TechnologyName = value;
                this.OnTechnologyNameChanged();
                this.OnPropertyChanged("TechnologyName");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _TechnologyName;
        partial void OnTechnologyNameChanging(string value);
        partial void OnTechnologyNameChanged();
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Nullable<bool> Urgent
        {
            get
            {
                return this._Urgent;
            }
            set
            {
                this.OnUrgentChanging(value);
                this._Urgent = value;
                this.OnUrgentChanged();
                this.OnPropertyChanged("Urgent");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Nullable<bool> _Urgent;
        partial void OnUrgentChanging(global::System.Nullable<bool> value);
        partial void OnUrgentChanged();
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this.OnStatusChanging(value);
                this._Status = value;
                this.OnStatusChanged();
                this.OnPropertyChanged("Status");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Status;
        partial void OnStatusChanging(string value);
        partial void OnStatusChanged();
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
