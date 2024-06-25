Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.Editors
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Windows.Media

Namespace DevExpress.Xpf.DemoBase.DemosHelpers.Grid

    Public Class FieldDescription

        Private _PropertyName As String, _ColumnName As String, _EditorDisplayName As String, _TemplateName As String, _ColumnType As Type, _Id As Integer, _ParentId As Integer

        Public Sub New(ByVal propertyName As String, ByVal columnName As String, ByVal editorDisplayName As String, ByVal templateName As String, ByVal columnType As Type, ByVal id As Integer, ByVal parentId As Integer)
            Me.PropertyName = propertyName
            Me.ColumnName = columnName
            Me.EditorDisplayName = editorDisplayName
            Me.TemplateName = templateName
            Me.ColumnType = columnType
            Me.Id = id
            Me.ParentId = parentId
        End Sub

        Public Property PropertyName As String
            Get
                Return _PropertyName
            End Get

            Private Set(ByVal value As String)
                _PropertyName = value
            End Set
        End Property

        Public Property ColumnName As String
            Get
                Return _ColumnName
            End Get

            Private Set(ByVal value As String)
                _ColumnName = value
            End Set
        End Property

        Public Property EditorDisplayName As String
            Get
                Return _EditorDisplayName
            End Get

            Private Set(ByVal value As String)
                _EditorDisplayName = value
            End Set
        End Property

        Public Property TemplateName As String
            Get
                Return _TemplateName
            End Get

            Private Set(ByVal value As String)
                _TemplateName = value
            End Set
        End Property

        Public Property ColumnType As Type
            Get
                Return _ColumnType
            End Get

            Private Set(ByVal value As Type)
                _ColumnType = value
            End Set
        End Property

        Public Property Id As Integer
            Get
                Return _Id
            End Get

            Private Set(ByVal value As Integer)
                _Id = value
            End Set
        End Property

        Public Property ParentId As Integer
            Get
                Return _ParentId
            End Get

            Private Set(ByVal value As Integer)
                _ParentId = value
            End Set
        End Property
    End Class

    Public MustInherit Class MultiEditorsListBaseSQLite
        Implements IList, ITypedList, IBindingList

        Private _ColumnCollection As PropertyDescriptorCollection, _Table As List(Of System.Collections.Generic.Dictionary(Of String, Object))

