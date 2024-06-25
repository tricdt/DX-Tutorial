Imports DevExpress.Mvvm.POCO
Imports DevExpress.Mvvm
Imports System
Imports System.Threading
Imports System.Threading.Tasks
Imports System.Collections.ObjectModel

Namespace MVVMDemo.Services

    Public Class DispatcherServiceViewModel

        Public Property Items As ObservableCollection(Of String)

        Public Function Generate() As Task
            Return Task.Factory.StartNew(New Action(AddressOf GenerateCore))
        End Function

        Private Sub GenerateCore()
            Dim service As IDispatcherService = GetRequiredService(Of IDispatcherService)()
            service.BeginInvoke(Sub() Items.Clear())
            For i As Integer = 0 To 10
                Dim item As String = "Item " & i
                service.BeginInvoke(Sub() Items.Add(item))
                Thread.Sleep(TimeSpan.FromMilliseconds(100))
            Next
        End Sub

        Protected Sub New()
            Items = New ObservableCollection(Of String)()
        End Sub
    End Class
End Namespace
