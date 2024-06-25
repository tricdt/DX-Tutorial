Imports System
Imports System.Linq
Imports System.ComponentModel
Imports System.Windows.Controls
Imports System.Windows.Media
Imports DevExpress.Xpf.DemoBase.DemosHelpers.Grid
Imports System.ComponentModel.DataAnnotations
Imports DevExpress.Data.Helpers
Imports DevExpress.DemoData.Models

Namespace PropertyGridDemo

    Public Class MultiEditorsList
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
            Next
        End Sub
    End Class
End Namespace