#Region "Field Descriptions"
        Private desc As List(Of FieldDescription)

        Public ReadOnly Property FieldDescriptions As List(Of FieldDescription)
            Get
                Return If(desc, Function()
                    desc = CreateFieldDescriptions()
                    Return desc
                End Function())
            End Get
        End Property

        Protected Overridable Function CreateFieldDescriptions() As List(Of FieldDescription)
            Return New List(Of FieldDescription)() From {New FieldDescription("ProductName", "Name", "TextEdit", "TextEdit", GetType(String), 1, -1), New FieldDescription("ProductID", "ID", "TextEdit (with numeric mask)", "NumericTextEdit", GetType(Integer), 0, 1), New FieldDescription("EAN13", "Code", "TextEdit (with RegEx mask)", "RegExTextEdit", GetType(String), 9, 1), New FieldDescription(String.Empty, "Password", "PasswordBoxEdit", "PasswordBoxEdit", GetType(String), 14, 1), New FieldDescription(String.Empty, "Countries", "ButtonEdit", "ButtonEdit", GetType(String), 13, -1), New FieldDescription("ProductName", "Name", "AutoSuggestEdit", "AutoSuggestEdit", GetType(String), 111, 13), New FieldDescription("CategoryID", "Category", "ComboBoxEdit", "ComboBoxEdit", GetType(Integer), 2, 13), New FieldDescription(String.Empty, "Country", "ComboBoxEdit (with AutoComplete)", "AutoCompleteComboBoxEdit", GetType(String), 3, 2), New FieldDescription("CategoryID", "Categories", "ComboBoxEdit (Token Mode)", "TokenComboBoxEdit", GetType(String), 26, 2), New FieldDescription("CategoryID", "Category", "SearchLookUpEdit", "SearchLookUpEdit", GetType(Integer), 20, 2), New FieldDescription("CategoryID", "Category", "LookUpEdit", "LookUpEdit", GetType(Integer), 21, 2), New FieldDescription(String.Empty, "Notes", "MemoEdit", "MemoEdit", GetType(String), 10, 13), New FieldDescription(String.Empty, "Date", "DateEdit", "DateEdit", GetType(Date), 11, 13), New FieldDescription(String.Empty, "Font", "FontEdit", "FontEdit", GetType(FontFamily), 19, 13), New FieldDescription("UnitPrice", "Unit Price", "PopupCalcEdit", "PopupCalcEdit", GetType(Double), 15, 13), New FieldDescription(String.Empty, "Color", "PopupColorEdit", "PopupColorEdit", GetType(Color), 16, 13), New FieldDescription(String.Empty, "Orders", "SparklineEdit", "SparklineEdit", GetType(List(Of Integer)), 23, 13), New FieldDescription(String.Empty, "Picture", "PopupImageEdit", "PopupImageEdit", GetType(ImageSource), 17, 13), New FieldDescription("UnitsInStock", "Units In Stock", "TrackBarEdit", "TrackBarEdit", GetType(Double), 5, -1), New FieldDescription("ReorderLevel", "Reorder Level", "ZoomTrackBarEdit", "ZoomTrackBarEdit", GetType(Double), 6, 5), New FieldDescription(String.Empty, "Range", "RangeTrackBarEdit", "RangeTrackBarEdit", GetType(TrackBarEditRange), 7, 5), New FieldDescription(String.Empty, "Units On Order", "ProgressBarEdit", "ProgressBarEdit", GetType(Double), 18, 5), New FieldDescription("Discontinued", "Discontinued", "CheckEdit", "CheckEdit", GetType(Boolean), 8, -1), New FieldDescription("Discontinued", "Discontinued", "ToggleSwitchEdit", "ToggleSwitchEdit", GetType(Boolean), 28, -1), New FieldDescription(String.Empty, "Discount", "SpinEdit", "SpinEdit", GetType(Decimal), 4, -1), New FieldDescription(String.Empty, "Pallete Size", "ListBoxEdit", "ListBoxEdit", GetType(String), 12, -1), New FieldDescription(String.Empty, "ID", "BarCodeEdit", "BarCodeEdit", GetType(String), 25, -1), New FieldDescription("Rating", "Rating", "RatingEdit", "RatingEdit", GetType(Double), 27, -1), New FieldDescription("HyperLink", "Hyper Link", "HyperlinkEdit", "HyperlinkEdit", GetType(String), 29, -1)}
        End Function

