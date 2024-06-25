Imports System
Imports System.ComponentModel
Imports System.Windows.Media
Imports DevExpress.Data.Helpers
Imports DevExpress.DemoData.Models
Imports DevExpress.Xpf.DemoBase
Imports System.Linq

Namespace EditorsDemo

    <CodeFile("ModuleResources/DynamicallyAssignDataEditorsResources.xaml")>
    Public Partial Class InplaceEditorsPropertyGridModule
        Inherits EditorsDemoModule

        Public Sub New()
            InitializeComponent()
        End Sub
    End Class

    Public Class PropertyGridMultiEditorsList
        Inherits MultiEditorsListBaseSQLite

        Public Sub New()
        End Sub

        Public Function GetValue(ByVal propertyName As String) As Object
            Return Table(0)(MasterDetailHelper.SplitPascalCaseString(propertyName))
        End Function

        Default Public ReadOnly Property Item(ByVal index As Integer) As Object
            Get
                Return Table(0)
            End Get
        End Property

        Protected Overrides Sub CreateColumnCollection()
            Dim pds = New MultiEditorsListPropertyDescriptorSQLite(0) {}
            pds(0) = New MultiEditorsListPropertyDescriptorSQLite(Me, 0, "Product #" & 0, False)
            ColumnCollection = New PropertyDescriptorCollection(pds)
        End Sub

        Public Overrides Function GetPropertyValue(ByVal rowIndex As Integer, ByVal columnIndex As Integer) As Object
            Throw New NotSupportedException()
        End Function

        Protected Overrides Sub CreateDataTable()
            MyBase.CreateDataTable()
            Dim rows = NWindContext.Create().Products.ToList()
            For i As Integer = 0 To rows.Count - 1
                Dim dict = Table(i)
                dict("Category1") = rows(i).CategoryID
                dict("Category2") = rows(i).CategoryID
                dict("Brush") = New SolidColorBrush(CType(dict("Color"), Color))
                dict("Date1") = Date.Today
                dict("Date2") = Date.Today
                dict("Discontinued2") = True
                dict("Product") = rows(i).ProductName
            Next
        End Sub
    End Class
End Namespace
