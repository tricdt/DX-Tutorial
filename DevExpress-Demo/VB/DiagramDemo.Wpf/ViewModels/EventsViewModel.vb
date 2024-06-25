Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Xpf.Diagram
Imports DevExpress.Mvvm.POCO
Imports System.Diagnostics

Namespace DiagramDemo

    <POCOViewModel>
    Public Class EventsViewModel

        Protected Sub New()
        End Sub

        Private ReadOnly logField As ObservableCollection(Of LogEntry) = New ObservableCollection(Of LogEntry)()

        Const LogEntriesMaxCount As Integer = 100

        Public ReadOnly Property Log As IEnumerable(Of LogEntry)
            Get
                Return logField
            End Get
        End Property

        Private Sub AddToLog(ByVal eventName As String, ByVal argsFormat As String, ParamArray args As Object())
            logField.Add(LogEntry.Create(eventName, String.Format(argsFormat, args)))
            If logField.Count > LogEntriesMaxCount Then logField.RemoveAt(0)
            Dim scrollService = GetService(Of IScrollToEndService)()
            If scrollService IsNot Nothing Then scrollService.ScrollToEnd()
        End Sub

        Public Sub ClearLog()
            logField.Clear()
        End Sub

        Public Overridable Property EventsInfo As IEnumerable(Of DiagramEventNode)

        Public Sub InitializeEventsInfo(ByVal eventsOwner As DiagramControl)
            EventsInfo = New DiagramEventsInfo(eventsOwner, AddressOf AddToLog).Initialize()
        End Sub
    End Class

    <POCOViewModel>
    Public Class LogEntry

        Public Shared Function Create(ByVal eventName As String, ByVal eventArgs As String) As LogEntry
            Return ViewModelSource.Create(Function() New LogEntry(eventName, eventArgs))
        End Function

        Protected Sub New(ByVal eventName As String, ByVal eventArgs As String)
            Me.EventName = eventName
            Me.EventArgs = eventArgs
        End Sub

        Public Overridable Property EventName As String

        Public Overridable Property EventArgs As String

        Public Sub ShowHelp()
            Call Process.Start(New ProcessStartInfo With {.FileName = "https://documentation.devexpress.com/WPF/DevExpress.Xpf.Diagram.DiagramControl." & EventName & ".event", .UseShellExecute = True})
        End Sub
    End Class
End Namespace
