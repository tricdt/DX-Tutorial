Imports System.Collections.ObjectModel
Imports System.Linq
Imports DevExpress.Xpf.DemoBase.DataClasses

Namespace GridDemo

    Public Class DragDropViewModel

        Public Sub New()
            Const threshold As Integer = 9
            ActiveEmployees = New ObservableCollection(Of Employee)(EmployeesData.DataSource.Skip(threshold))
            NewEmployees = New ObservableCollection(Of Employee)(EmployeesData.DataSource.Take(threshold))
        End Sub

        Public Property ActiveEmployees As ObservableCollection(Of Employee)

        Public Property NewEmployees As ObservableCollection(Of Employee)
    End Class
End Namespace