#End Region
        Private products As List(Of Product)

        Public Sub New()
            products = NWindContext.Create().Products.ToList()
            CreateDataTable()
            CreateColumnCollection()
        End Sub

        Protected Friend Property ColumnCollection As PropertyDescriptorCollection
            Get
                Return _ColumnCollection
            End Get

            Protected Set(ByVal value As PropertyDescriptorCollection)
                _ColumnCollection = value
            End Set
        End Property

        Protected Property Table As List(Of Dictionary(Of String, Object))
            Get
                Return _Table
            End Get

            Private Set(ByVal value As List(Of Dictionary(Of String, Object)))
                _Table = value
            End Set
        End Property

        Private palleteSizes As String() = New String() {"4x4", "10x10", "16x16", "20x20", "25x25"}

        Private random As Random = New Random()

        Protected Overridable Sub CreateDataTable()
            Table = New List(Of Dictionary(Of String, Object))()
            For i As Integer = 0 To products.Count - 1
                Dim dataRow As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
                For Each description As FieldDescription In FieldDescriptions
                    If Not String.IsNullOrEmpty(description.PropertyName) Then dataRow(description.ColumnName) = GetProductPropertyValue(i, description.PropertyName)
                Next

                dataRow("Country") = GetRandomCountry()
                dataRow("Units On Order") = random.Next(100)
                dataRow("Notes") = "Notes: " & Environment.NewLine & "notes for Product #" & i + 1
                dataRow("Date") = Date.Today.AddYears(-5).AddDays(-random.Next(1000))
                dataRow("Pallete Size") = palleteSizes(random.Next(palleteSizes.Length))
                dataRow("Countries") = GetRandomCountry()
                Dim selectionStart As Double = random.Next(50)
                dataRow("Range") = New TrackBarEditRange(selectionStart, selectionStart + random.Next(50))
                dataRow("Color") = GenerateRandomColor()
                dataRow("Brush") = New LinearGradientBrush(GenerateRandomColor(), GenerateRandomColor(), random.Next(180))
                dataRow("Orders") = CreateOrdersList()
                dataRow("Password") = GenerateRandomPassword()
                dataRow("Discount") = GenerateRandomDiscount()
                dataRow("Picture") = GetPhoto(i)
                dataRow("Font") = GetFont(i)
                dataRow("Data") = i + 3 + i * i
                dataRow("Categories") = GenerateRandomCategories(i)
                dataRow("Rating") = random.Next(5)
                dataRow("Hyper Link") = dataRow("Name")
                Table.Add(dataRow)
            Next
        End Sub

        Private Function GenerateRandomCategories(ByVal i As Integer) As Object
            If random.Next(8) > 5 Then Return Nothing
            i = i Mod NWindContext.Create().Categories.Count()
            Return New List(Of Object)() From {Queryable.First(Of Category)(Queryable.Skip(Of Category)(Queryable.OrderBy(Of Category, Global.System.[String])(NWindContext.Create().Categories, CType(Function(c) CStr(c.CategoryName), Expressions.Expression(Of Func(Of Category, String)))), CInt(i))).CategoryID}
        End Function

        Protected Function GetProductPropertyValue(ByVal index As Integer, ByVal propertyName As String) As Object
            Dim product As Product = products(index)
            Dim [property] = product.GetType().GetProperty(propertyName)
            Return If([property] IsNot Nothing, [property].GetValue(product, Nothing), Nothing)
        End Function

        Private Function CreateOrdersList() As List(Of Integer)
            Dim list As List(Of Integer) = New List(Of Integer)()
            For i As Integer = 0 To 10 - 1
                list.Add(random.Next(5))
            Next

            Return list
        End Function

        Private Function GetPhoto(ByVal i As Integer) As ImageSource
            If random.Next(8) > 5 Then Return Nothing
            i = i Mod NWindContext.Create().Categories.Count()
            Dim bytes As Byte() = Queryable.First(Of Category)(Queryable.Skip(Of Category)(Queryable.OrderBy(Of Category, Global.System.[String])(NWindContext.Create().Categories, CType(Function(c) CStr(c.CategoryName), Expressions.Expression(Of Func(Of Category, String)))), CInt(i))).Picture
            Return Core.Native.ImageHelper.CreateImageFromStream(New MemoryStream(bytes))
        End Function

        Private Function GetFont(ByVal i As Integer) As FontFamily
            Dim fontSource As IList = Nothing
            fontSource = New List(Of Object)(Fonts.SystemFontFamilies)
            Return New FontFamily(fontSource(random.Next(fontSource.Count)).ToString())
        End Function

        Private Function GenerateRandomColor() As Color
            Dim bytes As Byte() = New Byte(2) {}
            random.NextBytes(bytes)
            Return Color.FromArgb(Byte.MaxValue, bytes(0), bytes(1), bytes(2))
        End Function

        Private Function GenerateRandomPassword() As String
            Return New String("p"c, random.Next(3, 15))
        End Function

        Private Function GenerateRandomDiscount() As Double
            Return random.NextDouble() * 0.1R
        End Function

        Private Function GetRandomCountry() As String
            Return NWindContext.Create().CountriesArray(random.Next(NWindContext.Create().CountriesArray.Length))
        End Function

        Public Sub SetPropertyValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer, ByVal value As Object)
            If columnIndex > 0 AndAlso columnIndex <= Table.Count Then
                Table(columnIndex - 1)(FieldDescriptions(rowIndex).ColumnName) = value
                RaiseListChanged(rowIndex, columnIndex)
            End If
        End Sub

        Private Sub RaiseListChanged(ByVal rowIndex As Integer, ByVal columnIndex As Integer)
            If listChangedEventHandlerEvent Is Nothing Then Return
            Dim rowIndexes As Integer() = GetRowIndexes(rowIndex)
            For Each index As Integer In rowIndexes
                RaiseEvent listChangedEventHandler(Me, New System.ComponentModel.ListChangedEventArgs(System.ComponentModel.ListChangedType.ItemChanged, index))
            Next
        End Sub

        Private Function GetRowIndexes(ByVal rowIndex As Integer) As Integer()
            Dim indexes As List(Of Integer) = New List(Of Integer)()
            Dim propertyName As String = FieldDescriptions(rowIndex).PropertyName
            For i As Integer = 0 To Count - 1
                If String.Equals(propertyName, FieldDescriptions(i).PropertyName, StringComparison.InvariantCultureIgnoreCase) Then
                    indexes.Add(i)
                End If
            Next

            Return indexes.ToArray()
        End Function

        Protected MustOverride Sub CreateColumnCollection()

        Public MustOverride Function GetPropertyValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Object

