Imports System.Runtime.InteropServices

Namespace Modules.DataBinding

    <Global.System.SerializableAttribute()>
    <Global.System.ComponentModel.DesignerCategoryAttribute("code")>
    <Global.System.ComponentModel.ToolboxItemAttribute(True)>
    <Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedDataSetSchema")>
    <Global.System.Xml.Serialization.XmlRootAttribute("nwindOrders")>
    <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.DataSet")>
    Public Partial Class nwindOrders
        Inherits Global.System.Data.DataSet

        Private tableOrderDetails As Modules.DataBinding.nwindOrders.OrderDetailsDataTable

        Private tableOrders As Modules.DataBinding.nwindOrders.OrdersDataTable

        Private tableProducts As Modules.DataBinding.nwindOrders.ProductsDataTable

        Private _schemaSerializationMode As Global.System.Data.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Sub New()
            Me.BeginInit()
            Me.InitClass()
            Dim schemaChangedHandler As Global.System.ComponentModel.CollectionChangeEventHandler = New Global.System.ComponentModel.CollectionChangeEventHandler(AddressOf Me.SchemaChanged)
            AddHandler MyBase.Tables.CollectionChanged, schemaChangedHandler
            AddHandler MyBase.Relations.CollectionChanged, schemaChangedHandler
            Me.EndInit()
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
            MyBase.New(info, context, False)
            If(Me.IsBinarySerialized(info, context) = True) Then
                Me.InitVars(False)
                Dim schemaChangedHandler1 As Global.System.ComponentModel.CollectionChangeEventHandler = New Global.System.ComponentModel.CollectionChangeEventHandler(AddressOf Me.SchemaChanged)
                AddHandler Me.Tables.CollectionChanged, schemaChangedHandler1
                AddHandler Me.Relations.CollectionChanged, schemaChangedHandler1
                Return
            End If

            Dim strSchema As String =(CStr((info.GetValue("XmlSchema", GetType(String)))))
            If(Me.DetermineSchemaSerializationMode(info, context) = Global.System.Data.SchemaSerializationMode.IncludeSchema) Then
                Dim ds As Global.System.Data.DataSet = New Global.System.Data.DataSet()
                ds.ReadXmlSchema(New Global.System.Xml.XmlTextReader(New Global.System.IO.StringReader(strSchema)))
                If(ds.Tables("OrderDetails") IsNot Nothing) Then
                    MyBase.Tables.Add(New Modules.DataBinding.nwindOrders.OrderDetailsDataTable(ds.Tables("OrderDetails")))
                End If

                If(ds.Tables("Orders") IsNot Nothing) Then
                    MyBase.Tables.Add(New Modules.DataBinding.nwindOrders.OrdersDataTable(ds.Tables("Orders")))
                End If

                If(ds.Tables("Products") IsNot Nothing) Then
                    MyBase.Tables.Add(New Modules.DataBinding.nwindOrders.ProductsDataTable(ds.Tables("Products")))
                End If

                Me.DataSetName = ds.DataSetName
                Me.Prefix = ds.Prefix
                Me.[Namespace] = ds.[Namespace]
                Me.Locale = ds.Locale
                Me.CaseSensitive = ds.CaseSensitive
                Me.EnforceConstraints = ds.EnforceConstraints
                Me.Merge(ds, False, Global.System.Data.MissingSchemaAction.Add)
                Me.InitVars()
            Else
                Me.ReadXmlSchema(New Global.System.Xml.XmlTextReader(New Global.System.IO.StringReader(strSchema)))
            End If

            Me.GetSerializationData(info, context)
            Dim schemaChangedHandler As Global.System.ComponentModel.CollectionChangeEventHandler = New Global.System.ComponentModel.CollectionChangeEventHandler(AddressOf Me.SchemaChanged)
            AddHandler MyBase.Tables.CollectionChanged, schemaChangedHandler
            AddHandler Me.Relations.CollectionChanged, schemaChangedHandler
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.BrowsableAttribute(False)>
        <Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Content)>
        Public ReadOnly Property OrderDetails As OrderDetailsDataTable
            Get
                Return Me.tableOrderDetails
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.BrowsableAttribute(False)>
        <Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Content)>
        Public ReadOnly Property Orders As OrdersDataTable
            Get
                Return Me.tableOrders
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.BrowsableAttribute(False)>
        <Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Content)>
        Public ReadOnly Property Products As ProductsDataTable
            Get
                Return Me.tableProducts
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.BrowsableAttribute(True)>
        <Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Visible)>
        Public Overrides Property SchemaSerializationMode As Global.System.Data.SchemaSerializationMode
            Get
                Return Me._schemaSerializationMode
            End Get

            Set(ByVal value As Global.System.Data.SchemaSerializationMode)
                Me._schemaSerializationMode = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Overloads ReadOnly Property Tables As Global.System.Data.DataTableCollection
            Get
                Return MyBase.Tables
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.DesignerSerializationVisibilityAttribute(Global.System.ComponentModel.DesignerSerializationVisibility.Hidden)>
        Public Overloads ReadOnly Property Relations As Global.System.Data.DataRelationCollection
            Get
                Return MyBase.Relations
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overrides Sub InitializeDerivedDataSet()
            Me.BeginInit()
            Me.InitClass()
            Me.EndInit()
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Overrides Function Clone() As Global.System.Data.DataSet
            Dim cln As Modules.DataBinding.nwindOrders = CType((MyBase.Clone()), Modules.DataBinding.nwindOrders)
            cln.InitVars()
            cln.SchemaSerializationMode = Me.SchemaSerializationMode
            Return cln
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overrides Function ShouldSerializeTables() As Boolean
            Return False
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overrides Function ShouldSerializeRelations() As Boolean
            Return False
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overrides Sub ReadXmlSerializable(ByVal reader As Global.System.Xml.XmlReader)
            If(Me.DetermineSchemaSerializationMode(reader) = Global.System.Data.SchemaSerializationMode.IncludeSchema) Then
                Me.Reset()
                Dim ds As Global.System.Data.DataSet = New Global.System.Data.DataSet()
                ds.ReadXml(reader)
                If(ds.Tables("OrderDetails") IsNot Nothing) Then
                    MyBase.Tables.Add(New Modules.DataBinding.nwindOrders.OrderDetailsDataTable(ds.Tables("OrderDetails")))
                End If

                If(ds.Tables("Orders") IsNot Nothing) Then
                    MyBase.Tables.Add(New Modules.DataBinding.nwindOrders.OrdersDataTable(ds.Tables("Orders")))
                End If

                If(ds.Tables("Products") IsNot Nothing) Then
                    MyBase.Tables.Add(New Modules.DataBinding.nwindOrders.ProductsDataTable(ds.Tables("Products")))
                End If

                Me.DataSetName = ds.DataSetName
                Me.Prefix = ds.Prefix
                Me.[Namespace] = ds.[Namespace]
                Me.Locale = ds.Locale
                Me.CaseSensitive = ds.CaseSensitive
                Me.EnforceConstraints = ds.EnforceConstraints
                Me.Merge(ds, False, Global.System.Data.MissingSchemaAction.Add)
                Me.InitVars()
            Else
                Me.ReadXml(reader)
                Me.InitVars()
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overrides Function GetSchemaSerializable() As Global.System.Xml.Schema.XmlSchema
            Dim stream As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream()
            Me.WriteXmlSchema(New Global.System.Xml.XmlTextWriter(stream, Nothing))
            stream.Position = 0
            Return Global.System.Xml.Schema.XmlSchema.Read(New Global.System.Xml.XmlTextReader(stream), Nothing)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Sub InitVars()
            Me.InitVars(True)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Sub InitVars(ByVal initTable As Boolean)
            Me.tableOrderDetails = CType((MyBase.Tables("OrderDetails")), Modules.DataBinding.nwindOrders.OrderDetailsDataTable)
            If(initTable = True) Then
                If(Me.tableOrderDetails IsNot Nothing) Then
                    Me.tableOrderDetails.InitVars()
                End If
            End If

            Me.tableOrders = CType((MyBase.Tables("Orders")), Modules.DataBinding.nwindOrders.OrdersDataTable)
            If(initTable = True) Then
                If(Me.tableOrders IsNot Nothing) Then
                    Me.tableOrders.InitVars()
                End If
            End If

            Me.tableProducts = CType((MyBase.Tables("Products")), Modules.DataBinding.nwindOrders.ProductsDataTable)
            If(initTable = True) Then
                If(Me.tableProducts IsNot Nothing) Then
                    Me.tableProducts.InitVars()
                End If
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitClass()
            Me.DataSetName = "nwindOrders"
            Me.Prefix = ""
            Me.[Namespace] = "http://tempuri.org/nwindOrders.xsd"
            Me.EnforceConstraints = True
            Me.SchemaSerializationMode = Global.System.Data.SchemaSerializationMode.IncludeSchema
            Me.tableOrderDetails = New Modules.DataBinding.nwindOrders.OrderDetailsDataTable()
            MyBase.Tables.Add(Me.tableOrderDetails)
            Me.tableOrders = New Modules.DataBinding.nwindOrders.OrdersDataTable()
            MyBase.Tables.Add(Me.tableOrders)
            Me.tableProducts = New Modules.DataBinding.nwindOrders.ProductsDataTable()
            MyBase.Tables.Add(Me.tableProducts)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function ShouldSerializeOrderDetails() As Boolean
            Return False
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function ShouldSerializeOrders() As Boolean
            Return False
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function ShouldSerializeProducts() As Boolean
            Return False
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub SchemaChanged(ByVal sender As Object, ByVal e As Global.System.ComponentModel.CollectionChangeEventArgs)
            If(e.Action = Global.System.ComponentModel.CollectionChangeAction.Remove) Then
                Me.InitVars()
            End If
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Shared Function GetTypedDataSetSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
            Dim ds As Modules.DataBinding.nwindOrders = New Modules.DataBinding.nwindOrders()
            Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType()
            Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence()
            Dim any As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny()
            any.[Namespace] = ds.[Namespace]
            sequence.Items.Add(any)
            type.Particle = sequence
            Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable()
            If xs.Contains(dsSchema.TargetNamespace) Then
                Dim s1 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream()
                Dim s2 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream()
                Try
                    Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                    dsSchema.Write(s1)
                    Dim schemas As Global.System.Collections.IEnumerator = xs.Schemas(CStr((dsSchema.TargetNamespace))).GetEnumerator()
                    While schemas.MoveNext()
                        schema = CType((schemas.Current), Global.System.Xml.Schema.XmlSchema)
                        s2.SetLength(0)
                        schema.Write(s2)
                        If(s1.Length = s2.Length) Then
                            s1.Position = 0
                            s2.Position = 0
                            While((s1.Position <> s1.Length) AndAlso (s1.ReadByte() = s2.ReadByte()))
                            End While

                            If(s1.Position = s1.Length) Then
                                Return type
                            End If
                        End If
                    End While
                Finally
                    If(s1 IsNot Nothing) Then
                        s1.Close()
                    End If

                    If(s2 IsNot Nothing) Then
                        s2.Close()
                    End If
                End Try
            End If

            xs.Add(dsSchema)
            Return type
        End Function

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Delegate Sub OrderDetailsRowChangeEventHandler(ByVal sender As Object, ByVal e As Modules.DataBinding.nwindOrders.OrderDetailsRowChangeEvent)

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Delegate Sub OrdersRowChangeEventHandler(ByVal sender As Object, ByVal e As Modules.DataBinding.nwindOrders.OrdersRowChangeEvent)

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Delegate Sub ProductsRowChangeEventHandler(ByVal sender As Object, ByVal e As Modules.DataBinding.nwindOrders.ProductsRowChangeEvent)

        <Global.System.SerializableAttribute()>
        <Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>
        Public Partial Class OrderDetailsDataTable
            Inherits Global.System.Data.TypedTableBase(Of Modules.DataBinding.nwindOrders.OrderDetailsRow)

            Private columnOrderID As Global.System.Data.DataColumn

            Private columnQuantity As Global.System.Data.DataColumn

            Private columnUnitPrice As Global.System.Data.DataColumn

            Private columnDiscount As Global.System.Data.DataColumn

            Private columnProductName As Global.System.Data.DataColumn

            Private columnSupplier As Global.System.Data.DataColumn

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New()
                Me.TableName = "OrderDetails"
                Me.BeginInit()
                Me.InitClass()
                Me.EndInit()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal table As Global.System.Data.DataTable)
                Me.TableName = table.TableName
                If(table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                    Me.CaseSensitive = table.CaseSensitive
                End If

                If(Not Equals(table.Locale.ToString(), table.DataSet.Locale.ToString())) Then
                    Me.Locale = table.Locale
                End If

                If(Not Equals(table.[Namespace], table.DataSet.[Namespace])) Then
                    Me.[Namespace] = table.[Namespace]
                End If

                Me.Prefix = table.Prefix
                Me.MinimumCapacity = table.MinimumCapacity
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
                MyBase.New(info, context)
                Me.InitVars()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property OrderIDColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnOrderID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property QuantityColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnQuantity
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property UnitPriceColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnUnitPrice
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property DiscountColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnDiscount
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ProductNameColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnProductName
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property SupplierColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnSupplier
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            <Global.System.ComponentModel.BrowsableAttribute(False)>
            Public ReadOnly Property Count As Integer
                Get
                    Return Me.Rows.Count
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Default Public ReadOnly Property Item(ByVal index As Integer) As OrderDetailsRow
                Get
                    Return CType((Me.Rows(index)), Modules.DataBinding.nwindOrders.OrderDetailsRow)
                End Get
            End Property

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrderDetailsRowChanging As Modules.DataBinding.nwindOrders.OrderDetailsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrderDetailsRowChanged As Modules.DataBinding.nwindOrders.OrderDetailsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrderDetailsRowDeleting As Modules.DataBinding.nwindOrders.OrderDetailsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrderDetailsRowDeleted As Modules.DataBinding.nwindOrders.OrderDetailsRowChangeEventHandler

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub AddOrderDetailsRow(ByVal row As Modules.DataBinding.nwindOrders.OrderDetailsRow)
                Me.Rows.Add(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function AddOrderDetailsRow(ByVal OrderID As Integer, ByVal Quantity As Short, ByVal UnitPrice As Decimal, ByVal Discount As Single, ByVal ProductName As String, ByVal Supplier As String) As OrderDetailsRow
                Dim rowOrderDetailsRow As Modules.DataBinding.nwindOrders.OrderDetailsRow = CType((Me.NewRow()), Modules.DataBinding.nwindOrders.OrderDetailsRow)
                Dim columnValuesArray As Object() = New Object() {OrderID, Quantity, UnitPrice, Discount, ProductName, Supplier}
                rowOrderDetailsRow.ItemArray = columnValuesArray
                Me.Rows.Add(rowOrderDetailsRow)
                Return rowOrderDetailsRow
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Overrides Function Clone() As Global.System.Data.DataTable
                Dim cln As Modules.DataBinding.nwindOrders.OrderDetailsDataTable = CType((MyBase.Clone()), Modules.DataBinding.nwindOrders.OrderDetailsDataTable)
                cln.InitVars()
                Return cln
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
                Return New Modules.DataBinding.nwindOrders.OrderDetailsDataTable()
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub InitVars()
                Me.columnOrderID = Me.Columns("OrderID")
                Me.columnProductName = Me.Columns("ProductName")
                Me.columnSupplier = Me.Columns("Supplier")
                Me.columnUnitPrice = Me.Columns("UnitPrice")
                Me.columnQuantity = Me.Columns("Quantity")
                Me.columnDiscount = Me.Columns("Discount")
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Private Sub InitClass()
                Me.columnOrderID = New Global.System.Data.DataColumn("OrderID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                Me.columnOrderID.[ReadOnly] = True
                Me.Columns.Add(Me.columnOrderID)
                Me.columnProductName = New Global.System.Data.DataColumn("ProductName", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.columnProductName.[ReadOnly] = True
                Me.Columns.Add(Me.columnProductName)
                Me.columnSupplier = New Global.System.Data.DataColumn("Supplier", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnSupplier)
                Me.columnProductName.MaxLength = 40
                Me.columnSupplier.[ReadOnly] = True
                Me.columnSupplier.MaxLength = 255
                Me.columnUnitPrice = New Global.System.Data.DataColumn("UnitPrice", GetType(Decimal), Nothing, Global.System.Data.MappingType.Element)
                Me.columnUnitPrice.[ReadOnly] = True
                Me.Columns.Add(Me.columnUnitPrice)
                Me.columnQuantity = New Global.System.Data.DataColumn("Quantity", GetType(Short), Nothing, Global.System.Data.MappingType.Element)
                Me.columnQuantity.[ReadOnly] = True
                Me.Columns.Add(Me.columnQuantity)
                Me.columnDiscount = New Global.System.Data.DataColumn("Discount", GetType(Single), Nothing, Global.System.Data.MappingType.Element)
                Me.columnDiscount.[ReadOnly] = True
                Me.Columns.Add(Me.columnDiscount)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function NewOrderDetailsRow() As OrderDetailsRow
                Return CType((Me.NewRow()), Modules.DataBinding.nwindOrders.OrderDetailsRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
                Return New Modules.DataBinding.nwindOrders.OrderDetailsRow(builder)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function GetRowType() As Global.System.Type
                Return GetType(Modules.DataBinding.nwindOrders.OrderDetailsRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanged(e)
                RaiseEvent OrderDetailsRowChanged(Me, New Modules.DataBinding.nwindOrders.OrderDetailsRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.OrderDetailsRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanging(e)
                RaiseEvent OrderDetailsRowChanging(Me, New Modules.DataBinding.nwindOrders.OrderDetailsRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.OrderDetailsRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleted(e)
                RaiseEvent OrderDetailsRowDeleted(Me, New Modules.DataBinding.nwindOrders.OrderDetailsRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.OrderDetailsRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleting(e)
                RaiseEvent OrderDetailsRowDeleting(Me, New Modules.DataBinding.nwindOrders.OrderDetailsRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.OrderDetailsRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub RemoveOrderDetailsRow(ByVal row As Modules.DataBinding.nwindOrders.OrderDetailsRow)
                Me.Rows.Remove(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
                Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType()
                Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence()
                Dim ds As Modules.DataBinding.nwindOrders = New Modules.DataBinding.nwindOrders()
                Dim any1 As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny()
                any1.[Namespace] = "http://www.w3.org/2001/XMLSchema"
                any1.MinOccurs = New Decimal(0)
                any1.MaxOccurs = Decimal.MaxValue
                any1.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any1)
                Dim any2 As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny()
                any2.[Namespace] = "urn:schemas-microsoft-com:xml-diffgram-v1"
                any2.MinOccurs = New Decimal(1)
                any2.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any2)
                Dim attribute1 As Global.System.Xml.Schema.XmlSchemaAttribute = New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute1.Name = "namespace"
                attribute1.FixedValue = ds.[Namespace]
                type.Attributes.Add(attribute1)
                Dim attribute2 As Global.System.Xml.Schema.XmlSchemaAttribute = New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute2.Name = "tableTypeName"
                attribute2.FixedValue = "OrderDetailsDataTable"
                type.Attributes.Add(attribute2)
                type.Particle = sequence
                Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable()
                If xs.Contains(dsSchema.TargetNamespace) Then
                    Dim s1 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream()
                    Dim s2 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream()
                    Try
                        Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                        dsSchema.Write(s1)
                        Dim schemas As Global.System.Collections.IEnumerator = xs.Schemas(CStr((dsSchema.TargetNamespace))).GetEnumerator()
                        While schemas.MoveNext()
                            schema = CType((schemas.Current), Global.System.Xml.Schema.XmlSchema)
                            s2.SetLength(0)
                            schema.Write(s2)
                            If(s1.Length = s2.Length) Then
                                s1.Position = 0
                                s2.Position = 0
                                While((s1.Position <> s1.Length) AndAlso (s1.ReadByte() = s2.ReadByte()))
                                End While

                                If(s1.Position = s1.Length) Then
                                    Return type
                                End If
                            End If
                        End While
                    Finally
                        If(s1 IsNot Nothing) Then
                            s1.Close()
                        End If

                        If(s2 IsNot Nothing) Then
                            s2.Close()
                        End If
                    End Try
                End If

                xs.Add(dsSchema)
                Return type
            End Function
        End Class

        <Global.System.SerializableAttribute()>
        <Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>
        Public Partial Class OrdersDataTable
            Inherits Global.System.Data.TypedTableBase(Of Modules.DataBinding.nwindOrders.OrdersRow)

            Private columnOrderID As Global.System.Data.DataColumn

            Private columnCustomerID As Global.System.Data.DataColumn

            Private columnEmployeeID As Global.System.Data.DataColumn

            Private columnOrderDate As Global.System.Data.DataColumn

            Private columnRequiredDate As Global.System.Data.DataColumn

            Private columnShippedDate As Global.System.Data.DataColumn

            Private columnShipVia As Global.System.Data.DataColumn

            Private columnFreight As Global.System.Data.DataColumn

            Private columnShipName As Global.System.Data.DataColumn

            Private columnShipAddress As Global.System.Data.DataColumn

            Private columnShipCity As Global.System.Data.DataColumn

            Private columnShipRegion As Global.System.Data.DataColumn

            Private columnShipPostalCode As Global.System.Data.DataColumn

            Private columnShipCountry As Global.System.Data.DataColumn

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New()
                Me.TableName = "Orders"
                Me.BeginInit()
                Me.InitClass()
                Me.EndInit()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal table As Global.System.Data.DataTable)
                Me.TableName = table.TableName
                If(table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                    Me.CaseSensitive = table.CaseSensitive
                End If

                If(Not Equals(table.Locale.ToString(), table.DataSet.Locale.ToString())) Then
                    Me.Locale = table.Locale
                End If

                If(Not Equals(table.[Namespace], table.DataSet.[Namespace])) Then
                    Me.[Namespace] = table.[Namespace]
                End If

                Me.Prefix = table.Prefix
                Me.MinimumCapacity = table.MinimumCapacity
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
                MyBase.New(info, context)
                Me.InitVars()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property OrderIDColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnOrderID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property CustomerIDColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnCustomerID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property EmployeeIDColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnEmployeeID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property OrderDateColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnOrderDate
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property RequiredDateColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnRequiredDate
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShippedDateColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnShippedDate
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipViaColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipVia
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property FreightColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnFreight
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipNameColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipName
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipAddressColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipAddress
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipCityColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipCity
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipRegionColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipRegion
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipPostalCodeColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipPostalCode
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ShipCountryColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnShipCountry
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            <Global.System.ComponentModel.BrowsableAttribute(False)>
            Public ReadOnly Property Count As Integer
                Get
                    Return Me.Rows.Count
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Default Public ReadOnly Property Item(ByVal index As Integer) As OrdersRow
                Get
                    Return CType((Me.Rows(index)), Modules.DataBinding.nwindOrders.OrdersRow)
                End Get
            End Property

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrdersRowChanging As Modules.DataBinding.nwindOrders.OrdersRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrdersRowChanged As Modules.DataBinding.nwindOrders.OrdersRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrdersRowDeleting As Modules.DataBinding.nwindOrders.OrdersRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event OrdersRowDeleted As Modules.DataBinding.nwindOrders.OrdersRowChangeEventHandler

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub AddOrdersRow(ByVal row As Modules.DataBinding.nwindOrders.OrdersRow)
                Me.Rows.Add(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function AddOrdersRow(ByVal CustomerID As String, ByVal EmployeeID As Integer, ByVal OrderDate As System.DateTime, ByVal RequiredDate As System.DateTime, ByVal ShippedDate As System.DateTime, ByVal ShipVia As Integer, ByVal Freight As Decimal, ByVal ShipName As String, ByVal ShipAddress As String, ByVal ShipCity As String, ByVal ShipRegion As String, ByVal ShipPostalCode As String, ByVal ShipCountry As String) As OrdersRow
                Dim rowOrdersRow As Modules.DataBinding.nwindOrders.OrdersRow = CType((Me.NewRow()), Modules.DataBinding.nwindOrders.OrdersRow)
                Dim columnValuesArray As Object() = New Object() {Nothing, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry}
                rowOrdersRow.ItemArray = columnValuesArray
                Me.Rows.Add(rowOrdersRow)
                Return rowOrdersRow
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function FindByOrderID(ByVal OrderID As Integer) As OrdersRow
                Return CType((Me.Rows.Find(New Object() {OrderID})), Modules.DataBinding.nwindOrders.OrdersRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Overrides Function Clone() As Global.System.Data.DataTable
                Dim cln As Modules.DataBinding.nwindOrders.OrdersDataTable = CType((MyBase.Clone()), Modules.DataBinding.nwindOrders.OrdersDataTable)
                cln.InitVars()
                Return cln
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
                Return New Modules.DataBinding.nwindOrders.OrdersDataTable()
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub InitVars()
                Me.columnOrderID = Me.Columns("OrderID")
                Me.columnCustomerID = Me.Columns("CustomerID")
                Me.columnEmployeeID = Me.Columns("EmployeeID")
                Me.columnOrderDate = Me.Columns("OrderDate")
                Me.columnRequiredDate = Me.Columns("RequiredDate")
                Me.columnShippedDate = Me.Columns("ShippedDate")
                Me.columnShipVia = Me.Columns("ShipVia")
                Me.columnFreight = Me.Columns("Freight")
                Me.columnShipName = Me.Columns("ShipName")
                Me.columnShipAddress = Me.Columns("ShipAddress")
                Me.columnShipCity = Me.Columns("ShipCity")
                Me.columnShipRegion = Me.Columns("ShipRegion")
                Me.columnShipPostalCode = Me.Columns("ShipPostalCode")
                Me.columnShipCountry = Me.Columns("ShipCountry")
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Private Sub InitClass()
                Me.columnOrderID = New Global.System.Data.DataColumn("OrderID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnOrderID)
                Me.columnCustomerID = New Global.System.Data.DataColumn("CustomerID", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnCustomerID)
                Me.columnEmployeeID = New Global.System.Data.DataColumn("EmployeeID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnEmployeeID)
                Me.columnOrderDate = New Global.System.Data.DataColumn("OrderDate", GetType(Global.System.DateTime), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnOrderDate)
                Me.columnRequiredDate = New Global.System.Data.DataColumn("RequiredDate", GetType(Global.System.DateTime), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnRequiredDate)
                Me.columnShippedDate = New Global.System.Data.DataColumn("ShippedDate", GetType(Global.System.DateTime), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnShippedDate)
                Me.columnShipVia = New Global.System.Data.DataColumn("ShipVia", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnShipVia)
                Me.columnFreight = New Global.System.Data.DataColumn("Freight", GetType(Decimal), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnFreight)
                Me.columnShipName = New Global.System.Data.DataColumn("ShipName", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnShipName)
                Me.columnShipAddress = New Global.System.Data.DataColumn("ShipAddress", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnShipAddress)
                Me.columnShipCity = New Global.System.Data.DataColumn("ShipCity", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnShipCity)
                Me.columnShipRegion = New Global.System.Data.DataColumn("ShipRegion", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnShipRegion)
                Me.columnShipPostalCode = New Global.System.Data.DataColumn("ShipPostalCode", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnShipPostalCode)
                Me.columnShipCountry = New Global.System.Data.DataColumn("ShipCountry", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnShipCountry)
                Me.Constraints.Add(New Global.System.Data.UniqueConstraint("Constraint1", New Global.System.Data.DataColumn() {Me.columnOrderID}, True))
                Me.columnOrderID.AutoIncrement = True
                Me.columnOrderID.AutoIncrementSeed = -1
                Me.columnOrderID.AutoIncrementStep = -1
                Me.columnOrderID.AllowDBNull = False
                Me.columnOrderID.Unique = True
                Me.columnCustomerID.MaxLength = 5
                Me.columnShipName.MaxLength = 40
                Me.columnShipAddress.MaxLength = 60
                Me.columnShipCity.MaxLength = 15
                Me.columnShipRegion.MaxLength = 15
                Me.columnShipPostalCode.MaxLength = 10
                Me.columnShipCountry.MaxLength = 15
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function NewOrdersRow() As OrdersRow
                Return CType((Me.NewRow()), Modules.DataBinding.nwindOrders.OrdersRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
                Return New Modules.DataBinding.nwindOrders.OrdersRow(builder)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function GetRowType() As Global.System.Type
                Return GetType(Modules.DataBinding.nwindOrders.OrdersRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanged(e)
                RaiseEvent OrdersRowChanged(Me, New Modules.DataBinding.nwindOrders.OrdersRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.OrdersRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanging(e)
                RaiseEvent OrdersRowChanging(Me, New Modules.DataBinding.nwindOrders.OrdersRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.OrdersRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleted(e)
                RaiseEvent OrdersRowDeleted(Me, New Modules.DataBinding.nwindOrders.OrdersRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.OrdersRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleting(e)
                RaiseEvent OrdersRowDeleting(Me, New Modules.DataBinding.nwindOrders.OrdersRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.OrdersRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub RemoveOrdersRow(ByVal row As Modules.DataBinding.nwindOrders.OrdersRow)
                Me.Rows.Remove(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
                Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType()
                Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence()
                Dim ds As Modules.DataBinding.nwindOrders = New Modules.DataBinding.nwindOrders()
                Dim any1 As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny()
                any1.[Namespace] = "http://www.w3.org/2001/XMLSchema"
                any1.MinOccurs = New Decimal(0)
                any1.MaxOccurs = Decimal.MaxValue
                any1.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any1)
                Dim any2 As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny()
                any2.[Namespace] = "urn:schemas-microsoft-com:xml-diffgram-v1"
                any2.MinOccurs = New Decimal(1)
                any2.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any2)
                Dim attribute1 As Global.System.Xml.Schema.XmlSchemaAttribute = New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute1.Name = "namespace"
                attribute1.FixedValue = ds.[Namespace]
                type.Attributes.Add(attribute1)
                Dim attribute2 As Global.System.Xml.Schema.XmlSchemaAttribute = New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute2.Name = "tableTypeName"
                attribute2.FixedValue = "OrdersDataTable"
                type.Attributes.Add(attribute2)
                type.Particle = sequence
                Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable()
                If xs.Contains(dsSchema.TargetNamespace) Then
                    Dim s1 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream()
                    Dim s2 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream()
                    Try
                        Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                        dsSchema.Write(s1)
                        Dim schemas As Global.System.Collections.IEnumerator = xs.Schemas(CStr((dsSchema.TargetNamespace))).GetEnumerator()
                        While schemas.MoveNext()
                            schema = CType((schemas.Current), Global.System.Xml.Schema.XmlSchema)
                            s2.SetLength(0)
                            schema.Write(s2)
                            If(s1.Length = s2.Length) Then
                                s1.Position = 0
                                s2.Position = 0
                                While((s1.Position <> s1.Length) AndAlso (s1.ReadByte() = s2.ReadByte()))
                                End While

                                If(s1.Position = s1.Length) Then
                                    Return type
                                End If
                            End If
                        End While
                    Finally
                        If(s1 IsNot Nothing) Then
                            s1.Close()
                        End If

                        If(s2 IsNot Nothing) Then
                            s2.Close()
                        End If
                    End Try
                End If

                xs.Add(dsSchema)
                Return type
            End Function
        End Class

        <Global.System.SerializableAttribute()>
        <Global.System.Xml.Serialization.XmlSchemaProviderAttribute("GetTypedTableSchema")>
        Public Partial Class ProductsDataTable
            Inherits Global.System.Data.TypedTableBase(Of Modules.DataBinding.nwindOrders.ProductsRow)

            Private columnProductID As Global.System.Data.DataColumn

            Private columnProductName As Global.System.Data.DataColumn

            Private columnSupplierID As Global.System.Data.DataColumn

            Private columnCategoryID As Global.System.Data.DataColumn

            Private columnQuantityPerUnit As Global.System.Data.DataColumn

            Private columnUnitPrice As Global.System.Data.DataColumn

            Private columnUnitsInStock As Global.System.Data.DataColumn

            Private columnUnitsOnOrder As Global.System.Data.DataColumn

            Private columnReorderLevel As Global.System.Data.DataColumn

            Private columnDiscontinued As Global.System.Data.DataColumn

            Private columnEAN13 As Global.System.Data.DataColumn

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New()
                Me.TableName = "Products"
                Me.BeginInit()
                Me.InitClass()
                Me.EndInit()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal table As Global.System.Data.DataTable)
                Me.TableName = table.TableName
                If(table.CaseSensitive <> table.DataSet.CaseSensitive) Then
                    Me.CaseSensitive = table.CaseSensitive
                End If

                If(Not Equals(table.Locale.ToString(), table.DataSet.Locale.ToString())) Then
                    Me.Locale = table.Locale
                End If

                If(Not Equals(table.[Namespace], table.DataSet.[Namespace])) Then
                    Me.[Namespace] = table.[Namespace]
                End If

                Me.Prefix = table.Prefix
                Me.MinimumCapacity = table.MinimumCapacity
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Sub New(ByVal info As Global.System.Runtime.Serialization.SerializationInfo, ByVal context As Global.System.Runtime.Serialization.StreamingContext)
                MyBase.New(info, context)
                Me.InitVars()
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ProductIDColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnProductID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ProductNameColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnProductName
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property SupplierIDColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnSupplierID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property CategoryIDColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnCategoryID
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property QuantityPerUnitColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnQuantityPerUnit
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property UnitPriceColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnUnitPrice
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property UnitsInStockColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnUnitsInStock
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property UnitsOnOrderColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnUnitsOnOrder
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property ReorderLevelColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnReorderLevel
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property DiscontinuedColumn As Global.System.Data.DataColumn
                Get
                    Return Me.columnDiscontinued
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property EAN13Column As Global.System.Data.DataColumn
                Get
                    Return Me.columnEAN13
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            <Global.System.ComponentModel.BrowsableAttribute(False)>
            Public ReadOnly Property Count As Integer
                Get
                    Return Me.Rows.Count
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Default Public ReadOnly Property Item(ByVal index As Integer) As ProductsRow
                Get
                    Return CType((Me.Rows(index)), Modules.DataBinding.nwindOrders.ProductsRow)
                End Get
            End Property

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event ProductsRowChanging As Modules.DataBinding.nwindOrders.ProductsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event ProductsRowChanged As Modules.DataBinding.nwindOrders.ProductsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event ProductsRowDeleting As Modules.DataBinding.nwindOrders.ProductsRowChangeEventHandler

            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Event ProductsRowDeleted As Modules.DataBinding.nwindOrders.ProductsRowChangeEventHandler

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub AddProductsRow(ByVal row As Modules.DataBinding.nwindOrders.ProductsRow)
                Me.Rows.Add(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function AddProductsRow(ByVal ProductName As String, ByVal SupplierID As Integer, ByVal CategoryID As Integer, ByVal QuantityPerUnit As String, ByVal UnitPrice As Decimal, ByVal UnitsInStock As Short, ByVal UnitsOnOrder As Short, ByVal ReorderLevel As Short, ByVal Discontinued As Boolean, ByVal EAN13 As String) As ProductsRow
                Dim rowProductsRow As Modules.DataBinding.nwindOrders.ProductsRow = CType((Me.NewRow()), Modules.DataBinding.nwindOrders.ProductsRow)
                Dim columnValuesArray As Object() = New Object() {Nothing, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, EAN13}
                rowProductsRow.ItemArray = columnValuesArray
                Me.Rows.Add(rowProductsRow)
                Return rowProductsRow
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function FindByProductID(ByVal ProductID As Integer) As ProductsRow
                Return CType((Me.Rows.Find(New Object() {ProductID})), Modules.DataBinding.nwindOrders.ProductsRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Overrides Function Clone() As Global.System.Data.DataTable
                Dim cln As Modules.DataBinding.nwindOrders.ProductsDataTable = CType((MyBase.Clone()), Modules.DataBinding.nwindOrders.ProductsDataTable)
                cln.InitVars()
                Return cln
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function CreateInstance() As Global.System.Data.DataTable
                Return New Modules.DataBinding.nwindOrders.ProductsDataTable()
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub InitVars()
                Me.columnProductID = Me.Columns("ProductID")
                Me.columnProductName = Me.Columns("ProductName")
                Me.columnSupplierID = Me.Columns("SupplierID")
                Me.columnCategoryID = Me.Columns("CategoryID")
                Me.columnQuantityPerUnit = Me.Columns("QuantityPerUnit")
                Me.columnUnitPrice = Me.Columns("UnitPrice")
                Me.columnUnitsInStock = Me.Columns("UnitsInStock")
                Me.columnUnitsOnOrder = Me.Columns("UnitsOnOrder")
                Me.columnReorderLevel = Me.Columns("ReorderLevel")
                Me.columnDiscontinued = Me.Columns("Discontinued")
                Me.columnEAN13 = Me.Columns("EAN13")
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Private Sub InitClass()
                Me.columnProductID = New Global.System.Data.DataColumn("ProductID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnProductID)
                Me.columnProductName = New Global.System.Data.DataColumn("ProductName", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnProductName)
                Me.columnSupplierID = New Global.System.Data.DataColumn("SupplierID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnSupplierID)
                Me.columnCategoryID = New Global.System.Data.DataColumn("CategoryID", GetType(Integer), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnCategoryID)
                Me.columnQuantityPerUnit = New Global.System.Data.DataColumn("QuantityPerUnit", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnQuantityPerUnit)
                Me.columnUnitPrice = New Global.System.Data.DataColumn("UnitPrice", GetType(Decimal), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnUnitPrice)
                Me.columnUnitsInStock = New Global.System.Data.DataColumn("UnitsInStock", GetType(Short), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnUnitsInStock)
                Me.columnUnitsOnOrder = New Global.System.Data.DataColumn("UnitsOnOrder", GetType(Short), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnUnitsOnOrder)
                Me.columnReorderLevel = New Global.System.Data.DataColumn("ReorderLevel", GetType(Short), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnReorderLevel)
                Me.columnDiscontinued = New Global.System.Data.DataColumn("Discontinued", GetType(Boolean), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnDiscontinued)
                Me.columnEAN13 = New Global.System.Data.DataColumn("EAN13", GetType(String), Nothing, Global.System.Data.MappingType.Element)
                Me.Columns.Add(Me.columnEAN13)
                Me.Constraints.Add(New Global.System.Data.UniqueConstraint("Constraint1", New Global.System.Data.DataColumn() {Me.columnProductID}, True))
                Me.columnProductID.AutoIncrement = True
                Me.columnProductID.AutoIncrementSeed = -1
                Me.columnProductID.AutoIncrementStep = -1
                Me.columnProductID.AllowDBNull = False
                Me.columnProductID.Unique = True
                Me.columnProductName.MaxLength = 40
                Me.columnQuantityPerUnit.MaxLength = 20
                Me.columnEAN13.MaxLength = 12
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function NewProductsRow() As ProductsRow
                Return CType((Me.NewRow()), Modules.DataBinding.nwindOrders.ProductsRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function NewRowFromBuilder(ByVal builder As Global.System.Data.DataRowBuilder) As Global.System.Data.DataRow
                Return New Modules.DataBinding.nwindOrders.ProductsRow(builder)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Function GetRowType() As Global.System.Type
                Return GetType(Modules.DataBinding.nwindOrders.ProductsRow)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanged(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanged(e)
                RaiseEvent ProductsRowChanged(Me, New Modules.DataBinding.nwindOrders.ProductsRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.ProductsRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowChanging(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowChanging(e)
                RaiseEvent ProductsRowChanging(Me, New Modules.DataBinding.nwindOrders.ProductsRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.ProductsRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleted(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleted(e)
                RaiseEvent ProductsRowDeleted(Me, New Modules.DataBinding.nwindOrders.ProductsRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.ProductsRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Protected Overrides Sub OnRowDeleting(ByVal e As Global.System.Data.DataRowChangeEventArgs)
                MyBase.OnRowDeleting(e)
                RaiseEvent ProductsRowDeleting(Me, New Modules.DataBinding.nwindOrders.ProductsRowChangeEvent(CType((e.Row), Modules.DataBinding.nwindOrders.ProductsRow), e.Action))
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub RemoveProductsRow(ByVal row As Modules.DataBinding.nwindOrders.ProductsRow)
                Me.Rows.Remove(row)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Shared Function GetTypedTableSchema(ByVal xs As Global.System.Xml.Schema.XmlSchemaSet) As Global.System.Xml.Schema.XmlSchemaComplexType
                Dim type As Global.System.Xml.Schema.XmlSchemaComplexType = New Global.System.Xml.Schema.XmlSchemaComplexType()
                Dim sequence As Global.System.Xml.Schema.XmlSchemaSequence = New Global.System.Xml.Schema.XmlSchemaSequence()
                Dim ds As Modules.DataBinding.nwindOrders = New Modules.DataBinding.nwindOrders()
                Dim any1 As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny()
                any1.[Namespace] = "http://www.w3.org/2001/XMLSchema"
                any1.MinOccurs = New Decimal(0)
                any1.MaxOccurs = Decimal.MaxValue
                any1.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any1)
                Dim any2 As Global.System.Xml.Schema.XmlSchemaAny = New Global.System.Xml.Schema.XmlSchemaAny()
                any2.[Namespace] = "urn:schemas-microsoft-com:xml-diffgram-v1"
                any2.MinOccurs = New Decimal(1)
                any2.ProcessContents = Global.System.Xml.Schema.XmlSchemaContentProcessing.Lax
                sequence.Items.Add(any2)
                Dim attribute1 As Global.System.Xml.Schema.XmlSchemaAttribute = New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute1.Name = "namespace"
                attribute1.FixedValue = ds.[Namespace]
                type.Attributes.Add(attribute1)
                Dim attribute2 As Global.System.Xml.Schema.XmlSchemaAttribute = New Global.System.Xml.Schema.XmlSchemaAttribute()
                attribute2.Name = "tableTypeName"
                attribute2.FixedValue = "ProductsDataTable"
                type.Attributes.Add(attribute2)
                type.Particle = sequence
                Dim dsSchema As Global.System.Xml.Schema.XmlSchema = ds.GetSchemaSerializable()
                If xs.Contains(dsSchema.TargetNamespace) Then
                    Dim s1 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream()
                    Dim s2 As Global.System.IO.MemoryStream = New Global.System.IO.MemoryStream()
                    Try
                        Dim schema As Global.System.Xml.Schema.XmlSchema = Nothing
                        dsSchema.Write(s1)
                        Dim schemas As Global.System.Collections.IEnumerator = xs.Schemas(CStr((dsSchema.TargetNamespace))).GetEnumerator()
                        While schemas.MoveNext()
                            schema = CType((schemas.Current), Global.System.Xml.Schema.XmlSchema)
                            s2.SetLength(0)
                            schema.Write(s2)
                            If(s1.Length = s2.Length) Then
                                s1.Position = 0
                                s2.Position = 0
                                While((s1.Position <> s1.Length) AndAlso (s1.ReadByte() = s2.ReadByte()))
                                End While

                                If(s1.Position = s1.Length) Then
                                    Return type
                                End If
                            End If
                        End While
                    Finally
                        If(s1 IsNot Nothing) Then
                            s1.Close()
                        End If

                        If(s2 IsNot Nothing) Then
                            s2.Close()
                        End If
                    End Try
                End If

                xs.Add(dsSchema)
                Return type
            End Function
        End Class

        Public Partial Class OrderDetailsRow
            Inherits Global.System.Data.DataRow

            Private tableOrderDetails As Modules.DataBinding.nwindOrders.OrderDetailsDataTable

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
                MyBase.New(rb)
                Me.tableOrderDetails = CType((Me.Table), Modules.DataBinding.nwindOrders.OrderDetailsDataTable)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property OrderID As Integer
                Get
                    Try
                        Return(CInt((Me(Me.tableOrderDetails.OrderIDColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'OrderID' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Integer)
                    Me(Me.tableOrderDetails.OrderIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property Quantity As Short
                Get
                    Try
                        Return(CShort((Me(Me.tableOrderDetails.QuantityColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'Quantity' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Short)
                    Me(Me.tableOrderDetails.QuantityColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property UnitPrice As Decimal
                Get
                    Try
                        Return(CDec((Me(Me.tableOrderDetails.UnitPriceColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'UnitPrice' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Decimal)
                    Me(Me.tableOrderDetails.UnitPriceColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property Discount As Single
                Get
                    Try
                        Return(CSng((Me(Me.tableOrderDetails.DiscountColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'Discount' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Single)
                    Me(Me.tableOrderDetails.DiscountColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ProductName As String
                Get
                    Try
                        Return(CStr((Me(Me.tableOrderDetails.ProductNameColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ProductName' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableOrderDetails.ProductNameColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property Supplier As String
                Get
                    Try
                        Return(CStr((Me(Me.tableOrderDetails.SupplierColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'Supplier' in table 'OrderDetails' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableOrderDetails.SupplierColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsOrderIDNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.OrderIDColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetOrderIDNull()
                Me(Me.tableOrderDetails.OrderIDColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsQuantityNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.QuantityColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetQuantityNull()
                Me(Me.tableOrderDetails.QuantityColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsUnitPriceNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.UnitPriceColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetUnitPriceNull()
                Me(Me.tableOrderDetails.UnitPriceColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsDiscountNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.DiscountColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetDiscountNull()
                Me(Me.tableOrderDetails.DiscountColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsProductNameNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.ProductNameColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetProductNameNull()
                Me(Me.tableOrderDetails.ProductNameColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsSupplierNull() As Boolean
                Return Me.IsNull(Me.tableOrderDetails.SupplierColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetSupplierNull()
                Me(Me.tableOrderDetails.SupplierColumn) = Global.System.Convert.DBNull
            End Sub
        End Class

        Public Partial Class OrdersRow
            Inherits Global.System.Data.DataRow

            Private tableOrders As Modules.DataBinding.nwindOrders.OrdersDataTable

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
                MyBase.New(rb)
                Me.tableOrders = CType((Me.Table), Modules.DataBinding.nwindOrders.OrdersDataTable)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property OrderID As Integer
                Get
                    Return(CInt((Me(Me.tableOrders.OrderIDColumn))))
                End Get

                Set(ByVal value As Integer)
                    Me(Me.tableOrders.OrderIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property CustomerID As String
                Get
                    Try
                        Return(CStr((Me(Me.tableOrders.CustomerIDColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'CustomerID' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableOrders.CustomerIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property EmployeeID As Integer
                Get
                    Try
                        Return(CInt((Me(Me.tableOrders.EmployeeIDColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'EmployeeID' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Integer)
                    Me(Me.tableOrders.EmployeeIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property OrderDate As System.DateTime
                Get
                    Try
                        Return(CDate((Me(Me.tableOrders.OrderDateColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'OrderDate' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As System.DateTime)
                    Me(Me.tableOrders.OrderDateColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property RequiredDate As System.DateTime
                Get
                    Try
                        Return(CDate((Me(Me.tableOrders.RequiredDateColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'RequiredDate' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As System.DateTime)
                    Me(Me.tableOrders.RequiredDateColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShippedDate As System.DateTime
                Get
                    Try
                        Return(CDate((Me(Me.tableOrders.ShippedDateColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShippedDate' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As System.DateTime)
                    Me(Me.tableOrders.ShippedDateColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipVia As Integer
                Get
                    Try
                        Return(CInt((Me(Me.tableOrders.ShipViaColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipVia' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Integer)
                    Me(Me.tableOrders.ShipViaColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property Freight As Decimal
                Get
                    Try
                        Return(CDec((Me(Me.tableOrders.FreightColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'Freight' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Decimal)
                    Me(Me.tableOrders.FreightColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipName As String
                Get
                    Try
                        Return(CStr((Me(Me.tableOrders.ShipNameColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipName' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipNameColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipAddress As String
                Get
                    Try
                        Return(CStr((Me(Me.tableOrders.ShipAddressColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipAddress' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipAddressColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipCity As String
                Get
                    Try
                        Return(CStr((Me(Me.tableOrders.ShipCityColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipCity' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipCityColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipRegion As String
                Get
                    Try
                        Return(CStr((Me(Me.tableOrders.ShipRegionColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipRegion' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipRegionColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipPostalCode As String
                Get
                    Try
                        Return(CStr((Me(Me.tableOrders.ShipPostalCodeColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipPostalCode' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipPostalCodeColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ShipCountry As String
                Get
                    Try
                        Return(CStr((Me(Me.tableOrders.ShipCountryColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ShipCountry' in table 'Orders' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableOrders.ShipCountryColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsCustomerIDNull() As Boolean
                Return Me.IsNull(Me.tableOrders.CustomerIDColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetCustomerIDNull()
                Me(Me.tableOrders.CustomerIDColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsEmployeeIDNull() As Boolean
                Return Me.IsNull(Me.tableOrders.EmployeeIDColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetEmployeeIDNull()
                Me(Me.tableOrders.EmployeeIDColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsOrderDateNull() As Boolean
                Return Me.IsNull(Me.tableOrders.OrderDateColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetOrderDateNull()
                Me(Me.tableOrders.OrderDateColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsRequiredDateNull() As Boolean
                Return Me.IsNull(Me.tableOrders.RequiredDateColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetRequiredDateNull()
                Me(Me.tableOrders.RequiredDateColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShippedDateNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShippedDateColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShippedDateNull()
                Me(Me.tableOrders.ShippedDateColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipViaNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipViaColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipViaNull()
                Me(Me.tableOrders.ShipViaColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsFreightNull() As Boolean
                Return Me.IsNull(Me.tableOrders.FreightColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetFreightNull()
                Me(Me.tableOrders.FreightColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipNameNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipNameColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipNameNull()
                Me(Me.tableOrders.ShipNameColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipAddressNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipAddressColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipAddressNull()
                Me(Me.tableOrders.ShipAddressColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipCityNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipCityColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipCityNull()
                Me(Me.tableOrders.ShipCityColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipRegionNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipRegionColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipRegionNull()
                Me(Me.tableOrders.ShipRegionColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipPostalCodeNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipPostalCodeColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipPostalCodeNull()
                Me(Me.tableOrders.ShipPostalCodeColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsShipCountryNull() As Boolean
                Return Me.IsNull(Me.tableOrders.ShipCountryColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetShipCountryNull()
                Me(Me.tableOrders.ShipCountryColumn) = Global.System.Convert.DBNull
            End Sub
        End Class

        Public Partial Class ProductsRow
            Inherits Global.System.Data.DataRow

            Private tableProducts As Modules.DataBinding.nwindOrders.ProductsDataTable

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal rb As Global.System.Data.DataRowBuilder)
                MyBase.New(rb)
                Me.tableProducts = CType((Me.Table), Modules.DataBinding.nwindOrders.ProductsDataTable)
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ProductID As Integer
                Get
                    Return(CInt((Me(Me.tableProducts.ProductIDColumn))))
                End Get

                Set(ByVal value As Integer)
                    Me(Me.tableProducts.ProductIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ProductName As String
                Get
                    Try
                        Return(CStr((Me(Me.tableProducts.ProductNameColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ProductName' in table 'Products' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableProducts.ProductNameColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property SupplierID As Integer
                Get
                    Try
                        Return(CInt((Me(Me.tableProducts.SupplierIDColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'SupplierID' in table 'Products' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Integer)
                    Me(Me.tableProducts.SupplierIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property CategoryID As Integer
                Get
                    Try
                        Return(CInt((Me(Me.tableProducts.CategoryIDColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'CategoryID' in table 'Products' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Integer)
                    Me(Me.tableProducts.CategoryIDColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property QuantityPerUnit As String
                Get
                    Try
                        Return(CStr((Me(Me.tableProducts.QuantityPerUnitColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'QuantityPerUnit' in table 'Products' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableProducts.QuantityPerUnitColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property UnitPrice As Decimal
                Get
                    Try
                        Return(CDec((Me(Me.tableProducts.UnitPriceColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'UnitPrice' in table 'Products' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Decimal)
                    Me(Me.tableProducts.UnitPriceColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property UnitsInStock As Short
                Get
                    Try
                        Return(CShort((Me(Me.tableProducts.UnitsInStockColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'UnitsInStock' in table 'Products' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Short)
                    Me(Me.tableProducts.UnitsInStockColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property UnitsOnOrder As Short
                Get
                    Try
                        Return(CShort((Me(Me.tableProducts.UnitsOnOrderColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'UnitsOnOrder' in table 'Products' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Short)
                    Me(Me.tableProducts.UnitsOnOrderColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property ReorderLevel As Short
                Get
                    Try
                        Return(CShort((Me(Me.tableProducts.ReorderLevelColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'ReorderLevel' in table 'Products' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Short)
                    Me(Me.tableProducts.ReorderLevelColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property Discontinued As Boolean
                Get
                    Try
                        Return(CBool((Me(Me.tableProducts.DiscontinuedColumn))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'Discontinued' in table 'Products' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As Boolean)
                    Me(Me.tableProducts.DiscontinuedColumn) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Property EAN13 As String
                Get
                    Try
                        Return(CStr((Me(Me.tableProducts.EAN13Column))))
                    Catch e As Global.System.InvalidCastException
                        Throw New Global.System.Data.StrongTypingException("The value for column 'EAN13' in table 'Products' is DBNull.", e)
                    End Try
                End Get

                Set(ByVal value As String)
                    Me(Me.tableProducts.EAN13Column) = value
                End Set
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsProductNameNull() As Boolean
                Return Me.IsNull(Me.tableProducts.ProductNameColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetProductNameNull()
                Me(Me.tableProducts.ProductNameColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsSupplierIDNull() As Boolean
                Return Me.IsNull(Me.tableProducts.SupplierIDColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetSupplierIDNull()
                Me(Me.tableProducts.SupplierIDColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsCategoryIDNull() As Boolean
                Return Me.IsNull(Me.tableProducts.CategoryIDColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetCategoryIDNull()
                Me(Me.tableProducts.CategoryIDColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsQuantityPerUnitNull() As Boolean
                Return Me.IsNull(Me.tableProducts.QuantityPerUnitColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetQuantityPerUnitNull()
                Me(Me.tableProducts.QuantityPerUnitColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsUnitPriceNull() As Boolean
                Return Me.IsNull(Me.tableProducts.UnitPriceColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetUnitPriceNull()
                Me(Me.tableProducts.UnitPriceColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsUnitsInStockNull() As Boolean
                Return Me.IsNull(Me.tableProducts.UnitsInStockColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetUnitsInStockNull()
                Me(Me.tableProducts.UnitsInStockColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsUnitsOnOrderNull() As Boolean
                Return Me.IsNull(Me.tableProducts.UnitsOnOrderColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetUnitsOnOrderNull()
                Me(Me.tableProducts.UnitsOnOrderColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsReorderLevelNull() As Boolean
                Return Me.IsNull(Me.tableProducts.ReorderLevelColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetReorderLevelNull()
                Me(Me.tableProducts.ReorderLevelColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsDiscontinuedNull() As Boolean
                Return Me.IsNull(Me.tableProducts.DiscontinuedColumn)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetDiscontinuedNull()
                Me(Me.tableProducts.DiscontinuedColumn) = Global.System.Convert.DBNull
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Function IsEAN13Null() As Boolean
                Return Me.IsNull(Me.tableProducts.EAN13Column)
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub SetEAN13Null()
                Me(Me.tableProducts.EAN13Column) = Global.System.Convert.DBNull
            End Sub
        End Class

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Class OrderDetailsRowChangeEvent
            Inherits Global.System.EventArgs

            Private eventRow As Modules.DataBinding.nwindOrders.OrderDetailsRow

            Private eventAction As Global.System.Data.DataRowAction

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New(ByVal row As Modules.DataBinding.nwindOrders.OrderDetailsRow, ByVal action As Global.System.Data.DataRowAction)
                Me.eventRow = row
                Me.eventAction = action
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Row As OrderDetailsRow
                Get
                    Return Me.eventRow
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Action As Global.System.Data.DataRowAction
                Get
                    Return Me.eventAction
                End Get
            End Property
        End Class

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Class OrdersRowChangeEvent
            Inherits Global.System.EventArgs

            Private eventRow As Modules.DataBinding.nwindOrders.OrdersRow

            Private eventAction As Global.System.Data.DataRowAction

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New(ByVal row As Modules.DataBinding.nwindOrders.OrdersRow, ByVal action As Global.System.Data.DataRowAction)
                Me.eventRow = row
                Me.eventAction = action
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Row As OrdersRow
                Get
                    Return Me.eventRow
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Action As Global.System.Data.DataRowAction
                Get
                    Return Me.eventAction
                End Get
            End Property
        End Class

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Class ProductsRowChangeEvent
            Inherits Global.System.EventArgs

            Private eventRow As Modules.DataBinding.nwindOrders.ProductsRow

            Private eventAction As Global.System.Data.DataRowAction

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public Sub New(ByVal row As Modules.DataBinding.nwindOrders.ProductsRow, ByVal action As Global.System.Data.DataRowAction)
                Me.eventRow = row
                Me.eventAction = action
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Row As ProductsRow
                Get
                    Return Me.eventRow
                End Get
            End Property

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Public ReadOnly Property Action As Global.System.Data.DataRowAction
                Get
                    Return Me.eventAction
                End Get
            End Property
        End Class
    End Class
End Namespace

Namespace Modules.DataBinding.nwindOrdersTableAdapters

    <Global.System.ComponentModel.DesignerCategoryAttribute("code")>
    <Global.System.ComponentModel.ToolboxItemAttribute(True)>
    <Global.System.ComponentModel.DataObjectAttribute(True)>
    <Global.System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" & ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")>
    <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
    Public Partial Class OrderDetailsTableAdapter
        Inherits Global.System.ComponentModel.Component

        Private _adapter As Global.System.Data.OleDb.OleDbDataAdapter

        Private _connection As Global.System.Data.OleDb.OleDbConnection

        Private _transaction As Global.System.Data.OleDb.OleDbTransaction

        Private _commandCollection As Global.System.Data.OleDb.OleDbCommand()

        Private _clearBeforeFill As Boolean

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Sub New()
            Me.ClearBeforeFill = True
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Friend ReadOnly Property Adapter As Global.System.Data.OleDb.OleDbDataAdapter
            Get
                If(Me._adapter Is Nothing) Then
                    Me.InitAdapter()
                End If

                Return Me._adapter
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Connection As Global.System.Data.OleDb.OleDbConnection
            Get
                If(Me._connection Is Nothing) Then
                    Me.InitConnection()
                End If

                Return Me._connection
            End Get

            Set(ByVal value As Global.System.Data.OleDb.OleDbConnection)
                Me._connection = value
                If(Me.Adapter.InsertCommand IsNot Nothing) Then
                    Me.Adapter.InsertCommand.Connection = value
                End If

                If(Me.Adapter.DeleteCommand IsNot Nothing) Then
                    Me.Adapter.DeleteCommand.Connection = value
                End If

                If(Me.Adapter.UpdateCommand IsNot Nothing) Then
                    Me.Adapter.UpdateCommand.Connection = value
                End If

                Dim i As Integer = 0
                While(i < Me.CommandCollection.Length)
                    If(Me.CommandCollection(i) IsNot Nothing) Then
                        CType((Me.CommandCollection(CInt((i)))), Global.System.Data.OleDb.OleDbCommand).Connection = value
                    End If

                    i =(i + 1)
                End While
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Transaction As Global.System.Data.OleDb.OleDbTransaction
            Get
                Return Me._transaction
            End Get

            Set(ByVal value As Global.System.Data.OleDb.OleDbTransaction)
                Me._transaction = value
                Dim i As Integer = 0
                While(i < Me.CommandCollection.Length)
                    Me.CommandCollection(CInt((i))).Transaction = Me._transaction
                    i =(i + 1)
                End While

                If((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.DeleteCommand IsNot Nothing)) Then
                    Me.Adapter.DeleteCommand.Transaction = Me._transaction
                End If

                If((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.InsertCommand IsNot Nothing)) Then
                    Me.Adapter.InsertCommand.Transaction = Me._transaction
                End If

                If((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.UpdateCommand IsNot Nothing)) Then
                    Me.Adapter.UpdateCommand.Transaction = Me._transaction
                End If
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected ReadOnly Property CommandCollection As Global.System.Data.OleDb.OleDbCommand()
            Get
                If(Me._commandCollection Is Nothing) Then
                    Me.InitCommandCollection()
                End If

                Return Me._commandCollection
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Property ClearBeforeFill As Boolean
            Get
                Return Me._clearBeforeFill
            End Get

            Set(ByVal value As Boolean)
                Me._clearBeforeFill = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitAdapter()
            Me._adapter = New Global.System.Data.OleDb.OleDbDataAdapter()
            Dim tableMapping As Global.System.Data.Common.DataTableMapping = New Global.System.Data.Common.DataTableMapping()
            tableMapping.SourceTable = "Table"
            tableMapping.DataSetTable = "OrderDetails"
            tableMapping.ColumnMappings.Add("OrderID", "OrderID")
            tableMapping.ColumnMappings.Add("Quantity", "Quantity")
            tableMapping.ColumnMappings.Add("UnitPrice", "UnitPrice")
            tableMapping.ColumnMappings.Add("Discount", "Discount")
            tableMapping.ColumnMappings.Add("ProductName", "ProductName")
            tableMapping.ColumnMappings.Add("Supplier", "Supplier")
            Me._adapter.TableMappings.Add(tableMapping)
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitConnection()
            Me._connection = New Global.System.Data.OleDb.OleDbConnection()
            Me._connection.ConnectionString = Global.SpreadsheetDemo.Properties.Settings.[Default].nwindConnectionString
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitCommandCollection()
            Me._commandCollection = New Global.System.Data.OleDb.OleDbCommand(0) {}
            Me._commandCollection(0) = New Global.System.Data.OleDb.OleDbCommand()
            Me._commandCollection(CInt((0))).Connection = Me.Connection
            Me._commandCollection(CInt((0))).CommandText = "SELECT OrderID, Quantity, UnitPrice, Discount, ProductName, Supplier FROM OrderDe" & "tails"
            Me._commandCollection(CInt((0))).CommandType = Global.System.Data.CommandType.Text
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Fill, True)>
        Public Overridable Function Fill(ByVal dataTable As Modules.DataBinding.nwindOrders.OrderDetailsDataTable) As Integer
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            If(Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If

            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.[Select], True)>
        Public Overridable Function GetData() As Modules.DataBinding.nwindOrders.OrderDetailsDataTable
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            Dim dataTable As Modules.DataBinding.nwindOrders.OrderDetailsDataTable = New Modules.DataBinding.nwindOrders.OrderDetailsDataTable()
            Me.Adapter.Fill(dataTable)
            Return dataTable
        End Function
    End Class

    <Global.System.ComponentModel.DesignerCategoryAttribute("code")>
    <Global.System.ComponentModel.ToolboxItemAttribute(True)>
    <Global.System.ComponentModel.DataObjectAttribute(True)>
    <Global.System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" & ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")>
    <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
    Public Partial Class OrdersTableAdapter
        Inherits Global.System.ComponentModel.Component

        Private _adapter As Global.System.Data.OleDb.OleDbDataAdapter

        Private _connection As Global.System.Data.OleDb.OleDbConnection

        Private _transaction As Global.System.Data.OleDb.OleDbTransaction

        Private _commandCollection As Global.System.Data.OleDb.OleDbCommand()

        Private _clearBeforeFill As Boolean

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Sub New()
            Me.ClearBeforeFill = True
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Friend ReadOnly Property Adapter As Global.System.Data.OleDb.OleDbDataAdapter
            Get
                If(Me._adapter Is Nothing) Then
                    Me.InitAdapter()
                End If

                Return Me._adapter
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Connection As Global.System.Data.OleDb.OleDbConnection
            Get
                If(Me._connection Is Nothing) Then
                    Me.InitConnection()
                End If

                Return Me._connection
            End Get

            Set(ByVal value As Global.System.Data.OleDb.OleDbConnection)
                Me._connection = value
                If(Me.Adapter.InsertCommand IsNot Nothing) Then
                    Me.Adapter.InsertCommand.Connection = value
                End If

                If(Me.Adapter.DeleteCommand IsNot Nothing) Then
                    Me.Adapter.DeleteCommand.Connection = value
                End If

                If(Me.Adapter.UpdateCommand IsNot Nothing) Then
                    Me.Adapter.UpdateCommand.Connection = value
                End If

                Dim i As Integer = 0
                While(i < Me.CommandCollection.Length)
                    If(Me.CommandCollection(i) IsNot Nothing) Then
                        CType((Me.CommandCollection(CInt((i)))), Global.System.Data.OleDb.OleDbCommand).Connection = value
                    End If

                    i =(i + 1)
                End While
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Transaction As Global.System.Data.OleDb.OleDbTransaction
            Get
                Return Me._transaction
            End Get

            Set(ByVal value As Global.System.Data.OleDb.OleDbTransaction)
                Me._transaction = value
                Dim i As Integer = 0
                While(i < Me.CommandCollection.Length)
                    Me.CommandCollection(CInt((i))).Transaction = Me._transaction
                    i =(i + 1)
                End While

                If((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.DeleteCommand IsNot Nothing)) Then
                    Me.Adapter.DeleteCommand.Transaction = Me._transaction
                End If

                If((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.InsertCommand IsNot Nothing)) Then
                    Me.Adapter.InsertCommand.Transaction = Me._transaction
                End If

                If((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.UpdateCommand IsNot Nothing)) Then
                    Me.Adapter.UpdateCommand.Transaction = Me._transaction
                End If
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected ReadOnly Property CommandCollection As Global.System.Data.OleDb.OleDbCommand()
            Get
                If(Me._commandCollection Is Nothing) Then
                    Me.InitCommandCollection()
                End If

                Return Me._commandCollection
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Property ClearBeforeFill As Boolean
            Get
                Return Me._clearBeforeFill
            End Get

            Set(ByVal value As Boolean)
                Me._clearBeforeFill = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitAdapter()
            Me._adapter = New Global.System.Data.OleDb.OleDbDataAdapter()
            Dim tableMapping As Global.System.Data.Common.DataTableMapping = New Global.System.Data.Common.DataTableMapping()
            tableMapping.SourceTable = "Table"
            tableMapping.DataSetTable = "Orders"
            tableMapping.ColumnMappings.Add("OrderID", "OrderID")
            tableMapping.ColumnMappings.Add("CustomerID", "CustomerID")
            tableMapping.ColumnMappings.Add("EmployeeID", "EmployeeID")
            tableMapping.ColumnMappings.Add("OrderDate", "OrderDate")
            tableMapping.ColumnMappings.Add("RequiredDate", "RequiredDate")
            tableMapping.ColumnMappings.Add("ShippedDate", "ShippedDate")
            tableMapping.ColumnMappings.Add("ShipVia", "ShipVia")
            tableMapping.ColumnMappings.Add("Freight", "Freight")
            tableMapping.ColumnMappings.Add("ShipName", "ShipName")
            tableMapping.ColumnMappings.Add("ShipAddress", "ShipAddress")
            tableMapping.ColumnMappings.Add("ShipCity", "ShipCity")
            tableMapping.ColumnMappings.Add("ShipRegion", "ShipRegion")
            tableMapping.ColumnMappings.Add("ShipPostalCode", "ShipPostalCode")
            tableMapping.ColumnMappings.Add("ShipCountry", "ShipCountry")
            Me._adapter.TableMappings.Add(tableMapping)
            Me._adapter.DeleteCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.DeleteCommand.Connection = Me.Connection
            Me._adapter.DeleteCommand.CommandText = "DELETE FROM `Orders` WHERE ((`OrderID` = ?) AND ((? = 1 AND `CustomerID` IS NULL) OR (`CustomerID` = ?)) AND ((? = 1 AND `EmployeeID` IS NULL) OR (`EmployeeID` = ?)) AND ((? = 1 AND `OrderDate` IS NULL) OR (`OrderDate` = ?)) AND ((? = 1 AND `RequiredDate` IS NULL) OR (`RequiredDate` = ?)) AND ((? = 1 AND `ShippedDate` IS NULL) OR (`ShippedDate` = ?)) AND ((? = 1 AND `ShipVia` IS NULL) OR (`ShipVia` = ?)) AND ((? = 1 AND `Freight` IS NULL) OR (`Freight` = ?)) AND ((? = 1 AND `ShipName` IS NULL) OR (`ShipName` = ?)) AND ((? = 1 AND `ShipAddress` IS NULL) OR (`ShipAddress` = ?)) AND ((? = 1 AND `ShipCity` IS NULL) OR (`ShipCity` = ?)) AND ((? = 1 AND `ShipRegion` IS NULL) OR (`ShipRegion` = ?)) AND ((? = 1 AND `ShipPostalCode` IS NULL) OR (`ShipPostalCode` = ?)) AND ((? = 1 AND `ShipCountry` IS NULL) OR (`ShipCountry` = ?)))"
            Me._adapter.DeleteCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_OrderID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "OrderID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_CustomerID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CustomerID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_CustomerID", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CustomerID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_EmployeeID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EmployeeID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_EmployeeID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EmployeeID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_OrderDate", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "OrderDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_OrderDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "OrderDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_RequiredDate", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "RequiredDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_RequiredDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "RequiredDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShippedDate", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShippedDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShippedDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShippedDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipVia", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipVia", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipVia", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipVia", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_Freight", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Freight", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_Freight", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Freight", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipName", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipName", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipName", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipAddress", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipAddress", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipAddress", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipAddress", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipCity", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCity", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipCity", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCity", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipRegion", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipRegion", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipRegion", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipRegion", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipPostalCode", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipPostalCode", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipPostalCode", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipPostalCode", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipCountry", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCountry", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipCountry", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCountry", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.InsertCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.InsertCommand.Connection = Me.Connection
            Me._adapter.InsertCommand.CommandText = "INSERT INTO `Orders` (`CustomerID`, `EmployeeID`, `OrderDate`, `RequiredDate`, `S" & "hippedDate`, `ShipVia`, `Freight`, `ShipName`, `ShipAddress`, `ShipCity`, `ShipR" & "egion`, `ShipPostalCode`, `ShipCountry`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?" & ", ?, ?)"
            Me._adapter.InsertCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("CustomerID", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CustomerID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("EmployeeID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EmployeeID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("OrderDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "OrderDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("RequiredDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "RequiredDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShippedDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShippedDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipVia", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipVia", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Freight", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Freight", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipName", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipAddress", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipAddress", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipCity", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCity", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipRegion", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipRegion", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipPostalCode", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipPostalCode", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipCountry", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCountry", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.UpdateCommand.Connection = Me.Connection
            Me._adapter.UpdateCommand.CommandText = "UPDATE `Orders` SET `CustomerID` = ?, `EmployeeID` = ?, `OrderDate` = ?, `RequiredDate` = ?, `ShippedDate` = ?, `ShipVia` = ?, `Freight` = ?, `ShipName` = ?, `ShipAddress` = ?, `ShipCity` = ?, `ShipRegion` = ?, `ShipPostalCode` = ?, `ShipCountry` = ? WHERE ((`OrderID` = ?) AND ((? = 1 AND `CustomerID` IS NULL) OR (`CustomerID` = ?)) AND ((? = 1 AND `EmployeeID` IS NULL) OR (`EmployeeID` = ?)) AND ((? = 1 AND `OrderDate` IS NULL) OR (`OrderDate` = ?)) AND ((? = 1 AND `RequiredDate` IS NULL) OR (`RequiredDate` = ?)) AND ((? = 1 AND `ShippedDate` IS NULL) OR (`ShippedDate` = ?)) AND ((? = 1 AND `ShipVia` IS NULL) OR (`ShipVia` = ?)) AND ((? = 1 AND `Freight` IS NULL) OR (`Freight` = ?)) AND ((? = 1 AND `ShipName` IS NULL) OR (`ShipName` = ?)) AND ((? = 1 AND `ShipAddress` IS NULL) OR (`ShipAddress` = ?)) AND ((? = 1 AND `ShipCity` IS NULL) OR (`ShipCity` = ?)) AND ((? = 1 AND `ShipRegion` IS NULL) OR (`ShipRegion` = ?)) AND ((? = 1 AND `ShipPostalCode` IS NULL) OR (`ShipPostalCode` = ?)) AND ((? = 1 AND `ShipCountry` IS NULL) OR (`ShipCountry` = ?)))"
            Me._adapter.UpdateCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("CustomerID", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CustomerID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("EmployeeID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EmployeeID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("OrderDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "OrderDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("RequiredDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "RequiredDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShippedDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShippedDate", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipVia", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipVia", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Freight", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Freight", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipName", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipAddress", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipAddress", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipCity", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCity", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipRegion", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipRegion", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipPostalCode", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipPostalCode", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ShipCountry", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCountry", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_OrderID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "OrderID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_CustomerID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CustomerID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_CustomerID", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CustomerID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_EmployeeID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EmployeeID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_EmployeeID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EmployeeID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_OrderDate", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "OrderDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_OrderDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "OrderDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_RequiredDate", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "RequiredDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_RequiredDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "RequiredDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShippedDate", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShippedDate", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShippedDate", Global.System.Data.OleDb.OleDbType.[Date], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShippedDate", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipVia", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipVia", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipVia", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipVia", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_Freight", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Freight", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_Freight", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Freight", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipName", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipName", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipName", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipAddress", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipAddress", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipAddress", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipAddress", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipCity", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCity", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipCity", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCity", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipRegion", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipRegion", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipRegion", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipRegion", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipPostalCode", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipPostalCode", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipPostalCode", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipPostalCode", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ShipCountry", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCountry", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ShipCountry", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ShipCountry", Global.System.Data.DataRowVersion.Original, False, Nothing))
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitConnection()
            Me._connection = New Global.System.Data.OleDb.OleDbConnection()
            Me._connection.ConnectionString = Global.SpreadsheetDemo.Properties.Settings.[Default].nwindConnectionString
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitCommandCollection()
            Me._commandCollection = New Global.System.Data.OleDb.OleDbCommand(0) {}
            Me._commandCollection(0) = New Global.System.Data.OleDb.OleDbCommand()
            Me._commandCollection(CInt((0))).Connection = Me.Connection
            Me._commandCollection(CInt((0))).CommandText = "SELECT OrderID, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, Shi" & "pVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, Ship" & "Country FROM Orders"
            Me._commandCollection(CInt((0))).CommandType = Global.System.Data.CommandType.Text
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Fill, True)>
        Public Overridable Function Fill(ByVal dataTable As Modules.DataBinding.nwindOrders.OrdersDataTable) As Integer
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            If(Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If

            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.[Select], True)>
        Public Overridable Function GetData() As Modules.DataBinding.nwindOrders.OrdersDataTable
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            Dim dataTable As Modules.DataBinding.nwindOrders.OrdersDataTable = New Modules.DataBinding.nwindOrders.OrdersDataTable()
            Me.Adapter.Fill(dataTable)
            Return dataTable
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataTable As Modules.DataBinding.nwindOrders.OrdersDataTable) As Integer
            Return Me.Adapter.Update(dataTable)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataSet As Modules.DataBinding.nwindOrders) As Integer
            Return Me.Adapter.Update(dataSet, "Orders")
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataRow As Global.System.Data.DataRow) As Integer
            Return Me.Adapter.Update(New Global.System.Data.DataRow() {dataRow})
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataRows As Global.System.Data.DataRow()) As Integer
            Return Me.Adapter.Update(dataRows)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Delete, True)>
        Public Overridable Function Delete(ByVal Original_OrderID As Integer, ByVal Original_CustomerID As String, ByVal Original_EmployeeID As Global.System.Nullable(Of Integer), ByVal Original_OrderDate As Global.System.Nullable(Of Global.System.DateTime), ByVal Original_RequiredDate As Global.System.Nullable(Of Global.System.DateTime), ByVal Original_ShippedDate As Global.System.Nullable(Of Global.System.DateTime), ByVal Original_ShipVia As Global.System.Nullable(Of Integer), ByVal Original_Freight As Global.System.Nullable(Of Decimal), ByVal Original_ShipName As String, ByVal Original_ShipAddress As String, ByVal Original_ShipCity As String, ByVal Original_ShipRegion As String, ByVal Original_ShipPostalCode As String, ByVal Original_ShipCountry As String) As Integer
            Me.Adapter.DeleteCommand.Parameters(CInt((0))).Value =(CInt((Original_OrderID)))
            If(Equals(Original_CustomerID, Nothing)) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((1))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((2))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((1))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((2))).Value =(CStr((Original_CustomerID)))
            End If

            If(Original_EmployeeID.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((3))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((4))).Value =(CInt((Original_EmployeeID.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((3))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((4))).Value = Global.System.DBNull.Value
            End If

            If(Original_OrderDate.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((5))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((6))).Value =(CDate((Original_OrderDate.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((5))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((6))).Value = Global.System.DBNull.Value
            End If

            If(Original_RequiredDate.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((7))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((8))).Value =(CDate((Original_RequiredDate.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((7))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((8))).Value = Global.System.DBNull.Value
            End If

            If(Original_ShippedDate.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((9))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((10))).Value =(CDate((Original_ShippedDate.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((9))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((10))).Value = Global.System.DBNull.Value
            End If

            If(Original_ShipVia.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((11))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((12))).Value =(CInt((Original_ShipVia.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((11))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((12))).Value = Global.System.DBNull.Value
            End If

            If(Original_Freight.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((13))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((14))).Value =(CDec((Original_Freight.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((13))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((14))).Value = Global.System.DBNull.Value
            End If

            If(Equals(Original_ShipName, Nothing)) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((15))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((16))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((15))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((16))).Value =(CStr((Original_ShipName)))
            End If

            If(Equals(Original_ShipAddress, Nothing)) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((17))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((18))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((17))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((18))).Value =(CStr((Original_ShipAddress)))
            End If

            If(Equals(Original_ShipCity, Nothing)) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((19))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((20))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((19))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((20))).Value =(CStr((Original_ShipCity)))
            End If

            If(Equals(Original_ShipRegion, Nothing)) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((21))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((22))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((21))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((22))).Value =(CStr((Original_ShipRegion)))
            End If

            If(Equals(Original_ShipPostalCode, Nothing)) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((23))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((24))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((23))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((24))).Value =(CStr((Original_ShipPostalCode)))
            End If

            If(Equals(Original_ShipCountry, Nothing)) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((25))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((26))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((25))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((26))).Value =(CStr((Original_ShipCountry)))
            End If

            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.DeleteCommand.Connection.State
            If(CInt((Me.Adapter.DeleteCommand.Connection.State And Global.System.Data.ConnectionState.Open)) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.DeleteCommand.Connection.Open()
            End If

            Try
                Dim returnValue As Integer = Me.Adapter.DeleteCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If(previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.DeleteCommand.Connection.Close()
                End If
            End Try
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Insert, True)>
        Public Overridable Function Insert(ByVal CustomerID As String, ByVal EmployeeID As Global.System.Nullable(Of Integer), ByVal OrderDate As Global.System.Nullable(Of Global.System.DateTime), ByVal RequiredDate As Global.System.Nullable(Of Global.System.DateTime), ByVal ShippedDate As Global.System.Nullable(Of Global.System.DateTime), ByVal ShipVia As Global.System.Nullable(Of Integer), ByVal Freight As Global.System.Nullable(Of Decimal), ByVal ShipName As String, ByVal ShipAddress As String, ByVal ShipCity As String, ByVal ShipRegion As String, ByVal ShipPostalCode As String, ByVal ShipCountry As String) As Integer
            If(Equals(CustomerID, Nothing)) Then
                Me.Adapter.InsertCommand.Parameters(CInt((0))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((0))).Value =(CStr((CustomerID)))
            End If

            If(EmployeeID.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((1))).Value =(CInt((EmployeeID.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((1))).Value = Global.System.DBNull.Value
            End If

            If(OrderDate.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((2))).Value =(CDate((OrderDate.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((2))).Value = Global.System.DBNull.Value
            End If

            If(RequiredDate.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((3))).Value =(CDate((RequiredDate.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((3))).Value = Global.System.DBNull.Value
            End If

            If(ShippedDate.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((4))).Value =(CDate((ShippedDate.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((4))).Value = Global.System.DBNull.Value
            End If

            If(ShipVia.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((5))).Value =(CInt((ShipVia.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((5))).Value = Global.System.DBNull.Value
            End If

            If(Freight.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((6))).Value =(CDec((Freight.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((6))).Value = Global.System.DBNull.Value
            End If

            If(Equals(ShipName, Nothing)) Then
                Me.Adapter.InsertCommand.Parameters(CInt((7))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((7))).Value =(CStr((ShipName)))
            End If

            If(Equals(ShipAddress, Nothing)) Then
                Me.Adapter.InsertCommand.Parameters(CInt((8))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((8))).Value =(CStr((ShipAddress)))
            End If

            If(Equals(ShipCity, Nothing)) Then
                Me.Adapter.InsertCommand.Parameters(CInt((9))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((9))).Value =(CStr((ShipCity)))
            End If

            If(Equals(ShipRegion, Nothing)) Then
                Me.Adapter.InsertCommand.Parameters(CInt((10))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((10))).Value =(CStr((ShipRegion)))
            End If

            If(Equals(ShipPostalCode, Nothing)) Then
                Me.Adapter.InsertCommand.Parameters(CInt((11))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((11))).Value =(CStr((ShipPostalCode)))
            End If

            If(Equals(ShipCountry, Nothing)) Then
                Me.Adapter.InsertCommand.Parameters(CInt((12))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((12))).Value =(CStr((ShipCountry)))
            End If

            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.InsertCommand.Connection.State
            If(CInt((Me.Adapter.InsertCommand.Connection.State And Global.System.Data.ConnectionState.Open)) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.InsertCommand.Connection.Open()
            End If

            Try
                Dim returnValue As Integer = Me.Adapter.InsertCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If(previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.InsertCommand.Connection.Close()
                End If
            End Try
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Update, True)>
        Public Overridable Function Update(ByVal CustomerID As String, ByVal EmployeeID As Global.System.Nullable(Of Integer), ByVal OrderDate As Global.System.Nullable(Of Global.System.DateTime), ByVal RequiredDate As Global.System.Nullable(Of Global.System.DateTime), ByVal ShippedDate As Global.System.Nullable(Of Global.System.DateTime), ByVal ShipVia As Global.System.Nullable(Of Integer), ByVal Freight As Global.System.Nullable(Of Decimal), ByVal ShipName As String, ByVal ShipAddress As String, ByVal ShipCity As String, ByVal ShipRegion As String, ByVal ShipPostalCode As String, ByVal ShipCountry As String, ByVal Original_OrderID As Integer, ByVal Original_CustomerID As String, ByVal Original_EmployeeID As Global.System.Nullable(Of Integer), ByVal Original_OrderDate As Global.System.Nullable(Of Global.System.DateTime), ByVal Original_RequiredDate As Global.System.Nullable(Of Global.System.DateTime), ByVal Original_ShippedDate As Global.System.Nullable(Of Global.System.DateTime), ByVal Original_ShipVia As Global.System.Nullable(Of Integer), ByVal Original_Freight As Global.System.Nullable(Of Decimal), ByVal Original_ShipName As String, ByVal Original_ShipAddress As String, ByVal Original_ShipCity As String, ByVal Original_ShipRegion As String, ByVal Original_ShipPostalCode As String, ByVal Original_ShipCountry As String) As Integer
            If(Equals(CustomerID, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((0))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((0))).Value =(CStr((CustomerID)))
            End If

            If(EmployeeID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((1))).Value =(CInt((EmployeeID.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((1))).Value = Global.System.DBNull.Value
            End If

            If(OrderDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((2))).Value =(CDate((OrderDate.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((2))).Value = Global.System.DBNull.Value
            End If

            If(RequiredDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((3))).Value =(CDate((RequiredDate.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((3))).Value = Global.System.DBNull.Value
            End If

            If(ShippedDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((4))).Value =(CDate((ShippedDate.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((4))).Value = Global.System.DBNull.Value
            End If

            If(ShipVia.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((5))).Value =(CInt((ShipVia.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((5))).Value = Global.System.DBNull.Value
            End If

            If(Freight.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((6))).Value =(CDec((Freight.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((6))).Value = Global.System.DBNull.Value
            End If

            If(Equals(ShipName, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((7))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((7))).Value =(CStr((ShipName)))
            End If

            If(Equals(ShipAddress, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((8))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((8))).Value =(CStr((ShipAddress)))
            End If

            If(Equals(ShipCity, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((9))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((9))).Value =(CStr((ShipCity)))
            End If

            If(Equals(ShipRegion, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((10))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((10))).Value =(CStr((ShipRegion)))
            End If

            If(Equals(ShipPostalCode, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((11))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((11))).Value =(CStr((ShipPostalCode)))
            End If

            If(Equals(ShipCountry, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((12))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((12))).Value =(CStr((ShipCountry)))
            End If

            Me.Adapter.UpdateCommand.Parameters(CInt((13))).Value =(CInt((Original_OrderID)))
            If(Equals(Original_CustomerID, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((14))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((15))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((14))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((15))).Value =(CStr((Original_CustomerID)))
            End If

            If(Original_EmployeeID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((16))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((17))).Value =(CInt((Original_EmployeeID.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((16))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((17))).Value = Global.System.DBNull.Value
            End If

            If(Original_OrderDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((18))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((19))).Value =(CDate((Original_OrderDate.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((18))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((19))).Value = Global.System.DBNull.Value
            End If

            If(Original_RequiredDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((20))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((21))).Value =(CDate((Original_RequiredDate.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((20))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((21))).Value = Global.System.DBNull.Value
            End If

            If(Original_ShippedDate.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((22))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((23))).Value =(CDate((Original_ShippedDate.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((22))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((23))).Value = Global.System.DBNull.Value
            End If

            If(Original_ShipVia.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((24))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((25))).Value =(CInt((Original_ShipVia.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((24))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((25))).Value = Global.System.DBNull.Value
            End If

            If(Original_Freight.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((26))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((27))).Value =(CDec((Original_Freight.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((26))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((27))).Value = Global.System.DBNull.Value
            End If

            If(Equals(Original_ShipName, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((28))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((29))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((28))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((29))).Value =(CStr((Original_ShipName)))
            End If

            If(Equals(Original_ShipAddress, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((30))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((31))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((30))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((31))).Value =(CStr((Original_ShipAddress)))
            End If

            If(Equals(Original_ShipCity, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((32))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((33))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((32))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((33))).Value =(CStr((Original_ShipCity)))
            End If

            If(Equals(Original_ShipRegion, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((34))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((35))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((34))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((35))).Value =(CStr((Original_ShipRegion)))
            End If

            If(Equals(Original_ShipPostalCode, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((36))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((37))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((36))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((37))).Value =(CStr((Original_ShipPostalCode)))
            End If

            If(Equals(Original_ShipCountry, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((38))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((39))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((38))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((39))).Value =(CStr((Original_ShipCountry)))
            End If

            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.UpdateCommand.Connection.State
            If(CInt((Me.Adapter.UpdateCommand.Connection.State And Global.System.Data.ConnectionState.Open)) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.UpdateCommand.Connection.Open()
            End If

            Try
                Dim returnValue As Integer = Me.Adapter.UpdateCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If(previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.UpdateCommand.Connection.Close()
                End If
            End Try
        End Function
    End Class

    <Global.System.ComponentModel.DesignerCategoryAttribute("code")>
    <Global.System.ComponentModel.ToolboxItemAttribute(True)>
    <Global.System.ComponentModel.DataObjectAttribute(True)>
    <Global.System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner" & ", Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")>
    <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
    Public Partial Class ProductsTableAdapter
        Inherits Global.System.ComponentModel.Component

        Private _adapter As Global.System.Data.OleDb.OleDbDataAdapter

        Private _connection As Global.System.Data.OleDb.OleDbConnection

        Private _transaction As Global.System.Data.OleDb.OleDbTransaction

        Private _commandCollection As Global.System.Data.OleDb.OleDbCommand()

        Private _clearBeforeFill As Boolean

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Sub New()
            Me.ClearBeforeFill = True
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Friend ReadOnly Property Adapter As Global.System.Data.OleDb.OleDbDataAdapter
            Get
                If(Me._adapter Is Nothing) Then
                    Me.InitAdapter()
                End If

                Return Me._adapter
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Connection As Global.System.Data.OleDb.OleDbConnection
            Get
                If(Me._connection Is Nothing) Then
                    Me.InitConnection()
                End If

                Return Me._connection
            End Get

            Set(ByVal value As Global.System.Data.OleDb.OleDbConnection)
                Me._connection = value
                If(Me.Adapter.InsertCommand IsNot Nothing) Then
                    Me.Adapter.InsertCommand.Connection = value
                End If

                If(Me.Adapter.DeleteCommand IsNot Nothing) Then
                    Me.Adapter.DeleteCommand.Connection = value
                End If

                If(Me.Adapter.UpdateCommand IsNot Nothing) Then
                    Me.Adapter.UpdateCommand.Connection = value
                End If

                Dim i As Integer = 0
                While(i < Me.CommandCollection.Length)
                    If(Me.CommandCollection(i) IsNot Nothing) Then
                        CType((Me.CommandCollection(CInt((i)))), Global.System.Data.OleDb.OleDbCommand).Connection = value
                    End If

                    i =(i + 1)
                End While
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Friend Property Transaction As Global.System.Data.OleDb.OleDbTransaction
            Get
                Return Me._transaction
            End Get

            Set(ByVal value As Global.System.Data.OleDb.OleDbTransaction)
                Me._transaction = value
                Dim i As Integer = 0
                While(i < Me.CommandCollection.Length)
                    Me.CommandCollection(CInt((i))).Transaction = Me._transaction
                    i =(i + 1)
                End While

                If((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.DeleteCommand IsNot Nothing)) Then
                    Me.Adapter.DeleteCommand.Transaction = Me._transaction
                End If

                If((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.InsertCommand IsNot Nothing)) Then
                    Me.Adapter.InsertCommand.Transaction = Me._transaction
                End If

                If((Me.Adapter IsNot Nothing) AndAlso (Me.Adapter.UpdateCommand IsNot Nothing)) Then
                    Me.Adapter.UpdateCommand.Transaction = Me._transaction
                End If
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected ReadOnly Property CommandCollection As Global.System.Data.OleDb.OleDbCommand()
            Get
                If(Me._commandCollection Is Nothing) Then
                    Me.InitCommandCollection()
                End If

                Return Me._commandCollection
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Property ClearBeforeFill As Boolean
            Get
                Return Me._clearBeforeFill
            End Get

            Set(ByVal value As Boolean)
                Me._clearBeforeFill = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitAdapter()
            Me._adapter = New Global.System.Data.OleDb.OleDbDataAdapter()
            Dim tableMapping As Global.System.Data.Common.DataTableMapping = New Global.System.Data.Common.DataTableMapping()
            tableMapping.SourceTable = "Table"
            tableMapping.DataSetTable = "Products"
            tableMapping.ColumnMappings.Add("ProductID", "ProductID")
            tableMapping.ColumnMappings.Add("ProductName", "ProductName")
            tableMapping.ColumnMappings.Add("SupplierID", "SupplierID")
            tableMapping.ColumnMappings.Add("CategoryID", "CategoryID")
            tableMapping.ColumnMappings.Add("QuantityPerUnit", "QuantityPerUnit")
            tableMapping.ColumnMappings.Add("UnitPrice", "UnitPrice")
            tableMapping.ColumnMappings.Add("UnitsInStock", "UnitsInStock")
            tableMapping.ColumnMappings.Add("UnitsOnOrder", "UnitsOnOrder")
            tableMapping.ColumnMappings.Add("ReorderLevel", "ReorderLevel")
            tableMapping.ColumnMappings.Add("Discontinued", "Discontinued")
            tableMapping.ColumnMappings.Add("EAN13", "EAN13")
            Me._adapter.TableMappings.Add(tableMapping)
            Me._adapter.DeleteCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.DeleteCommand.Connection = Me.Connection
            Me._adapter.DeleteCommand.CommandText = "DELETE FROM `Products` WHERE ((`ProductID` = ?) AND ((? = 1 AND `ProductName` IS NULL) OR (`ProductName` = ?)) AND ((? = 1 AND `SupplierID` IS NULL) OR (`SupplierID` = ?)) AND ((? = 1 AND `CategoryID` IS NULL) OR (`CategoryID` = ?)) AND ((? = 1 AND `QuantityPerUnit` IS NULL) OR (`QuantityPerUnit` = ?)) AND ((? = 1 AND `UnitPrice` IS NULL) OR (`UnitPrice` = ?)) AND ((? = 1 AND `UnitsInStock` IS NULL) OR (`UnitsInStock` = ?)) AND ((? = 1 AND `UnitsOnOrder` IS NULL) OR (`UnitsOnOrder` = ?)) AND ((? = 1 AND `ReorderLevel` IS NULL) OR (`ReorderLevel` = ?)) AND ((? = 1 AND `Discontinued` IS NULL) OR (`Discontinued` = ?)) AND ((? = 1 AND `EAN13` IS NULL) OR (`EAN13` = ?)))"
            Me._adapter.DeleteCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ProductID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ProductID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ProductName", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ProductName", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ProductName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ProductName", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_SupplierID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "SupplierID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_SupplierID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "SupplierID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_CategoryID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CategoryID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_CategoryID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CategoryID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_QuantityPerUnit", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "QuantityPerUnit", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_QuantityPerUnit", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "QuantityPerUnit", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitPrice", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitPrice", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitPrice", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitPrice", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitsInStock", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsInStock", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitsInStock", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsInStock", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitsOnOrder", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsOnOrder", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitsOnOrder", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsOnOrder", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ReorderLevel", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ReorderLevel", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ReorderLevel", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ReorderLevel", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_Discontinued", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Discontinued", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_Discontinued", Global.System.Data.OleDb.OleDbType.[Boolean], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Discontinued", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_EAN13", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EAN13", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.DeleteCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_EAN13", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EAN13", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.InsertCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.InsertCommand.Connection = Me.Connection
            Me._adapter.InsertCommand.CommandText = "INSERT INTO `Products` (`ProductName`, `SupplierID`, `CategoryID`, `QuantityPerUn" & "it`, `UnitPrice`, `UnitsInStock`, `UnitsOnOrder`, `ReorderLevel`, `Discontinued`" & ", `EAN13`) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)"
            Me._adapter.InsertCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ProductName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ProductName", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("SupplierID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "SupplierID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("CategoryID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CategoryID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("QuantityPerUnit", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "QuantityPerUnit", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitPrice", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitPrice", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitsInStock", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsInStock", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitsOnOrder", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsOnOrder", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ReorderLevel", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ReorderLevel", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Discontinued", Global.System.Data.OleDb.OleDbType.[Boolean], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Discontinued", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.InsertCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("EAN13", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EAN13", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand = New Global.System.Data.OleDb.OleDbCommand()
            Me._adapter.UpdateCommand.Connection = Me.Connection
            Me._adapter.UpdateCommand.CommandText = "UPDATE `Products` SET `ProductName` = ?, `SupplierID` = ?, `CategoryID` = ?, `QuantityPerUnit` = ?, `UnitPrice` = ?, `UnitsInStock` = ?, `UnitsOnOrder` = ?, `ReorderLevel` = ?, `Discontinued` = ?, `EAN13` = ? WHERE ((`ProductID` = ?) AND ((? = 1 AND `ProductName` IS NULL) OR (`ProductName` = ?)) AND ((? = 1 AND `SupplierID` IS NULL) OR (`SupplierID` = ?)) AND ((? = 1 AND `CategoryID` IS NULL) OR (`CategoryID` = ?)) AND ((? = 1 AND `QuantityPerUnit` IS NULL) OR (`QuantityPerUnit` = ?)) AND ((? = 1 AND `UnitPrice` IS NULL) OR (`UnitPrice` = ?)) AND ((? = 1 AND `UnitsInStock` IS NULL) OR (`UnitsInStock` = ?)) AND ((? = 1 AND `UnitsOnOrder` IS NULL) OR (`UnitsOnOrder` = ?)) AND ((? = 1 AND `ReorderLevel` IS NULL) OR (`ReorderLevel` = ?)) AND ((? = 1 AND `Discontinued` IS NULL) OR (`Discontinued` = ?)) AND ((? = 1 AND `EAN13` IS NULL) OR (`EAN13` = ?)))"
            Me._adapter.UpdateCommand.CommandType = Global.System.Data.CommandType.Text
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ProductName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ProductName", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("SupplierID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "SupplierID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("CategoryID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CategoryID", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("QuantityPerUnit", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "QuantityPerUnit", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitPrice", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitPrice", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitsInStock", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsInStock", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("UnitsOnOrder", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsOnOrder", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("ReorderLevel", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ReorderLevel", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Discontinued", Global.System.Data.OleDb.OleDbType.[Boolean], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Discontinued", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("EAN13", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EAN13", Global.System.Data.DataRowVersion.Current, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ProductID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ProductID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ProductName", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ProductName", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ProductName", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ProductName", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_SupplierID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "SupplierID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_SupplierID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "SupplierID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_CategoryID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CategoryID", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_CategoryID", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "CategoryID", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_QuantityPerUnit", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "QuantityPerUnit", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_QuantityPerUnit", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "QuantityPerUnit", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitPrice", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitPrice", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitPrice", Global.System.Data.OleDb.OleDbType.Currency, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitPrice", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitsInStock", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsInStock", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitsInStock", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsInStock", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_UnitsOnOrder", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsOnOrder", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_UnitsOnOrder", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "UnitsOnOrder", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_ReorderLevel", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ReorderLevel", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_ReorderLevel", Global.System.Data.OleDb.OleDbType.SmallInt, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "ReorderLevel", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_Discontinued", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Discontinued", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_Discontinued", Global.System.Data.OleDb.OleDbType.[Boolean], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "Discontinued", Global.System.Data.DataRowVersion.Original, False, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("IsNull_EAN13", Global.System.Data.OleDb.OleDbType.[Integer], 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EAN13", Global.System.Data.DataRowVersion.Original, True, Nothing))
            Me._adapter.UpdateCommand.Parameters.Add(New Global.System.Data.OleDb.OleDbParameter("Original_EAN13", Global.System.Data.OleDb.OleDbType.VarWChar, 0, Global.System.Data.ParameterDirection.Input, (CByte((0))), (CByte((0))), "EAN13", Global.System.Data.DataRowVersion.Original, False, Nothing))
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitConnection()
            Me._connection = New Global.System.Data.OleDb.OleDbConnection()
            Me._connection.ConnectionString = Global.SpreadsheetDemo.Properties.Settings.[Default].nwindConnectionString
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Sub InitCommandCollection()
            Me._commandCollection = New Global.System.Data.OleDb.OleDbCommand(0) {}
            Me._commandCollection(0) = New Global.System.Data.OleDb.OleDbCommand()
            Me._commandCollection(CInt((0))).Connection = Me.Connection
            Me._commandCollection(CInt((0))).CommandText = "SELECT ProductID, ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice" & ", UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued, EAN13 FROM Products"
            Me._commandCollection(CInt((0))).CommandType = Global.System.Data.CommandType.Text
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Fill, True)>
        Public Overridable Function Fill(ByVal dataTable As Modules.DataBinding.nwindOrders.ProductsDataTable) As Integer
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            If(Me.ClearBeforeFill = True) Then
                dataTable.Clear()
            End If

            Dim returnValue As Integer = Me.Adapter.Fill(dataTable)
            Return returnValue
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.[Select], True)>
        Public Overridable Function GetData() As Modules.DataBinding.nwindOrders.ProductsDataTable
            Me.Adapter.SelectCommand = Me.CommandCollection(0)
            Dim dataTable As Modules.DataBinding.nwindOrders.ProductsDataTable = New Modules.DataBinding.nwindOrders.ProductsDataTable()
            Me.Adapter.Fill(dataTable)
            Return dataTable
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataTable As Modules.DataBinding.nwindOrders.ProductsDataTable) As Integer
            Return Me.Adapter.Update(dataTable)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataSet As Modules.DataBinding.nwindOrders) As Integer
            Return Me.Adapter.Update(dataSet, "Products")
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataRow As Global.System.Data.DataRow) As Integer
            Return Me.Adapter.Update(New Global.System.Data.DataRow() {dataRow})
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        Public Overridable Function Update(ByVal dataRows As Global.System.Data.DataRow()) As Integer
            Return Me.Adapter.Update(dataRows)
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Delete, True)>
        Public Overridable Function Delete(ByVal Original_ProductID As Integer, ByVal Original_ProductName As String, ByVal Original_SupplierID As Global.System.Nullable(Of Integer), ByVal Original_CategoryID As Global.System.Nullable(Of Integer), ByVal Original_QuantityPerUnit As String, ByVal Original_UnitPrice As Global.System.Nullable(Of Decimal), ByVal Original_UnitsInStock As Global.System.Nullable(Of Short), ByVal Original_UnitsOnOrder As Global.System.Nullable(Of Short), ByVal Original_ReorderLevel As Global.System.Nullable(Of Short), ByVal Original_Discontinued As Boolean, ByVal Original_EAN13 As String) As Integer
            Me.Adapter.DeleteCommand.Parameters(CInt((0))).Value =(CInt((Original_ProductID)))
            If(Equals(Original_ProductName, Nothing)) Then
                Throw New Global.System.ArgumentNullException("Original_ProductName")
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((1))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((2))).Value =(CStr((Original_ProductName)))
            End If

            If(Original_SupplierID.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((3))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((4))).Value =(CInt((Original_SupplierID.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((3))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((4))).Value = Global.System.DBNull.Value
            End If

            If(Original_CategoryID.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((5))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((6))).Value =(CInt((Original_CategoryID.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((5))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((6))).Value = Global.System.DBNull.Value
            End If

            If(Equals(Original_QuantityPerUnit, Nothing)) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((7))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((8))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((7))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((8))).Value =(CStr((Original_QuantityPerUnit)))
            End If

            If(Original_UnitPrice.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((9))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((10))).Value =(CDec((Original_UnitPrice.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((9))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((10))).Value = Global.System.DBNull.Value
            End If

            If(Original_UnitsInStock.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((11))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((12))).Value =(CShort((Original_UnitsInStock.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((11))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((12))).Value = Global.System.DBNull.Value
            End If

            If(Original_UnitsOnOrder.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((13))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((14))).Value =(CShort((Original_UnitsOnOrder.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((13))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((14))).Value = Global.System.DBNull.Value
            End If

            If(Original_ReorderLevel.HasValue = True) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((15))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((16))).Value =(CShort((Original_ReorderLevel.Value)))
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((15))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((16))).Value = Global.System.DBNull.Value
            End If

            Me.Adapter.DeleteCommand.Parameters(CInt((17))).Value =(CObj((0)))
            Me.Adapter.DeleteCommand.Parameters(CInt((18))).Value =(CBool((Original_Discontinued)))
            If(Equals(Original_EAN13, Nothing)) Then
                Me.Adapter.DeleteCommand.Parameters(CInt((19))).Value =(CObj((1)))
                Me.Adapter.DeleteCommand.Parameters(CInt((20))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.DeleteCommand.Parameters(CInt((19))).Value =(CObj((0)))
                Me.Adapter.DeleteCommand.Parameters(CInt((20))).Value =(CStr((Original_EAN13)))
            End If

            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.DeleteCommand.Connection.State
            If(CInt((Me.Adapter.DeleteCommand.Connection.State And Global.System.Data.ConnectionState.Open)) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.DeleteCommand.Connection.Open()
            End If

            Try
                Dim returnValue As Integer = Me.Adapter.DeleteCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If(previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.DeleteCommand.Connection.Close()
                End If
            End Try
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Insert, True)>
        Public Overridable Function Insert(ByVal ProductName As String, ByVal SupplierID As Global.System.Nullable(Of Integer), ByVal CategoryID As Global.System.Nullable(Of Integer), ByVal QuantityPerUnit As String, ByVal UnitPrice As Global.System.Nullable(Of Decimal), ByVal UnitsInStock As Global.System.Nullable(Of Short), ByVal UnitsOnOrder As Global.System.Nullable(Of Short), ByVal ReorderLevel As Global.System.Nullable(Of Short), ByVal Discontinued As Boolean, ByVal EAN13 As String) As Integer
            If(Equals(ProductName, Nothing)) Then
                Throw New Global.System.ArgumentNullException("ProductName")
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((0))).Value =(CStr((ProductName)))
            End If

            If(SupplierID.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((1))).Value =(CInt((SupplierID.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((1))).Value = Global.System.DBNull.Value
            End If

            If(CategoryID.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((2))).Value =(CInt((CategoryID.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((2))).Value = Global.System.DBNull.Value
            End If

            If(Equals(QuantityPerUnit, Nothing)) Then
                Me.Adapter.InsertCommand.Parameters(CInt((3))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((3))).Value =(CStr((QuantityPerUnit)))
            End If

            If(UnitPrice.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((4))).Value =(CDec((UnitPrice.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((4))).Value = Global.System.DBNull.Value
            End If

            If(UnitsInStock.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((5))).Value =(CShort((UnitsInStock.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((5))).Value = Global.System.DBNull.Value
            End If

            If(UnitsOnOrder.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((6))).Value =(CShort((UnitsOnOrder.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((6))).Value = Global.System.DBNull.Value
            End If

            If(ReorderLevel.HasValue = True) Then
                Me.Adapter.InsertCommand.Parameters(CInt((7))).Value =(CShort((ReorderLevel.Value)))
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((7))).Value = Global.System.DBNull.Value
            End If

            Me.Adapter.InsertCommand.Parameters(CInt((8))).Value =(CBool((Discontinued)))
            If(Equals(EAN13, Nothing)) Then
                Me.Adapter.InsertCommand.Parameters(CInt((9))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.InsertCommand.Parameters(CInt((9))).Value =(CStr((EAN13)))
            End If

            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.InsertCommand.Connection.State
            If(CInt((Me.Adapter.InsertCommand.Connection.State And Global.System.Data.ConnectionState.Open)) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.InsertCommand.Connection.Open()
            End If

            Try
                Dim returnValue As Integer = Me.Adapter.InsertCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If(previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.InsertCommand.Connection.Close()
                End If
            End Try
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapter")>
        <Global.System.ComponentModel.DataObjectMethodAttribute(Global.System.ComponentModel.DataObjectMethodType.Update, True)>
        Public Overridable Function Update(ByVal ProductName As String, ByVal SupplierID As Global.System.Nullable(Of Integer), ByVal CategoryID As Global.System.Nullable(Of Integer), ByVal QuantityPerUnit As String, ByVal UnitPrice As Global.System.Nullable(Of Decimal), ByVal UnitsInStock As Global.System.Nullable(Of Short), ByVal UnitsOnOrder As Global.System.Nullable(Of Short), ByVal ReorderLevel As Global.System.Nullable(Of Short), ByVal Discontinued As Boolean, ByVal EAN13 As String, ByVal Original_ProductID As Integer, ByVal Original_ProductName As String, ByVal Original_SupplierID As Global.System.Nullable(Of Integer), ByVal Original_CategoryID As Global.System.Nullable(Of Integer), ByVal Original_QuantityPerUnit As String, ByVal Original_UnitPrice As Global.System.Nullable(Of Decimal), ByVal Original_UnitsInStock As Global.System.Nullable(Of Short), ByVal Original_UnitsOnOrder As Global.System.Nullable(Of Short), ByVal Original_ReorderLevel As Global.System.Nullable(Of Short), ByVal Original_Discontinued As Boolean, ByVal Original_EAN13 As String) As Integer
            If(Equals(ProductName, Nothing)) Then
                Throw New Global.System.ArgumentNullException("ProductName")
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((0))).Value =(CStr((ProductName)))
            End If

            If(SupplierID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((1))).Value =(CInt((SupplierID.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((1))).Value = Global.System.DBNull.Value
            End If

            If(CategoryID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((2))).Value =(CInt((CategoryID.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((2))).Value = Global.System.DBNull.Value
            End If

            If(Equals(QuantityPerUnit, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((3))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((3))).Value =(CStr((QuantityPerUnit)))
            End If

            If(UnitPrice.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((4))).Value =(CDec((UnitPrice.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((4))).Value = Global.System.DBNull.Value
            End If

            If(UnitsInStock.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((5))).Value =(CShort((UnitsInStock.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((5))).Value = Global.System.DBNull.Value
            End If

            If(UnitsOnOrder.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((6))).Value =(CShort((UnitsOnOrder.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((6))).Value = Global.System.DBNull.Value
            End If

            If(ReorderLevel.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((7))).Value =(CShort((ReorderLevel.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((7))).Value = Global.System.DBNull.Value
            End If

            Me.Adapter.UpdateCommand.Parameters(CInt((8))).Value =(CBool((Discontinued)))
            If(Equals(EAN13, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((9))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((9))).Value =(CStr((EAN13)))
            End If

            Me.Adapter.UpdateCommand.Parameters(CInt((10))).Value =(CInt((Original_ProductID)))
            If(Equals(Original_ProductName, Nothing)) Then
                Throw New Global.System.ArgumentNullException("Original_ProductName")
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((11))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((12))).Value =(CStr((Original_ProductName)))
            End If

            If(Original_SupplierID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((13))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((14))).Value =(CInt((Original_SupplierID.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((13))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((14))).Value = Global.System.DBNull.Value
            End If

            If(Original_CategoryID.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((15))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((16))).Value =(CInt((Original_CategoryID.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((15))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((16))).Value = Global.System.DBNull.Value
            End If

            If(Equals(Original_QuantityPerUnit, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((17))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((18))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((17))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((18))).Value =(CStr((Original_QuantityPerUnit)))
            End If

            If(Original_UnitPrice.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((19))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((20))).Value =(CDec((Original_UnitPrice.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((19))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((20))).Value = Global.System.DBNull.Value
            End If

            If(Original_UnitsInStock.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((21))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((22))).Value =(CShort((Original_UnitsInStock.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((21))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((22))).Value = Global.System.DBNull.Value
            End If

            If(Original_UnitsOnOrder.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((23))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((24))).Value =(CShort((Original_UnitsOnOrder.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((23))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((24))).Value = Global.System.DBNull.Value
            End If

            If(Original_ReorderLevel.HasValue = True) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((25))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((26))).Value =(CShort((Original_ReorderLevel.Value)))
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((25))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((26))).Value = Global.System.DBNull.Value
            End If

            Me.Adapter.UpdateCommand.Parameters(CInt((27))).Value =(CObj((0)))
            Me.Adapter.UpdateCommand.Parameters(CInt((28))).Value =(CBool((Original_Discontinued)))
            If(Equals(Original_EAN13, Nothing)) Then
                Me.Adapter.UpdateCommand.Parameters(CInt((29))).Value =(CObj((1)))
                Me.Adapter.UpdateCommand.Parameters(CInt((30))).Value = Global.System.DBNull.Value
            Else
                Me.Adapter.UpdateCommand.Parameters(CInt((29))).Value =(CObj((0)))
                Me.Adapter.UpdateCommand.Parameters(CInt((30))).Value =(CStr((Original_EAN13)))
            End If

            Dim previousConnectionState As Global.System.Data.ConnectionState = Me.Adapter.UpdateCommand.Connection.State
            If(CInt((Me.Adapter.UpdateCommand.Connection.State And Global.System.Data.ConnectionState.Open)) <> CInt(Global.System.Data.ConnectionState.Open)) Then
                Me.Adapter.UpdateCommand.Connection.Open()
            End If

            Try
                Dim returnValue As Integer = Me.Adapter.UpdateCommand.ExecuteNonQuery()
                Return returnValue
            Finally
                If(previousConnectionState = Global.System.Data.ConnectionState.Closed) Then
                    Me.Adapter.UpdateCommand.Connection.Close()
                End If
            End Try
        End Function
    End Class

    <Global.System.ComponentModel.DesignerCategoryAttribute("code")>
    <Global.System.ComponentModel.ToolboxItemAttribute(True)>
    <Global.System.ComponentModel.DesignerAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerDesigner, Microsoft.VSD" & "esigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")>
    <Global.System.ComponentModel.Design.HelpKeywordAttribute("vs.data.TableAdapterManager")>
    Public Partial Class TableAdapterManager
        Inherits Global.System.ComponentModel.Component

        Private _updateOrder As Modules.DataBinding.nwindOrdersTableAdapters.TableAdapterManager.UpdateOrderOption

        Private _ordersTableAdapter As Modules.DataBinding.nwindOrdersTableAdapters.OrdersTableAdapter

        Private _productsTableAdapter As Modules.DataBinding.nwindOrdersTableAdapters.ProductsTableAdapter

        Private _backupDataSetBeforeUpdate As Boolean

        Private _connection As Global.System.Data.IDbConnection

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Property UpdateOrder As UpdateOrderOption
            Get
                Return Me._updateOrder
            End Get

            Set(ByVal value As UpdateOrderOption)
                Me._updateOrder = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" & "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" & "a", "System.Drawing.Design.UITypeEditor")>
        Public Property OrdersTableAdapter As OrdersTableAdapter
            Get
                Return Me._ordersTableAdapter
            End Get

            Set(ByVal value As OrdersTableAdapter)
                Me._ordersTableAdapter = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.EditorAttribute("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microso" & "ft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3" & "a", "System.Drawing.Design.UITypeEditor")>
        Public Property ProductsTableAdapter As ProductsTableAdapter
            Get
                Return Me._productsTableAdapter
            End Get

            Set(ByVal value As ProductsTableAdapter)
                Me._productsTableAdapter = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Property BackupDataSetBeforeUpdate As Boolean
            Get
                Return Me._backupDataSetBeforeUpdate
            End Get

            Set(ByVal value As Boolean)
                Me._backupDataSetBeforeUpdate = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.BrowsableAttribute(False)>
        Public Property Connection As Global.System.Data.IDbConnection
            Get
                If(Me._connection IsNot Nothing) Then
                    Return Me._connection
                End If

                If((Me._ordersTableAdapter IsNot Nothing) AndAlso (Me._ordersTableAdapter.Connection IsNot Nothing)) Then
                    Return Me._ordersTableAdapter.Connection
                End If

                If((Me._productsTableAdapter IsNot Nothing) AndAlso (Me._productsTableAdapter.Connection IsNot Nothing)) Then
                    Return Me._productsTableAdapter.Connection
                End If

                Return Nothing
            End Get

            Set(ByVal value As Global.System.Data.IDbConnection)
                Me._connection = value
            End Set
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        <Global.System.ComponentModel.BrowsableAttribute(False)>
        Public ReadOnly Property TableAdapterInstanceCount As Integer
            Get
                Dim count As Integer = 0
                If(Me._ordersTableAdapter IsNot Nothing) Then
                    count =(count + 1)
                End If

                If(Me._productsTableAdapter IsNot Nothing) Then
                    count =(count + 1)
                End If

                Return count
            End Get
        End Property

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function UpdateUpdatedRows(ByVal dataSet As Modules.DataBinding.nwindOrders, ByVal allChangedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow), ByVal allAddedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)) As Integer
            Dim result As Integer = 0
            If(Me._ordersTableAdapter IsNot Nothing) Then
                Dim updatedRows As Global.System.Data.DataRow() = dataSet.Orders.[Select](Nothing, Nothing, Global.System.Data.DataViewRowState.ModifiedCurrent)
                updatedRows = Me.GetRealUpdatedRows(updatedRows, allAddedRows)
                If((updatedRows IsNot Nothing) AndAlso (0 < updatedRows.Length)) Then
                    result =(result + Me._ordersTableAdapter.Update(updatedRows))
                    allChangedRows.AddRange(updatedRows)
                End If
            End If

            If(Me._productsTableAdapter IsNot Nothing) Then
                Dim updatedRows As Global.System.Data.DataRow() = dataSet.Products.[Select](Nothing, Nothing, Global.System.Data.DataViewRowState.ModifiedCurrent)
                updatedRows = Me.GetRealUpdatedRows(updatedRows, allAddedRows)
                If((updatedRows IsNot Nothing) AndAlso (0 < updatedRows.Length)) Then
                    result =(result + Me._productsTableAdapter.Update(updatedRows))
                    allChangedRows.AddRange(updatedRows)
                End If
            End If

            Return result
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function UpdateInsertedRows(ByVal dataSet As Modules.DataBinding.nwindOrders, ByVal allAddedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)) As Integer
            Dim result As Integer = 0
            If(Me._ordersTableAdapter IsNot Nothing) Then
                Dim addedRows As Global.System.Data.DataRow() = dataSet.Orders.[Select](Nothing, Nothing, Global.System.Data.DataViewRowState.Added)
                If((addedRows IsNot Nothing) AndAlso (0 < addedRows.Length)) Then
                    result =(result + Me._ordersTableAdapter.Update(addedRows))
                    allAddedRows.AddRange(addedRows)
                End If
            End If

            If(Me._productsTableAdapter IsNot Nothing) Then
                Dim addedRows As Global.System.Data.DataRow() = dataSet.Products.[Select](Nothing, Nothing, Global.System.Data.DataViewRowState.Added)
                If((addedRows IsNot Nothing) AndAlso (0 < addedRows.Length)) Then
                    result =(result + Me._productsTableAdapter.Update(addedRows))
                    allAddedRows.AddRange(addedRows)
                End If
            End If

            Return result
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function UpdateDeletedRows(ByVal dataSet As Modules.DataBinding.nwindOrders, ByVal allChangedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)) As Integer
            Dim result As Integer = 0
            If(Me._productsTableAdapter IsNot Nothing) Then
                Dim deletedRows As Global.System.Data.DataRow() = dataSet.Products.[Select](Nothing, Nothing, Global.System.Data.DataViewRowState.Deleted)
                If((deletedRows IsNot Nothing) AndAlso (0 < deletedRows.Length)) Then
                    result =(result + Me._productsTableAdapter.Update(deletedRows))
                    allChangedRows.AddRange(deletedRows)
                End If
            End If

            If(Me._ordersTableAdapter IsNot Nothing) Then
                Dim deletedRows As Global.System.Data.DataRow() = dataSet.Orders.[Select](Nothing, Nothing, Global.System.Data.DataViewRowState.Deleted)
                If((deletedRows IsNot Nothing) AndAlso (0 < deletedRows.Length)) Then
                    result =(result + Me._ordersTableAdapter.Update(deletedRows))
                    allChangedRows.AddRange(deletedRows)
                End If
            End If

            Return result
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Function GetRealUpdatedRows(ByVal updatedRows As Global.System.Data.DataRow(), ByVal allAddedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)) As Global.System.Data.DataRow()
            If((updatedRows Is Nothing) OrElse (updatedRows.Length < 1)) Then
                Return updatedRows
            End If

            If((allAddedRows Is Nothing) OrElse (allAddedRows.Count < 1)) Then
                Return updatedRows
            End If

            Dim realUpdatedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow) = New Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)()
            Dim i As Integer = 0
            While(i < updatedRows.Length)
                Dim row As Global.System.Data.DataRow = updatedRows(i)
                If(allAddedRows.Contains(row) = False) Then
                    realUpdatedRows.Add(row)
                End If

                i =(i + 1)
            End While

            Return realUpdatedRows.ToArray()
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Overridable Function UpdateAll(ByVal dataSet As Modules.DataBinding.nwindOrders) As Integer
            If(dataSet Is Nothing) Then
                Throw New Global.System.ArgumentNullException("dataSet")
            End If

            If(dataSet.HasChanges() = False) Then
                Return 0
            End If

            If((Me._ordersTableAdapter IsNot Nothing) AndAlso (Me.MatchTableAdapterConnection(Me._ordersTableAdapter.Connection) = False)) Then
                Throw New Global.System.ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection s" & "tring.")
            End If

            If((Me._productsTableAdapter IsNot Nothing) AndAlso (Me.MatchTableAdapterConnection(Me._productsTableAdapter.Connection) = False)) Then
                Throw New Global.System.ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection s" & "tring.")
            End If

            Dim workConnection As Global.System.Data.IDbConnection = Me.Connection
            If(workConnection Is Nothing) Then
                Throw New Global.System.ApplicationException("TableAdapterManager contains no connection information. Set each TableAdapterMana" & "ger TableAdapter property to a valid TableAdapter instance.")
            End If

            Dim workConnOpened As Boolean = False
            If(CInt((workConnection.State And Global.System.Data.ConnectionState.Broken)) = CInt(Global.System.Data.ConnectionState.Broken)) Then
                workConnection.Close()
            End If

            If(workConnection.State = Global.System.Data.ConnectionState.Closed) Then
                workConnection.Open()
                workConnOpened = True
            End If

            Dim workTransaction As Global.System.Data.IDbTransaction = workConnection.BeginTransaction()
            If(workTransaction Is Nothing) Then
                Throw New Global.System.ApplicationException("The transaction cannot begin. The current data connection does not support transa" & "ctions or the current state is not allowing the transaction to begin.")
            End If

            Dim allChangedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow) = New Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)()
            Dim allAddedRows As Global.System.Collections.Generic.List(Of Global.System.Data.DataRow) = New Global.System.Collections.Generic.List(Of Global.System.Data.DataRow)()
            Dim adaptersWithAcceptChangesDuringUpdate As Global.System.Collections.Generic.List(Of Global.System.Data.Common.DataAdapter) = New Global.System.Collections.Generic.List(Of Global.System.Data.Common.DataAdapter)()
            Dim revertConnections As Global.System.Collections.Generic.Dictionary(Of Object, Global.System.Data.IDbConnection) = New Global.System.Collections.Generic.Dictionary(Of Object, Global.System.Data.IDbConnection)()
            Dim result As Integer = 0
            Dim backupDataSet As Global.System.Data.DataSet = Nothing
            If Me.BackupDataSetBeforeUpdate Then
                backupDataSet = New Global.System.Data.DataSet()
                backupDataSet.Merge(dataSet)
            End If

            Try
                If(Me._ordersTableAdapter IsNot Nothing) Then
                    revertConnections.Add(Me._ordersTableAdapter, Me._ordersTableAdapter.Connection)
                    Me._ordersTableAdapter.Connection = CType((workConnection), Global.System.Data.OleDb.OleDbConnection)
                    Me._ordersTableAdapter.Transaction = CType((workTransaction), Global.System.Data.OleDb.OleDbTransaction)
                    If Me._ordersTableAdapter.Adapter.AcceptChangesDuringUpdate Then
                        Me._ordersTableAdapter.Adapter.AcceptChangesDuringUpdate = False
                        adaptersWithAcceptChangesDuringUpdate.Add(Me._ordersTableAdapter.Adapter)
                    End If
                End If

                If(Me._productsTableAdapter IsNot Nothing) Then
                    revertConnections.Add(Me._productsTableAdapter, Me._productsTableAdapter.Connection)
                    Me._productsTableAdapter.Connection = CType((workConnection), Global.System.Data.OleDb.OleDbConnection)
                    Me._productsTableAdapter.Transaction = CType((workTransaction), Global.System.Data.OleDb.OleDbTransaction)
                    If Me._productsTableAdapter.Adapter.AcceptChangesDuringUpdate Then
                        Me._productsTableAdapter.Adapter.AcceptChangesDuringUpdate = False
                        adaptersWithAcceptChangesDuringUpdate.Add(Me._productsTableAdapter.Adapter)
                    End If
                End If

                If(Me.UpdateOrder = Modules.DataBinding.nwindOrdersTableAdapters.TableAdapterManager.UpdateOrderOption.UpdateInsertDelete) Then
                    result =(result + Me.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows))
                    result =(result + Me.UpdateInsertedRows(dataSet, allAddedRows))
                Else
                    result =(result + Me.UpdateInsertedRows(dataSet, allAddedRows))
                    result =(result + Me.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows))
                End If

                result =(result + Me.UpdateDeletedRows(dataSet, allChangedRows))
                workTransaction.Commit()
                If(0 < allAddedRows.Count) Then
                    Dim rows As Global.System.Data.DataRow() = New System.Data.DataRow(allAddedRows.Count - 1) {}
                    allAddedRows.CopyTo(rows)
                    Dim i As Integer = 0
                    While(i < rows.Length)
                        Dim row As Global.System.Data.DataRow = rows(i)
                        row.AcceptChanges()
                        i =(i + 1)
                    End While
                End If

                If(0 < allChangedRows.Count) Then
                    Dim rows As Global.System.Data.DataRow() = New System.Data.DataRow(allChangedRows.Count - 1) {}
                    allChangedRows.CopyTo(rows)
                    Dim i As Integer = 0
                    While(i < rows.Length)
                        Dim row As Global.System.Data.DataRow = rows(i)
                        row.AcceptChanges()
                        i =(i + 1)
                    End While
                End If
            Catch ex As Global.System.Exception
                workTransaction.Rollback()
                If Me.BackupDataSetBeforeUpdate Then
                    Call Global.System.Diagnostics.Debug.Assert((backupDataSet IsNot Nothing))
                    dataSet.Clear()
                    dataSet.Merge(backupDataSet)
                Else
                    If(0 < allAddedRows.Count) Then
                        Dim rows As Global.System.Data.DataRow() = New System.Data.DataRow(allAddedRows.Count - 1) {}
                        allAddedRows.CopyTo(rows)
                        Dim i As Integer = 0
                        While(i < rows.Length)
                            Dim row As Global.System.Data.DataRow = rows(i)
                            row.AcceptChanges()
                            row.SetAdded()
                            i =(i + 1)
                        End While
                    End If
                End If

                Throw ex
            Finally
                If workConnOpened Then
                    workConnection.Close()
                End If

                If(Me._ordersTableAdapter IsNot Nothing) Then
                    Me._ordersTableAdapter.Connection = CType((revertConnections(Me._ordersTableAdapter)), Global.System.Data.OleDb.OleDbConnection)
                    Me._ordersTableAdapter.Transaction = Nothing
                End If

                If(Me._productsTableAdapter IsNot Nothing) Then
                    Me._productsTableAdapter.Connection = CType((revertConnections(Me._productsTableAdapter)), Global.System.Data.OleDb.OleDbConnection)
                    Me._productsTableAdapter.Transaction = Nothing
                End If

                If(0 < adaptersWithAcceptChangesDuringUpdate.Count) Then
                    Dim adapters As Global.System.Data.Common.DataAdapter() = New System.Data.Common.DataAdapter(adaptersWithAcceptChangesDuringUpdate.Count - 1) {}
                    adaptersWithAcceptChangesDuringUpdate.CopyTo(adapters)
                    Dim i As Integer = 0
                    While(i < adapters.Length)
                        Dim adapter As Global.System.Data.Common.DataAdapter = adapters(i)
                        adapter.AcceptChangesDuringUpdate = True
                        i =(i + 1)
                    End While
                End If
            End Try

            Return result
        End Function

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overridable Sub SortSelfReferenceRows(ByVal rows As Global.System.Data.DataRow(), ByVal relation As Global.System.Data.DataRelation, ByVal childFirst As Boolean)
            Call Global.System.Array.Sort(Of Global.System.Data.DataRow)(rows, New Modules.DataBinding.nwindOrdersTableAdapters.TableAdapterManager.SelfReferenceComparer(relation, childFirst))
        End Sub

        <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Protected Overridable Function MatchTableAdapterConnection(ByVal inputConnection As Global.System.Data.IDbConnection) As Boolean
            If(Me._connection IsNot Nothing) Then
                Return True
            End If

            If((Me.Connection Is Nothing) OrElse (inputConnection Is Nothing)) Then
                Return True
            End If

            If String.Equals(Me.Connection.ConnectionString, inputConnection.ConnectionString, Global.System.StringComparison.Ordinal) Then
                Return True
            End If

            Return False
        End Function

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Public Enum UpdateOrderOption
            InsertUpdateDelete = 0
            UpdateInsertDelete = 1
        End Enum

        <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
        Private Class SelfReferenceComparer
            Inherits Object
            Implements Global.System.Collections.Generic.IComparer(Of Global.System.Data.DataRow)

            Private _relation As Global.System.Data.DataRelation

            Private _childFirst As Integer

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Friend Sub New(ByVal relation As Global.System.Data.DataRelation, ByVal childFirst As Boolean)
                Me._relation = relation
                If childFirst Then
                    Me._childFirst = -1
                Else
                    Me._childFirst = 1
                End If
            End Sub

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Private Function GetRoot(ByVal row As Global.System.Data.DataRow, <Out> ByRef distance As Integer) As Global.System.Data.DataRow
                Call Global.System.Diagnostics.Debug.Assert((row IsNot Nothing))
                Dim root As Global.System.Data.DataRow = row
                distance = 0
                Dim traversedRows As Global.System.Collections.Generic.IDictionary(Of Global.System.Data.DataRow, Global.System.Data.DataRow) = New Global.System.Collections.Generic.Dictionary(Of Global.System.Data.DataRow, Global.System.Data.DataRow)()
                traversedRows(row) = row
                Dim parent As Global.System.Data.DataRow = row.GetParentRow(Me._relation, Global.System.Data.DataRowVersion.[Default])
                While((parent IsNot Nothing) AndAlso (traversedRows.ContainsKey(parent) = False))
                    distance =(distance + 1)
                    root = parent
                    traversedRows(parent) = parent
                    parent = parent.GetParentRow(Me._relation, Global.System.Data.DataRowVersion.[Default])
                End While

                If(distance = 0) Then
                    traversedRows.Clear()
                    traversedRows(row) = row
                    parent = row.GetParentRow(Me._relation, Global.System.Data.DataRowVersion.Original)
                    While((parent IsNot Nothing) AndAlso (traversedRows.ContainsKey(parent) = False))
                        distance =(distance + 1)
                        root = parent
                        traversedRows(parent) = parent
                        parent = parent.GetParentRow(Me._relation, Global.System.Data.DataRowVersion.Original)
                    End While
                End If

                Return root
            End Function

            <Global.System.Diagnostics.DebuggerNonUserCodeAttribute()>
            <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")>
            Private Function Compare(ByVal row1 As Global.System.Data.DataRow, ByVal row2 As Global.System.Data.DataRow) As Integer Implements Global.System.Collections.Generic.IComparer(Of Global.System.Data.DataRow).Compare
                If Object.ReferenceEquals(row1, row2) Then
                    Return 0
                End If

                If(row1 Is Nothing) Then
                    Return -1
                End If

                If(row2 Is Nothing) Then
                    Return 1
                End If

                Dim distance1 As Integer = 0
                Dim root1 As Global.System.Data.DataRow = Me.GetRoot(row1, distance1)
                Dim distance2 As Integer = 0
                Dim root2 As Global.System.Data.DataRow = Me.GetRoot(row2, distance2)
                If Object.ReferenceEquals(root1, root2) Then Return(Me._childFirst * distance1.CompareTo(distance2))
                Call Global.System.Diagnostics.Debug.Assert(((root1.Table IsNot Nothing) AndAlso (root2.Table IsNot Nothing)))
                If(root1.Table.Rows.IndexOf(root1) < root2.Table.Rows.IndexOf(root2)) Then Return -1
                Return 1
            End Function
        End Class
    End Class
End Namespace
