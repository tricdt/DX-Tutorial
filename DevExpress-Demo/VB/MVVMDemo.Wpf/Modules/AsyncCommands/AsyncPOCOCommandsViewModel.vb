Imports DevExpress.Mvvm.POCO
Imports System.Threading.Tasks

Namespace MVVMDemo.AsyncCommands

    Public Class AsyncPOCOCommandsViewModel

        Public Overridable Property Progress As Integer

        Public Async Function Calculate() As Task
            For i As Integer = 0 To 100
                If POCOViewModelExtensions.GetAsyncCommand(Me, Function(x) x.Calculate()).IsCancellationRequested Then
                    Progress = 0
                    Return
                End If

                Progress = i
                Await Task.Delay(20)
            Next
        End Function
    End Class
End Namespace