#Region "ITypedList Interface"
        Private Function GetItemProperties(ByVal descs As PropertyDescriptor()) As PropertyDescriptorCollection Implements ITypedList.GetItemProperties
            Return ColumnCollection
        End Function

        Private Function GetListName(ByVal descs As PropertyDescriptor()) As String Implements ITypedList.GetListName
            Return ""
        End Function

#End Region
#Region "IList Interface"
        Public Overridable ReadOnly Property Count As Integer Implements ICollection.Count
            Get
                Return FieldDescriptions.Count
            End Get
        End Property

        Public Overridable ReadOnly Property IsSynchronized As Boolean Implements ICollection.IsSynchronized
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property SyncRoot As Object Implements ICollection.SyncRoot
            Get
                Return True
            End Get
        End Property

        Public Overridable ReadOnly Property IsReadOnly As Boolean Implements IList.IsReadOnly
            Get
                Return False
            End Get
        End Property

        Public Overridable ReadOnly Property IsFixedSize As Boolean Implements IList.IsFixedSize
            Get
                Return True
            End Get
        End Property

        Public Overridable Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
            Return Nothing
        End Function

        Public Overridable Sub CopyTo(ByVal array As Array, ByVal fIndex As Integer) Implements ICollection.CopyTo
        End Sub

        Public Overridable Function Add(ByVal val As Object) As Integer Implements IList.Add
            Throw New NotImplementedException()
        End Function

        Public Overridable Sub Clear() Implements IList.Clear
            Throw New NotImplementedException()
        End Sub

        Public Overridable Function Contains(ByVal val As Object) As Boolean Implements IList.Contains
            Throw New NotImplementedException()
        End Function

        Public Overridable Function IndexOf(ByVal val As Object) As Integer Implements IList.IndexOf
            Throw New NotImplementedException()
        End Function

        Public Overridable Sub Insert(ByVal fIndex As Integer, ByVal val As Object) Implements IList.Insert
            Throw New NotImplementedException()
        End Sub

        Public Overridable Sub Remove(ByVal val As Object) Implements IList.Remove
            Throw New NotImplementedException()
        End Sub

        Public Overridable Sub RemoveAt(ByVal fIndex As Integer) Implements IList.RemoveAt
            Throw New NotImplementedException()
        End Sub

        Private Property Item(ByVal fIndex As Integer) As Object Implements IList.Item
            Get
                Return fIndex
            End Get

            Set(ByVal value As Object)
            End Set
        End Property

