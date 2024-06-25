Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports DevExpress.DemoData.Models
Imports DevExpress.Office.Services

Namespace RichEditDemo

    Public Class DBUriStreamProvider
        Implements IUriStreamProvider

        Private Shared ReadOnly prefix As String = "dbimg://"

        Private ReadOnly employees As IList(Of Employee)

        Public Sub New(ByVal employees As IList(Of Employee))
            Me.employees = employees
        End Sub

        Private Function GetStream(ByVal uri As String) As Stream Implements IUriStreamProvider.GetStream
            uri = uri.Trim()
            If Not uri.StartsWith(prefix) Then Return Nothing
            Dim strId As String = uri.Substring(prefix.Length).Trim()
            Dim id As Integer
            If Not Integer.TryParse(strId, id) Then Return Nothing
            Dim bytes As Byte() = Enumerable.First(employees, Function(employee) employee.EmployeeID = id).Photo
            If bytes Is Nothing Then Return Nothing
            Return New MemoryStream(bytes)
        End Function
    End Class
End Namespace
