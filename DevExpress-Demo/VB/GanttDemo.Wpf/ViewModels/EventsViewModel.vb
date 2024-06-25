Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Diagnostics
Imports DevExpress.Mvvm.DataAnnotations
Imports DevExpress.Mvvm.Gantt
Imports DevExpress.Mvvm.POCO
Imports DevExpress.Xpf.Gantt

Namespace GanttDemo

    <POCOViewModel>
    Public Class EventsViewModel

        Private _Items As List(Of DevExpress.Mvvm.Gantt.GanttTask), _Resources As List(Of DevExpress.Mvvm.Gantt.GanttResource)

        Protected Sub New()
            Dim startupPlanModel = New StartupBusinessPlanViewModel()
            Items = startupPlanModel.Items
            Resources = startupPlanModel.Resources
        End Sub

        Private ReadOnly logField As ObservableCollection(Of LogEntry) = New ObservableCollection(Of LogEntry)()

        Const LogEntriesMaxCount As Integer = 100

        Public ReadOnly Property Log As IEnumerable(Of LogEntry)
            Get
                Return logField
            End Get
        End Property

        Public Property Items As List(Of GanttTask)
            Get
                Return _Items
            End Get

            Private Set(ByVal value As List(Of GanttTask))
                _Items = value
            End Set
        End Property

        Public Property Resources As List(Of GanttResource)
            Get
                Return _Resources
            End Get

            Private Set(ByVal value As List(Of GanttResource))
                _Resources = value
            End Set
        End Property

        Private Sub AddToLog(ByVal eventName As String, ByVal ownerClassName As String, ByVal argsFormat As String, ParamArray args As Object())
            logField.Add(LogEntry.Create(eventName, String.Format(argsFormat, args), ownerClassName))
            If logField.Count > LogEntriesMaxCount Then logField.RemoveAt(0)
            Dim scrollService = GetService(Of IScrollToEndService)()
            If scrollService IsNot Nothing Then scrollService.ScrollToEnd()
        End Sub

        Public Sub ClearLog()
            logField.Clear()
        End Sub

        Public Overridable Property EventsInfo As IEnumerable(Of GanttEventNode)

        Public Sub InitializeEventsInfo(ByVal eventsOwner As GanttControl)
            EventsInfo = New GanttEventsInfo(eventsOwner, AddressOf AddToLog).Initialize()
        End Sub
    End Class

    <POCOViewModel>
    Public Class LogEntry

        Public Shared Function Create(ByVal eventName As String, ByVal eventArgs As String, ByVal ownerClassName As String) As LogEntry
            Return ViewModelSource.Create(Function() New LogEntry(eventName, eventArgs, ownerClassName))
        End Function

        Protected Sub New(ByVal eventName As String, ByVal eventArgs As String, ByVal ownerClassName As String)
            Me.EventName = eventName
            Me.EventArgs = eventArgs
            Me.ownerClassName = ownerClassName
        End Sub

        Private ReadOnly ownerClassName As String

        Public Overridable Property EventName As String

        Public Overridable Property EventArgs As String

        Public Sub ShowHelp()
            Call Process.Start(New ProcessStartInfo With {.FileName = "https://documentation.devexpress.com/WPF/" & ownerClassName & "." & EventName & ".event", .UseShellExecute = True})
        End Sub
    End Class
End Namespace