#End Region
#Region "IBindingList Members"
        Private Sub AddIndex(ByVal [property] As PropertyDescriptor) Implements IBindingList.AddIndex
            Throw New NotImplementedException()
        End Sub

        Private Function AddNew() As Object Implements IBindingList.AddNew
            Throw New NotImplementedException()
        End Function

        Private ReadOnly Property AllowEdit As Boolean Implements IBindingList.AllowEdit
            Get
                Return True
            End Get
        End Property

        Private ReadOnly Property AllowNew As Boolean Implements IBindingList.AllowNew
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property AllowRemove As Boolean Implements IBindingList.AllowRemove
            Get
                Return False
            End Get
        End Property

        Private Sub ApplySort(ByVal [property] As PropertyDescriptor, ByVal direction As System.ComponentModel.ListSortDirection) Implements IBindingList.ApplySort
            Throw New NotImplementedException()
        End Sub

        Private Function Find(ByVal [property] As PropertyDescriptor, ByVal key As Object) As Integer Implements IBindingList.Find
            Throw New NotImplementedException()
        End Function

        Private ReadOnly Property IsSorted As Boolean Implements IBindingList.IsSorted
            Get
                Return False
            End Get
        End Property

        Private Event listChangedEventHandler As System.ComponentModel.ListChangedEventHandler

        Private Custom Event ListChanged As System.ComponentModel.ListChangedEventHandler Implements IBindingList.ListChanged
            AddHandler(ByVal value As System.ComponentModel.ListChangedEventHandler)
                AddHandler listChangedEventHandler, value
            End AddHandler

            RemoveHandler(ByVal value As System.ComponentModel.ListChangedEventHandler)
                RemoveHandler listChangedEventHandler, value
            End RemoveHandler

            RaiseEvent(ByVal sender As Object, ByVal e As ListChangedEventArgs)
                RaiseEvent listChangedEventHandler(sender, e)
            End RaiseEvent
        End Event

        Private Sub RemoveIndex(ByVal [property] As PropertyDescriptor) Implements IBindingList.RemoveIndex
            Throw New NotImplementedException()
        End Sub

        Private Sub RemoveSort() Implements IBindingList.RemoveSort
            Throw New NotImplementedException()
        End Sub

        Private ReadOnly Property SortDirection As System.ComponentModel.ListSortDirection Implements IBindingList.SortDirection
            Get
                Throw New NotImplementedException()
            End Get
        End Property

        Private ReadOnly Property SortProperty As PropertyDescriptor Implements IBindingList.SortProperty
            Get
                Return Nothing
            End Get
        End Property

        Private ReadOnly Property SupportsChangeNotification As Boolean Implements IBindingList.SupportsChangeNotification
            Get
                Return True
            End Get
        End Property

        Private ReadOnly Property SupportsSearching As Boolean Implements IBindingList.SupportsSearching
            Get
                Return False
            End Get
        End Property

        Private ReadOnly Property SupportsSorting As Boolean Implements IBindingList.SupportsSorting
            Get
                Return False
            End Get
        End Property
#End Region
    End Class

    Public Class MultiEditorsListPropertyDescriptorSQLite
        Inherits PropertyDescriptor

        Private propertyName As String

        Private isReadOnlyField As Boolean

        Private list As MultiEditorsListBaseSQLite

        Private index As Integer

        Public Sub New(ByVal list As MultiEditorsListBaseSQLite, ByVal index As Integer, ByVal propertyName As String, ByVal isReadOnly As Boolean)
            MyBase.New(propertyName, Nothing)
            Me.propertyName = propertyName
            isReadOnlyField = isReadOnly
            Me.list = list
            Me.index = index
        End Sub

        Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
            Return False
        End Function

        Public Overrides Function GetValue(ByVal component As Object) As Object
            Return list.GetPropertyValue(CInt(component), index)
        End Function

        Public Overrides Sub SetValue(ByVal component As Object, ByVal val As Object)
            list.SetPropertyValue(CInt(component), index, val)
        End Sub

        Public Overrides ReadOnly Property IsReadOnly As Boolean
            Get
                Return isReadOnlyField
            End Get
        End Property

        Public Overrides ReadOnly Property Name As String
            Get
                Return propertyName
            End Get
        End Property

        Public Overrides ReadOnly Property ComponentType As Type
            Get
                Return list.GetType()
            End Get
        End Property

        Public Overrides ReadOnly Property PropertyType As Type
            Get
                Return GetType(Object)
            End Get
        End Property

        Public Overrides Sub ResetValue(ByVal component As Object)
        End Sub

        Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
            Return True
        End Function
    End Class
End Namespace
