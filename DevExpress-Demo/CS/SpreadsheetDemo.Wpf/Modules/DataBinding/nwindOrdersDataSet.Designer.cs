









#pragma warning disable 1591

namespace Modules.DataBinding {
    
    
    
    
    
    [global::System.Serializable()]
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")]
    [global::System.Xml.Serialization.XmlRootAttribute("nwindOrders")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")]
    public partial class nwindOrders : global::System.Data.DataSet {
        
        private OrderDetailsDataTable tableOrderDetails;
        
        private OrdersDataTable tableOrders;
        
        private ProductsDataTable tableProducts;
        
        private global::System.Data.SchemaSerializationMode _schemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public nwindOrders() {
            this.BeginInit();
            this.InitClass();
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            base.Relations.CollectionChanged += schemaChangedHandler;
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected nwindOrders(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                base(info, context, false) {
            if ((this.IsBinarySerialized(info, context) == true)) {
                this.InitVars(false);
                global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler1 = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
                this.Tables.CollectionChanged += schemaChangedHandler1;
                this.Relations.CollectionChanged += schemaChangedHandler1;
                return;
            }
            string strSchema = ((string)(info.GetValue("XmlSchema", typeof(string))));
            if ((this.DetermineSchemaSerializationMode(info, context) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
                if ((ds.Tables["OrderDetails"] != null)) {
                    base.Tables.Add(new OrderDetailsDataTable(ds.Tables["OrderDetails"]));
                }
                if ((ds.Tables["Orders"] != null)) {
                    base.Tables.Add(new OrdersDataTable(ds.Tables["Orders"]));
                }
                if ((ds.Tables["Products"] != null)) {
                    base.Tables.Add(new ProductsDataTable(ds.Tables["Products"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXmlSchema(new global::System.Xml.XmlTextReader(new global::System.IO.StringReader(strSchema)));
            }
            this.GetSerializationData(info, context);
            global::System.ComponentModel.CollectionChangeEventHandler schemaChangedHandler = new global::System.ComponentModel.CollectionChangeEventHandler(this.SchemaChanged);
            base.Tables.CollectionChanged += schemaChangedHandler;
            this.Relations.CollectionChanged += schemaChangedHandler;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public OrderDetailsDataTable OrderDetails {
            get {
                return this.tableOrderDetails;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public OrdersDataTable Orders {
            get {
                return this.tableOrders;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        [global::System.ComponentModel.DesignerSerializationVisibility(global::System.ComponentModel.DesignerSerializationVisibility.Content)]
        public ProductsDataTable Products {
            get {
                return this.tableProducts;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.BrowsableAttribute(true)]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Visible)]
        public override global::System.Data.SchemaSerializationMode SchemaSerializationMode {
            get {
                return this._schemaSerializationMode;
            }
            set {
                this._schemaSerializationMode = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataTableCollection Tables {
            get {
                return base.Tables;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.DesignerSerializationVisibilityAttribute(global::System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public new global::System.Data.DataRelationCollection Relations {
            get {
                return base.Relations;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void InitializeDerivedDataSet() {
            this.BeginInit();
            this.InitClass();
            this.EndInit();
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public override global::System.Data.DataSet Clone() {
            nwindOrders cln = ((nwindOrders)(base.Clone()));
            cln.InitVars();
            cln.SchemaSerializationMode = this.SchemaSerializationMode;
            return cln;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override bool ShouldSerializeTables() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override bool ShouldSerializeRelations() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override void ReadXmlSerializable(global::System.Xml.XmlReader reader) {
            if ((this.DetermineSchemaSerializationMode(reader) == global::System.Data.SchemaSerializationMode.IncludeSchema)) {
                this.Reset();
                global::System.Data.DataSet ds = new global::System.Data.DataSet();
                ds.ReadXml(reader);
                if ((ds.Tables["OrderDetails"] != null)) {
                    base.Tables.Add(new OrderDetailsDataTable(ds.Tables["OrderDetails"]));
                }
                if ((ds.Tables["Orders"] != null)) {
                    base.Tables.Add(new OrdersDataTable(ds.Tables["Orders"]));
                }
                if ((ds.Tables["Products"] != null)) {
                    base.Tables.Add(new ProductsDataTable(ds.Tables["Products"]));
                }
                this.DataSetName = ds.DataSetName;
                this.Prefix = ds.Prefix;
                this.Namespace = ds.Namespace;
                this.Locale = ds.Locale;
                this.CaseSensitive = ds.CaseSensitive;
                this.EnforceConstraints = ds.EnforceConstraints;
                this.Merge(ds, false, global::System.Data.MissingSchemaAction.Add);
                this.InitVars();
            }
            else {
                this.ReadXml(reader);
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected override global::System.Xml.Schema.XmlSchema GetSchemaSerializable() {
            global::System.IO.MemoryStream stream = new global::System.IO.MemoryStream();
            this.WriteXmlSchema(new global::System.Xml.XmlTextWriter(stream, null));
            stream.Position = 0;
            return global::System.Xml.Schema.XmlSchema.Read(new global::System.Xml.XmlTextReader(stream), null);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars() {
            this.InitVars(true);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal void InitVars(bool initTable) {
            this.tableOrderDetails = ((OrderDetailsDataTable)(base.Tables["OrderDetails"]));
            if ((initTable == true)) {
                if ((this.tableOrderDetails != null)) {
                    this.tableOrderDetails.InitVars();
                }
            }
            this.tableOrders = ((OrdersDataTable)(base.Tables["Orders"]));
            if ((initTable == true)) {
                if ((this.tableOrders != null)) {
                    this.tableOrders.InitVars();
                }
            }
            this.tableProducts = ((ProductsDataTable)(base.Tables["Products"]));
            if ((initTable == true)) {
                if ((this.tableProducts != null)) {
                    this.tableProducts.InitVars();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitClass() {
            this.DataSetName = "nwindOrders";
            this.Prefix = "";
            this.Namespace = "http://tempuri.org/nwindOrders.xsd";
            this.EnforceConstraints = true;
            this.SchemaSerializationMode = global::System.Data.SchemaSerializationMode.IncludeSchema;
            this.tableOrderDetails = new OrderDetailsDataTable();
            base.Tables.Add(this.tableOrderDetails);
            this.tableOrders = new OrdersDataTable();
            base.Tables.Add(this.tableOrders);
            this.tableProducts = new ProductsDataTable();
            base.Tables.Add(this.tableProducts);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private bool ShouldSerializeOrderDetails() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private bool ShouldSerializeOrders() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private bool ShouldSerializeProducts() {
            return false;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void SchemaChanged(object sender, global::System.ComponentModel.CollectionChangeEventArgs e) {
            if ((e.Action == global::System.ComponentModel.CollectionChangeAction.Remove)) {
                this.InitVars();
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedDataSetSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
            nwindOrders ds = new nwindOrders();
            global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
            global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
            global::System.Xml.Schema.XmlSchemaAny any = new global::System.Xml.Schema.XmlSchemaAny();
            any.Namespace = ds.Namespace;
            sequence.Items.Add(any);
            type.Particle = sequence;
            global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
            if (xs.Contains(dsSchema.TargetNamespace)) {
                global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                try {
                    global::System.Xml.Schema.XmlSchema schema = null;
                    dsSchema.Write(s1);
                    for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                        schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                        s2.SetLength(0);
                        schema.Write(s2);
                        if ((s1.Length == s2.Length)) {
                            s1.Position = 0;
                            s2.Position = 0;
                            for (; ((s1.Position != s1.Length) 
                                        && (s1.ReadByte() == s2.ReadByte())); ) {
                                ;
                            }
                            if ((s1.Position == s1.Length)) {
                                return type;
                            }
                        }
                    }
                }
                finally {
                    if ((s1 != null)) {
                        s1.Close();
                    }
                    if ((s2 != null)) {
                        s2.Close();
                    }
                }
            }
            xs.Add(dsSchema);
            return type;
        }
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public delegate void OrderDetailsRowChangeEventHandler(object sender, OrderDetailsRowChangeEvent e);
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public delegate void OrdersRowChangeEventHandler(object sender, OrdersRowChangeEvent e);
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public delegate void ProductsRowChangeEventHandler(object sender, ProductsRowChangeEvent e);
        
        
        
        
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class OrderDetailsDataTable : global::System.Data.TypedTableBase<OrderDetailsRow> {
            
            private global::System.Data.DataColumn columnOrderID;
            
            private global::System.Data.DataColumn columnQuantity;
            
            private global::System.Data.DataColumn columnUnitPrice;
            
            private global::System.Data.DataColumn columnDiscount;
            
            private global::System.Data.DataColumn columnProductName;
            
            private global::System.Data.DataColumn columnSupplier;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrderDetailsDataTable() {
                this.TableName = "OrderDetails";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal OrderDetailsDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected OrderDetailsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn OrderIDColumn {
                get {
                    return this.columnOrderID;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn QuantityColumn {
                get {
                    return this.columnQuantity;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn UnitPriceColumn {
                get {
                    return this.columnUnitPrice;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn DiscountColumn {
                get {
                    return this.columnDiscount;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ProductNameColumn {
                get {
                    return this.columnProductName;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn SupplierColumn {
                get {
                    return this.columnSupplier;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrderDetailsRow this[int index] {
                get {
                    return ((OrderDetailsRow)(this.Rows[index]));
                }
            }
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event OrderDetailsRowChangeEventHandler OrderDetailsRowChanging;
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event OrderDetailsRowChangeEventHandler OrderDetailsRowChanged;
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event OrderDetailsRowChangeEventHandler OrderDetailsRowDeleting;
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event OrderDetailsRowChangeEventHandler OrderDetailsRowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void AddOrderDetailsRow(OrderDetailsRow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrderDetailsRow AddOrderDetailsRow(int OrderID, short Quantity, decimal UnitPrice, float Discount, string ProductName, string Supplier) {
                OrderDetailsRow rowOrderDetailsRow = ((OrderDetailsRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        OrderID,
                        Quantity,
                        UnitPrice,
                        Discount,
                        ProductName,
                        Supplier};
                rowOrderDetailsRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowOrderDetailsRow);
                return rowOrderDetailsRow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public override global::System.Data.DataTable Clone() {
                OrderDetailsDataTable cln = ((OrderDetailsDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override global::System.Data.DataTable CreateInstance() {
                return new OrderDetailsDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal void InitVars() {
                this.columnOrderID = base.Columns["OrderID"];
                this.columnProductName = base.Columns["ProductName"];
                this.columnSupplier = base.Columns["Supplier"];
                this.columnUnitPrice = base.Columns["UnitPrice"];
                this.columnQuantity = base.Columns["Quantity"];
                this.columnDiscount = base.Columns["Discount"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            private void InitClass() {
                this.columnOrderID = new global::System.Data.DataColumn("OrderID", typeof(int), null, global::System.Data.MappingType.Element);
                this.columnOrderID.ReadOnly = true;
                base.Columns.Add(this.columnOrderID);
                this.columnProductName = new global::System.Data.DataColumn("ProductName", typeof(string), null, global::System.Data.MappingType.Element);
                this.columnProductName.ReadOnly = true;
                base.Columns.Add(this.columnProductName);
                this.columnSupplier = new global::System.Data.DataColumn("Supplier", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnSupplier);
                this.columnProductName.MaxLength = 40;
                this.columnSupplier.ReadOnly = true;
                this.columnSupplier.MaxLength = 255;
                this.columnUnitPrice = new global::System.Data.DataColumn("UnitPrice", typeof(decimal), null, global::System.Data.MappingType.Element);
                this.columnUnitPrice.ReadOnly = true;
                base.Columns.Add(this.columnUnitPrice);
                this.columnQuantity = new global::System.Data.DataColumn("Quantity", typeof(short), null, global::System.Data.MappingType.Element);
                this.columnQuantity.ReadOnly = true;
                base.Columns.Add(this.columnQuantity);
                this.columnDiscount = new global::System.Data.DataColumn("Discount", typeof(float), null, global::System.Data.MappingType.Element);
                this.columnDiscount.ReadOnly = true;
                base.Columns.Add(this.columnDiscount);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrderDetailsRow NewOrderDetailsRow() {
                return ((OrderDetailsRow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new OrderDetailsRow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override global::System.Type GetRowType() {
                return typeof(OrderDetailsRow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.OrderDetailsRowChanged != null)) {
                    this.OrderDetailsRowChanged(this, new OrderDetailsRowChangeEvent(((OrderDetailsRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.OrderDetailsRowChanging != null)) {
                    this.OrderDetailsRowChanging(this, new OrderDetailsRowChangeEvent(((OrderDetailsRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.OrderDetailsRowDeleted != null)) {
                    this.OrderDetailsRowDeleted(this, new OrderDetailsRowChangeEvent(((OrderDetailsRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.OrderDetailsRowDeleting != null)) {
                    this.OrderDetailsRowDeleting(this, new OrderDetailsRowChangeEvent(((OrderDetailsRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void RemoveOrderDetailsRow(OrderDetailsRow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                nwindOrders ds = new nwindOrders();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "OrderDetailsDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        
        
        
        
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class OrdersDataTable : global::System.Data.TypedTableBase<OrdersRow> {
            
            private global::System.Data.DataColumn columnOrderID;
            
            private global::System.Data.DataColumn columnCustomerID;
            
            private global::System.Data.DataColumn columnEmployeeID;
            
            private global::System.Data.DataColumn columnOrderDate;
            
            private global::System.Data.DataColumn columnRequiredDate;
            
            private global::System.Data.DataColumn columnShippedDate;
            
            private global::System.Data.DataColumn columnShipVia;
            
            private global::System.Data.DataColumn columnFreight;
            
            private global::System.Data.DataColumn columnShipName;
            
            private global::System.Data.DataColumn columnShipAddress;
            
            private global::System.Data.DataColumn columnShipCity;
            
            private global::System.Data.DataColumn columnShipRegion;
            
            private global::System.Data.DataColumn columnShipPostalCode;
            
            private global::System.Data.DataColumn columnShipCountry;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrdersDataTable() {
                this.TableName = "Orders";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal OrdersDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected OrdersDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn OrderIDColumn {
                get {
                    return this.columnOrderID;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn CustomerIDColumn {
                get {
                    return this.columnCustomerID;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn EmployeeIDColumn {
                get {
                    return this.columnEmployeeID;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn OrderDateColumn {
                get {
                    return this.columnOrderDate;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn RequiredDateColumn {
                get {
                    return this.columnRequiredDate;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ShippedDateColumn {
                get {
                    return this.columnShippedDate;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ShipViaColumn {
                get {
                    return this.columnShipVia;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn FreightColumn {
                get {
                    return this.columnFreight;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ShipNameColumn {
                get {
                    return this.columnShipName;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ShipAddressColumn {
                get {
                    return this.columnShipAddress;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ShipCityColumn {
                get {
                    return this.columnShipCity;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ShipRegionColumn {
                get {
                    return this.columnShipRegion;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ShipPostalCodeColumn {
                get {
                    return this.columnShipPostalCode;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ShipCountryColumn {
                get {
                    return this.columnShipCountry;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrdersRow this[int index] {
                get {
                    return ((OrdersRow)(this.Rows[index]));
                }
            }
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event OrdersRowChangeEventHandler OrdersRowChanging;
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event OrdersRowChangeEventHandler OrdersRowChanged;
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event OrdersRowChangeEventHandler OrdersRowDeleting;
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event OrdersRowChangeEventHandler OrdersRowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void AddOrdersRow(OrdersRow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrdersRow AddOrdersRow(string CustomerID, int EmployeeID, System.DateTime OrderDate, System.DateTime RequiredDate, System.DateTime ShippedDate, int ShipVia, decimal Freight, string ShipName, string ShipAddress, string ShipCity, string ShipRegion, string ShipPostalCode, string ShipCountry) {
                OrdersRow rowOrdersRow = ((OrdersRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        null,
                        CustomerID,
                        EmployeeID,
                        OrderDate,
                        RequiredDate,
                        ShippedDate,
                        ShipVia,
                        Freight,
                        ShipName,
                        ShipAddress,
                        ShipCity,
                        ShipRegion,
                        ShipPostalCode,
                        ShipCountry};
                rowOrdersRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowOrdersRow);
                return rowOrdersRow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrdersRow FindByOrderID(int OrderID) {
                return ((OrdersRow)(this.Rows.Find(new object[] {
                            OrderID})));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public override global::System.Data.DataTable Clone() {
                OrdersDataTable cln = ((OrdersDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override global::System.Data.DataTable CreateInstance() {
                return new OrdersDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal void InitVars() {
                this.columnOrderID = base.Columns["OrderID"];
                this.columnCustomerID = base.Columns["CustomerID"];
                this.columnEmployeeID = base.Columns["EmployeeID"];
                this.columnOrderDate = base.Columns["OrderDate"];
                this.columnRequiredDate = base.Columns["RequiredDate"];
                this.columnShippedDate = base.Columns["ShippedDate"];
                this.columnShipVia = base.Columns["ShipVia"];
                this.columnFreight = base.Columns["Freight"];
                this.columnShipName = base.Columns["ShipName"];
                this.columnShipAddress = base.Columns["ShipAddress"];
                this.columnShipCity = base.Columns["ShipCity"];
                this.columnShipRegion = base.Columns["ShipRegion"];
                this.columnShipPostalCode = base.Columns["ShipPostalCode"];
                this.columnShipCountry = base.Columns["ShipCountry"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            private void InitClass() {
                this.columnOrderID = new global::System.Data.DataColumn("OrderID", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnOrderID);
                this.columnCustomerID = new global::System.Data.DataColumn("CustomerID", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCustomerID);
                this.columnEmployeeID = new global::System.Data.DataColumn("EmployeeID", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnEmployeeID);
                this.columnOrderDate = new global::System.Data.DataColumn("OrderDate", typeof(global::System.DateTime), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnOrderDate);
                this.columnRequiredDate = new global::System.Data.DataColumn("RequiredDate", typeof(global::System.DateTime), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnRequiredDate);
                this.columnShippedDate = new global::System.Data.DataColumn("ShippedDate", typeof(global::System.DateTime), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnShippedDate);
                this.columnShipVia = new global::System.Data.DataColumn("ShipVia", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnShipVia);
                this.columnFreight = new global::System.Data.DataColumn("Freight", typeof(decimal), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnFreight);
                this.columnShipName = new global::System.Data.DataColumn("ShipName", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnShipName);
                this.columnShipAddress = new global::System.Data.DataColumn("ShipAddress", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnShipAddress);
                this.columnShipCity = new global::System.Data.DataColumn("ShipCity", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnShipCity);
                this.columnShipRegion = new global::System.Data.DataColumn("ShipRegion", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnShipRegion);
                this.columnShipPostalCode = new global::System.Data.DataColumn("ShipPostalCode", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnShipPostalCode);
                this.columnShipCountry = new global::System.Data.DataColumn("ShipCountry", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnShipCountry);
                this.Constraints.Add(new global::System.Data.UniqueConstraint("Constraint1", new global::System.Data.DataColumn[] {
                                this.columnOrderID}, true));
                this.columnOrderID.AutoIncrement = true;
                this.columnOrderID.AutoIncrementSeed = -1;
                this.columnOrderID.AutoIncrementStep = -1;
                this.columnOrderID.AllowDBNull = false;
                this.columnOrderID.Unique = true;
                this.columnCustomerID.MaxLength = 5;
                this.columnShipName.MaxLength = 40;
                this.columnShipAddress.MaxLength = 60;
                this.columnShipCity.MaxLength = 15;
                this.columnShipRegion.MaxLength = 15;
                this.columnShipPostalCode.MaxLength = 10;
                this.columnShipCountry.MaxLength = 15;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrdersRow NewOrdersRow() {
                return ((OrdersRow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new OrdersRow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override global::System.Type GetRowType() {
                return typeof(OrdersRow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.OrdersRowChanged != null)) {
                    this.OrdersRowChanged(this, new OrdersRowChangeEvent(((OrdersRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.OrdersRowChanging != null)) {
                    this.OrdersRowChanging(this, new OrdersRowChangeEvent(((OrdersRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.OrdersRowDeleted != null)) {
                    this.OrdersRowDeleted(this, new OrdersRowChangeEvent(((OrdersRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.OrdersRowDeleting != null)) {
                    this.OrdersRowDeleting(this, new OrdersRowChangeEvent(((OrdersRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void RemoveOrdersRow(OrdersRow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                nwindOrders ds = new nwindOrders();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "OrdersDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        
        
        
        
        [global::System.Serializable()]
        [global::System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")]
        public partial class ProductsDataTable : global::System.Data.TypedTableBase<ProductsRow> {
            
            private global::System.Data.DataColumn columnProductID;
            
            private global::System.Data.DataColumn columnProductName;
            
            private global::System.Data.DataColumn columnSupplierID;
            
            private global::System.Data.DataColumn columnCategoryID;
            
            private global::System.Data.DataColumn columnQuantityPerUnit;
            
            private global::System.Data.DataColumn columnUnitPrice;
            
            private global::System.Data.DataColumn columnUnitsInStock;
            
            private global::System.Data.DataColumn columnUnitsOnOrder;
            
            private global::System.Data.DataColumn columnReorderLevel;
            
            private global::System.Data.DataColumn columnDiscontinued;
            
            private global::System.Data.DataColumn columnEAN13;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public ProductsDataTable() {
                this.TableName = "Products";
                this.BeginInit();
                this.InitClass();
                this.EndInit();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal ProductsDataTable(global::System.Data.DataTable table) {
                this.TableName = table.TableName;
                if ((table.CaseSensitive != table.DataSet.CaseSensitive)) {
                    this.CaseSensitive = table.CaseSensitive;
                }
                if ((table.Locale.ToString() != table.DataSet.Locale.ToString())) {
                    this.Locale = table.Locale;
                }
                if ((table.Namespace != table.DataSet.Namespace)) {
                    this.Namespace = table.Namespace;
                }
                this.Prefix = table.Prefix;
                this.MinimumCapacity = table.MinimumCapacity;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected ProductsDataTable(global::System.Runtime.Serialization.SerializationInfo info, global::System.Runtime.Serialization.StreamingContext context) : 
                    base(info, context) {
                this.InitVars();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ProductIDColumn {
                get {
                    return this.columnProductID;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ProductNameColumn {
                get {
                    return this.columnProductName;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn SupplierIDColumn {
                get {
                    return this.columnSupplierID;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn CategoryIDColumn {
                get {
                    return this.columnCategoryID;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn QuantityPerUnitColumn {
                get {
                    return this.columnQuantityPerUnit;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn UnitPriceColumn {
                get {
                    return this.columnUnitPrice;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn UnitsInStockColumn {
                get {
                    return this.columnUnitsInStock;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn UnitsOnOrderColumn {
                get {
                    return this.columnUnitsOnOrder;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn ReorderLevelColumn {
                get {
                    return this.columnReorderLevel;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn DiscontinuedColumn {
                get {
                    return this.columnDiscontinued;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataColumn EAN13Column {
                get {
                    return this.columnEAN13;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            [global::System.ComponentModel.Browsable(false)]
            public int Count {
                get {
                    return this.Rows.Count;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public ProductsRow this[int index] {
                get {
                    return ((ProductsRow)(this.Rows[index]));
                }
            }
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event ProductsRowChangeEventHandler ProductsRowChanging;
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event ProductsRowChangeEventHandler ProductsRowChanged;
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event ProductsRowChangeEventHandler ProductsRowDeleting;
            
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public event ProductsRowChangeEventHandler ProductsRowDeleted;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void AddProductsRow(ProductsRow row) {
                this.Rows.Add(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public ProductsRow AddProductsRow(string ProductName, int SupplierID, int CategoryID, string QuantityPerUnit, decimal UnitPrice, short UnitsInStock, short UnitsOnOrder, short ReorderLevel, bool Discontinued, string EAN13) {
                ProductsRow rowProductsRow = ((ProductsRow)(this.NewRow()));
                object[] columnValuesArray = new object[] {
                        null,
                        ProductName,
                        SupplierID,
                        CategoryID,
                        QuantityPerUnit,
                        UnitPrice,
                        UnitsInStock,
                        UnitsOnOrder,
                        ReorderLevel,
                        Discontinued,
                        EAN13};
                rowProductsRow.ItemArray = columnValuesArray;
                this.Rows.Add(rowProductsRow);
                return rowProductsRow;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public ProductsRow FindByProductID(int ProductID) {
                return ((ProductsRow)(this.Rows.Find(new object[] {
                            ProductID})));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public override global::System.Data.DataTable Clone() {
                ProductsDataTable cln = ((ProductsDataTable)(base.Clone()));
                cln.InitVars();
                return cln;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override global::System.Data.DataTable CreateInstance() {
                return new ProductsDataTable();
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal void InitVars() {
                this.columnProductID = base.Columns["ProductID"];
                this.columnProductName = base.Columns["ProductName"];
                this.columnSupplierID = base.Columns["SupplierID"];
                this.columnCategoryID = base.Columns["CategoryID"];
                this.columnQuantityPerUnit = base.Columns["QuantityPerUnit"];
                this.columnUnitPrice = base.Columns["UnitPrice"];
                this.columnUnitsInStock = base.Columns["UnitsInStock"];
                this.columnUnitsOnOrder = base.Columns["UnitsOnOrder"];
                this.columnReorderLevel = base.Columns["ReorderLevel"];
                this.columnDiscontinued = base.Columns["Discontinued"];
                this.columnEAN13 = base.Columns["EAN13"];
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            private void InitClass() {
                this.columnProductID = new global::System.Data.DataColumn("ProductID", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnProductID);
                this.columnProductName = new global::System.Data.DataColumn("ProductName", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnProductName);
                this.columnSupplierID = new global::System.Data.DataColumn("SupplierID", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnSupplierID);
                this.columnCategoryID = new global::System.Data.DataColumn("CategoryID", typeof(int), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnCategoryID);
                this.columnQuantityPerUnit = new global::System.Data.DataColumn("QuantityPerUnit", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnQuantityPerUnit);
                this.columnUnitPrice = new global::System.Data.DataColumn("UnitPrice", typeof(decimal), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnUnitPrice);
                this.columnUnitsInStock = new global::System.Data.DataColumn("UnitsInStock", typeof(short), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnUnitsInStock);
                this.columnUnitsOnOrder = new global::System.Data.DataColumn("UnitsOnOrder", typeof(short), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnUnitsOnOrder);
                this.columnReorderLevel = new global::System.Data.DataColumn("ReorderLevel", typeof(short), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnReorderLevel);
                this.columnDiscontinued = new global::System.Data.DataColumn("Discontinued", typeof(bool), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnDiscontinued);
                this.columnEAN13 = new global::System.Data.DataColumn("EAN13", typeof(string), null, global::System.Data.MappingType.Element);
                base.Columns.Add(this.columnEAN13);
                this.Constraints.Add(new global::System.Data.UniqueConstraint("Constraint1", new global::System.Data.DataColumn[] {
                                this.columnProductID}, true));
                this.columnProductID.AutoIncrement = true;
                this.columnProductID.AutoIncrementSeed = -1;
                this.columnProductID.AutoIncrementStep = -1;
                this.columnProductID.AllowDBNull = false;
                this.columnProductID.Unique = true;
                this.columnProductName.MaxLength = 40;
                this.columnQuantityPerUnit.MaxLength = 20;
                this.columnEAN13.MaxLength = 12;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public ProductsRow NewProductsRow() {
                return ((ProductsRow)(this.NewRow()));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override global::System.Data.DataRow NewRowFromBuilder(global::System.Data.DataRowBuilder builder) {
                return new ProductsRow(builder);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override global::System.Type GetRowType() {
                return typeof(ProductsRow);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowChanged(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanged(e);
                if ((this.ProductsRowChanged != null)) {
                    this.ProductsRowChanged(this, new ProductsRowChangeEvent(((ProductsRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowChanging(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowChanging(e);
                if ((this.ProductsRowChanging != null)) {
                    this.ProductsRowChanging(this, new ProductsRowChangeEvent(((ProductsRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowDeleted(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleted(e);
                if ((this.ProductsRowDeleted != null)) {
                    this.ProductsRowDeleted(this, new ProductsRowChangeEvent(((ProductsRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            protected override void OnRowDeleting(global::System.Data.DataRowChangeEventArgs e) {
                base.OnRowDeleting(e);
                if ((this.ProductsRowDeleting != null)) {
                    this.ProductsRowDeleting(this, new ProductsRowChangeEvent(((ProductsRow)(e.Row)), e.Action));
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void RemoveProductsRow(ProductsRow row) {
                this.Rows.Remove(row);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public static global::System.Xml.Schema.XmlSchemaComplexType GetTypedTableSchema(global::System.Xml.Schema.XmlSchemaSet xs) {
                global::System.Xml.Schema.XmlSchemaComplexType type = new global::System.Xml.Schema.XmlSchemaComplexType();
                global::System.Xml.Schema.XmlSchemaSequence sequence = new global::System.Xml.Schema.XmlSchemaSequence();
                nwindOrders ds = new nwindOrders();
                global::System.Xml.Schema.XmlSchemaAny any1 = new global::System.Xml.Schema.XmlSchemaAny();
                any1.Namespace = "http://www.w3.org/2001/XMLSchema";
                any1.MinOccurs = new decimal(0);
                any1.MaxOccurs = decimal.MaxValue;
                any1.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any1);
                global::System.Xml.Schema.XmlSchemaAny any2 = new global::System.Xml.Schema.XmlSchemaAny();
                any2.Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1";
                any2.MinOccurs = new decimal(1);
                any2.ProcessContents = global::System.Xml.Schema.XmlSchemaContentProcessing.Lax;
                sequence.Items.Add(any2);
                global::System.Xml.Schema.XmlSchemaAttribute attribute1 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute1.Name = "namespace";
                attribute1.FixedValue = ds.Namespace;
                type.Attributes.Add(attribute1);
                global::System.Xml.Schema.XmlSchemaAttribute attribute2 = new global::System.Xml.Schema.XmlSchemaAttribute();
                attribute2.Name = "tableTypeName";
                attribute2.FixedValue = "ProductsDataTable";
                type.Attributes.Add(attribute2);
                type.Particle = sequence;
                global::System.Xml.Schema.XmlSchema dsSchema = ds.GetSchemaSerializable();
                if (xs.Contains(dsSchema.TargetNamespace)) {
                    global::System.IO.MemoryStream s1 = new global::System.IO.MemoryStream();
                    global::System.IO.MemoryStream s2 = new global::System.IO.MemoryStream();
                    try {
                        global::System.Xml.Schema.XmlSchema schema = null;
                        dsSchema.Write(s1);
                        for (global::System.Collections.IEnumerator schemas = xs.Schemas(dsSchema.TargetNamespace).GetEnumerator(); schemas.MoveNext(); ) {
                            schema = ((global::System.Xml.Schema.XmlSchema)(schemas.Current));
                            s2.SetLength(0);
                            schema.Write(s2);
                            if ((s1.Length == s2.Length)) {
                                s1.Position = 0;
                                s2.Position = 0;
                                for (; ((s1.Position != s1.Length) 
                                            && (s1.ReadByte() == s2.ReadByte())); ) {
                                    ;
                                }
                                if ((s1.Position == s1.Length)) {
                                    return type;
                                }
                            }
                        }
                    }
                    finally {
                        if ((s1 != null)) {
                            s1.Close();
                        }
                        if ((s2 != null)) {
                            s2.Close();
                        }
                    }
                }
                xs.Add(dsSchema);
                return type;
            }
        }
        
        
        
        
        public partial class OrderDetailsRow : global::System.Data.DataRow {
            
            private OrderDetailsDataTable tableOrderDetails;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal OrderDetailsRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableOrderDetails = ((OrderDetailsDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public int OrderID {
                get {
                    try {
                        return ((int)(this[this.tableOrderDetails.OrderIDColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'OrderID\' in table \'OrderDetails\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrderDetails.OrderIDColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public short Quantity {
                get {
                    try {
                        return ((short)(this[this.tableOrderDetails.QuantityColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'Quantity\' in table \'OrderDetails\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrderDetails.QuantityColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public decimal UnitPrice {
                get {
                    try {
                        return ((decimal)(this[this.tableOrderDetails.UnitPriceColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'UnitPrice\' in table \'OrderDetails\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrderDetails.UnitPriceColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public float Discount {
                get {
                    try {
                        return ((float)(this[this.tableOrderDetails.DiscountColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'Discount\' in table \'OrderDetails\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrderDetails.DiscountColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string ProductName {
                get {
                    try {
                        return ((string)(this[this.tableOrderDetails.ProductNameColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ProductName\' in table \'OrderDetails\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrderDetails.ProductNameColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string Supplier {
                get {
                    try {
                        return ((string)(this[this.tableOrderDetails.SupplierColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'Supplier\' in table \'OrderDetails\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrderDetails.SupplierColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsOrderIDNull() {
                return this.IsNull(this.tableOrderDetails.OrderIDColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetOrderIDNull() {
                this[this.tableOrderDetails.OrderIDColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsQuantityNull() {
                return this.IsNull(this.tableOrderDetails.QuantityColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetQuantityNull() {
                this[this.tableOrderDetails.QuantityColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsUnitPriceNull() {
                return this.IsNull(this.tableOrderDetails.UnitPriceColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetUnitPriceNull() {
                this[this.tableOrderDetails.UnitPriceColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsDiscountNull() {
                return this.IsNull(this.tableOrderDetails.DiscountColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetDiscountNull() {
                this[this.tableOrderDetails.DiscountColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsProductNameNull() {
                return this.IsNull(this.tableOrderDetails.ProductNameColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetProductNameNull() {
                this[this.tableOrderDetails.ProductNameColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsSupplierNull() {
                return this.IsNull(this.tableOrderDetails.SupplierColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetSupplierNull() {
                this[this.tableOrderDetails.SupplierColumn] = global::System.Convert.DBNull;
            }
        }
        
        
        
        
        public partial class OrdersRow : global::System.Data.DataRow {
            
            private OrdersDataTable tableOrders;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal OrdersRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableOrders = ((OrdersDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public int OrderID {
                get {
                    return ((int)(this[this.tableOrders.OrderIDColumn]));
                }
                set {
                    this[this.tableOrders.OrderIDColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string CustomerID {
                get {
                    try {
                        return ((string)(this[this.tableOrders.CustomerIDColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'CustomerID\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.CustomerIDColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public int EmployeeID {
                get {
                    try {
                        return ((int)(this[this.tableOrders.EmployeeIDColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'EmployeeID\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.EmployeeIDColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public System.DateTime OrderDate {
                get {
                    try {
                        return ((global::System.DateTime)(this[this.tableOrders.OrderDateColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'OrderDate\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.OrderDateColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public System.DateTime RequiredDate {
                get {
                    try {
                        return ((global::System.DateTime)(this[this.tableOrders.RequiredDateColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'RequiredDate\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.RequiredDateColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public System.DateTime ShippedDate {
                get {
                    try {
                        return ((global::System.DateTime)(this[this.tableOrders.ShippedDateColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ShippedDate\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.ShippedDateColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public int ShipVia {
                get {
                    try {
                        return ((int)(this[this.tableOrders.ShipViaColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ShipVia\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.ShipViaColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public decimal Freight {
                get {
                    try {
                        return ((decimal)(this[this.tableOrders.FreightColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'Freight\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.FreightColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string ShipName {
                get {
                    try {
                        return ((string)(this[this.tableOrders.ShipNameColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ShipName\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.ShipNameColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string ShipAddress {
                get {
                    try {
                        return ((string)(this[this.tableOrders.ShipAddressColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ShipAddress\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.ShipAddressColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string ShipCity {
                get {
                    try {
                        return ((string)(this[this.tableOrders.ShipCityColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ShipCity\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.ShipCityColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string ShipRegion {
                get {
                    try {
                        return ((string)(this[this.tableOrders.ShipRegionColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ShipRegion\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.ShipRegionColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string ShipPostalCode {
                get {
                    try {
                        return ((string)(this[this.tableOrders.ShipPostalCodeColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ShipPostalCode\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.ShipPostalCodeColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string ShipCountry {
                get {
                    try {
                        return ((string)(this[this.tableOrders.ShipCountryColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ShipCountry\' in table \'Orders\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableOrders.ShipCountryColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsCustomerIDNull() {
                return this.IsNull(this.tableOrders.CustomerIDColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetCustomerIDNull() {
                this[this.tableOrders.CustomerIDColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsEmployeeIDNull() {
                return this.IsNull(this.tableOrders.EmployeeIDColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetEmployeeIDNull() {
                this[this.tableOrders.EmployeeIDColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsOrderDateNull() {
                return this.IsNull(this.tableOrders.OrderDateColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetOrderDateNull() {
                this[this.tableOrders.OrderDateColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsRequiredDateNull() {
                return this.IsNull(this.tableOrders.RequiredDateColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetRequiredDateNull() {
                this[this.tableOrders.RequiredDateColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsShippedDateNull() {
                return this.IsNull(this.tableOrders.ShippedDateColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetShippedDateNull() {
                this[this.tableOrders.ShippedDateColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsShipViaNull() {
                return this.IsNull(this.tableOrders.ShipViaColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetShipViaNull() {
                this[this.tableOrders.ShipViaColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsFreightNull() {
                return this.IsNull(this.tableOrders.FreightColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetFreightNull() {
                this[this.tableOrders.FreightColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsShipNameNull() {
                return this.IsNull(this.tableOrders.ShipNameColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetShipNameNull() {
                this[this.tableOrders.ShipNameColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsShipAddressNull() {
                return this.IsNull(this.tableOrders.ShipAddressColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetShipAddressNull() {
                this[this.tableOrders.ShipAddressColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsShipCityNull() {
                return this.IsNull(this.tableOrders.ShipCityColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetShipCityNull() {
                this[this.tableOrders.ShipCityColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsShipRegionNull() {
                return this.IsNull(this.tableOrders.ShipRegionColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetShipRegionNull() {
                this[this.tableOrders.ShipRegionColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsShipPostalCodeNull() {
                return this.IsNull(this.tableOrders.ShipPostalCodeColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetShipPostalCodeNull() {
                this[this.tableOrders.ShipPostalCodeColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsShipCountryNull() {
                return this.IsNull(this.tableOrders.ShipCountryColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetShipCountryNull() {
                this[this.tableOrders.ShipCountryColumn] = global::System.Convert.DBNull;
            }
        }
        
        
        
        
        public partial class ProductsRow : global::System.Data.DataRow {
            
            private ProductsDataTable tableProducts;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal ProductsRow(global::System.Data.DataRowBuilder rb) : 
                    base(rb) {
                this.tableProducts = ((ProductsDataTable)(this.Table));
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public int ProductID {
                get {
                    return ((int)(this[this.tableProducts.ProductIDColumn]));
                }
                set {
                    this[this.tableProducts.ProductIDColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string ProductName {
                get {
                    try {
                        return ((string)(this[this.tableProducts.ProductNameColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ProductName\' in table \'Products\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableProducts.ProductNameColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public int SupplierID {
                get {
                    try {
                        return ((int)(this[this.tableProducts.SupplierIDColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'SupplierID\' in table \'Products\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableProducts.SupplierIDColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public int CategoryID {
                get {
                    try {
                        return ((int)(this[this.tableProducts.CategoryIDColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'CategoryID\' in table \'Products\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableProducts.CategoryIDColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string QuantityPerUnit {
                get {
                    try {
                        return ((string)(this[this.tableProducts.QuantityPerUnitColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'QuantityPerUnit\' in table \'Products\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableProducts.QuantityPerUnitColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public decimal UnitPrice {
                get {
                    try {
                        return ((decimal)(this[this.tableProducts.UnitPriceColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'UnitPrice\' in table \'Products\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableProducts.UnitPriceColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public short UnitsInStock {
                get {
                    try {
                        return ((short)(this[this.tableProducts.UnitsInStockColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'UnitsInStock\' in table \'Products\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableProducts.UnitsInStockColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public short UnitsOnOrder {
                get {
                    try {
                        return ((short)(this[this.tableProducts.UnitsOnOrderColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'UnitsOnOrder\' in table \'Products\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableProducts.UnitsOnOrderColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public short ReorderLevel {
                get {
                    try {
                        return ((short)(this[this.tableProducts.ReorderLevelColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'ReorderLevel\' in table \'Products\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableProducts.ReorderLevelColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool Discontinued {
                get {
                    try {
                        return ((bool)(this[this.tableProducts.DiscontinuedColumn]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'Discontinued\' in table \'Products\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableProducts.DiscontinuedColumn] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public string EAN13 {
                get {
                    try {
                        return ((string)(this[this.tableProducts.EAN13Column]));
                    }
                    catch (global::System.InvalidCastException e) {
                        throw new global::System.Data.StrongTypingException("The value for column \'EAN13\' in table \'Products\' is DBNull.", e);
                    }
                }
                set {
                    this[this.tableProducts.EAN13Column] = value;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsProductNameNull() {
                return this.IsNull(this.tableProducts.ProductNameColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetProductNameNull() {
                this[this.tableProducts.ProductNameColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsSupplierIDNull() {
                return this.IsNull(this.tableProducts.SupplierIDColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetSupplierIDNull() {
                this[this.tableProducts.SupplierIDColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsCategoryIDNull() {
                return this.IsNull(this.tableProducts.CategoryIDColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetCategoryIDNull() {
                this[this.tableProducts.CategoryIDColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsQuantityPerUnitNull() {
                return this.IsNull(this.tableProducts.QuantityPerUnitColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetQuantityPerUnitNull() {
                this[this.tableProducts.QuantityPerUnitColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsUnitPriceNull() {
                return this.IsNull(this.tableProducts.UnitPriceColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetUnitPriceNull() {
                this[this.tableProducts.UnitPriceColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsUnitsInStockNull() {
                return this.IsNull(this.tableProducts.UnitsInStockColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetUnitsInStockNull() {
                this[this.tableProducts.UnitsInStockColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsUnitsOnOrderNull() {
                return this.IsNull(this.tableProducts.UnitsOnOrderColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetUnitsOnOrderNull() {
                this[this.tableProducts.UnitsOnOrderColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsReorderLevelNull() {
                return this.IsNull(this.tableProducts.ReorderLevelColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetReorderLevelNull() {
                this[this.tableProducts.ReorderLevelColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsDiscontinuedNull() {
                return this.IsNull(this.tableProducts.DiscontinuedColumn);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetDiscontinuedNull() {
                this[this.tableProducts.DiscontinuedColumn] = global::System.Convert.DBNull;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public bool IsEAN13Null() {
                return this.IsNull(this.tableProducts.EAN13Column);
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public void SetEAN13Null() {
                this[this.tableProducts.EAN13Column] = global::System.Convert.DBNull;
            }
        }
        
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public class OrderDetailsRowChangeEvent : global::System.EventArgs {
            
            private OrderDetailsRow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrderDetailsRowChangeEvent(OrderDetailsRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrderDetailsRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
        
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public class OrdersRowChangeEvent : global::System.EventArgs {
            
            private OrdersRow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrdersRowChangeEvent(OrdersRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public OrdersRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
        
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public class ProductsRowChangeEvent : global::System.EventArgs {
            
            private ProductsRow eventRow;
            
            private global::System.Data.DataRowAction eventAction;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public ProductsRowChangeEvent(ProductsRow row, global::System.Data.DataRowAction action) {
                this.eventRow = row;
                this.eventAction = action;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public ProductsRow Row {
                get {
                    return this.eventRow;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            public global::System.Data.DataRowAction Action {
                get {
                    return this.eventAction;
                }
            }
        }
    }
}
namespace Modules.DataBinding.nwindOrdersTableAdapters {
    
    
    
    
    
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class OrderDetailsTableAdapter : global::System.ComponentModel.Component {
        
        private global::System.Data.OleDb.OleDbDataAdapter _adapter;
        
        private global::System.Data.OleDb.OleDbConnection _connection;
        
        private global::System.Data.OleDb.OleDbTransaction _transaction;
        
        private global::System.Data.OleDb.OleDbCommand[] _commandCollection;
        
        private bool _clearBeforeFill;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public OrderDetailsTableAdapter() {
            this.ClearBeforeFill = true;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected internal global::System.Data.OleDb.OleDbDataAdapter Adapter {
            get {
                if ((this._adapter == null)) {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal global::System.Data.OleDb.OleDbConnection Connection {
            get {
                if ((this._connection == null)) {
                    this.InitConnection();
                }
                return this._connection;
            }
            set {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null)) {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null)) {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null)) {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    if ((this.CommandCollection[i] != null)) {
                        ((global::System.Data.OleDb.OleDbCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal global::System.Data.OleDb.OleDbTransaction Transaction {
            get {
                return this._transaction;
            }
            set {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.DeleteCommand != null))) {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.InsertCommand != null))) {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.UpdateCommand != null))) {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected global::System.Data.OleDb.OleDbCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool ClearBeforeFill {
            get {
                return this._clearBeforeFill;
            }
            set {
                this._clearBeforeFill = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitAdapter() {
            this._adapter = new global::System.Data.OleDb.OleDbDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "OrderDetails";
            tableMapping.ColumnMappings.Add("OrderID", "OrderID");
            tableMapping.ColumnMappings.Add("Quantity", "Quantity");
            tableMapping.ColumnMappings.Add("UnitPrice", "UnitPrice");
            tableMapping.ColumnMappings.Add("Discount", "Discount");
            tableMapping.ColumnMappings.Add("ProductName", "ProductName");
            tableMapping.ColumnMappings.Add("Supplier", "Supplier");
            this._adapter.TableMappings.Add(tableMapping);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitConnection() {
            this._connection = new global::System.Data.OleDb.OleDbConnection();
            this._connection.ConnectionString = global::SpreadsheetDemo.Properties.Settings.Default.nwindConnectionString;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.OleDb.OleDbCommand[1];
            this._commandCollection[0] = new global::System.Data.OleDb.OleDbCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT OrderID, Quantity, UnitPrice, Discount, ProductName, Supplier FROM OrderDe" +
                "tails";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(nwindOrders.OrderDetailsDataTable dataTable) {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true)) {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual nwindOrders.OrderDetailsDataTable GetData() {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            nwindOrders.OrderDetailsDataTable dataTable = new nwindOrders.OrderDetailsDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
    }
    
    
    
    
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class OrdersTableAdapter : global::System.ComponentModel.Component {
        
        private global::System.Data.OleDb.OleDbDataAdapter _adapter;
        
        private global::System.Data.OleDb.OleDbConnection _connection;
        
        private global::System.Data.OleDb.OleDbTransaction _transaction;
        
        private global::System.Data.OleDb.OleDbCommand[] _commandCollection;
        
        private bool _clearBeforeFill;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public OrdersTableAdapter() {
            this.ClearBeforeFill = true;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected internal global::System.Data.OleDb.OleDbDataAdapter Adapter {
            get {
                if ((this._adapter == null)) {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal global::System.Data.OleDb.OleDbConnection Connection {
            get {
                if ((this._connection == null)) {
                    this.InitConnection();
                }
                return this._connection;
            }
            set {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null)) {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null)) {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null)) {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    if ((this.CommandCollection[i] != null)) {
                        ((global::System.Data.OleDb.OleDbCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal global::System.Data.OleDb.OleDbTransaction Transaction {
            get {
                return this._transaction;
            }
            set {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.DeleteCommand != null))) {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.InsertCommand != null))) {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.UpdateCommand != null))) {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected global::System.Data.OleDb.OleDbCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool ClearBeforeFill {
            get {
                return this._clearBeforeFill;
            }
            set {
                this._clearBeforeFill = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitAdapter() {
            this._adapter = new global::System.Data.OleDb.OleDbDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Orders";
            tableMapping.ColumnMappings.Add("OrderID", "OrderID");
            tableMapping.ColumnMappings.Add("CustomerID", "CustomerID");
            tableMapping.ColumnMappings.Add("EmployeeID", "EmployeeID");
            tableMapping.ColumnMappings.Add("OrderDate", "OrderDate");
            tableMapping.ColumnMappings.Add("RequiredDate", "RequiredDate");
            tableMapping.ColumnMappings.Add("ShippedDate", "ShippedDate");
            tableMapping.ColumnMappings.Add("ShipVia", "ShipVia");
            tableMapping.ColumnMappings.Add("Freight", "Freight");
            tableMapping.ColumnMappings.Add("ShipName", "ShipName");
            tableMapping.ColumnMappings.Add("ShipAddress", "ShipAddress");
            tableMapping.ColumnMappings.Add("ShipCity", "ShipCity");
            tableMapping.ColumnMappings.Add("ShipRegion", "ShipRegion");
            tableMapping.ColumnMappings.Add("ShipPostalCode", "ShipPostalCode");
            tableMapping.ColumnMappings.Add("ShipCountry", "ShipCountry");
            this._adapter.TableMappings.Add(tableMapping);
            this._adapter.DeleteCommand = new global::System.Data.OleDb.OleDbCommand();
            this._adapter.DeleteCommand.Connection = this.Connection;
            this._adapter.DeleteCommand.CommandText = @"DELETE FROM `Orders` WHERE ((`OrderID` = ?) AND ((? = 1 AND `CustomerID` IS NULL) OR (`CustomerID` = ?)) AND ((? = 1 AND `EmployeeID` IS NULL) OR (`EmployeeID` = ?)) AND ((? = 1 AND `OrderDate` IS NULL) OR (`OrderDate` = ?)) AND ((? = 1 AND `RequiredDate` IS NULL) OR (`RequiredDate` = ?)) AND ((? = 1 AND `ShippedDate` IS NULL) OR (`ShippedDate` = ?)) AND ((? = 1 AND `ShipVia` IS NULL) OR (`ShipVia` = ?)) AND ((? = 1 AND `Freight` IS NULL) OR (`Freight` = ?)) AND ((? = 1 AND `ShipName` IS NULL) OR (`ShipName` = ?)) AND ((? = 1 AND `ShipAddress` IS NULL) OR (`ShipAddress` = ?)) AND ((? = 1 AND `ShipCity` IS NULL) OR (`ShipCity` = ?)) AND ((? = 1 AND `ShipRegion` IS NULL) OR (`ShipRegion` = ?)) AND ((? = 1 AND `ShipPostalCode` IS NULL) OR (`ShipPostalCode` = ?)) AND ((? = 1 AND `ShipCountry` IS NULL) OR (`ShipCountry` = ?)))";
            this._adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_OrderID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "OrderID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_CustomerID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CustomerID", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_CustomerID", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CustomerID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_EmployeeID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmployeeID", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_EmployeeID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmployeeID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_OrderDate", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "OrderDate", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_OrderDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "OrderDate", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_RequiredDate", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RequiredDate", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_RequiredDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RequiredDate", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShippedDate", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShippedDate", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShippedDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShippedDate", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipVia", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipVia", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipVia", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipVia", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_Freight", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Freight", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_Freight", global::System.Data.OleDb.OleDbType.Currency, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Freight", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipName", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipName", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipName", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipName", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipAddress", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipAddress", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipAddress", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipAddress", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipCity", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCity", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipCity", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCity", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipRegion", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipRegion", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipRegion", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipRegion", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipPostalCode", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipPostalCode", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipPostalCode", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipPostalCode", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipCountry", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCountry", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipCountry", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCountry", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.InsertCommand = new global::System.Data.OleDb.OleDbCommand();
            this._adapter.InsertCommand.Connection = this.Connection;
            this._adapter.InsertCommand.CommandText = "INSERT INTO `Orders` (`CustomerID`, `EmployeeID`, `OrderDate`, `RequiredDate`, `S" +
                "hippedDate`, `ShipVia`, `Freight`, `ShipName`, `ShipAddress`, `ShipCity`, `ShipR" +
                "egion`, `ShipPostalCode`, `ShipCountry`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?" +
                ", ?, ?)";
            this._adapter.InsertCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("CustomerID", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CustomerID", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("EmployeeID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmployeeID", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("OrderDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "OrderDate", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("RequiredDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RequiredDate", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShippedDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShippedDate", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipVia", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipVia", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Freight", global::System.Data.OleDb.OleDbType.Currency, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Freight", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipName", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipName", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipAddress", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipAddress", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipCity", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCity", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipRegion", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipRegion", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipPostalCode", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipPostalCode", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipCountry", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCountry", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand = new global::System.Data.OleDb.OleDbCommand();
            this._adapter.UpdateCommand.Connection = this.Connection;
            this._adapter.UpdateCommand.CommandText = @"UPDATE `Orders` SET `CustomerID` = ?, `EmployeeID` = ?, `OrderDate` = ?, `RequiredDate` = ?, `ShippedDate` = ?, `ShipVia` = ?, `Freight` = ?, `ShipName` = ?, `ShipAddress` = ?, `ShipCity` = ?, `ShipRegion` = ?, `ShipPostalCode` = ?, `ShipCountry` = ? WHERE ((`OrderID` = ?) AND ((? = 1 AND `CustomerID` IS NULL) OR (`CustomerID` = ?)) AND ((? = 1 AND `EmployeeID` IS NULL) OR (`EmployeeID` = ?)) AND ((? = 1 AND `OrderDate` IS NULL) OR (`OrderDate` = ?)) AND ((? = 1 AND `RequiredDate` IS NULL) OR (`RequiredDate` = ?)) AND ((? = 1 AND `ShippedDate` IS NULL) OR (`ShippedDate` = ?)) AND ((? = 1 AND `ShipVia` IS NULL) OR (`ShipVia` = ?)) AND ((? = 1 AND `Freight` IS NULL) OR (`Freight` = ?)) AND ((? = 1 AND `ShipName` IS NULL) OR (`ShipName` = ?)) AND ((? = 1 AND `ShipAddress` IS NULL) OR (`ShipAddress` = ?)) AND ((? = 1 AND `ShipCity` IS NULL) OR (`ShipCity` = ?)) AND ((? = 1 AND `ShipRegion` IS NULL) OR (`ShipRegion` = ?)) AND ((? = 1 AND `ShipPostalCode` IS NULL) OR (`ShipPostalCode` = ?)) AND ((? = 1 AND `ShipCountry` IS NULL) OR (`ShipCountry` = ?)))";
            this._adapter.UpdateCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("CustomerID", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CustomerID", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("EmployeeID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmployeeID", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("OrderDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "OrderDate", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("RequiredDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RequiredDate", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShippedDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShippedDate", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipVia", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipVia", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Freight", global::System.Data.OleDb.OleDbType.Currency, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Freight", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipName", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipName", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipAddress", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipAddress", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipCity", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCity", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipRegion", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipRegion", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipPostalCode", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipPostalCode", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ShipCountry", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCountry", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_OrderID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "OrderID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_CustomerID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CustomerID", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_CustomerID", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CustomerID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_EmployeeID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmployeeID", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_EmployeeID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EmployeeID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_OrderDate", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "OrderDate", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_OrderDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "OrderDate", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_RequiredDate", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RequiredDate", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_RequiredDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "RequiredDate", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShippedDate", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShippedDate", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShippedDate", global::System.Data.OleDb.OleDbType.Date, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShippedDate", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipVia", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipVia", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipVia", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipVia", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_Freight", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Freight", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_Freight", global::System.Data.OleDb.OleDbType.Currency, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Freight", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipName", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipName", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipName", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipName", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipAddress", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipAddress", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipAddress", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipAddress", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipCity", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCity", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipCity", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCity", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipRegion", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipRegion", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipRegion", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipRegion", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipPostalCode", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipPostalCode", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipPostalCode", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipPostalCode", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ShipCountry", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCountry", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ShipCountry", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ShipCountry", global::System.Data.DataRowVersion.Original, false, null));
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitConnection() {
            this._connection = new global::System.Data.OleDb.OleDbConnection();
            this._connection.ConnectionString = global::SpreadsheetDemo.Properties.Settings.Default.nwindConnectionString;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.OleDb.OleDbCommand[1];
            this._commandCollection[0] = new global::System.Data.OleDb.OleDbCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT OrderID, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, Shi" +
                "pVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, Ship" +
                "Country FROM Orders";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(nwindOrders.OrdersDataTable dataTable) {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true)) {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual nwindOrders.OrdersDataTable GetData() {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            nwindOrders.OrdersDataTable dataTable = new nwindOrders.OrdersDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(nwindOrders.OrdersDataTable dataTable) {
            return this.Adapter.Update(dataTable);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(nwindOrders dataSet) {
            return this.Adapter.Update(dataSet, "Orders");
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow dataRow) {
            return this.Adapter.Update(new global::System.Data.DataRow[] {
                        dataRow});
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow[] dataRows) {
            return this.Adapter.Update(dataRows);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_OrderID, string Original_CustomerID, global::System.Nullable<int> Original_EmployeeID, global::System.Nullable<global::System.DateTime> Original_OrderDate, global::System.Nullable<global::System.DateTime> Original_RequiredDate, global::System.Nullable<global::System.DateTime> Original_ShippedDate, global::System.Nullable<int> Original_ShipVia, global::System.Nullable<decimal> Original_Freight, string Original_ShipName, string Original_ShipAddress, string Original_ShipCity, string Original_ShipRegion, string Original_ShipPostalCode, string Original_ShipCountry) {
            this.Adapter.DeleteCommand.Parameters[0].Value = ((int)(Original_OrderID));
            if ((Original_CustomerID == null)) {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[2].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[2].Value = ((string)(Original_CustomerID));
            }
            if ((Original_EmployeeID.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[3].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[4].Value = ((int)(Original_EmployeeID.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[3].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[4].Value = global::System.DBNull.Value;
            }
            if ((Original_OrderDate.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[5].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[6].Value = ((System.DateTime)(Original_OrderDate.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[5].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[6].Value = global::System.DBNull.Value;
            }
            if ((Original_RequiredDate.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[7].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[8].Value = ((System.DateTime)(Original_RequiredDate.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[7].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[8].Value = global::System.DBNull.Value;
            }
            if ((Original_ShippedDate.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[9].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[10].Value = ((System.DateTime)(Original_ShippedDate.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[9].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[10].Value = global::System.DBNull.Value;
            }
            if ((Original_ShipVia.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[11].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[12].Value = ((int)(Original_ShipVia.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[11].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[12].Value = global::System.DBNull.Value;
            }
            if ((Original_Freight.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[13].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[14].Value = ((decimal)(Original_Freight.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[13].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[14].Value = global::System.DBNull.Value;
            }
            if ((Original_ShipName == null)) {
                this.Adapter.DeleteCommand.Parameters[15].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[16].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.DeleteCommand.Parameters[15].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[16].Value = ((string)(Original_ShipName));
            }
            if ((Original_ShipAddress == null)) {
                this.Adapter.DeleteCommand.Parameters[17].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[18].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.DeleteCommand.Parameters[17].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[18].Value = ((string)(Original_ShipAddress));
            }
            if ((Original_ShipCity == null)) {
                this.Adapter.DeleteCommand.Parameters[19].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[20].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.DeleteCommand.Parameters[19].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[20].Value = ((string)(Original_ShipCity));
            }
            if ((Original_ShipRegion == null)) {
                this.Adapter.DeleteCommand.Parameters[21].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[22].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.DeleteCommand.Parameters[21].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[22].Value = ((string)(Original_ShipRegion));
            }
            if ((Original_ShipPostalCode == null)) {
                this.Adapter.DeleteCommand.Parameters[23].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[24].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.DeleteCommand.Parameters[23].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[24].Value = ((string)(Original_ShipPostalCode));
            }
            if ((Original_ShipCountry == null)) {
                this.Adapter.DeleteCommand.Parameters[25].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[26].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.DeleteCommand.Parameters[25].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[26].Value = ((string)(Original_ShipCountry));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((int)(this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open)
                        != (int)global::System.Data.ConnectionState.Open)) {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(string CustomerID, global::System.Nullable<int> EmployeeID, global::System.Nullable<global::System.DateTime> OrderDate, global::System.Nullable<global::System.DateTime> RequiredDate, global::System.Nullable<global::System.DateTime> ShippedDate, global::System.Nullable<int> ShipVia, global::System.Nullable<decimal> Freight, string ShipName, string ShipAddress, string ShipCity, string ShipRegion, string ShipPostalCode, string ShipCountry) {
            if ((CustomerID == null)) {
                this.Adapter.InsertCommand.Parameters[0].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.InsertCommand.Parameters[0].Value = ((string)(CustomerID));
            }
            if ((EmployeeID.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[1].Value = ((int)(EmployeeID.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[1].Value = global::System.DBNull.Value;
            }
            if ((OrderDate.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[2].Value = ((System.DateTime)(OrderDate.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[2].Value = global::System.DBNull.Value;
            }
            if ((RequiredDate.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[3].Value = ((System.DateTime)(RequiredDate.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[3].Value = global::System.DBNull.Value;
            }
            if ((ShippedDate.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[4].Value = ((System.DateTime)(ShippedDate.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[4].Value = global::System.DBNull.Value;
            }
            if ((ShipVia.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[5].Value = ((int)(ShipVia.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[5].Value = global::System.DBNull.Value;
            }
            if ((Freight.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[6].Value = ((decimal)(Freight.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[6].Value = global::System.DBNull.Value;
            }
            if ((ShipName == null)) {
                this.Adapter.InsertCommand.Parameters[7].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.InsertCommand.Parameters[7].Value = ((string)(ShipName));
            }
            if ((ShipAddress == null)) {
                this.Adapter.InsertCommand.Parameters[8].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.InsertCommand.Parameters[8].Value = ((string)(ShipAddress));
            }
            if ((ShipCity == null)) {
                this.Adapter.InsertCommand.Parameters[9].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.InsertCommand.Parameters[9].Value = ((string)(ShipCity));
            }
            if ((ShipRegion == null)) {
                this.Adapter.InsertCommand.Parameters[10].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.InsertCommand.Parameters[10].Value = ((string)(ShipRegion));
            }
            if ((ShipPostalCode == null)) {
                this.Adapter.InsertCommand.Parameters[11].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.InsertCommand.Parameters[11].Value = ((string)(ShipPostalCode));
            }
            if ((ShipCountry == null)) {
                this.Adapter.InsertCommand.Parameters[12].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.InsertCommand.Parameters[12].Value = ((string)(ShipCountry));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((int)(this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != (int)global::System.Data.ConnectionState.Open)) {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(
                    string CustomerID, 
                    global::System.Nullable<int> EmployeeID, 
                    global::System.Nullable<global::System.DateTime> OrderDate, 
                    global::System.Nullable<global::System.DateTime> RequiredDate, 
                    global::System.Nullable<global::System.DateTime> ShippedDate, 
                    global::System.Nullable<int> ShipVia, 
                    global::System.Nullable<decimal> Freight, 
                    string ShipName, 
                    string ShipAddress, 
                    string ShipCity, 
                    string ShipRegion, 
                    string ShipPostalCode, 
                    string ShipCountry, 
                    int Original_OrderID, 
                    string Original_CustomerID, 
                    global::System.Nullable<int> Original_EmployeeID, 
                    global::System.Nullable<global::System.DateTime> Original_OrderDate, 
                    global::System.Nullable<global::System.DateTime> Original_RequiredDate, 
                    global::System.Nullable<global::System.DateTime> Original_ShippedDate, 
                    global::System.Nullable<int> Original_ShipVia, 
                    global::System.Nullable<decimal> Original_Freight, 
                    string Original_ShipName, 
                    string Original_ShipAddress, 
                    string Original_ShipCity, 
                    string Original_ShipRegion, 
                    string Original_ShipPostalCode, 
                    string Original_ShipCountry) {
            if ((CustomerID == null)) {
                this.Adapter.UpdateCommand.Parameters[0].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[0].Value = ((string)(CustomerID));
            }
            if ((EmployeeID.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(EmployeeID.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[1].Value = global::System.DBNull.Value;
            }
            if ((OrderDate.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((System.DateTime)(OrderDate.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[2].Value = global::System.DBNull.Value;
            }
            if ((RequiredDate.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((System.DateTime)(RequiredDate.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[3].Value = global::System.DBNull.Value;
            }
            if ((ShippedDate.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((System.DateTime)(ShippedDate.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[4].Value = global::System.DBNull.Value;
            }
            if ((ShipVia.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[5].Value = ((int)(ShipVia.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[5].Value = global::System.DBNull.Value;
            }
            if ((Freight.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[6].Value = ((decimal)(Freight.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[6].Value = global::System.DBNull.Value;
            }
            if ((ShipName == null)) {
                this.Adapter.UpdateCommand.Parameters[7].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[7].Value = ((string)(ShipName));
            }
            if ((ShipAddress == null)) {
                this.Adapter.UpdateCommand.Parameters[8].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[8].Value = ((string)(ShipAddress));
            }
            if ((ShipCity == null)) {
                this.Adapter.UpdateCommand.Parameters[9].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[9].Value = ((string)(ShipCity));
            }
            if ((ShipRegion == null)) {
                this.Adapter.UpdateCommand.Parameters[10].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[10].Value = ((string)(ShipRegion));
            }
            if ((ShipPostalCode == null)) {
                this.Adapter.UpdateCommand.Parameters[11].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[11].Value = ((string)(ShipPostalCode));
            }
            if ((ShipCountry == null)) {
                this.Adapter.UpdateCommand.Parameters[12].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[12].Value = ((string)(ShipCountry));
            }
            this.Adapter.UpdateCommand.Parameters[13].Value = ((int)(Original_OrderID));
            if ((Original_CustomerID == null)) {
                this.Adapter.UpdateCommand.Parameters[14].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[15].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[14].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[15].Value = ((string)(Original_CustomerID));
            }
            if ((Original_EmployeeID.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[16].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[17].Value = ((int)(Original_EmployeeID.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[16].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[17].Value = global::System.DBNull.Value;
            }
            if ((Original_OrderDate.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[18].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[19].Value = ((System.DateTime)(Original_OrderDate.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[18].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[19].Value = global::System.DBNull.Value;
            }
            if ((Original_RequiredDate.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[20].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[21].Value = ((System.DateTime)(Original_RequiredDate.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[20].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[21].Value = global::System.DBNull.Value;
            }
            if ((Original_ShippedDate.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[22].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[23].Value = ((System.DateTime)(Original_ShippedDate.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[22].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[23].Value = global::System.DBNull.Value;
            }
            if ((Original_ShipVia.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[24].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[25].Value = ((int)(Original_ShipVia.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[24].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[25].Value = global::System.DBNull.Value;
            }
            if ((Original_Freight.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[26].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[27].Value = ((decimal)(Original_Freight.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[26].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[27].Value = global::System.DBNull.Value;
            }
            if ((Original_ShipName == null)) {
                this.Adapter.UpdateCommand.Parameters[28].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[29].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[28].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[29].Value = ((string)(Original_ShipName));
            }
            if ((Original_ShipAddress == null)) {
                this.Adapter.UpdateCommand.Parameters[30].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[31].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[30].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[31].Value = ((string)(Original_ShipAddress));
            }
            if ((Original_ShipCity == null)) {
                this.Adapter.UpdateCommand.Parameters[32].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[33].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[32].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[33].Value = ((string)(Original_ShipCity));
            }
            if ((Original_ShipRegion == null)) {
                this.Adapter.UpdateCommand.Parameters[34].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[35].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[34].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[35].Value = ((string)(Original_ShipRegion));
            }
            if ((Original_ShipPostalCode == null)) {
                this.Adapter.UpdateCommand.Parameters[36].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[37].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[36].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[37].Value = ((string)(Original_ShipPostalCode));
            }
            if ((Original_ShipCountry == null)) {
                this.Adapter.UpdateCommand.Parameters[38].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[39].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[38].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[39].Value = ((string)(Original_ShipCountry));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((int)(this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != (int)global::System.Data.ConnectionState.Open)) {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }
    }
    
    
    
    
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DataObjectAttribute(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" +
        ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
    public partial class ProductsTableAdapter : global::System.ComponentModel.Component {
        
        private global::System.Data.OleDb.OleDbDataAdapter _adapter;
        
        private global::System.Data.OleDb.OleDbConnection _connection;
        
        private global::System.Data.OleDb.OleDbTransaction _transaction;
        
        private global::System.Data.OleDb.OleDbCommand[] _commandCollection;
        
        private bool _clearBeforeFill;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public ProductsTableAdapter() {
            this.ClearBeforeFill = true;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected internal global::System.Data.OleDb.OleDbDataAdapter Adapter {
            get {
                if ((this._adapter == null)) {
                    this.InitAdapter();
                }
                return this._adapter;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal global::System.Data.OleDb.OleDbConnection Connection {
            get {
                if ((this._connection == null)) {
                    this.InitConnection();
                }
                return this._connection;
            }
            set {
                this._connection = value;
                if ((this.Adapter.InsertCommand != null)) {
                    this.Adapter.InsertCommand.Connection = value;
                }
                if ((this.Adapter.DeleteCommand != null)) {
                    this.Adapter.DeleteCommand.Connection = value;
                }
                if ((this.Adapter.UpdateCommand != null)) {
                    this.Adapter.UpdateCommand.Connection = value;
                }
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    if ((this.CommandCollection[i] != null)) {
                        ((global::System.Data.OleDb.OleDbCommand)(this.CommandCollection[i])).Connection = value;
                    }
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        internal global::System.Data.OleDb.OleDbTransaction Transaction {
            get {
                return this._transaction;
            }
            set {
                this._transaction = value;
                for (int i = 0; (i < this.CommandCollection.Length); i = (i + 1)) {
                    this.CommandCollection[i].Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.DeleteCommand != null))) {
                    this.Adapter.DeleteCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.InsertCommand != null))) {
                    this.Adapter.InsertCommand.Transaction = this._transaction;
                }
                if (((this.Adapter != null) 
                            && (this.Adapter.UpdateCommand != null))) {
                    this.Adapter.UpdateCommand.Transaction = this._transaction;
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected global::System.Data.OleDb.OleDbCommand[] CommandCollection {
            get {
                if ((this._commandCollection == null)) {
                    this.InitCommandCollection();
                }
                return this._commandCollection;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool ClearBeforeFill {
            get {
                return this._clearBeforeFill;
            }
            set {
                this._clearBeforeFill = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitAdapter() {
            this._adapter = new global::System.Data.OleDb.OleDbDataAdapter();
            global::System.Data.Common.DataTableMapping tableMapping = new global::System.Data.Common.DataTableMapping();
            tableMapping.SourceTable = "Table";
            tableMapping.DataSetTable = "Products";
            tableMapping.ColumnMappings.Add("ProductID", "ProductID");
            tableMapping.ColumnMappings.Add("ProductName", "ProductName");
            tableMapping.ColumnMappings.Add("SupplierID", "SupplierID");
            tableMapping.ColumnMappings.Add("CategoryID", "CategoryID");
            tableMapping.ColumnMappings.Add("QuantityPerUnit", "QuantityPerUnit");
            tableMapping.ColumnMappings.Add("UnitPrice", "UnitPrice");
            tableMapping.ColumnMappings.Add("UnitsInStock", "UnitsInStock");
            tableMapping.ColumnMappings.Add("UnitsOnOrder", "UnitsOnOrder");
            tableMapping.ColumnMappings.Add("ReorderLevel", "ReorderLevel");
            tableMapping.ColumnMappings.Add("Discontinued", "Discontinued");
            tableMapping.ColumnMappings.Add("EAN13", "EAN13");
            this._adapter.TableMappings.Add(tableMapping);
            this._adapter.DeleteCommand = new global::System.Data.OleDb.OleDbCommand();
            this._adapter.DeleteCommand.Connection = this.Connection;
            this._adapter.DeleteCommand.CommandText = @"DELETE FROM `Products` WHERE ((`ProductID` = ?) AND ((? = 1 AND `ProductName` IS NULL) OR (`ProductName` = ?)) AND ((? = 1 AND `SupplierID` IS NULL) OR (`SupplierID` = ?)) AND ((? = 1 AND `CategoryID` IS NULL) OR (`CategoryID` = ?)) AND ((? = 1 AND `QuantityPerUnit` IS NULL) OR (`QuantityPerUnit` = ?)) AND ((? = 1 AND `UnitPrice` IS NULL) OR (`UnitPrice` = ?)) AND ((? = 1 AND `UnitsInStock` IS NULL) OR (`UnitsInStock` = ?)) AND ((? = 1 AND `UnitsOnOrder` IS NULL) OR (`UnitsOnOrder` = ?)) AND ((? = 1 AND `ReorderLevel` IS NULL) OR (`ReorderLevel` = ?)) AND ((? = 1 AND `Discontinued` IS NULL) OR (`Discontinued` = ?)) AND ((? = 1 AND `EAN13` IS NULL) OR (`EAN13` = ?)))";
            this._adapter.DeleteCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ProductID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ProductID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ProductName", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ProductName", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ProductName", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ProductName", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_SupplierID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SupplierID", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_SupplierID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SupplierID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_CategoryID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CategoryID", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_CategoryID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CategoryID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_QuantityPerUnit", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "QuantityPerUnit", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_QuantityPerUnit", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "QuantityPerUnit", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_UnitPrice", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitPrice", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_UnitPrice", global::System.Data.OleDb.OleDbType.Currency, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitPrice", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_UnitsInStock", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsInStock", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_UnitsInStock", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsInStock", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_UnitsOnOrder", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsOnOrder", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_UnitsOnOrder", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsOnOrder", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ReorderLevel", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ReorderLevel", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ReorderLevel", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ReorderLevel", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_Discontinued", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Discontinued", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_Discontinued", global::System.Data.OleDb.OleDbType.Boolean, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Discontinued", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_EAN13", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EAN13", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.DeleteCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_EAN13", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EAN13", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.InsertCommand = new global::System.Data.OleDb.OleDbCommand();
            this._adapter.InsertCommand.Connection = this.Connection;
            this._adapter.InsertCommand.CommandText = "INSERT INTO `Products` (`ProductName`, `SupplierID`, `CategoryID`, `QuantityPerUn" +
                "it`, `UnitPrice`, `UnitsInStock`, `UnitsOnOrder`, `ReorderLevel`, `Discontinued`" +
                ", `EAN13`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            this._adapter.InsertCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ProductName", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ProductName", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("SupplierID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SupplierID", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("CategoryID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CategoryID", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("QuantityPerUnit", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "QuantityPerUnit", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("UnitPrice", global::System.Data.OleDb.OleDbType.Currency, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitPrice", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("UnitsInStock", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsInStock", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("UnitsOnOrder", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsOnOrder", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ReorderLevel", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ReorderLevel", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Discontinued", global::System.Data.OleDb.OleDbType.Boolean, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Discontinued", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.InsertCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("EAN13", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EAN13", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand = new global::System.Data.OleDb.OleDbCommand();
            this._adapter.UpdateCommand.Connection = this.Connection;
            this._adapter.UpdateCommand.CommandText = @"UPDATE `Products` SET `ProductName` = ?, `SupplierID` = ?, `CategoryID` = ?, `QuantityPerUnit` = ?, `UnitPrice` = ?, `UnitsInStock` = ?, `UnitsOnOrder` = ?, `ReorderLevel` = ?, `Discontinued` = ?, `EAN13` = ? WHERE ((`ProductID` = ?) AND ((? = 1 AND `ProductName` IS NULL) OR (`ProductName` = ?)) AND ((? = 1 AND `SupplierID` IS NULL) OR (`SupplierID` = ?)) AND ((? = 1 AND `CategoryID` IS NULL) OR (`CategoryID` = ?)) AND ((? = 1 AND `QuantityPerUnit` IS NULL) OR (`QuantityPerUnit` = ?)) AND ((? = 1 AND `UnitPrice` IS NULL) OR (`UnitPrice` = ?)) AND ((? = 1 AND `UnitsInStock` IS NULL) OR (`UnitsInStock` = ?)) AND ((? = 1 AND `UnitsOnOrder` IS NULL) OR (`UnitsOnOrder` = ?)) AND ((? = 1 AND `ReorderLevel` IS NULL) OR (`ReorderLevel` = ?)) AND ((? = 1 AND `Discontinued` IS NULL) OR (`Discontinued` = ?)) AND ((? = 1 AND `EAN13` IS NULL) OR (`EAN13` = ?)))";
            this._adapter.UpdateCommand.CommandType = global::System.Data.CommandType.Text;
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ProductName", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ProductName", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("SupplierID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SupplierID", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("CategoryID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CategoryID", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("QuantityPerUnit", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "QuantityPerUnit", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("UnitPrice", global::System.Data.OleDb.OleDbType.Currency, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitPrice", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("UnitsInStock", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsInStock", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("UnitsOnOrder", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsOnOrder", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("ReorderLevel", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ReorderLevel", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Discontinued", global::System.Data.OleDb.OleDbType.Boolean, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Discontinued", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("EAN13", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EAN13", global::System.Data.DataRowVersion.Current, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ProductID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ProductID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ProductName", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ProductName", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ProductName", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ProductName", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_SupplierID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SupplierID", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_SupplierID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "SupplierID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_CategoryID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CategoryID", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_CategoryID", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "CategoryID", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_QuantityPerUnit", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "QuantityPerUnit", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_QuantityPerUnit", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "QuantityPerUnit", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_UnitPrice", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitPrice", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_UnitPrice", global::System.Data.OleDb.OleDbType.Currency, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitPrice", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_UnitsInStock", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsInStock", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_UnitsInStock", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsInStock", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_UnitsOnOrder", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsOnOrder", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_UnitsOnOrder", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "UnitsOnOrder", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_ReorderLevel", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ReorderLevel", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_ReorderLevel", global::System.Data.OleDb.OleDbType.SmallInt, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "ReorderLevel", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_Discontinued", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Discontinued", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_Discontinued", global::System.Data.OleDb.OleDbType.Boolean, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "Discontinued", global::System.Data.DataRowVersion.Original, false, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("IsNull_EAN13", global::System.Data.OleDb.OleDbType.Integer, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EAN13", global::System.Data.DataRowVersion.Original, true, null));
            this._adapter.UpdateCommand.Parameters.Add(new global::System.Data.OleDb.OleDbParameter("Original_EAN13", global::System.Data.OleDb.OleDbType.VarWChar, 0, global::System.Data.ParameterDirection.Input, ((byte)(0)), ((byte)(0)), "EAN13", global::System.Data.DataRowVersion.Original, false, null));
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitConnection() {
            this._connection = new global::System.Data.OleDb.OleDbConnection();
            this._connection.ConnectionString = global::SpreadsheetDemo.Properties.Settings.Default.nwindConnectionString;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private void InitCommandCollection() {
            this._commandCollection = new global::System.Data.OleDb.OleDbCommand[1];
            this._commandCollection[0] = new global::System.Data.OleDb.OleDbCommand();
            this._commandCollection[0].Connection = this.Connection;
            this._commandCollection[0].CommandText = "SELECT ProductID, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice" +
                ", UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, EAN13 FROM Products";
            this._commandCollection[0].CommandType = global::System.Data.CommandType.Text;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Fill, true)]
        public virtual int Fill(nwindOrders.ProductsDataTable dataTable) {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            if ((this.ClearBeforeFill == true)) {
                dataTable.Clear();
            }
            int returnValue = this.Adapter.Fill(dataTable);
            return returnValue;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Select, true)]
        public virtual nwindOrders.ProductsDataTable GetData() {
            this.Adapter.SelectCommand = this.CommandCollection[0];
            nwindOrders.ProductsDataTable dataTable = new nwindOrders.ProductsDataTable();
            this.Adapter.Fill(dataTable);
            return dataTable;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(nwindOrders.ProductsDataTable dataTable) {
            return this.Adapter.Update(dataTable);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(nwindOrders dataSet) {
            return this.Adapter.Update(dataSet, "Products");
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow dataRow) {
            return this.Adapter.Update(new global::System.Data.DataRow[] {
                        dataRow});
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        public virtual int Update(global::System.Data.DataRow[] dataRows) {
            return this.Adapter.Update(dataRows);
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Delete, true)]
        public virtual int Delete(int Original_ProductID, string Original_ProductName, global::System.Nullable<int> Original_SupplierID, global::System.Nullable<int> Original_CategoryID, string Original_QuantityPerUnit, global::System.Nullable<decimal> Original_UnitPrice, global::System.Nullable<short> Original_UnitsInStock, global::System.Nullable<short> Original_UnitsOnOrder, global::System.Nullable<short> Original_ReorderLevel, bool Original_Discontinued, string Original_EAN13) {
            this.Adapter.DeleteCommand.Parameters[0].Value = ((int)(Original_ProductID));
            if ((Original_ProductName == null)) {
                throw new global::System.ArgumentNullException("Original_ProductName");
            }
            else {
                this.Adapter.DeleteCommand.Parameters[1].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[2].Value = ((string)(Original_ProductName));
            }
            if ((Original_SupplierID.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[3].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[4].Value = ((int)(Original_SupplierID.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[3].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[4].Value = global::System.DBNull.Value;
            }
            if ((Original_CategoryID.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[5].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[6].Value = ((int)(Original_CategoryID.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[5].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[6].Value = global::System.DBNull.Value;
            }
            if ((Original_QuantityPerUnit == null)) {
                this.Adapter.DeleteCommand.Parameters[7].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[8].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.DeleteCommand.Parameters[7].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[8].Value = ((string)(Original_QuantityPerUnit));
            }
            if ((Original_UnitPrice.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[9].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[10].Value = ((decimal)(Original_UnitPrice.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[9].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[10].Value = global::System.DBNull.Value;
            }
            if ((Original_UnitsInStock.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[11].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[12].Value = ((short)(Original_UnitsInStock.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[11].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[12].Value = global::System.DBNull.Value;
            }
            if ((Original_UnitsOnOrder.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[13].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[14].Value = ((short)(Original_UnitsOnOrder.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[13].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[14].Value = global::System.DBNull.Value;
            }
            if ((Original_ReorderLevel.HasValue == true)) {
                this.Adapter.DeleteCommand.Parameters[15].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[16].Value = ((short)(Original_ReorderLevel.Value));
            }
            else {
                this.Adapter.DeleteCommand.Parameters[15].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[16].Value = global::System.DBNull.Value;
            }
            this.Adapter.DeleteCommand.Parameters[17].Value = ((object)(0));
            this.Adapter.DeleteCommand.Parameters[18].Value = ((bool)(Original_Discontinued));
            if ((Original_EAN13 == null)) {
                this.Adapter.DeleteCommand.Parameters[19].Value = ((object)(1));
                this.Adapter.DeleteCommand.Parameters[20].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.DeleteCommand.Parameters[19].Value = ((object)(0));
                this.Adapter.DeleteCommand.Parameters[20].Value = ((string)(Original_EAN13));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.DeleteCommand.Connection.State;
            if (((int)(this.Adapter.DeleteCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != (int)global::System.Data.ConnectionState.Open)) {
                this.Adapter.DeleteCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.DeleteCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.DeleteCommand.Connection.Close();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Insert, true)]
        public virtual int Insert(string ProductName, global::System.Nullable<int> SupplierID, global::System.Nullable<int> CategoryID, string QuantityPerUnit, global::System.Nullable<decimal> UnitPrice, global::System.Nullable<short> UnitsInStock, global::System.Nullable<short> UnitsOnOrder, global::System.Nullable<short> ReorderLevel, bool Discontinued, string EAN13) {
            if ((ProductName == null)) {
                throw new global::System.ArgumentNullException("ProductName");
            }
            else {
                this.Adapter.InsertCommand.Parameters[0].Value = ((string)(ProductName));
            }
            if ((SupplierID.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[1].Value = ((int)(SupplierID.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[1].Value = global::System.DBNull.Value;
            }
            if ((CategoryID.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[2].Value = ((int)(CategoryID.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[2].Value = global::System.DBNull.Value;
            }
            if ((QuantityPerUnit == null)) {
                this.Adapter.InsertCommand.Parameters[3].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.InsertCommand.Parameters[3].Value = ((string)(QuantityPerUnit));
            }
            if ((UnitPrice.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[4].Value = ((decimal)(UnitPrice.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[4].Value = global::System.DBNull.Value;
            }
            if ((UnitsInStock.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[5].Value = ((short)(UnitsInStock.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[5].Value = global::System.DBNull.Value;
            }
            if ((UnitsOnOrder.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[6].Value = ((short)(UnitsOnOrder.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[6].Value = global::System.DBNull.Value;
            }
            if ((ReorderLevel.HasValue == true)) {
                this.Adapter.InsertCommand.Parameters[7].Value = ((short)(ReorderLevel.Value));
            }
            else {
                this.Adapter.InsertCommand.Parameters[7].Value = global::System.DBNull.Value;
            }
            this.Adapter.InsertCommand.Parameters[8].Value = ((bool)(Discontinued));
            if ((EAN13 == null)) {
                this.Adapter.InsertCommand.Parameters[9].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.InsertCommand.Parameters[9].Value = ((string)(EAN13));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.InsertCommand.Connection.State;
            if (((int)(this.Adapter.InsertCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != (int)global::System.Data.ConnectionState.Open)) {
                this.Adapter.InsertCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.InsertCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.InsertCommand.Connection.Close();
                }
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")]
        [global::System.ComponentModel.DataObjectMethodAttribute(global::System.ComponentModel.DataObjectMethodType.Update, true)]
        public virtual int Update(
                    string ProductName, 
                    global::System.Nullable<int> SupplierID, 
                    global::System.Nullable<int> CategoryID, 
                    string QuantityPerUnit, 
                    global::System.Nullable<decimal> UnitPrice, 
                    global::System.Nullable<short> UnitsInStock, 
                    global::System.Nullable<short> UnitsOnOrder, 
                    global::System.Nullable<short> ReorderLevel, 
                    bool Discontinued, 
                    string EAN13, 
                    int Original_ProductID, 
                    string Original_ProductName, 
                    global::System.Nullable<int> Original_SupplierID, 
                    global::System.Nullable<int> Original_CategoryID, 
                    string Original_QuantityPerUnit, 
                    global::System.Nullable<decimal> Original_UnitPrice, 
                    global::System.Nullable<short> Original_UnitsInStock, 
                    global::System.Nullable<short> Original_UnitsOnOrder, 
                    global::System.Nullable<short> Original_ReorderLevel, 
                    bool Original_Discontinued, 
                    string Original_EAN13) {
            if ((ProductName == null)) {
                throw new global::System.ArgumentNullException("ProductName");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[0].Value = ((string)(ProductName));
            }
            if ((SupplierID.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[1].Value = ((int)(SupplierID.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[1].Value = global::System.DBNull.Value;
            }
            if ((CategoryID.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[2].Value = ((int)(CategoryID.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[2].Value = global::System.DBNull.Value;
            }
            if ((QuantityPerUnit == null)) {
                this.Adapter.UpdateCommand.Parameters[3].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[3].Value = ((string)(QuantityPerUnit));
            }
            if ((UnitPrice.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[4].Value = ((decimal)(UnitPrice.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[4].Value = global::System.DBNull.Value;
            }
            if ((UnitsInStock.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[5].Value = ((short)(UnitsInStock.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[5].Value = global::System.DBNull.Value;
            }
            if ((UnitsOnOrder.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[6].Value = ((short)(UnitsOnOrder.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[6].Value = global::System.DBNull.Value;
            }
            if ((ReorderLevel.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[7].Value = ((short)(ReorderLevel.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[7].Value = global::System.DBNull.Value;
            }
            this.Adapter.UpdateCommand.Parameters[8].Value = ((bool)(Discontinued));
            if ((EAN13 == null)) {
                this.Adapter.UpdateCommand.Parameters[9].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[9].Value = ((string)(EAN13));
            }
            this.Adapter.UpdateCommand.Parameters[10].Value = ((int)(Original_ProductID));
            if ((Original_ProductName == null)) {
                throw new global::System.ArgumentNullException("Original_ProductName");
            }
            else {
                this.Adapter.UpdateCommand.Parameters[11].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[12].Value = ((string)(Original_ProductName));
            }
            if ((Original_SupplierID.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[13].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[14].Value = ((int)(Original_SupplierID.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[13].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[14].Value = global::System.DBNull.Value;
            }
            if ((Original_CategoryID.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[15].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[16].Value = ((int)(Original_CategoryID.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[15].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[16].Value = global::System.DBNull.Value;
            }
            if ((Original_QuantityPerUnit == null)) {
                this.Adapter.UpdateCommand.Parameters[17].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[18].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[17].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[18].Value = ((string)(Original_QuantityPerUnit));
            }
            if ((Original_UnitPrice.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[19].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[20].Value = ((decimal)(Original_UnitPrice.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[19].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[20].Value = global::System.DBNull.Value;
            }
            if ((Original_UnitsInStock.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[21].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[22].Value = ((short)(Original_UnitsInStock.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[21].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[22].Value = global::System.DBNull.Value;
            }
            if ((Original_UnitsOnOrder.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[23].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[24].Value = ((short)(Original_UnitsOnOrder.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[23].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[24].Value = global::System.DBNull.Value;
            }
            if ((Original_ReorderLevel.HasValue == true)) {
                this.Adapter.UpdateCommand.Parameters[25].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[26].Value = ((short)(Original_ReorderLevel.Value));
            }
            else {
                this.Adapter.UpdateCommand.Parameters[25].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[26].Value = global::System.DBNull.Value;
            }
            this.Adapter.UpdateCommand.Parameters[27].Value = ((object)(0));
            this.Adapter.UpdateCommand.Parameters[28].Value = ((bool)(Original_Discontinued));
            if ((Original_EAN13 == null)) {
                this.Adapter.UpdateCommand.Parameters[29].Value = ((object)(1));
                this.Adapter.UpdateCommand.Parameters[30].Value = global::System.DBNull.Value;
            }
            else {
                this.Adapter.UpdateCommand.Parameters[29].Value = ((object)(0));
                this.Adapter.UpdateCommand.Parameters[30].Value = ((string)(Original_EAN13));
            }
            global::System.Data.ConnectionState previousConnectionState = this.Adapter.UpdateCommand.Connection.State;
            if (((int)(this.Adapter.UpdateCommand.Connection.State & global::System.Data.ConnectionState.Open) 
                        != (int)global::System.Data.ConnectionState.Open)) {
                this.Adapter.UpdateCommand.Connection.Open();
            }
            try {
                int returnValue = this.Adapter.UpdateCommand.ExecuteNonQuery();
                return returnValue;
            }
            finally {
                if ((previousConnectionState == global::System.Data.ConnectionState.Closed)) {
                    this.Adapter.UpdateCommand.Connection.Close();
                }
            }
        }
    }
    
    
    
    
    [global::System.ComponentModel.DesignerCategoryAttribute("code")]
    [global::System.ComponentModel.ToolboxItem(true)]
    [global::System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerDesigner, Microsoft.VSD" +
        "esigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
    [global::System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapterManager")]
    public partial class TableAdapterManager : global::System.ComponentModel.Component {
        
        private UpdateOrderOption _updateOrder;
        
        private OrdersTableAdapter _ordersTableAdapter;
        
        private ProductsTableAdapter _productsTableAdapter;
        
        private bool _backupDataSetBeforeUpdate;
        
        private global::System.Data.IDbConnection _connection;
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public UpdateOrderOption UpdateOrder {
            get {
                return this._updateOrder;
            }
            set {
                this._updateOrder = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" +
            "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" +
            "a", "System.Drawing.Design.UITypeEditor")]
        public OrdersTableAdapter OrdersTableAdapter {
            get {
                return this._ordersTableAdapter;
            }
            set {
                this._ordersTableAdapter = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" +
            "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" +
            "a", "System.Drawing.Design.UITypeEditor")]
        public ProductsTableAdapter ProductsTableAdapter {
            get {
                return this._productsTableAdapter;
            }
            set {
                this._productsTableAdapter = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public bool BackupDataSetBeforeUpdate {
            get {
                return this._backupDataSetBeforeUpdate;
            }
            set {
                this._backupDataSetBeforeUpdate = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        public global::System.Data.IDbConnection Connection {
            get {
                if ((this._connection != null)) {
                    return this._connection;
                }
                if (((this._ordersTableAdapter != null) 
                            && (this._ordersTableAdapter.Connection != null))) {
                    return this._ordersTableAdapter.Connection;
                }
                if (((this._productsTableAdapter != null) 
                            && (this._productsTableAdapter.Connection != null))) {
                    return this._productsTableAdapter.Connection;
                }
                return null;
            }
            set {
                this._connection = value;
            }
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        [global::System.ComponentModel.Browsable(false)]
        public int TableAdapterInstanceCount {
            get {
                int count = 0;
                if ((this._ordersTableAdapter != null)) {
                    count = (count + 1);
                }
                if ((this._productsTableAdapter != null)) {
                    count = (count + 1);
                }
                return count;
            }
        }
        
        
        
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private int UpdateUpdatedRows(nwindOrders dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows) {
            int result = 0;
            if ((this._ordersTableAdapter != null)) {
                global::System.Data.DataRow[] updatedRows = dataSet.Orders.Select(null, null, global::System.Data.DataViewRowState.ModifiedCurrent);
                updatedRows = this.GetRealUpdatedRows(updatedRows, allAddedRows);
                if (((updatedRows != null) 
                            && (0 < updatedRows.Length))) {
                    result = (result + this._ordersTableAdapter.Update(updatedRows));
                    allChangedRows.AddRange(updatedRows);
                }
            }
            if ((this._productsTableAdapter != null)) {
                global::System.Data.DataRow[] updatedRows = dataSet.Products.Select(null, null, global::System.Data.DataViewRowState.ModifiedCurrent);
                updatedRows = this.GetRealUpdatedRows(updatedRows, allAddedRows);
                if (((updatedRows != null) 
                            && (0 < updatedRows.Length))) {
                    result = (result + this._productsTableAdapter.Update(updatedRows));
                    allChangedRows.AddRange(updatedRows);
                }
            }
            return result;
        }
        
        
        
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private int UpdateInsertedRows(nwindOrders dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows) {
            int result = 0;
            if ((this._ordersTableAdapter != null)) {
                global::System.Data.DataRow[] addedRows = dataSet.Orders.Select(null, null, global::System.Data.DataViewRowState.Added);
                if (((addedRows != null) 
                            && (0 < addedRows.Length))) {
                    result = (result + this._ordersTableAdapter.Update(addedRows));
                    allAddedRows.AddRange(addedRows);
                }
            }
            if ((this._productsTableAdapter != null)) {
                global::System.Data.DataRow[] addedRows = dataSet.Products.Select(null, null, global::System.Data.DataViewRowState.Added);
                if (((addedRows != null) 
                            && (0 < addedRows.Length))) {
                    result = (result + this._productsTableAdapter.Update(addedRows));
                    allAddedRows.AddRange(addedRows);
                }
            }
            return result;
        }
        
        
        
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private int UpdateDeletedRows(nwindOrders dataSet, global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows) {
            int result = 0;
            if ((this._productsTableAdapter != null)) {
                global::System.Data.DataRow[] deletedRows = dataSet.Products.Select(null, null, global::System.Data.DataViewRowState.Deleted);
                if (((deletedRows != null) 
                            && (0 < deletedRows.Length))) {
                    result = (result + this._productsTableAdapter.Update(deletedRows));
                    allChangedRows.AddRange(deletedRows);
                }
            }
            if ((this._ordersTableAdapter != null)) {
                global::System.Data.DataRow[] deletedRows = dataSet.Orders.Select(null, null, global::System.Data.DataViewRowState.Deleted);
                if (((deletedRows != null) 
                            && (0 < deletedRows.Length))) {
                    result = (result + this._ordersTableAdapter.Update(deletedRows));
                    allChangedRows.AddRange(deletedRows);
                }
            }
            return result;
        }
        
        
        
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private global::System.Data.DataRow[] GetRealUpdatedRows(global::System.Data.DataRow[] updatedRows, global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows) {
            if (((updatedRows == null) 
                        || (updatedRows.Length < 1))) {
                return updatedRows;
            }
            if (((allAddedRows == null) 
                        || (allAddedRows.Count < 1))) {
                return updatedRows;
            }
            global::System.Collections.Generic.List<global::System.Data.DataRow> realUpdatedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            for (int i = 0; (i < updatedRows.Length); i = (i + 1)) {
                global::System.Data.DataRow row = updatedRows[i];
                if ((allAddedRows.Contains(row) == false)) {
                    realUpdatedRows.Add(row);
                }
            }
            return realUpdatedRows.ToArray();
        }
        
        
        
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public virtual int UpdateAll(nwindOrders dataSet) {
            if ((dataSet == null)) {
                throw new global::System.ArgumentNullException("dataSet");
            }
            if ((dataSet.HasChanges() == false)) {
                return 0;
            }
            if (((this._ordersTableAdapter != null) 
                        && (this.MatchTableAdapterConnection(this._ordersTableAdapter.Connection) == false))) {
                throw new global::System.ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection s" +
                        "tring.");
            }
            if (((this._productsTableAdapter != null) 
                        && (this.MatchTableAdapterConnection(this._productsTableAdapter.Connection) == false))) {
                throw new global::System.ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection s" +
                        "tring.");
            }
            global::System.Data.IDbConnection workConnection = this.Connection;
            if ((workConnection == null)) {
                throw new global::System.ApplicationException("TableAdapterManager contains no connection information. Set each TableAdapterMana" +
                        "ger TableAdapter property to a valid TableAdapter instance.");
            }
            bool workConnOpened = false;
            if (((int)(workConnection.State & global::System.Data.ConnectionState.Broken) 
                        == (int)global::System.Data.ConnectionState.Broken)) {
                workConnection.Close();
            }
            if ((workConnection.State == global::System.Data.ConnectionState.Closed)) {
                workConnection.Open();
                workConnOpened = true;
            }
            global::System.Data.IDbTransaction workTransaction = workConnection.BeginTransaction();
            if ((workTransaction == null)) {
                throw new global::System.ApplicationException("The transaction cannot begin. The current data connection does not support transa" +
                        "ctions or the current state is not allowing the transaction to begin.");
            }
            global::System.Collections.Generic.List<global::System.Data.DataRow> allChangedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            global::System.Collections.Generic.List<global::System.Data.DataRow> allAddedRows = new global::System.Collections.Generic.List<global::System.Data.DataRow>();
            global::System.Collections.Generic.List<global::System.Data.Common.DataAdapter> adaptersWithAcceptChangesDuringUpdate = new global::System.Collections.Generic.List<global::System.Data.Common.DataAdapter>();
            global::System.Collections.Generic.Dictionary<object, global::System.Data.IDbConnection> revertConnections = new global::System.Collections.Generic.Dictionary<object, global::System.Data.IDbConnection>();
            int result = 0;
            global::System.Data.DataSet backupDataSet = null;
            if (this.BackupDataSetBeforeUpdate) {
                backupDataSet = new global::System.Data.DataSet();
                backupDataSet.Merge(dataSet);
            }
            try {
                
                
                if ((this._ordersTableAdapter != null)) {
                    revertConnections.Add(this._ordersTableAdapter, this._ordersTableAdapter.Connection);
                    this._ordersTableAdapter.Connection = ((global::System.Data.OleDb.OleDbConnection)(workConnection));
                    this._ordersTableAdapter.Transaction = ((global::System.Data.OleDb.OleDbTransaction)(workTransaction));
                    if (this._ordersTableAdapter.Adapter.AcceptChangesDuringUpdate) {
                        this._ordersTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
                        adaptersWithAcceptChangesDuringUpdate.Add(this._ordersTableAdapter.Adapter);
                    }
                }
                if ((this._productsTableAdapter != null)) {
                    revertConnections.Add(this._productsTableAdapter, this._productsTableAdapter.Connection);
                    this._productsTableAdapter.Connection = ((global::System.Data.OleDb.OleDbConnection)(workConnection));
                    this._productsTableAdapter.Transaction = ((global::System.Data.OleDb.OleDbTransaction)(workTransaction));
                    if (this._productsTableAdapter.Adapter.AcceptChangesDuringUpdate) {
                        this._productsTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
                        adaptersWithAcceptChangesDuringUpdate.Add(this._productsTableAdapter.Adapter);
                    }
                }
                
                
                
                if ((this.UpdateOrder == UpdateOrderOption.UpdateInsertDelete)) {
                    result = (result + this.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows));
                    result = (result + this.UpdateInsertedRows(dataSet, allAddedRows));
                }
                else {
                    result = (result + this.UpdateInsertedRows(dataSet, allAddedRows));
                    result = (result + this.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows));
                }
                result = (result + this.UpdateDeletedRows(dataSet, allChangedRows));
                
                
                
                workTransaction.Commit();
                if ((0 < allAddedRows.Count)) {
                    global::System.Data.DataRow[] rows = new System.Data.DataRow[allAddedRows.Count];
                    allAddedRows.CopyTo(rows);
                    for (int i = 0; (i < rows.Length); i = (i + 1)) {
                        global::System.Data.DataRow row = rows[i];
                        row.AcceptChanges();
                    }
                }
                if ((0 < allChangedRows.Count)) {
                    global::System.Data.DataRow[] rows = new System.Data.DataRow[allChangedRows.Count];
                    allChangedRows.CopyTo(rows);
                    for (int i = 0; (i < rows.Length); i = (i + 1)) {
                        global::System.Data.DataRow row = rows[i];
                        row.AcceptChanges();
                    }
                }
            }
            catch (global::System.Exception ex) {
                workTransaction.Rollback();
                
                if (this.BackupDataSetBeforeUpdate) {
                    global::System.Diagnostics.Debug.Assert((backupDataSet != null));
                    dataSet.Clear();
                    dataSet.Merge(backupDataSet);
                }
                else {
                    if ((0 < allAddedRows.Count)) {
                        global::System.Data.DataRow[] rows = new System.Data.DataRow[allAddedRows.Count];
                        allAddedRows.CopyTo(rows);
                        for (int i = 0; (i < rows.Length); i = (i + 1)) {
                            global::System.Data.DataRow row = rows[i];
                            row.AcceptChanges();
                            row.SetAdded();
                        }
                    }
                }
                throw ex;
            }
            finally {
                if (workConnOpened) {
                    workConnection.Close();
                }
                if ((this._ordersTableAdapter != null)) {
                    this._ordersTableAdapter.Connection = ((global::System.Data.OleDb.OleDbConnection)(revertConnections[this._ordersTableAdapter]));
                    this._ordersTableAdapter.Transaction = null;
                }
                if ((this._productsTableAdapter != null)) {
                    this._productsTableAdapter.Connection = ((global::System.Data.OleDb.OleDbConnection)(revertConnections[this._productsTableAdapter]));
                    this._productsTableAdapter.Transaction = null;
                }
                if ((0 < adaptersWithAcceptChangesDuringUpdate.Count)) {
                    global::System.Data.Common.DataAdapter[] adapters = new System.Data.Common.DataAdapter[adaptersWithAcceptChangesDuringUpdate.Count];
                    adaptersWithAcceptChangesDuringUpdate.CopyTo(adapters);
                    for (int i = 0; (i < adapters.Length); i = (i + 1)) {
                        global::System.Data.Common.DataAdapter adapter = adapters[i];
                        adapter.AcceptChangesDuringUpdate = true;
                    }
                }
            }
            return result;
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected virtual void SortSelfReferenceRows(global::System.Data.DataRow[] rows, global::System.Data.DataRelation relation, bool childFirst) {
            global::System.Array.Sort<global::System.Data.DataRow>(rows, new SelfReferenceComparer(relation, childFirst));
        }
        
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        protected virtual bool MatchTableAdapterConnection(global::System.Data.IDbConnection inputConnection) {
            if ((this._connection != null)) {
                return true;
            }
            if (((this.Connection == null) 
                        || (inputConnection == null))) {
                return true;
            }
            if (string.Equals(this.Connection.ConnectionString, inputConnection.ConnectionString, global::System.StringComparison.Ordinal)) {
                return true;
            }
            return false;
        }
        
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        public enum UpdateOrderOption {
            
            InsertUpdateDelete = 0,
            
            UpdateInsertDelete = 1,
        }
        
        
        
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
        private class SelfReferenceComparer : object, global::System.Collections.Generic.IComparer<global::System.Data.DataRow> {
            
            private global::System.Data.DataRelation _relation;
            
            private int _childFirst;
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            internal SelfReferenceComparer(global::System.Data.DataRelation relation, bool childFirst) {
                this._relation = relation;
                if (childFirst) {
                    this._childFirst = -1;
                }
                else {
                    this._childFirst = 1;
                }
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            private global::System.Data.DataRow GetRoot(global::System.Data.DataRow row, out int distance) {
                global::System.Diagnostics.Debug.Assert((row != null));
                global::System.Data.DataRow root = row;
                distance = 0;

                global::System.Collections.Generic.IDictionary<global::System.Data.DataRow, global::System.Data.DataRow> traversedRows = new global::System.Collections.Generic.Dictionary<global::System.Data.DataRow, global::System.Data.DataRow>();
                traversedRows[row] = row;

                global::System.Data.DataRow parent = row.GetParentRow(this._relation, global::System.Data.DataRowVersion.Default);
                for (
                ; ((parent != null) 
                            && (traversedRows.ContainsKey(parent) == false)); 
                ) {
                    distance = (distance + 1);
                    root = parent;
                    traversedRows[parent] = parent;
                    parent = parent.GetParentRow(this._relation, global::System.Data.DataRowVersion.Default);
                }

                if ((distance == 0)) {
                    traversedRows.Clear();
                    traversedRows[row] = row;
                    parent = row.GetParentRow(this._relation, global::System.Data.DataRowVersion.Original);
                    for (
                    ; ((parent != null) 
                                && (traversedRows.ContainsKey(parent) == false)); 
                    ) {
                        distance = (distance + 1);
                        root = parent;
                        traversedRows[parent] = parent;
                        parent = parent.GetParentRow(this._relation, global::System.Data.DataRowVersion.Original);
                    }
                }

                return root;
            }
            
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
            int global::System.Collections.Generic.IComparer<global::System.Data.DataRow>.Compare(global::System.Data.DataRow row1, global::System.Data.DataRow row2) {
                if (object.ReferenceEquals(row1, row2)) {
                    return 0;
                }
                if ((row1 == null)) {
                    return -1;
                }
                if ((row2 == null)) {
                    return 1;
                }

                int distance1 = 0;
                global::System.Data.DataRow root1 = this.GetRoot(row1, out distance1);

                int distance2 = 0;
                global::System.Data.DataRow root2 = this.GetRoot(row2, out distance2);

                if (object.ReferenceEquals(root1, root2)) 
                    return (this._childFirst * distance1.CompareTo(distance2));

                global::System.Diagnostics.Debug.Assert(((root1.Table != null) && (root2.Table != null)));
                if ((root1.Table.Rows.IndexOf(root1) < root2.Table.Rows.IndexOf(root2)))
                    return -1;

                return 1;
            }
        }
    }
}

#pragma warning restore 1591
