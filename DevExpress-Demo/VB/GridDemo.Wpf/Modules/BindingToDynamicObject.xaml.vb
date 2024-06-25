Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Dynamic
Imports System.Linq
Imports System.Windows
Imports System.Windows.Data

Namespace GridDemo

    Public Partial Class BindingToDynamicObject
        Inherits GridDemoModule

        Private list As ObservableCollection(Of ExpandoObject) = New ObservableCollection(Of ExpandoObject)()

        Public Sub New()
            InitializeComponent()
            For i As Integer = 0 To 50 - 1
                Dim row As IDictionary(Of String, Object) = New ExpandoObject()
                row("Id") = i
                row("FirstName") = DataGenerator.GetFirstName()
                row("LastName") = DataGenerator.GetLastName()
                list.Add(CType(row, ExpandoObject))
            Next

            grid.ItemsSource = list
        End Sub

        Private Sub OnCreateNewColumnButtonClick(ByVal sender As Object, ByVal e As RoutedEventArgs)
            Dim fieldName As String = CType(TypeBox.SelectedItem, ValueSelectorItem).DisplayName & " Column"
            Dim index As Integer = GetUniqueColumnIndex(fieldName)
            fieldName = fieldName & " " & index.ToString()
            For Each item As IDictionary(Of String, Object) In list
                item(fieldName) = GetRandomValue()
            Next

            Dim column As GridColumn = New GridColumn()
            column.Binding = New Binding(fieldName) With {.Mode = BindingMode.TwoWay}
            column.Header = fieldName
            grid.Columns.Add(column)
        End Sub

        Private Function GetUniqueColumnIndex(ByVal fieldName As String) As Integer
            Return Enumerable.Range(1, Integer.MaxValue).First(Function(i) Not grid.Columns.Any(Function(col) Equals(CStr(col.Header), (fieldName & " " & i.ToString()))))
        End Function

        Private Function GetRandomValue() As Object
            Dim selectedType = CType(TypeBox.EditValue, Type)
            If selectedType Is GetType(Integer) Then Return DataGenerator.GetInt()
            If selectedType Is GetType(String) Then Return DataGenerator.GetString()
            If selectedType Is GetType(Date) Then Return DataGenerator.GetDateTime()
            If selectedType Is GetType(Boolean) Then Return DataGenerator.GetBool()
            Throw New NotSupportedException()
        End Function
    End Class
End Namespace
